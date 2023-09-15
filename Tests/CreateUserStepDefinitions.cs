using APIAutomation;
using APIAutomation.Model.Request;
using APIAutomation.Model.Response;
using APIAutomation.Utility;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace Tests
{
    [Binding]
    public class CreateUserStepDefinitions
    {
        private createUserReq createUserReq;
        private RestResponse response;
        private ScenarioContext scenarioContext;
        private HttpStatusCode statusCode;
        private APIClient api;

        public CreateUserStepDefinitions(createUserReq createUserReq, ScenarioContext scenarioContext)
        {
            this.createUserReq = createUserReq;
            this.scenarioContext = scenarioContext;
            api = new ApiClient1();
        }

        [Given(@"User payload ""([^""]*)""")]
        public void GivenUserPayload(string filename)
        {
            string file = HandleContent.GetFilePath(filename);
            var payload = HandleContent.Parsejson<createUserReq>(file);
            payload.name = "";
            scenarioContext.Add("createUser_payload", payload);
        }


        [When(@"Send request to create user")]
        public async Task WhenSendRequestToCreateUser()
        {
            createUserReq = scenarioContext.Get<createUserReq>("createUser_payload");
            response = await api.CreateUser<createUserReq>(createUserReq);
        }

        [Then(@"Validate user is created")]
        public void ThenValidateUserIsCreated()
        {
            statusCode = response.StatusCode;
            var code = (int)statusCode;
            Assert.AreEqual(201, code);

            var content = HandleContent.GetContent<CreateUserRes>(response);
        }
    }
}
