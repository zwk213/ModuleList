using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;

namespace WebHelper
{
    public static class HttpHelper
    {
        public static string Get(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest("", Method.GET, DataFormat.Json);
            var response = client.Execute(request);
            return response.Content;
        }

        public static string Get(string url, Dictionary<string, object> paras)
        {
            var client = new RestClient(url);
            var request = new RestRequest("", Method.GET, DataFormat.Json);
            foreach (var para in paras)
            {
                request.AddParameter(para.Key, para.Value);
            }
            var response = client.Execute(request);
            return response.Content;
        }

        public static string GetWithJwt(string url, Dictionary<string, object> paras, string accessToken)
        {
            var client = new RestClient(url) { Authenticator = new JwtAuthenticator(accessToken) };
            var request = new RestRequest("", Method.GET, DataFormat.Json);
            foreach (var para in paras)
            {
                request.AddParameter(para.Key, para.Value);
            }
            var response = client.Execute(request);
            return response.Content;
        }

        public static string Post(string url, object body)
        {
            var client = new RestClient(url);
            var request = new RestRequest("", Method.POST, DataFormat.Json);
            request.AddHeader("Accept", "application/json");
            request.AddBody(body);
            var response = client.Execute(request);
            return response.Content;
        }

        public static string PostWithJwt(string url, object body, string accessToken)
        {
            var client = new RestClient(url) { Authenticator = new JwtAuthenticator(accessToken) };
            var request = new RestRequest("", Method.POST, DataFormat.Json);
            request.AddHeader("Accept", "application/json");
            request.AddBody(body);
            var response = client.Execute(request);
            return response.Content;
        }

    }

}
