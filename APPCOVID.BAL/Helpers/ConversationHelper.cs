using APPCOVID.DAL.DataManagers.Managers;
using APPCOVID.Entity.DTO;
using APPCOVID.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APPCOVID.BAL.Helpers
{
    public class ConversationHelper
    {
        private ConversationManager _conversationManager;
        public ConversationHelper()
        {
            _conversationManager = new ConversationManager();
        }

        public List<ConversationViewModel> GetAllConversation() {
            List<ConversationViewModel> conversations = new List<ConversationViewModel>();
            foreach (ConversationDto conversation in _conversationManager.GetConversationData()) {
                conversations.Add(CommonHelper.ConvertTo<ConversationViewModel>(conversation));
            }
            return conversations;
        }

        public List<ConversationViewModel> GetConversationByParams(int? userId = null, string convType = null) {
            List<ConversationViewModel> conversations = GetAllConversation();
            if (userId != null)
            {
                conversations = conversations.Where(t => t.USERID == userId).OrderByDescending(t=>t.CONVERSID).ToList();
            }
            if (convType != null)
            {
                conversations = conversations.Where(t => t.TYPE == convType).OrderByDescending(t => t.CONVERSID).ToList();
            }
            if (userId != null && convType != null) {
                conversations = conversations.Where(t => t.USERID == userId && t.TYPE == convType).OrderByDescending(t => t.CONVERSID).ToList();
            }
            return conversations;
        }

        public bool CreateConversation(ConversationViewModel conversation) {
           return _conversationManager.CreateInsuranceProduct(CommonHelper.ConvertTo<ConversationDto>(conversation));
        }
    }
}
