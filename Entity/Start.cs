using Autofac;
using RepositoryRule.Entity;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Entity
{
   public  class Start
    {
        public static void Build(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
     .Where(t => t.IsAssignableTo<IEntity<int>>()).AsImplementedInterfaces();
        }
    }
}
