
using RepositoryRule.Attributes;
using RepositoryRule.Entity;
using RepositoryRule.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    [Entity]
    public class Data : IEntity<string>
    {
        [Props(DefaultLabel:"Edit", font: FontType.EditDelete, showAdd:false, isShow:true)]
       // [BsonId]
        public string Id { get; set; }
        [Props("Name", FontType.String)]
        public string Name { get; set; }
        public string sdds { get; set; }
       // [BsonIgnoreIfNull]
        [Props("Description Select", FontType.Select)]
        public string Description { get; set; }
        [Props("Hello world",FontType.StringList)]
        public List<string> Lists { get; set; }
    }
    
}
