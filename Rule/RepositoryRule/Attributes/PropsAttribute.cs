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
        public string Url { get; set; }
        public string JWTKey { get; set; }
        public string Regular { get; set; }
        public string TypeId { get; set; }
        public bool Show { get; set; }
        public string FrontUrl { get; set; }
        public bool Required { get; set; }
        public string UserReference { get; set; }
        public short LangId { get; set; }
        public FontType[] Types { get; set; }
        #endregion 
        #region Default Constructors
        public PropsAttribute(
            string name = null,
            string jwtKey= null,
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
           ShowAdd = hideAdd;
            JWTKey = jwtKey;
            Show = hideshow;
        }
       public PropsAttribute(string DefaultLabel, 
              FontType font,
             string jwtKey = null,
            FontType[] types= null,
            string foreignTable=null, 
            string name=null,
            string url=null,
            string reg=null,
            bool getAllForeign= false, 
            bool hideAdd = false,
            bool hideshow = false,
            short langId=0,
            string regular=null,
            string UserReference=null
            )
        {
            JWTKey = jwtKey;
            FontType = font;
            Label = DefaultLabel;
            ForeignTable= foreignTable;
            LangId = langId;
            Regular = regular;
            Name = name;
            Types = types;
            JWTKey = jwtKey;
            
            ShowAdd = hideAdd;
            Show = hideshow;
        }
        
        #endregion
       
    }

}
