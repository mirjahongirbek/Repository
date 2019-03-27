using System.Collections.Generic;

namespace LangEntity
{
    public class Model
    {
        public string EntityModel{ get; set; }
        public string Project { get; set; }
      public  Dictionary<string, string> Fields = new Dictionary<string, string>();

    }
    public class FrontResult
    {
        public Dictionary<string, string> Result { get; set; }
    }
    public class DataModal
    {
        public Dictionary<string, Dictionary<string, string>> Str = new Dictionary<string, Dictionary<string, string>>();
    }
}
