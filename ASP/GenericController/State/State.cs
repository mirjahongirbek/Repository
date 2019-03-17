using System.Collections.Generic;
using System.Linq;

namespace GenericController.State
{
    public static class State
    {
        static Dictionary<string, string> Data = new Dictionary<string, string>() {
            { "Int32","Number"},
            { "Int16","Number"},
            { "Int64","Number"},
            { "UInt64","Number"},
            { "UInt32", "Number"},
            { "Double","Number"},
            { "Single","Number"},
            { "List`1","List"},
            { "Dictionary`2","Dictionary"}
        };
        public static string ConvertFront(this string sdt)
        {
            var result = Data.FirstOrDefault(m => m.Key == sdt).Value;
            if (string.IsNullOrEmpty(result))
                return sdt;
            return result;
        }

    }
}

