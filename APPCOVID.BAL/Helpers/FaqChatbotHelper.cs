using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.Assistant.v1;
using IBM.Watson.Assistant.v1.Model;
using System;
using System.Collections.Generic;

namespace APPCOVID.BAL.Helpers
{
    public class FaqChatbotHelper
    {
        #region Constants
        private const string API_KEY = "lKnLZps8EorCtKpwGexlYzr1CwQOfze2dZsNHStiXufv";
        private string VERSION_DATE = DateTime.Now.ToString("yyyy-mm-dd");
        private const string SERVICE_URL = "https://api.eu-gb.assistant.watson.cloud.ibm.com/instances/54dfc500-aa8a-4e48-ad61-6966509bfa86";                                                                                                       
        private const string WORKSPACE_ID_COVID_FAQ = "3f0fbb4a-01a0-40f6-8a70-fea314e687fc";

        #endregion
        //public static CovidChatbotHelper _getInstance { get; set; }

        public FaqChatbotHelper()
        {

        }

        #region Message
        public OutputData MessageToFaqCovid19Bot(string message)
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: API_KEY);

            AssistantService service = new AssistantService(VERSION_DATE, authenticator);
            service.SetServiceUrl(SERVICE_URL);
            //List<RuntimeIntent> runtimeIntents = new List<RuntimeIntent>();
            //runtimeIntents.Add(new RuntimeIntent { Intent="Agree"});

            DetailedResponse<MessageResponse> result = service.Message(
                workspaceId: WORKSPACE_ID_COVID_FAQ,
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
