
using LangEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LangServer.Services.Interfaces
{
    public  interface IEntityService
    {
        Task<IEnumerable<EntityData>> GetAll(string name);
        Task<IEnumerable<EntityData>> EntityLang(string name, int id, Guid guid);
        Task AddEntity(EntityData model);

    }
}
