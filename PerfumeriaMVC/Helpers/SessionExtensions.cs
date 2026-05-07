using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace PerfumeriaMVC.Helpers
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetObjectFromJson<T>(this ISession session, string key)
       {
            var value = session.GetString(key);

            if (string.IsNullOrEmpty(value)) 
            return default;

            return JsonSerializer.Deserialize<T>(value);
        }
    }
}