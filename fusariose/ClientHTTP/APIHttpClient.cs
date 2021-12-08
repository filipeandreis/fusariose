using fusariose.Models;
using HTTPRequest;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace fusariose.ClientHttp
{
    public class APIHttpClient
    {
        private readonly string baseAPI = "http://localhost:64339/api/";
        public APIHttpClient()
        {
            this.baseAPI = baseAPI;
        }

        public Guid Put<T>(string action, Guid id, T data)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(baseAPI);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PutAsJsonAsync(action + id.ToString(), data).Result;
            if (response.IsSuccessStatusCode)
            {
                var sucesso = response.Content.ReadAsAsync<Guid>().Result;
                return sucesso;
            }
            else
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
        }

        public T Posst<T>(string action, T data)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(baseAPI);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync(action, data).Result;
            if (response.IsSuccessStatusCode)
            {
                var sucesso = response.Content.ReadAsAsync<T>().Result;
                return sucesso;
            }
            else
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }

            
        }

        public string Post<T>(string url, T data)
        {
            var client = new RestClient(baseAPI + url)
            {
                Timeout = -1
            };

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");

            var body = JsonConvert.SerializeObject(data);
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            
            if(response.IsSuccessful)
            {
                return response.Content;
            }
            else
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public string Get<T>(string url)
        {
            var client = new RestClient(baseAPI + url)
            {
                Timeout = -1
            };

            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return response.Content;
            }
            else
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public T Delete<T>(string action, Guid id)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(baseAPI);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.DeleteAsync(action + id.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                var sucesso = response.Content.ReadAsAsync<T>().Result;
                return sucesso;
            }
            else
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}