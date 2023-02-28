using ApiAuthomation.Support;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Data;
using System.Net;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ApiAuthomation.StepDefinitions
{
    [Binding]
    public class UsersApiStepDefinitions
    {
        private HttpClientHelper httphelper;

        private ApiResponse apiResponse;

        private class UserResponseData
        {
            public int id;
            public string first_name;
            public string email;
            public string last_name;
            public string avatar;
        }

        private class ResourceResponseData
        {
            public int id;
            public string name;
            public string year;
            public string color;
            public string pantone_value;
        }


        [Given(@"""([^""]*)"" request")]
        public void GivenRequest(string requestMethod)
        {
            httphelper = new HttpClientHelper(requestMethod);
        }

        [Given(@"Pass the userId ""([^""]*)"" in the request for ""([^""]*)"" endpoint")]
        public void GivenPassTheUserIdInTheRequestForEndpoint(string userId, string endpointName)
        {
            string endpoint = httphelper.configHelper.EnvironmentVariables["usersEndpoint"];

            if (!string.IsNullOrEmpty(userId))
            {
                endpoint = endpoint + "/" + userId;

            }
            apiResponse = httphelper.makeRequest(endpoint, "", "");
        }

        [Given(@"Pass the resourceId ""([^""]*)"" in the request for ""([^""]*)"" endpoint")]
        public void GivenPassTheResourceIdInTheRequestForEndpoint(string resourceId, string endpointName)
        {
            string endpoint = httphelper.configHelper.EnvironmentVariables["usersListEndpoint"];

            if (!string.IsNullOrEmpty(resourceId))
            {
                endpoint = httphelper.configHelper.EnvironmentVariables["usersListEndpoint"] + "/" + resourceId;
            }

            apiResponse = httphelper.makeRequest(endpoint, "", "");
        }

        [Given(@"Pass the ""([^""]*)"" param as ""([^""]*)"" in the request  for ""([^""]*)"" endpoint")]
        public void GivenPassTheParamAsInTheRequestForEndpoint(string param, string paramValue, string _endpoint)
        {
            string endpoint = httphelper.configHelper.EnvironmentVariables["usersListEndpoint"] + "?" + param + "=" + paramValue;
            apiResponse = httphelper.makeRequest(endpoint, "", "");
        }

 


        [Given(@"Pass the json input file as payload for ""([^""]*)"" endpoint")]
        public void GivenPassTheJsonInputFileAsPayloadForEndpoint(string _endpoint, Table table)
        {
            string endpoint = string.Empty;
            switch (_endpoint)
            {
                case "User":
                    endpoint = httphelper.configHelper.EnvironmentVariables["usersListEndpoint"];
                    break;
                case "Login":
                    endpoint = httphelper.configHelper.EnvironmentVariables["loginUrl"];
                    break;
                case "Register":
                    endpoint = httphelper.configHelper.EnvironmentVariables["registerUrl"];
                    break;
            }
            string payload = httphelper.readJsonFile(table);
            apiResponse = httphelper.makeRequest(endpoint, "", payload);
        }


        [Then(@"The response status should be ""([^""]*)""")]
        public void ThenTheResponseStatusShouldBe(int expectedStatus)
        {
            Assert.AreEqual(expectedStatus, apiResponse.status);
        }

        [Given(@"Pass the json input file as payload for ""([^""]*)"" endpoint for userId ""([^""]*)"" in the request")]
        public void GivenPassTheJsonInputFileAsPayloadForEndpointForUserIdInTheRequest(string _endpoint, string userId, Table table)
        {
            string endpoint = httphelper.configHelper.EnvironmentVariables["usersEndpoint"];

            if (!string.IsNullOrEmpty(userId))
            {
                endpoint = endpoint + "/" + userId;

            }
            string payload = httphelper.readJsonFile(table);
            apiResponse = httphelper.makeRequest(endpoint, "", payload);
        }


        [Then(@"The ""([^""]*)"" reponse record should be:")]
        public void ThenTheReponseRecordShouldBe(string type, Table responseTable)
        {
            JObject jsonResponse = JObject.Parse(apiResponse.responseData);

            switch (type)
            {
                case "user":
                    {
                        UserResponseData expectedResponseData = responseTable.CreateInstance<UserResponseData>();

                        UserResponseData actualResponseData = jsonResponse["data"].ToObject<UserResponseData>();

                        AssertHelper.HasEqualFieldValues(expectedResponseData, actualResponseData);
                        break;
                    }
                case "resource":
                    {
                        ResourceResponseData expectedResponseData = responseTable.CreateInstance<ResourceResponseData>();

                        ResourceResponseData actualResponseData = jsonResponse["data"].ToObject<ResourceResponseData>();

                        AssertHelper.HasEqualFieldValues(expectedResponseData, actualResponseData);
                        break;
                    }
            }
        }

      

    }
}
