using RepositoryRule.Attributes;
using RepositoryRule.Entity;
using RepositoryRule.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Product : IEntity<int>
    {
        [Key]
        [Props("Edit", font:FontType.EditDelete)]
        public int Id { get; set; }
        [Props(name: "companyId", jwtKey: "CompanyId", ShowAdd = false)]
        public int CompanyId { get; set; }
        [Props(name:"category", FontType =FontType.Select, ForeignTable = "Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Props(name:"isActive", FontType =FontType.CheckBox)]
        public bool IsActive { get; set; }
        [Props(name: "CreateDate", FontType =FontType.DateTime)]
        public DateTime CreateDate { get; set; }
        public string Name { get; set;  }
    }
 

}
/*
 { "name": "categoryId", 
 "options": 
 { "serviceName": "Product", 
 "data": { "showAdd": true, 
 "fontType": 28,
 "foreignTable": "Category",
 "name": "category",
 "label": "category",
 "url": null, 
 "jwtKey": null,
 "regular": null,
 "typeId": null,
 "show": true, 
 "frontUrl": null, 
 "required": false, 
 "userReference": null,
 "langId": 0, 
 "types": null,
 "maxLength": 0,
 "minLength": 0 } } }
     */
