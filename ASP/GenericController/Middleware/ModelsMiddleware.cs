using GenericController.Attributes;
using GenericController.Store;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GenericController.Middleware
{
    public class ModelsMiddleware
    {
        private readonly RequestDelegate _next;

        public ModelsMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (InternalStore.Modals != null)
            {
                await _next.Invoke(context);
            }
            InternalStore.Modals = new List<ModalList>();
           var assembly = Assembly.GetEntryAssembly();
           var types = assembly.DefinedTypes;
           var models = types.Where(m => m.Namespace.Contains("Models")).ToList();
           RegisterModels(models);
            await _next.Invoke(context);
        }
        public void RegisterModels(List<TypeInfo> models)
        {
            foreach (var i in models)
            {
                var noda = i.GetCustomAttribute<ViewModelAttribute>();
                if (noda == null) continue;
                InternalStore.Modals.Add(new ModalList { Name= noda.Name.ToLower(), Actions=noda.Actions, ViewModal= i });
            }
        }
    }
}
