using APPCOVID.BAL.Helpers;
using APPCOVID.Controllers.Core;
using APPCOVID.Entity.ViewModels;
using APPCOVID.Models.Session;
using IBM.Watson.Assistant.v1.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace APPCOVID.Controllers
{
    public class ChatbotController : CovidController
    {
        private ConversationHelper _conversationHelper;
        public ChatbotController() {
            _conversationHelper = new ConversationHelper();
        }
        [HttpGet]
        public IActionResult ChatbotFaq()
        {
            Authorize();
            HttpContext.Session.SetObject("conversationCurrent", string.Empty);
            return View("~/Views/Chatbot/ChatbotFaq.cshtml");
        }

        [HttpPost]
        public IActionResult SendFaqMessage(UserQueryModel userQueryModel)
        {
            Authorize();
            ChatViewModel chatViewModel = new ChatViewModel();
            string message = Request.Headers["MessageToSend"];
            var conversation = new List<ConversatioMessage>();
            conversation.Add(new ConversatioMessage { Message = userQueryModel.UserQuery, SendBy = "user" });

            #region Store Conversation
            if (HttpContext.Session.GetObject("conversationCurrent") != null)
            {
                string testHist = !string.IsNullOrEmpty(HttpContext.Session.GetObject("conversationCurrent").ToString()) 
                    ? HttpContext.Session.GetObject("conversationCurrent").ToString() 
                    : string.Empty;
                testHist = $"{testHist}-{userQueryModel.UserQuery}";
                HttpContext.Session.SetObject("conversationCurrent", testHist.ToString());
                if (userQueryModel.UserQuery.ToLower() == "noq10" || userQueryModel.UserQuery.ToLower() == "yesq10")
                {
                   
                    bool createStatus = _conversationHelper.CreateConversation(new ConversationViewModel
                    {
                        TYPE = "OST",
                        CONVERSDATETIME = DateTime.Now.ToString("dd-mm-yyyy hh:MM:tt"),
                        USERID = CurrentUserId,
                        MESSAGE = testHist
                    });
                    return createStatus ? RedirectToActionPermanent("Index", "Home") : RedirectToActionPermanent("ChatbotFaq", "Chatbot");
                }
            }
            #endregion

            WatsonChatbotHelper watsonChatbotHelper = new WatsonChatbotHelper();
            OutputData response = watsonChatbotHelper.MessageToCovid19Bot(userQueryModel.UserQuery);
            if (response.Generic.Count > 0)
            {
                foreach (var ob in response.Generic)
                {
                    if (ob.ResponseType.ToLower() == "text")
                    {
                        conversation.Add(new ConversatioMessage
                        {
                            SendBy = "bot",
                            Message = ob.Text,
                            IsOption = 0
                        });
                    }
                    if (ob.ResponseType.ToLower() == "option")
                    {
                        if (ob.Title == "Q0")
                        {
                            conversation.Add(new ConversatioMessage
                            {
                                SendBy = "bot",
                                Message = "Are you ready to Take Test? Choose options carefully!!",
                                IsOption = 1,
                                QuestionNo = ob.Title
                            });
                        }
                        else
                        {
                            conversation.Add(new ConversatioMessage
                            {
                                SendBy = "bot",
                                Message = ob.Text,
                                IsOption = 1,
                                QuestionNo = ob.Title
                            });
                        }
                    }
                }
            }
            else
            {
                conversation.Add(new ConversatioMessage
                {
                    SendBy = "bot",
                    Message = response.Text.Count > 0 ? response.Text[0] : ""
                });
            }

            chatViewModel.ChatHistory = conversation;
            return Ok(chatViewModel);
        }

        //[HttpPost]
        //public IActionResult SendMessage(UserQueryModel userQueryModel)
        //{
        //    ChatViewModel chatViewModel = new ChatViewModel();
        //    string message = Request.Headers["MessageToSend"];
        //    var conversation = new List<ConversatioMessage>();
        //    conversation.Add(new ConversatioMessage { Message = userQueryModel.UserQuery, SendBy = "user" });
        //    string response = new WatsonChatbotHelper().MessageToCovid19Bot(userQueryModel.UserQuery);
        //    conversation.Add(new ConversatioMessage { SendBy = "IBM BOT", Message = response });
        //    chatViewModel.ChatHistory = conversation;
        //    return Index(chatViewModel);
        //}


    }
}