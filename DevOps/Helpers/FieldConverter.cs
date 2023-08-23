using DevOps.Logger;
using DevOps.Models;
using DevOps.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevOps.Helpers
{
    public class FieldConverter : IFieldConverter
    {
        private readonly ILogger _logger;

        public FieldConverter(
            ILogger logger)
        {
            _logger = logger;
        }

        public IDictionary<string, object?> ToDict(object config)
        {
            var configProp = config.GetType().GetProperties();
            var configDict = configProp.ToDictionary(x => x.Name, y => y.GetValue(config, null));
            return configDict;
        }

        public IDictionary<string, string> ToStrDict(object config)
        {
            var configDict = ToDict(config);
            var strDict = new Dictionary<string, string>();
            foreach (var item in configDict)
            {
                if (item.Value is IEnumerable<NugetConfig> || item.Value is IEnumerable<NugetJSONConfig>)
                {
                    var nugetStrDict = ObjToStrDict(item.Value);
                    MergeDict(strDict, nugetStrDict);
                }
                else
                {
                    strDict.Add(item.Key, FieldToStr(item.Value));
                }
            }
            return strDict;
        }

        private IDictionary<string,string> ObjToStrDict(object obj)
        {
            int index = 0;
            var strDict = new Dictionary<string, string>();
            if (obj is IEnumerable<NugetConfig> nugetConfigs)
            {
                foreach (var nugetConfig in nugetConfigs)
                {
                    var nugetConfigDict = ToDict(nugetConfig);
                    foreach (var item in nugetConfigDict)
                    {
                        if (item.Value is bool) continue;
                        strDict.Add($"{item.Key}_{index}", FieldToStr(item.Value));
                    }
                    index++;
                }
            }
            else if (obj is IEnumerable<NugetJSONConfig> nugetJSONConfigs)
            {
                foreach (var nugetConfig in nugetJSONConfigs)
                {
                    var nugetConfigDict = ToDict(nugetConfig);
                    foreach (var item in nugetConfigDict)
                    {
                        if (item.Value is bool) continue;
                        strDict.Add($"{item.Key}_{index}", FieldToStr(item.Value));
                    }
                    index++;
                }
            }
            return strDict;
        }

        private string FieldToStr(object? item) 
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

        public void MergeDict(IDictionary<string, string> baseDict, IDictionary<string, string> mergeDict)
        {
            foreach (var nugetItem in mergeDict)
            {
                if (baseDict.ContainsKey(nugetItem.Key))
                {
                    _logger.Error($"This field exists already. Unable to add to Dictionary again unless overriding value. " +
                        $"Key: {nugetItem.Key}, Value: {nugetItem.Value}");
                    continue;
                }
                baseDict.Add(nugetItem.Key, nugetItem.Value);
            }
        }
    }
}
