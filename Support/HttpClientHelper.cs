using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;

namespace ApiAuthomation.Support
{
    public class HttpClientHelper
    {
        string reqMethod = string.Empty;
        string reqUrl = string.Empty;
        HttpClient client;
        public ConfigHelper configHelper;

        public HttpClientHelper(string requestMethod) 
        {
            configHelper = new ConfigHelper();
            reqMethod = requestMethod;
        }

        public ApiResponse makeRequest(string endpoint, string headers, string payload)
        {
            switch (reqMethod)
            {
                case "GET":
                    return makeGetRequest(endpoint, headers, payload);

                case "POST":
                    return makePostRequest(endpoint, headers, payload);
                
                case "PUT":
                    return makePutRequest(endpoint, headers, payload);

                case "DELETE":
                    return makeDeleteRequest(endpoint, headers, payload);
            }
            return null;
        }

        public ApiResponse makePostRequest(string endpoint, string headers, string payload)
        {
            ApiResponse apiResponse = new ApiResponse();
            reqUrl = configHelper.EnvironmentVariables["baseUrl"] + endpoint;
            client = new HttpClient();
            client.BaseAddress = new Uri(reqUrl);
            var responseTask = client.PostAsync(client.BaseAddress, new StringContent(payload));
            responseTask.Wait();
            apiResponse = mapAPIResponse(apiResponse, responseTask);
            return apiResponse;
        }

        public ApiResponse makeGetRequest(string endpoint, string headers, string payload)
        {
            ApiResponse apiResponse = new ApiResponse();
            reqUrl = configHelper.EnvironmentVariables["baseUrl"] + endpoint;
            client = new HttpClient();
            client.BaseAddress = new Uri(buildRequestUrl(reqUrl, payload));
            var responseTask = client.GetAsync(client.BaseAddress);
            responseTask.Wait();
            apiResponse = mapAPIResponse(apiResponse, responseTask);
            return apiResponse;
        }

        public ApiResponse makePutRequest(string endpoint, string headers, string payload)
        {
            ApiResponse apiResponse = new ApiResponse();
            reqUrl = configHelper.EnvironmentVariables["baseUrl"] + endpoint;
            client = new HttpClient();
            client.BaseAddress = new Uri(reqUrl);
            var responseTask = client.PutAsync(client.BaseAddress, new StringContent(payload));
            responseTask.Wait();
            apiResponse = mapAPIResponse(apiResponse, responseTask);
            return apiResponse;
        }

        public ApiResponse makeDeleteRequest(string endpoint, string headers, string payload)
        {
            ApiResponse apiResponse = new ApiResponse();
            reqUrl = configHelper.EnvironmentVariables["baseUrl"] + endpoint;
            client = new HttpClient();
            client.BaseAddress = new Uri(reqUrl);
            var responseTask = client.DeleteAsync(client.BaseAddress);
            responseTask.Wait();
            apiResponse = mapAPIResponse(apiResponse, responseTask);
            return apiResponse;
        }


        private string buildRequestUrl(string requestUrl, string payload)
        {
            if (string.IsNullOrEmpty(payload))
            {
                return requestUrl;

            } else
            {
                return null;
            }
           

        }

        private ApiResponse mapAPIResponse(ApiResponse apiResponse, Task<HttpResponseMessage> responseTask)
        {
            var result = responseTask.Result.Content.ReadAsStringAsync().Result;
            apiResponse.status = (int)responseTask.Result.StatusCode;
            apiResponse.responseData = result;
            return apiResponse;
        }

        public string readJsonFile(Table table)
        {
            string filePathName = table.Rows[0]["FileName"];
            string assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
            var fullPathofFile = Path.Combine(assemblyPath, @"" + filePathName);
            string inputData = File.ReadAllText(fullPathofFile);
            return inputData;
        }
    }
}
