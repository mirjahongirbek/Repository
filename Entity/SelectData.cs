using Microsoft.EntityFrameworkCore;
using RepositoryRule.Attributes;
using RepositoryRule.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    #region Entity
    public class Company : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Product : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        [Props(name:"companyId",jwtKey: "CompanyId")]
        public int CompanyId { get; set; }
    }
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
    }

    public class RoleUser : IRoleUser<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public string Description { get; set; }
        public RoleEnum Roles { get; set; }

    }
    public class UserDevice : IUserDevice<int>
    {
        [Key]
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTime { get; set; }

    }
    public class DataBase : DbContext, EntityRepository.Context.IDataContext
    {
        public DataBase() 
        {
            Database.EnsureCreated();
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<UserDevice> UserDevices { get; set; }

        public DbContext DataContext { get { return this; } }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Mobile.db");
            //optionsBuilder.UseSqlServer("  optionsBuilder.UseSqlite("Filename = Mobile.db");");
        }

    }
    #endregion

}
