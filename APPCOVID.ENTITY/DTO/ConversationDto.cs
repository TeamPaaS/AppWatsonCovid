using System;
using System.Collections.Generic;
using System.Text;

namespace APPCOVID.Entity.DTO
{
    public class ConversationDto
    {
        public int CONVERSID { get; set; }
        public string TYPE { get; set; }
        public int USERID { get; set; }
        public string MESSAGE { get; set; }
        public string CONVERSDATETIME { get; set; }
    }
}
