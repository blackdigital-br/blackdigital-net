using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace BlackDigital.Rest
{
    /// <summary>
    /// Provides a wrapper around <see cref="HttpClient"/> for making REST requests with support for custom headers, authentication, retry logic, and error handling.
    /// </summary>
    public class RestClient : IDisposable
    {
        #region "Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient"/> class with a default base address of "/api".
        /// </summary>
        public RestClient()
            : this("/api")
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient"/> class with the specified base address and optional HTTP message handler.
        /// </summary>
        /// <param name="baseAddress">The base address for requests.</param>
        /// <param name="hanlder">An optional HTTP message handler for the underlying <see cref="HttpClient"/>.</param>
        public RestClient(string baseAddress, HttpMessageHandler? hanlder = null)
        {
            ThownType = RestThownType.OnlyBusiness;
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

        /// <summary>
        /// Gets or sets a value indicating whether failed requests should be retried.
        /// </summary>
        public bool RetryConection { get; set; }

        /// <summary>
        /// Gets or sets the delay, in milliseconds, between retry attempts.
        /// </summary>
        public int TimeRetryConnection { get; set; }

        /// <summary>
        /// Gets or sets the type of exceptions that should be thrown automatically.
        /// </summary>
        public RestThownType ThownType { get; set; }

        /// <summary>
        /// Gets the number of requests currently waiting to be retried.
        /// </summary>
        public int RetryCount => RequestsRetryConnections.Count;

        /// <summary>
        /// Occurs when a response with HTTP 401 Unauthorized status is received.
        /// </summary>
        public event RestEventHandler? Unauthorized;

        /// <summary>
        /// Occurs when a response with HTTP 403 Forbidden status is received.
        /// </summary>
        public event RestEventHandler? Forbidden;

        /// <summary>
        /// Occurs when a connection error happens.
        /// </summary>
        public event RestEventHandler? ConnectionError;

        /// <summary>
        /// Occurs when a response with HTTP 5xx status is received and <see cref="ThownType"/> is <see cref="RestThownType.All"/>.
        /// </summary>
        public event RestEventHandler? ServerError;

        /// <summary>
        /// Gets the underlying <see cref="HttpClient"/> used by this client.
        /// </summary>
        public HttpClient Client { get; private set; }

        /// <summary>
        /// The HTTP message handler used by the underlying <see cref="HttpClient"/>, if a custom one was provided.
        /// </summary>
        protected internal HttpMessageHandler? HttpHandler;

        /// <summary>
        /// The custom headers applied to every request sent by this client.
        /// </summary>
        protected Dictionary<string, List<string>> CustomHeaders;

        /// <summary>
        /// The requests that are pending retry due to a connection error.
        /// </summary>
        protected List<HttpRequestMessage> RequestsRetryConnections;

        private bool _threadRun;
        private object _lockThread = new();
        private Thread? _threadRetryConnection;

        #endregion "Properties"

        #region "Client Methods"

        private const string Authorization = "Authorization";

        /// <summary>
        /// Sends an HTTP GET request to the specified URI.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <returns>The HTTP response message.</returns>
        public Task<HttpResponseMessage> GetAsync(string requestUri) => Client.GetAsync(requestUri);

        /// <summary>
        /// Sends an HTTP POST request to the specified URI with the provided content.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="content">The HTTP request content.</param>
        /// <returns>The HTTP response message.</returns>
        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content) => Client.PostAsync(requestUri, content);

        /// <summary>
        /// Sends an HTTP PUT request to the specified URI with the provided content.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="content">The HTTP request content.</param>
        /// <returns>The HTTP response message.</returns>
        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content) => Client.PutAsync(requestUri, content);

        /// <summary>
        /// Sends an HTTP DELETE request to the specified URI.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <returns>The HTTP response message.</returns>
        public Task<HttpResponseMessage> DeleteAsync(string requestUri) => Client.DeleteAsync(requestUri);

        /// <summary>
        /// Sends an HTTP GET request and returns the response body as a byte array.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <returns>The response body as a byte array.</returns>
        public Task<byte[]> GetByteArrayAsync(string requestUri) => Client.GetByteArrayAsync(requestUri);

        //protected virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) => Client.SendAsync(request);

        #endregion "Client Methods"

        #region "Rest Methods"

        /// <summary>
        /// Sends an HTTP request and deserializes the response into the specified type.
        /// </summary>
        /// <param name="method">The HTTP method to use.</param>
        /// <param name="url">The request URL.</param>
        /// <param name="responseType">The type to deserialize the response into, or <c>null</c> to ignore the body.</param>
        /// <param name="content">The request body content, or <c>null</c>.</param>
        /// <param name="headers">Additional headers for this request, or <c>null</c>.</param>
        /// <param name="throwType">Defines which HTTP error statuses should throw an exception, or <c>null</c> to use the default.</param>
        /// <returns>The deserialized response, or <c>null</c>.</returns>
        internal protected virtual async Task<object?> RequestAsync(HttpMethod method,
                                                                    string url,
                                                                    Type? responseType = null,
                                                                    object? content = null,
                                                                    Dictionary<string, string>? headers = null,
                                                                    RestThownType? throwType = null)
        {
            HttpContent? httpContent = null;

            if (content != null)
            {
                string jsonString = JsonCast.ToJson(content);
                httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            }

            var httpResponse = await RequestAsync(method, url, httpContent, headers, throwType);

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

        /// <summary>
        /// Sends an HTTP request with the specified method, URL, content, and headers.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="url">The request URL.</param>
        /// <param name="content">The optional HTTP request content.</param>
        /// <param name="headers">The optional request headers.</param>
        /// <param name="throwType">The optional exception throwing behavior for this request.</param>
        /// <returns>The HTTP response message.</returns>
        protected virtual async Task<HttpResponseMessage> RequestAsync(HttpMethod method, 
                                                          string url, 
                                                          HttpContent? content = null,
                                                          Dictionary<string, string>? headers = null,
                                                          RestThownType? throwType = null)
        {
            var request = new HttpRequestMessage(method, url);

            if (headers != null)
            {
                foreach (var header in headers)
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            if (content != null)
                request.Content = content;

            return await RequestAsync(request, throwType);
        }

        /// <summary>
        /// Sends an HTTP request message and handles the response according to the configured throwing behavior.
        /// </summary>
        /// <param name="requestMessage">The HTTP request message.</param>
        /// <param name="throwType">The optional exception throwing behavior for this request.</param>
        /// <returns>The HTTP response message.</returns>
        protected virtual async Task<HttpResponseMessage> RequestAsync(HttpRequestMessage requestMessage, RestThownType? throwType = null)
        {
            throwType ??= ThownType;

            try
            {
                var httpResponse = await Client.SendAsync(requestMessage);

                if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Unauthorized?.Invoke(RestEventArgs.Create(requestMessage, httpResponse));
                }
                else if (httpResponse.StatusCode == HttpStatusCode.Forbidden)
                {
                    Forbidden?.Invoke(RestEventArgs.Create(requestMessage, httpResponse));
                }
                else if (throwType != RestThownType.None && httpResponse.StatusCode.IsClientError())
                {
                    var responseAsString = await httpResponse.Content.ReadAsStringAsync();
                    BusinessException.Throw(responseAsString, (int)httpResponse.StatusCode);
                }
                else if (throwType == RestThownType.All && httpResponse.StatusCode.IsServerError())
                {
                    ServerError?.Invoke(RestEventArgs.Create(requestMessage, httpResponse));
                    throw new Exception("Connection error", new(await httpResponse.Content.ReadAsStringAsync()));
                }

                EndRequest(requestMessage);

                return httpResponse;
            }
            catch (HttpRequestException requestError)
            {

                ErrorConnection(requestMessage, requestError);

                if (throwType == RestThownType.All)
                    throw;

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                EndRequest(requestMessage);

                if (throwType == RestThownType.All
                    || (throwType == RestThownType.OnlyBusiness && ex is BusinessException))
                    throw;


                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Sends an HTTP GET request and deserializes the JSON response into the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response into.</typeparam>
        /// <param name="url">The request URL.</param>
        /// <param name="headers">The optional request headers.</param>
        /// <param name="throwType">The optional exception throwing behavior for this request.</param>
        /// <returns>The deserialized response, or <c>null</c> if the request failed.</returns>
        public async Task<T?> GetRestAsync<T>(string url, Dictionary<string, string>? headers = null, RestThownType? throwType = null)
        {
            var httpResponse = await RequestAsync(HttpMethod.Get, url, null, headers, throwType);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();

                if (typeof(T) == typeof(string))
                    return (T)(object)responseAsString;

                return JsonCast.To<T>(responseAsString);
            }

            return default;
        }

        /// <summary>
        /// Sends an HTTP POST request with a JSON body and deserializes the JSON response into the specified return type.
        /// </summary>
        /// <typeparam name="TReturn">The type to deserialize the response into.</typeparam>
        /// <typeparam name="TSend">The type of the request body.</typeparam>
        /// <param name="url">The request URL.</param>
        /// <param name="sender">The object to serialize as the request body.</param>
        /// <param name="headers">The optional request headers.</param>
        /// <param name="throwType">The optional exception throwing behavior for this request.</param>
        /// <returns>The deserialized response, or <c>null</c> if the request failed.</returns>
        public async Task<TReturn?> PostRestAsync<TReturn, TSend>(string url, TSend sender, Dictionary<string, string>? headers = null, RestThownType? throwType = null)
        {
            string jsonString = JsonCast.ToJson(sender);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var httpResponse = await RequestAsync(HttpMethod.Post, url, stringContent, headers, throwType);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();

                if (typeof(TReturn) == typeof(string))
                    return (TReturn)(object)responseAsString;

                return JsonCast.To<TReturn>(responseAsString);
            }

            return default;
        }

        /// <summary>
        /// Sends an HTTP POST request with a JSON body and returns whether the request succeeded.
        /// </summary>
        /// <typeparam name="TSend">The type of the request body.</typeparam>
        /// <param name="url">The request URL.</param>
        /// <param name="sender">The object to serialize as the request body.</param>
        /// <param name="headers">The optional request headers.</param>
        /// <param name="throwType">The optional exception throwing behavior for this request.</param>
        /// <returns><c>true</c> if the request succeeded; otherwise, <c>false</c>.</returns>
        public async Task<bool> PostRestAsync<TSend>(string url, TSend sender, Dictionary<string, string>? headers = null, RestThownType? throwType = null)
        {
            string jsonString = JsonCast.ToJson(sender);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var httpResponse = await RequestAsync(HttpMethod.Post, url, stringContent, headers, throwType);

            return httpResponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Sends an HTTP POST request without a body and deserializes the JSON response into the specified return type.
        /// </summary>
        /// <typeparam name="TReturn">The type to deserialize the response into.</typeparam>
        /// <param name="url">The request URL.</param>
        /// <param name="headers">The optional request headers.</param>
        /// <param name="throwType">The optional exception throwing behavior for this request.</param>
        /// <returns>The deserialized response, or <c>null</c> if the request failed.</returns>
        public async Task<TReturn?> PostRestAsync<TReturn>(string url, Dictionary<string, string>? headers = null, RestThownType? throwType = null)
        {
            var httpResponse = await RequestAsync(HttpMethod.Post, url, null, headers, throwType);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();

                if (typeof(TReturn) == typeof(string))
                    return (TReturn)(object)responseAsString;

                return JsonCast.To<TReturn>(responseAsString);
            }

            return default;
        }

        /// <summary>
        /// Sends an HTTP PUT request with a JSON body and deserializes the JSON response into the specified return type.
        /// </summary>
        /// <typeparam name="TReturn">The type to deserialize the response into.</typeparam>
        /// <typeparam name="TSend">The type of the request body.</typeparam>
        /// <param name="url">The request URL.</param>
        /// <param name="sender">The object to serialize as the request body.</param>
        /// <param name="headers">The optional request headers.</param>
        /// <param name="throwType">The optional exception throwing behavior for this request.</param>
        /// <returns>The deserialized response, or <c>null</c> if the request failed.</returns>
        public async Task<TReturn?> PutRestAsync<TReturn, TSend>(string url, TSend sender, Dictionary<string, string>? headers = null, RestThownType? throwType = null)
        {
            string jsonString = JsonCast.ToJson(sender);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var httpResponse = await RequestAsync(HttpMethod.Put, url, stringContent, headers, throwType);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();

                if (typeof(TReturn) == typeof(string))
                    return (TReturn)(object)responseAsString;

                return JsonCast.To<TReturn>(responseAsString);
            }

            return default;
        }

        /// <summary>
        /// Sends an HTTP PUT request with a JSON body and returns whether the request succeeded.
        /// </summary>
        /// <typeparam name="TSend">The type of the request body.</typeparam>
        /// <param name="url">The request URL.</param>
        /// <param name="sender">The object to serialize as the request body.</param>
        /// <param name="headers">The optional request headers.</param>
        /// <param name="throwType">The optional exception throwing behavior for this request.</param>
        /// <returns><c>true</c> if the request succeeded; otherwise, <c>false</c>.</returns>
        public async Task<bool> PutRestAsync<TSend>(string url, TSend sender, Dictionary<string, string>? headers = null, RestThownType? throwType = null)
        {
            string jsonString = JsonCast.ToJson(sender);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var httpResponse = await RequestAsync(HttpMethod.Put, url, stringContent, headers, throwType);

            return httpResponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Sends an HTTP PUT request without a body and deserializes the JSON response into the specified return type.
        /// </summary>
        /// <typeparam name="TReturn">The type to deserialize the response into.</typeparam>
        /// <param name="url">The request URL.</param>
        /// <param name="headers">The optional request headers.</param>
        /// <param name="throwType">The optional exception throwing behavior for this request.</param>
        /// <returns>The deserialized response, or <c>null</c> if the request failed.</returns>
        public async Task<TReturn?> PutRestAsync<TReturn>(string url, Dictionary<string, string>? headers = null, RestThownType? throwType = null)
        {
            var httpResponse = await RequestAsync(HttpMethod.Put, url, null, headers, throwType);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();

                if (typeof(TReturn) == typeof(string))
                    return (TReturn)(object)responseAsString;

                return JsonCast.To<TReturn>(responseAsString);
            }

            return default;
        }

        /// <summary>
        /// Sends an HTTP DELETE request and returns whether the request succeeded.
        /// </summary>
        /// <param name="url">The request URL.</param>
        /// <param name="headers">The optional request headers.</param>
        /// <param name="throwType">The optional exception throwing behavior for this request.</param>
        /// <returns><c>true</c> if the request succeeded; otherwise, <c>false</c>.</returns>
        public async Task<bool> DeleteRestAsync(string url, Dictionary<string, string>? headers = null, RestThownType? throwType = null)
        {
            var httpResponse = await RequestAsync(HttpMethod.Delete, url, null, headers, throwType);

            return httpResponse.IsSuccessStatusCode;
        }

        #endregion "Rest Methods"

        #region "Internal Methods"

        /// <summary>
        /// Handles a connection error by logging it, queuing the request for retry, and raising the <see cref="ConnectionError"/> event.
        /// </summary>
        /// <param name="request">The request that failed.</param>
        /// <param name="requestError">The exception thrown during the request.</param>
        protected void ErrorConnection(HttpRequestMessage request, HttpRequestException requestError)
        {
            Console.WriteLine(requestError);

            if (RetryConection && !RequestsRetryConnections.Contains(request))
            {
                RequestsRetryConnections.Add(request);
                StartRetryProcess();
            }

            ConnectionError?.Invoke(RestEventArgs.Create(request, null));
        }

        /// <summary>
        /// Finalizes a request by disposing it and removing it from the retry queue.
        /// </summary>
        /// <param name="request">The request to finalize.</param>
        protected void EndRequest(HttpRequestMessage request)
        {
            request.Dispose();
            RequestsRetryConnections.Remove(request);
        }

        private void StartRetryProcess()
        {
            lock (_lockThread) 
            {
                if (!_threadRun)
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
                        RequestAsync(request, RestThownType.All).GetAwaiter().GetResult();
                    }
                    catch (HttpRequestException)
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

        /// <summary>
        /// Releases the resources used by the <see cref="RestClient"/>.
        /// </summary>
        public void Dispose()
        {
            Client.Dispose();
            _threadRun = false;
            _threadRetryConnection = null;

            RequestsRetryConnections.ForEach(r => r.Dispose());
            RequestsRetryConnections.Clear();
        }

        #endregion "Internal Methods"

        #region "Builder"

        /// <summary>
        /// Adds a header value to the client, appending to any existing values for the same key.
        /// </summary>
        /// <param name="key">The header name.</param>
        /// <param name="value">The header value.</param>
        /// <returns>The current <see cref="RestClient"/> instance for chaining.</returns>
        public RestClient AddHeader(string key, string value)
        {
            if (CustomHeaders.ContainsKey(key))
                CustomHeaders[key].Add(value);
            else
                CustomHeaders.Add(key, new List<string>() { value });

            UpdateHeaders();
            
            return this;
        }

        /// <summary>
        /// Adds a single header value to the client, replacing any existing values for the same key.
        /// </summary>
        /// <param name="key">The header name.</param>
        /// <param name="value">The header value.</param>
        /// <returns>The current <see cref="RestClient"/> instance for chaining.</returns>
        public RestClient AddSingleHeader(string key, string value)
        {
            if (CustomHeaders.ContainsKey(key))
                CustomHeaders.Remove(key);
            
            CustomHeaders.Add(key, new List<string>() { value });

            UpdateHeaders();

            return this;
        }

        /// <summary>
        /// Removes all values for the specified header key.
        /// </summary>
        /// <param name="key">The header name.</param>
        /// <returns>The current <see cref="RestClient"/> instance for chaining.</returns>
        public RestClient RemoveHeader(string key)
        {
            if (CustomHeaders.ContainsKey(key))
                CustomHeaders.Remove(key);

            UpdateHeaders();

            return this;
        }

        /// <summary>
        /// Sets the Authorization header using the provided authentication header value.
        /// </summary>
        /// <param name="customAuthentication">The authentication header value.</param>
        /// <returns>The current <see cref="RestClient"/> instance for chaining.</returns>
        public RestClient AddAuthentication(AuthenticationHeaderValue customAuthentication)
        {
            if (CustomHeaders.ContainsKey(Authorization))
                CustomHeaders.Remove(Authorization);

            if (customAuthentication != null)
                CustomHeaders.Add(Authorization,
                    new List<string>() { $"{customAuthentication.Scheme} {customAuthentication.Parameter}" });

            UpdateHeaders();

            return this;
        }

        /// <summary>
        /// Updates the base address of the client, recreating the underlying <see cref="HttpClient"/>.
        /// </summary>
        /// <param name="url">The new base address URL.</param>
        public void UpdateBaseAddress(string url)
        {
            Dispose();

            Client = new();
            Client.BaseAddress = new(url);
            UpdateHeaders();
        }

        /// <summary>
        /// Synchronizes the custom headers into the underlying <see cref="HttpClient"/> default request headers.
        /// </summary>
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
