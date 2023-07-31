using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
