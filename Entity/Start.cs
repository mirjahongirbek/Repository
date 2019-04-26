using Autofac;
using RepositoryRule.Entity;
using System.Reflection;

namespace Entity
{
    public  class Start
    {
        public static void Build(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.IsAssignableTo<IEntity<int>>()).AsImplementedInterfaces();
        }
    }
}
