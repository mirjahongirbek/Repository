using RepositoryRule.Attributes;
using RepositoryRule.Entity;

namespace Entity
{
    [Entity(GetAll =true)]
    public class Category:IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descriotion { get; set; }
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
