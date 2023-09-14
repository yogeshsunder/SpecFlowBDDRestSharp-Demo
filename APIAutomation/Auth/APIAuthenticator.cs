
using APIAutomation.Model.Response;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation.Auth
{
    public class APIAuthenticator : AuthenticatorBase
    {
        readonly string baseUrl;
        readonly string clientId;
        readonly string clientSecret;
        

        public APIAuthenticator() : base("")
        {
        }

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            var token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, token);
        }

        private async Task<string> GetToken()
        {
            var options = new RestClientOptions(baseUrl);
            options.Authenticator = new HttpBasicAuthenticator(clientId, clientSecret);
            var client = new RestClient(options)
            {
                
            };
            var request = new RestRequest("oAuth2/token")
                .AddParameter("grant_type", "client_credentials");
            var response = await client.PostAsync<TokenRes>(request);
            return $"{response.TokenType} {response.AccessToken}";
        }
    }
}
