using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RepositoryRule.Attributes;
using RepositoryRule.Entity;

namespace Entity
{
    public class EntityData : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Props(jwtKey:"Id")]
        public int UserId { get; set; }
    }
    public class User : IAuthUser<int>
    {
        [Key]
        [Auth]
        public int Id { get; set; }
        public string UserName { get; set; }
        [Auth]
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<IRoleUser<int>> Roles { get; set; }

        public ICollection<IUserDevice<int>> DeviceList { get; set; }
      
    }
}
