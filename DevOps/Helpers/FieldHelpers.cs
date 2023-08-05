using DevOps.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevOps.Helpers
{
    public static class FieldHelpers
    {
        public static IDictionary<string, object?> ToDict(object config)
        {
            var configProp = config.GetType().GetProperties();
            var configDict = configProp.ToDictionary(x => x.Name, y => y.GetValue(config, null));
            return configDict;
        }

        public static IDictionary<string, string> ToStrDict(object config)
        {
            var configProp = config.GetType().GetProperties();
            var configDict = configProp.ToDictionary(x => x.Name, y => FieldToStr(y.GetValue(config, null)));
            return configDict;
        }

        public static string FieldToStr(object? item) 
        {
            switch (item)
            {
                case string x:
                    return x.ToString();
                case DateTime x:
                    return x.ToString(CommonConst.DateTimeFormat);
                case IEnumerable<string> x:
                    return string.Join('+', x);
                default:
                    return string.Empty;
            }
        }
    }
}
