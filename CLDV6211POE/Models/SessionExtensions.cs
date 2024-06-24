using System.Text.Json;

namespace CLDV6211POE.Models
{
    //this class can take any datatype, being string ,int or an object
    //make a key of it and serialize/encrypt it using JSON
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        //this method takes the key and deserializes it to get the original
        //input so that it can be read by the program
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
    }
}
