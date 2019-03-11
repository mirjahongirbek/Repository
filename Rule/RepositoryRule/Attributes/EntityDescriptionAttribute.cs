using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryRule.Attributes
{
    public class EntityDescriptionAttribute : Attribute
    {
        #region Props
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public FontType FontType { get; set; }
        public bool ShowAdd { get; set; }
        public bool Show { get; set; }
        #endregion 
        #region Default Constructors
        public EntityDescriptionAttribute(string name, bool hideAdd= true, bool hideshow=true)
        {
            Name = name;
            ShowAdd = hideAdd;
            Show = hideshow;
        }
        public EntityDescriptionAttribute(string DefaultLabel,FontType font, bool hideAdd = true, bool hideshow = true)
        {
            FontType = font;
            DefaultLabel = Label;
            this.ShowAdd = hideAdd;
            this.Show = hideshow;
        }
        public EntityDescriptionAttribute(string DefaultLabel, FontType font, string OtherTable, bool hideAdd = true, bool hideshow = true)
        {
            FontType = font;
            Label = DefaultLabel;
            Name = OtherTable;
            this.ShowAdd = hideAdd;
            this.Show = hideshow;
        }
        public EntityDescriptionAttribute(string name, string description, bool hideAdd = true, bool hideshow = true)
        {
            Name = name;
            Description = description;
            this.ShowAdd = hideAdd;
            this.Show = hideshow;
        }
        #endregion
    }

}
