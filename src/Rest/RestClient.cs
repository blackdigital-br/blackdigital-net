using System.Net;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;

namespace BlackDigital.Rest
{
    public class RestClient : IDisposable
    {
        #region "Constructor"

        public RestClient()
            : this("/api")
        {
            
        }

        public RestClient(string baseAddress, HttpMessageHandler? hanlder = null)
        {
            CustomHeaders = new();
            HttpHandler = hanlder;
            RequestsRetryConnections = new();
            TimeRetryConnection = 3000;

            if (hanlder != null)
                Client = new(hanlder);
            else
                Client = new();
            
            Client.BaseAddress = new Uri(baseAddress);
        }

        #endregion "Constructor"

        #region "Properties"

        public bool RetryConection { get; set; }

        public int TimeRetryConnection { get; set; }

        public int RetryCount => RequestsRetryConnections.Count;

        public event Action? Unauthorized;
        public event Action? ConnectionError;
        public event Action? ServerError;

        public HttpClient Client { get; private set; }
        protected internal HttpMessageHandler? HttpHandler;
        protected Dictionary<string, List<string>> CustomHeaders;
        protected List<HttpRequestMessage> RequestsRetryConnections;

        private bool _threadRun;
        private object _lockThread = new();
        private Thread? _threadRetryConnection;

        #endregion "Properties"

        #region "Client Methods"

        private const string Authorization = "Authorization";

        public Task<HttpResponseMessage> GetAsync(string requestUri) => Client.GetAsync(requestUri);
        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content) => Client.PostAsync(requestUri, content);
        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content) => Client.PutAsync(requestUri, content);
        public Task<HttpResponseMessage> DeleteAsync(string requestUri) => Client.DeleteAsync(requestUri);
        public Task<byte[]> GetByteArrayAsync(string requestUri) => Client.GetByteArrayAsync(requestUri);

        //protected virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) => Client.SendAsync(request);

        #endregion "Client Methods"

        #region "Rest Methods"

        internal protected virtual async Task<object?> RequestAsync(HttpMethod method,
                                                                    string url,
                                                                    Type? responseType = null,
                                                                    object? content = null,
                                                                    Dictionary<string, string>? headers = null,
                                                                    bool thrownError = true)
        {
            HttpContent? httpContent = null;

            if (content != null)
            {
                string jsonString = JsonCast.ToJson(content);
                httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            }

            var httpResponse = await RequestAsync(method, url, httpContent, headers, thrownError);

            if (responseType == null)
                return httpResponse.IsSuccessStatusCode;

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();

                if (responseType == typeof(string))
                    return (string)(object)responseAsString;

                return JsonCast.To(responseAsString, responseType);
            }

            return null;
        }

        protected virtual async Task<HttpResponseMessage> RequestAsync(HttpMethod method, 
                                                          string url, 
                                                          HttpContent? content = null,
                                                          Dictionary<string, string>? headers = null, 
                                                          bool thrownError = true)
        {
            var request = new HttpRequestMessage(method, url);

            if (headers != null)
            {
                foreach (var header in headers)
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            if (content != null)
                request.Content = content;

           return await RequestAsync(request, thrownError);
        }

        protected virtual async Task<HttpResponseMessage> RequestAsync(HttpRequestMessage requestMessage, bool thrownError = false)
        {
            try
            {
                var httpResponse = await Client.SendAsync(requestMessage);

                if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Unauthorized?.Invoke();
                }
                else if (thrownError && httpResponse.StatusCode.IsClientError())
                {
                    var responseAsString = await httpResponse.Content.ReadAsStringAsync();
                    BusinessException.Throw(responseAsString, (int)httpResponse.StatusCode);
                }
                else if (thrownError && httpResponse.StatusCode.IsServerError())
                    throw new Exception("Connection error", new(await httpResponse.Content.ReadAsStringAsync()));

                EndRequest(requestMessage);

                return httpResponse;
            }
            catch (HttpRequestException requestError)
            {
                ErrorConnection(requestMessage, requestError, thrownError);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                EndRequest(requestMessage);

                if (thrownError)
                    throw;

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<T?> GetRestAsync<T>(string url, Dictionary<string, string>? headers = null, bool thrownError = true)
        {
            var httpResponse = await RequestAsync(HttpMethod.Get, url, null, headers, thrownError);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();

                if (typeof(T) == typeof(string))
                    return (T)(object)responseAsString;

                return JsonCast.To<T>(responseAsString);
            }

            return default;
        }

        public async Task<TReturn?> PostRestAsync<TReturn, TSend>(string url, TSend sender, Dictionary<string, string>? headers = null, bool thrownError = true)
        {
            string jsonString = JsonCast.ToJson(sender);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var httpResponse = await RequestAsync(HttpMethod.Post, url, stringContent, headers, thrownError);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();

                if (typeof(TReturn) == typeof(string))
                    return (TReturn)(object)responseAsString;

                return JsonCast.To<TReturn>(responseAsString);
            }

            return default;
        }

        public async Task<bool> PostRestAsync<TSend>(string url, TSend sender, Dictionary<string, string>? headers = null, bool thrownError = true)
        {
            string jsonString = JsonCast.ToJson(sender);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var httpResponse = await RequestAsync(HttpMethod.Post, url, stringContent, headers, thrownError);

            return httpResponse.IsSuccessStatusCode;
        }

        public async Task<TReturn?> PostRestAsync<TReturn>(string url, Dictionary<string, string>? headers = null, bool thrownError = true)
        {
            var httpResponse = await RequestAsync(HttpMethod.Post, url, null, headers, thrownError);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();

                if (typeof(TReturn) == typeof(string))
                    return (TReturn)(object)responseAsString;

                return JsonCast.To<TReturn>(responseAsString);
            }

            return default;
        }

        public async Task<TReturn?> PutRestAsync<TReturn, TSend>(string url, TSend sender, Dictionary<string, string>? headers = null, bool thrownError = true)
        {
            string jsonString = JsonCast.ToJson(sender);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var httpResponse = await RequestAsync(HttpMethod.Put, url, stringContent, headers, thrownError);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();

                if (typeof(TReturn) == typeof(string))
                    return (TReturn)(object)responseAsString;

                return JsonCast.To<TReturn>(responseAsString);
            }

            return default;
        }

        public async Task<bool> PutRestAsync<TSend>(string url, TSend sender, Dictionary<string, string>? headers = null, bool thrownError = true)
        {
            string jsonString = JsonCast.ToJson(sender);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var httpResponse = await RequestAsync(HttpMethod.Put, url, stringContent, headers, thrownError);

            return httpResponse.IsSuccessStatusCode;
        }

        public async Task<TReturn?> PutRestAsync<TReturn>(string url, Dictionary<string, string>? headers = null, bool thrownError = true)
        {
            var httpResponse = await RequestAsync(HttpMethod.Put, url, null, headers, thrownError);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();

                if (typeof(TReturn) == typeof(string))
                    return (TReturn)(object)responseAsString;

                return JsonCast.To<TReturn>(responseAsString);
            }

            return default;
        }

        public async Task<bool> DeleteRestAsync(string url, Dictionary<string, string>? headers = null, bool thrownError = true)
        {
            var httpResponse = await RequestAsync(HttpMethod.Put, url, null, headers, thrownError);

            return httpResponse.IsSuccessStatusCode;
        }

        #endregion "Rest Methods"

        #region "Internal Methods"

        protected void ErrorConnection(HttpRequestMessage request, HttpRequestException requestError, bool thrownError)
        {
            Console.WriteLine(requestError);

            if (RetryConection && !RequestsRetryConnections.Contains(request))
            {
                RequestsRetryConnections.Add(request);
                StartRetryProcess();
            }

            ConnectionError?.Invoke();

            if (thrownError)
                throw requestError;
        }

        protected void EndRequest(HttpRequestMessage request)
        {
            request?.Dispose();
            RequestsRetryConnections.Remove(request);
        }

        private void StartRetryProcess()
        {
            lock (_lockThread) 
            {
                if (_threadRun)
                {
                    _threadRun = true;
                    _threadRetryConnection = new Thread(RetryConnectionProcess);
                    _threadRetryConnection.Start();
                }
            }
        }

        private void RetryConnectionProcess()
        {
            while (_threadRun && RequestsRetryConnections.Any())
            {
                Thread.Sleep(TimeRetryConnection);
                var allRequests = RequestsRetryConnections.ToArray();

                foreach ( var request in allRequests)
                {
                    try
                    {
                        RequestAsync(request, true).Wait();
                    }
                    catch (HttpRequestException requestError)
                    {
                        break;
                    }
                    catch { }
                }
            }

            lock (_lockThread)
            {
                _threadRun = false;
                _threadRetryConnection = null;
            }
        }

        public void Dispose()
        {
            Client?.Dispose();
            Client = null;
            _threadRun = false;
            _threadRetryConnection = null;

            RequestsRetryConnections.ForEach(r => r.Dispose());
            RequestsRetryConnections.Clear();
        }

        #endregion "Internal Methods"

        #region "Builder"

        public RestClient AddHeader(string key, string value)
        {
            if (CustomHeaders.ContainsKey(key))
                CustomHeaders[key].Add(value);
            else
                CustomHeaders.Add(key, new List<string>() { value });

            UpdateHeaders();
            
            return this;
        }

        public RestClient AddAuthentication(AuthenticationHeaderValue customAuthentication)
        {
            if (CustomHeaders.ContainsKey(Authorization))
                CustomHeaders.Remove(Authorization);

            
            CustomHeaders.Add(Authorization,
                    new List<string>() { $"{customAuthentication.Scheme} {customAuthentication.Parameter}" });

            UpdateHeaders();

            return this;
        }

        public void UpdateBaseAddress(string url)
        {
            Dispose();

            Client = new();
            Client.BaseAddress = new(url);
            UpdateHeaders();
        }

        protected virtual void UpdateHeaders()
        {
            foreach (var header in CustomHeaders)
            {
                if (Client.DefaultRequestHeaders.Contains(header.Key))
                    Client.DefaultRequestHeaders.Remove(header.Key);

                Client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        #endregion "Builder"
    }
}