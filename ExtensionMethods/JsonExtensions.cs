using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class JsonExtensions
    {
        public static string SerializeAndIndent(this object obj)
        {
            var dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(Newtonsoft.Json.JsonConvert.SerializeObject(obj));

            dict.Add("Key", Guid.NewGuid());

            return Newtonsoft.Json.JsonConvert.SerializeObject(dict, Newtonsoft.Json.Formatting.Indented);
        }
    }
}
