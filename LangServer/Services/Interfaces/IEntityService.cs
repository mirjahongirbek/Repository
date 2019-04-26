
using LangEntity;
using MongoDB.Bson;
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
        Task<IEnumerable<EntityData>> Find(string projectName, BsonDocument docuement);
        Task<EntityData> GetFirst(string projectName, BsonDocument doc);
    }
}
