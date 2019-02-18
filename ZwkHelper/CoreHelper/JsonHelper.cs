using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CoreHelper
{
    public class JsonHelper
    {
        /// <summary>
        /// 默认序列化参数
        /// 微软时间格式，时间格式化，驼峰
        /// </summary>
        public static JsonSerializerSettings DefaultSetting => new JsonSerializerSettings()
        {
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, DefaultSetting);
        }

        public static string Serialize(object obj, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }

        public static T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public T Deserialize<T>(string input, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(input, settings);
        }

    }
}
