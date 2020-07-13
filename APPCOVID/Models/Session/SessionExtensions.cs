using Microsoft.AspNetCore.Http;

namespace APPCOVID.Models.Session
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, string value)
        {
            session.SetString(key, value);
        }

        public static string GetObject(this ISession session, string key)
        {
            string value = session.GetString(key);
            return value;
        }
    }
}
