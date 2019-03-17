using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryRule.Attributes
{
    [AttributeUsage(AttributeTargets.Field| AttributeTargets.Property| AttributeTargets.Module)]
    public class PropsAttribute : Attribute
    {
        #region Props
        public string Name { get; set; }
        public string ForeignTable { get; set; }
        public string Label { get; set; }
        public FontType FontType { get; set; }
        public bool ShowAdd { get; set; }
        public bool Show { get; set; }
        public bool GetAllForeign { get; set; }
        public string FrontUrl { get; set; }
        //public string  { get; set; }
        public string Regular { get; set; }
        public bool Required { get; set; }
        public string UserReference { get; set; }
        public short LangId { get; set; }
        public FontType[] Types { get; set; }
        #endregion 
        #region Default Constructors
        public PropsAttribute(
            string name = null,
            string foreignTable = null,
            bool getAllForeign = false,
            bool hideAdd =false,
            bool hideshow = false,
            short langId = 0,
            string regular = null,
            string UserReference = null)
        {
            ForeignTable = foreignTable;
            LangId = langId;
            Regular = regular;
            Name = name;
            GetAllForeign = getAllForeign;
            ShowAdd = hideAdd;
            Show = hideshow;
        }
       public PropsAttribute(string DefaultLabel, 
            FontType font,
            FontType[] types= null,
            string foreignTable=null, 
            string name=null,
            bool getAllForeign= false, 
            bool hideAdd = false,
            bool hideshow = false,
            short langId=0,
            string regular=null,
            string UserReference=null
            )
        {
            FontType = font;
            Label = DefaultLabel;
            ForeignTable= foreignTable;
            LangId = langId;
            Regular = regular;
            Name = name;
            Types = types;
            GetAllForeign = getAllForeign;
            ShowAdd = hideAdd;
            Show = hideshow;
        }
        
        #endregion
       
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class JohaAttribute : System.Attribute
    {
        public JohaAttribute(string name, bool getAll=true)
        {
            Name = name;
            GetAll = getAll;

        }
        public string Name { get; set; }
        public bool GetAll { get; set; }

    }

}
