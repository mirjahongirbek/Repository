using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryRule.Attributes
{
    public class EntityDescriptionAttribute : Attribute
    {
        #region Props
        public string Name { get; set; }
        public string ForeignTable { get; set; }
        public string Label { get; set; }
        public FontType FontType { get; set; }
        public bool ShowAdd { get; set; }
        public string Url { get; set; }
        public string Regular { get; set; }
        public string TypeId { get; set; }
        public bool Show { get; set; }
        public bool GetAllForeign { get; set; }
        public short LangId { get; set; }
        public FontType[] Types { get; set; }
        #endregion 
        #region Default Constructors
        public EntityDescriptionAttribute(string name, bool hideAdd= true, bool hideshow=true)
        {
            Name = name;
            ShowAdd = hideAdd;
            Show = hideshow;
        }
       public EntityDescriptionAttribute(string DefaultLabel, 
            FontType font,
            FontType[] types= null,
            string foreignTable=null, 
            string name=null,
            string url=null,
            string reg=null,
            bool getAllForeign= false, 
            bool hideAdd = true,
            bool hideshow = true,
            short langId=0
            )
        {
            FontType = font;
            Label = DefaultLabel;
            ForeignTable= foreignTable;
            LangId = langId;
            Name = name;
            Types = types;
            GetAllForeign = getAllForeign;
            ShowAdd = hideAdd;
            Show = hideshow;
        }
        
        #endregion
    }

}
