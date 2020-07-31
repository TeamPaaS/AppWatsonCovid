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

        public List<ConversationViewModel> GetAllConversation()
        {
            List<ConversationViewModel> conversations = new List<ConversationViewModel>();
            foreach (ConversationDto conversation in _conversationManager.GetConversationData())
            {
                conversations.Add(CommonHelper.ConvertTo<ConversationViewModel>(conversation));
            }
            return conversations;
        }

        public List<ConversationViewModel> GetConversationByParams(int? userId = null, string convType = null)
        {
            List<ConversationViewModel> conversations = GetAllConversation();
            if (userId != null)
            {
                conversations = conversations.Where(t => t.USERID == userId).OrderByDescending(t => t.CONVERSID).ToList();
            }
            if (convType != null)
            {
                conversations = conversations.Where(t => t.TYPE == convType).OrderByDescending(t => t.CONVERSID).ToList();
            }
            if (userId != null && convType != null)
            {
                conversations = conversations.Where(t => t.USERID == userId && t.TYPE == convType).OrderByDescending(t => t.CONVERSID).ToList();
            }
            return conversations;
        }

        public bool CreateConversation(ConversationViewModel conversation)
        {
            return _conversationManager.CreateConversation(CommonHelper.ConvertTo<ConversationDto>(conversation));
        }

        public IList<InfectionSeverityCodesModel> FindInfections(int? userId = null, string convType = null)
        {
            IList<InfectionSeverityCodesModel> infections = new List<InfectionSeverityCodesModel>();
            ConversationViewModel affectedInfections = new ConversationHelper().GetConversationByParams(userId, "OST").FirstOrDefault();
            if (affectedInfections == null) {
                return infections;
            }
            if (!string.IsNullOrEmpty(affectedInfections.MESSAGE))
            {
                string[] qustnAnsArr = affectedInfections.MESSAGE.Split('-');
                foreach (string itm in qustnAnsArr)
                {
                    if (!string.IsNullOrEmpty(itm) && 
                        (!string.Equals(itm, "welcome", StringComparison.CurrentCultureIgnoreCase)) && 
                        (!string.Equals(itm, "yesQ0", StringComparison.CurrentCultureIgnoreCase)))
                    {
                        if (itm.Substring(0, 2).ToString().ToLower() == "ye")
                        {
                            if (itm.Length == 6)
                            {
                                InfectionSeverityCodesModel infection =
                                    InfectionsRepo.InfectionsSeverityList
                                    .Where(t => string.Equals(t.InfectionQuestion, itm.Substring(itm.Length - 3, 3), StringComparison.CurrentCultureIgnoreCase))
                                    .FirstOrDefault();
                                infection.Answer = true;
                                infections.Add(infection);
                            }
                            else
                            {
                                InfectionSeverityCodesModel infection =
                                        InfectionsRepo.InfectionsSeverityList
                                        .Where(t => string.Equals(t.InfectionQuestion, itm.Substring(itm.Length - 2, 2), StringComparison.CurrentCultureIgnoreCase))
                                        .FirstOrDefault();
                                infection.Answer = true;
                                infections.Add(infection);
                            }
                        }
                        else {
                            if (itm.Length == 5)
                            {
                                InfectionSeverityCodesModel infection =
                                    InfectionsRepo.InfectionsSeverityList
                                    .Where(t => string.Equals(t.InfectionQuestion, itm.Substring(itm.Length - 3, 3), StringComparison.CurrentCultureIgnoreCase))
                                    .FirstOrDefault();                               
                                infections.Add(infection);
                            }
                            else
                            {
                                InfectionSeverityCodesModel infection =
                                        InfectionsRepo.InfectionsSeverityList
                                        .Where(t => string.Equals(t.InfectionQuestion, itm.Substring(itm.Length - 2, 2), StringComparison.CurrentCultureIgnoreCase))
                                        .FirstOrDefault();
                                infections.Add(infection);
                            }
                        }
                    }
                }
            }
            return infections;
        }
    }
}
