namespace APPCOVID.Entity.ViewModels
{
    public class UserQueryModel
    {
        public string UserQuery { get; set; }
    }

    public class ConversatioMessage
    {
        public string Message { get; set; }
        public string SendBy { get; set; }
        public int IsOption { get; set; }
        public string QuestionNo { get; set; }
    }
}
