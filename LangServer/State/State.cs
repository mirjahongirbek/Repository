using LangEntity.Project;
using Nest;
using System.Collections.Generic;

namespace LangServer.State
{
    public class State
    {
        public static List<string> Urls { get; set; }
        public static ElasticClient Client { get; set;}
        public static string ProjectName { get; set; }
        public static LangProject LangProject { get; set; }
    }
}
