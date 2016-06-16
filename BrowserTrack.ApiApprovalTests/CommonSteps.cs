using System.Collections.Generic;
using System.Linq;
using System.Net;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Deserializers;
using TechTalk.SpecFlow;
using Newtonsoft.Json.Linq;
using RestSpecApprovalTester.Helpers;
using System.Configuration;
using System;

namespace RestSpecApprovalTester
{

    [Binding]
#if DEBUG
    [UseReporter(typeof(BeyondCompareReporter))]
#endif
    public class CommonSteps
    {

        private string BaseURI
        {
            get
            {
                var _BaseUri = ConfigurationManager.AppSettings["BaseUri"];

                if (string.IsNullOrEmpty(_BaseUri))
                {
                    throw new ArgumentNullException("BaseUri URL not defined under applicaton settings");
                }
                else return _BaseUri.TrimEnd(new char[] { '/' });
            }
        }


        [Given(@"the service address '(.*)'")]
        public void GivenTheServiceAddress(string serviceAddress)
        {


            var client = new RestClient(BaseURI + serviceAddress);
            ScenarioContext.Current.Add("client", client);
        }


        [Given(@"successful bearer token authentication with user ""(.*)"" and password ""(.*)""")]
        public void GivenSuccessfulBearerTokenAuthenticationWithUserAndPassword(string user, string pass)
        {

            var authClient = new RestClient(BaseURI + "/token");
            var authRequest = new RestRequest(Method.POST);
            authRequest.AddParameter("grant_type", "password");
            authRequest.AddParameter("username", user);
            authRequest.AddParameter("password", pass);

            var response = authClient.Execute(authRequest);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new AssertFailedException("Bearer token authentication attempt failed");

            JObject json = JObject.Parse(response.Content);
            var bearerToken = json["access_token"].ToString();

            // Get the request for test case and add auth token to header
            var request = GetRequest();
            request.AddHeader("Authorization", "Bearer " + bearerToken);

        }



        [Given(@"the GET request")]
        public void GivenTheGetRequest()
        {
            var request = new RestRequest(Method.GET);
            ScenarioContext.Current.Add("request", request);
        }

        [Given(@"the POST request")]
        public void GivenThePOSTRequest()
        {
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;

            ScenarioContext.Current.Add("request", request);
        }


        [When(@"the request is sent")]
        public void WhenTheRequestIsSent()
        {
            var client = GetClient();
            var request = GetRequest();

            var response = client.Execute(request);

            ScenarioContext.Current.Add("response", response);
        }

        [Then(@"the response status is OK")]
        public void ThenTheResponseStatusIsOk()
        {
            var response = GetResponse();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Then(@"the response status is 401 Not Authenticated")]
        public void ThenTheResponseStatusIsNoAuth()
        {
            var response = GetResponse();

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Then(@"the response content type is JSON")]
        public void ThenTheResponseContentTypeIsJson()
        {
            var response = GetResponse();

            Assert.IsTrue(response.ContentType.Contains("application/json"));
        }

        [Then(@"these unpredictable elements match below regular expressions")]
        public void ThenTheseUnpredictableElementsMatchRegex(Table table)
        {
            var response = GetResponse();
            JObject json = JObject.Parse(response.Content);

            foreach (var row in table.Rows)
            {

                var jsonKey = row["ElementJsonPath"];

                var matchingTokens = json.SelectTokens(jsonKey);
                foreach (var token in matchingTokens)
                {
                    StringAssert.Matches(
                        token.ToString(Newtonsoft.Json.Formatting.None),
                        new System.Text.RegularExpressions.Regex(RegexDictionary.GetRegexString(row["RegexValue"])),
                        string.Format("The value for json token: {0} does not match expected regular expression: {1}", token.Parent.ToString(), row["RegexValue"])
                        );

                    token.Replace(JToken.Parse("'**Scrubbed**'"));
                }
            }
            response.Content = json.ToString(Newtonsoft.Json.Formatting.None);




            //foreach (var token in tokens) {
            //    token.Replace(JToken.Parse("'**Scrubbed**'"));

            //}


            //foreach (var el in portfolio["managedPortfolioHoldings"].Children()) {

            //}

        }






        [Then(@"the content matches the existing golden copy")]
        [Then(@"the remaining content matches the existing golden copy")]
        [Then(@"the response content looks legit to a human")]
        public void ThenTheResponseContentLooksLegitToAHuman()
        {
            var response = GetResponse();

            Approvals.VerifyJson(response.Content);

        }

        [Then(@"the response content keys look legit to a human")]
        public void ThenTheResponseContentKeysLookLegitToAHuman()
        {
            var response = GetResponse();

            var jsonDeserializer = new JsonDeserializer();
            var json = jsonDeserializer.Deserialize<Dictionary<string, string>>(response);

            Approvals.Verify(json.Keys.JoinWith(","));
        }

        [Then(@"the value of the response content key of '(.*)' will look legit to a human")]
        public void ThenTheValueOfTheResponseContentKeyOfWillLookLegitToAHuman(string key)
        {
            var response = GetResponse();

            var jsonDeserializer = new JsonDeserializer();
            var json = jsonDeserializer.Deserialize<Dictionary<string, string>>(response);

            var value = json.Single(s => s.Key == key).Value;

            Approvals.Verify(value);
        }

        [Given(@"the request query parameters")]
        public void GivenTheRequestQueryParameters(Table table)
        {
            var request = GetRequest();

            foreach (var tableRow in table.Rows)
            {
                request.AddQueryParameter(tableRow["Key"], tableRow["Value"]);
            }
        }

        [Given(@"the request body parameter ""(.*)""")]
        public void GivenTheRequestBodyParameter(string json)
        {
            var request = GetRequest();
            //request.AddBody(json);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
        }




        private static IRestResponse GetResponse()
        {
            return ScenarioContext.Current.Get<IRestResponse>("response");
        }

        private static IRestClient GetClient()
        {
            return ScenarioContext.Current.Get<IRestClient>("client");
        }

        private static IRestRequest GetRequest()
        {
            return ScenarioContext.Current.Get<IRestRequest>("request");
        }

    }
}
