using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.Assistant.v1.Model;
using System;

namespace IBM.Watson.Assistant.v1
{
    public class WatsonChatbotHelper
    {
        #region Constants
        private const string API_KEY = "lKnLZps8EorCtKpwGexlYzr1CwQOfze2dZsNHStiXufv";
        private string VERSION_DATE = DateTime.Now.ToString("yyyy-mm-dd");
        private const string SERVICE_URL = "https://api.eu-gb.assistant.watson.cloud.ibm.com/instances/54dfc500-aa8a-4e48-ad61-6966509bfa86";
        private const string WORKSPACE_ID_COVID_FAQ = "676ec3ad-ac10-4a4e-b8ec-d74e016f6ad0";

        #endregion
        //public static CovidChatbotHelper _getInstance { get; set; }

        public WatsonChatbotHelper()
        {

        }

        #region Message
        public string MessageToCovid19Bot(string message)
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: API_KEY);

            AssistantService service = new AssistantService(VERSION_DATE, authenticator);
            service.SetServiceUrl(SERVICE_URL);

            DetailedResponse<MessageResponse> result = service.Message(
                workspaceId: WORKSPACE_ID_COVID_FAQ,
                input: new MessageInput()
                {
                    Text = message
                });

            return result.Result.Output.Text[0];

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
