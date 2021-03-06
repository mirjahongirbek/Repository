﻿using RepositoryRule.Attributes;
using RepositoryRule.Entity;

namespace Entity
{
    public class EntityData : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Props(name:"userId",jwtKey:"Id")]
        public int UserId { get; set; }
    }
    //public class User : IAuthUser<int>
    //{
    //    public User()
    //    {
    //    }

    //    [Key]
    //    [Auth]
    //    public int Id { get; set; }
    //    public string UserName { get; set; }
    //    [Auth]
    //    public string Email { get; set; }
    //    public string Password { get; set; }

    //    public ICollection<IRoleUser<int>> Roles { get; set; }

    //    public ICollection<IUserDevice<int>> DeviceList { get; set; }
      
    //}
}
