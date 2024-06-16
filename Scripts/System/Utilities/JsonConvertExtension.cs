using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace UserSystemFramework.Scripts.System.Utilities
{
    public static class JsonConvertExtension
    {
        public static string SerializeObject([CanBeNull] object objectToSerialize)
        {
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                DateFormatString = "yyyy'-'MM'-'dd' 'HH':'mm':'ss",
                NullValueHandling = NullValueHandling.Include
            };
            return JsonConvert.SerializeObject(objectToSerialize, jsonSettings);
        }
    }
}