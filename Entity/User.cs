using RepositoryRule.Attributes;
using RepositoryRule.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class User : IAuthUser<int, RoleUser, UserDevice>
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Auth]
        public int CompanyId { get; set; }
        public IEnumerable<UserDevice> DeviceList { get; set; }
        public ICollection<RoleUser> Roles { get; set; }
        public string Salt { get; set; }
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
