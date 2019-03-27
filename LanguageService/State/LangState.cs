using LanguageService.Converter;
using RestClientDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LanguageService.State
{
    public class LangState
    {
        public static string Uri { get; set; }
        private static RestClient _client;
     static   List<Type> types = new List<Type>
        {
            typeof(string),
            typeof(int),
            typeof(double),
            typeof(float),
            typeof(float),

        };
        public static KeyValuePair<string, string> TT(PropertyInfo info)
        {
            var tip=  types.FirstOrDefault(m => m.Name == info.PropertyType.Name);
            if(tip== null) { return new KeyValuePair<string, string> { }; }
            return new KeyValuePair<string, string>(info.Name,tip.Name);

        }

        internal static RestClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new RestClientDotNet.RestClient(new NewtonsoftSerializationAdapter(), new Uri(Uri));
                }
                return _client;
            }
        }
    }
}
