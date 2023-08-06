using DevOps.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Serializer.JSON
{
    public class JSONSerializer : ISerializer
    {
        private readonly ILogger _logger;

        public JSONSerializer(ILogger logger)
        {
            _logger = logger;
        }

        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public bool TryDeserialize<T>(string str, out T result)
        {
            result = default(T);

            if (string.IsNullOrWhiteSpace(str)) 
            {
                return false;
            }

            try
            {
                result = JsonConvert.DeserializeObject<T>(str);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }

            return true;
        }

        public bool TrySerialize(object obj, out string result)
        {
            result = default(string);

            if (obj == null) 
            {
                return false;
            }

            try
            {
                result = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }

            return true;
        }
    }
}
