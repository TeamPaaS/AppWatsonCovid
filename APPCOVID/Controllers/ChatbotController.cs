using APPCOVID.Controllers.Core;
using APPCOVID.Entity.ViewModels;
using IBM.Watson.Assistant.v1;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace APPCOVID.Controllers
{
    public class ChatbotController : CovidController
    {
        [HttpGet]
        public IActionResult ChatbotFaq()
        {
            Authorize();
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
            string response = new WatsonChatbotHelper().MessageToCovid19Bot(userQueryModel.UserQuery);
            conversation.Add(new ConversatioMessage { SendBy = "bot", Message = response });
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