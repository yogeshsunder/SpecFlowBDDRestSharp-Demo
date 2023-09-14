using APIAutomation.Auth;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation
{
    public class ApiClient1 : APIClient, IDisposable
    {
        readonly RestClient client;
        const string BASE_URL = "https://reqres.in/";

        public ApiClient1()
        {
            var options = new RestClientOptions(BASE_URL);
            options.Authenticator = new HttpBasicAuthenticator("", "");
            client = new RestClient(options)
            {
                
            };
        }

        public async Task<RestResponse> CreateUser<T>(T payload) where T : class
        {
            var request = new RestRequest(EndPoint.CREATE_USER, Method.Post);
            request.AddBody(payload);
            return await client.ExecuteAsync<T>(request);
        }

        public async Task<RestResponse> DeleteUser(string id)
        {
           var request = new RestRequest(EndPoint.DELETE_USER, Method.Delete);
            request.AddUrlSegment(id, id);
            return await client.ExecuteAsync(request);
        }

        public void Dispose()
        {
            client?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<RestResponse> GetListofUsers(int pageNumber)
        {
            var request = new RestRequest(EndPoint.GET_LIST_OF_USERS, Method.Get);
            request.AddQueryParameter("page", pageNumber);
            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> GetUser(string id)
        {
            var request = new RestRequest(EndPoint.GET_SINGLE_USER, Method.Get);
            request.AddUrlSegment(id, id);
            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> UpdateUser<T>(T payload, string id) where T : class
        {
            var request = new RestRequest(EndPoint.GET_LIST_OF_USERS, Method.Get);
            request.AddUrlSegment(id, id);
            request.AddBody(payload);
            return await client.ExecuteAsync<T>(request);
        }
    }
}
