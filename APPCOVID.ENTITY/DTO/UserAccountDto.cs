using System;

namespace APPCOVID.Entity.DTO
{
    public class UserAccountDto
    {
        public int USERID { get; set; }
        public string PASSWORD { get; set; }
        public string PASSWORDKEY { get; set; }
        public string USERNAME { get; set; }
        public string REGISTRATIONDATE { get; set; }
        public string LASTLOGINDATE { get; set; }
        public string LASTPWDRESETDATE { get; set; }
        public string PWDRESETQUESTION { get; set; }
        public string PWDRESETANSWER { get; set; }
        public int ACTIVATEAC { get; set; }

    }
}
