using System.Net;
using System.Net.Http.Headers;
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

        public RestClient(string baseAddress)
        {
            CustomHeaders = new();
            Client = new();
            Client.BaseAddress = new Uri(baseAddress);
        }

        #endregion "Constructor"

        #region "Properties"

        public event Action? Unauthorized;
        public event Action? ConnectionError;
        public event Action? ServerError;

        private HttpClient Client;
        private Dictionary<string, List<string>> CustomHeaders;

        #endregion "Properties"

        #region "Client Methods"

        public Task<HttpResponseMessage> GetAsync(string requestUri) => Client.GetAsync(requestUri);
        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content) => Client.PostAsync(requestUri, content);
        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content) => Client.PutAsync(requestUri, content);
        public Task<HttpResponseMessage> DeleteAsync(string requestUri) => Client.DeleteAsync(requestUri);
        public Task<byte[]> GetByteArrayAsync(string requestUri) => Client.GetByteArrayAsync(requestUri);

        #endregion "Client Methods"

        #region "Rest Methods"

        protected virtual async Task<HttpResponseMessage> Request(HttpMethod method, 
                                                          string url, 
                                                          HttpContent? content = null,
                                                          Dictionary<string, string>? headers = null, 
                                                          bool thrownError = true)
        {
            using var request = new HttpRequestMessage(method, url);
            try
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                        request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }

                if (content != null)
                    request.Content = content;

                var httpResponse = await Client.SendAsync(request);

                if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Unauthorized?.Invoke();
                }
                else if (thrownError && httpResponse.StatusCode.IsServerError())
                    throw new Exception("Connection error");

                return httpResponse;
            }
            catch (HttpRequestException requestError)
            {
                Console.WriteLine(requestError);

                ConnectionError?.Invoke();

                if (thrownError)
                    throw;

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                if (thrownError)
                    throw;

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

        }

        public async Task<T?> GetRest<T>(string url, Dictionary<string, string>? headers = null, bool thrownError = true)
        {
            var httpResponse = await Request(HttpMethod.Get, url, null, headers, thrownError);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();

                if (typeof(T) == typeof(string))
                    return (T)(object)responseAsString;

                return JsonCast.To<T>(responseAsString);
            }

            return default;
        }

        public async Task<TReturn?> PostRest<TReturn, TSend>(string url, TSend sender, Dictionary<string, string>? headers = null, bool thrownError = true)
        {
            string jsonString = JsonCast.ToJson(sender);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var httpResponse = await Request(HttpMethod.Post, url, stringContent, headers, thrownError);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();

                if (typeof(TReturn) == typeof(string))
                    return (TReturn)(object)responseAsString;

                return JsonCast.To<TReturn>(responseAsString);
            }

            return default;
        }

        public async Task<bool> PostRest<TSend>(string url, TSend sender, Dictionary<string, string>? headers = null, bool thrownError = true)
        {
            string jsonString = JsonCast.ToJson(sender);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var httpResponse = await Request(HttpMethod.Post, url, stringContent, headers, thrownError);

            return httpResponse.IsSuccessStatusCode;
        }

        public async Task<TReturn?> PostRest<TReturn>(string url, Dictionary<string, string>? headers = null, bool thrownError = true)
        {
            var httpResponse = await Request(HttpMethod.Post, url, null, headers, thrownError);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();

                if (typeof(TReturn) == typeof(string))
                    return (TReturn)(object)responseAsString;

                return JsonCast.To<TReturn>(responseAsString);
            }

            return default;
        }

        public async Task<TReturn?> PutRest<TReturn, TSend>(string url, TSend sender, Dictionary<string, string>? headers = null, bool thrownError = true)
        {
            string jsonString = JsonCast.ToJson(sender);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var httpResponse = await Request(HttpMethod.Put, url, stringContent, headers, thrownError);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();

                if (typeof(TReturn) == typeof(string))
                    return (TReturn)(object)responseAsString;

                return JsonCast.To<TReturn>(responseAsString);
            }

            return default;
        }

        public async Task<bool> PutRest<TSend>(string url, TSend sender, Dictionary<string, string>? headers = null, bool thrownError = true)
        {
            string jsonString = JsonCast.ToJson(sender);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var httpResponse = await Request(HttpMethod.Put, url, stringContent, headers, thrownError);

            return httpResponse.IsSuccessStatusCode;
        }

        public async Task<TReturn?> PutRest<TReturn>(string url, Dictionary<string, string>? headers = null, bool thrownError = true)
        {
            var httpResponse = await Request(HttpMethod.Put, url, null, headers, thrownError);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();

                if (typeof(TReturn) == typeof(string))
                    return (TReturn)(object)responseAsString;

                return JsonCast.To<TReturn>(responseAsString);
            }

            return default;
        }

        public async Task<bool> DeleteRest(string url, Dictionary<string, string>? headers = null, bool thrownError = true)
        {
            var httpResponse = await Request(HttpMethod.Put, url, null, headers, thrownError);

            return httpResponse.IsSuccessStatusCode;
        }

        #endregion "Rest Methods"

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
            if (CustomHeaders.ContainsKey(customAuthentication.Scheme))
                CustomHeaders[customAuthentication.Scheme].Add(customAuthentication.Parameter);
            else
                CustomHeaders.Add(customAuthentication.Scheme, 
                    new List<string>() { customAuthentication.Parameter });

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

        public void Dispose()
        {
            Client?.Dispose();
            Client = null;
        }

        #endregion "Builder"
    }
}