using System.Linq;
using RepositoryRule.Attributes;
using System;
using System.Reflection;

namespace RepositoryMapper
{
    //Mapping Class bu now not ready 
  public  class RepositoryMapper
    {
        public T GetT<T>(object model)
        {
            try
            {
               var result= CreateObject<T>();
               var tip= model.GetType();
                var tipresult = result.GetType();
                foreach(var prop in tipresult.GetProperties())
                {
                   var existprops= tip.GetProperties().FirstOrDefault(m => m.Name.ToLower() == prop.Name.ToLower());
                    if (existprops == null) continue;
                    if (prop.PropertyType.FullName.ToLower() != existprops.PropertyType.FullName.ToLower())
                    {
                        continue;
                    }
                    prop.SetValue(result, existprops.GetValue(model));
                }
                return result;

            }
            catch(Exception ext)
            {
                throw ext;
            }

        }
        public void Join<T>(T result, T model)
        {
            var resulttip = result.GetType();
            var modeltip = model.GetType();
            foreach(var i in resulttip.GetProperties())
            {
               var exist= modeltip.GetProperties().FirstOrDefault(m=>m.Name.ToLower()==i.Name.ToLower());
                if (exist == null) continue;
                var attr=       i.GetCustomAttribute<PropAttribute>();
                if (attr != null && attr.NotUpdate) continue;
                if(i.PropertyType.FullName.ToLower()!= exist.PropertyType.FullName.ToLower())
                {
                    continue;
                }
                //TODO
                i.SetValue(result, exist.GetValue(model));
            
            }

        }
        private T CreateObject<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
