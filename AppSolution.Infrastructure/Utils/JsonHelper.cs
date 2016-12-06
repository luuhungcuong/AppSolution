using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AppSolution.Infrastructure.Utils
{
    public class JsonHelper
    {
        public static T ToModel<T>(string json) where T : new()
        {
            return !string.IsNullOrEmpty(json) ? JsonConvert.DeserializeObject<T>(json) : new T();           
        }
        public static string FromModel<T>(T json) where T : new()
        {
            return json != null ? JsonConvert.SerializeObject(json) : String.Empty;
        }
        
        public static object ToObject(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }
        public static string ToString(object json)
        {
            return JsonConvert.SerializeObject(json);
        }
    }
}