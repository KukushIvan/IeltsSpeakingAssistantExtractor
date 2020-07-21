using System;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace IeltsSpeakingAssistantExtractor
{
    internal class HttpSender
    {
        private HttpClient HttpClient
        {
            get
            {
                if (_postHttpClient == null)
                {
                    HttpClientHandler handler = new HttpClientHandler { Proxy = new WebProxy(), CookieContainer = CookieContainer };
                    _postHttpClient = new HttpClient(handler);
                    HttpClient.DefaultRequestHeaders.Add("User-Agent", "okhttp/3.3.0");
                    HttpClient.DefaultRequestHeaders.ConnectionClose = false;
                    HttpClient.Timeout = new TimeSpan(0, 0, 100);
                    HttpClient.DefaultRequestHeaders.ExpectContinue = false;
                }
                return _postHttpClient;
            }
        }
        //Cookie
        internal CookieContainer CookieContainer { get; }

        private HttpClient _postHttpClient;

        internal HttpSender()
        {
            CookieContainer = new CookieContainer();
        }

        internal string Get(string url)
        {
            var httpClient = HttpClient;
            try
            {
                var responseAsync = httpClient.GetAsync(url).Result;
                return responseAsync.Content.ReadAsStringAsync().Result;
            }
            catch (Exception)
            {
                httpClient.CancelPendingRequests();
                return "";
            }
        }
    }
}