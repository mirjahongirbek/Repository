using RepositoryRule.Entity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LanguageService.State
{
    public static class LangState
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
        public static Task GetEntity<TKey>(this IEntity<TKey> entity,
            string lang,
            string id = null
            )
        {

            return null;
        }
        internal static RestClient Client
        {
            get
            {
                if (_client == null)
                {
                    if(Uri== null)
                    {
                        Uri = "http://127.0.0.1:9001/api";
                    }
                    _client = new RestClient(Uri);
                }
                return _client;
            }
        }
    }
}
