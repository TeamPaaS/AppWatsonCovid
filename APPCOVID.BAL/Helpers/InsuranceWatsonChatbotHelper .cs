using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.Assistant.v1;
using IBM.Watson.Assistant.v1.Model;
using System;
using System.Collections.Generic;

namespace APPCOVID.BAL.Helpers
{
    public class InsuranceWatsonChatbotHelper
    {
        #region Constants
        private const string API_KEY = "lKnLZps8EorCtKpwGexlYzr1CwQOfze2dZsNHStiXufv";
        private string VERSION_DATE = DateTime.Now.ToString("yyyy-mm-dd");
        private const string SERVICE_URL = "https://api.eu-gb.assistant.watson.cloud.ibm.com/instances/54dfc500-aa8a-4e48-ad61-6966509bfa86";                                                                                                       
        private const string WORKSPACE_ID_COVID_INSURANCE_FAQ = "b39705b5-42fc-42fb-a952-583982f6822b";

        #endregion
        //public static CovidChatbotHelper _getInstance { get; set; }

        public InsuranceWatsonChatbotHelper()
        {

        }

        #region Message
        public OutputData MessageToInsuranceCovid19Bot(string message)
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: API_KEY);

            AssistantService service = new AssistantService(VERSION_DATE, authenticator);
            service.SetServiceUrl(SERVICE_URL);
            //List<RuntimeIntent> runtimeIntents = new List<RuntimeIntent>();
            //runtimeIntents.Add(new RuntimeIntent { Intent="Agree"});

            DetailedResponse<MessageResponse> result = service.Message(
                workspaceId: WORKSPACE_ID_COVID_INSURANCE_FAQ,
                input: new MessageInput()
                {
                    Text = message
                });

            return result.Result.Output;

        }
        #endregion

        #region User Data
        public void DeleteUserData()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2020-04-01", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteUserData(
                customerId: "{id}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
