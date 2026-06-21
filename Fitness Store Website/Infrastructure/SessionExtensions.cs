using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Fitness_Store_Website.Infrastructure
{
    public static class SessionExtensions
    {
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value, options));
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null) return default;
            return JsonSerializer.Deserialize<T>(data, options);
        }
    }
}