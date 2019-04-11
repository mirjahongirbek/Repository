

using LangEntity;
using MongoDB.Driver;
using MongoRepository.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using LangServer.Services.Interfaces;
using MongoDB.Bson;

namespace LangServer.Services.Logic
{
    public class EntityService:MongoRepository.MongoRepository<EntityData>, IEntityService
    {
        IMongoDatabase _data;
        public EntityService(IMongoContext context):base(context)
        {
            _data = context.Database;
        }
        public async Task<IEnumerable<EntityData>> GetAll(string name)
        {
           var db= _data.GetCollection<EntityData>(name);
           return (await db.FindAsync(mbox => true)).ToEnumerable();
        }
        public async Task<IEnumerable<EntityData>> EntityLang(string name, int id, Guid guid)
        {
          var db=  _data.GetCollection<EntityData>(name);
           return (await db.FindAsync(mbox => mbox.Guid == guid && mbox.LangId == id)).ToEnumerable();
        }

        public  Task AddEntity(EntityData model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new System.NullReferenceException();
            }
            if (string.IsNullOrEmpty(model.Id))
            {
                model.Id = ObjectId.GenerateNewId().ToString();
            }
           var db= _data.GetCollection<EntityData>(model.Name);
            db.InsertOne(model);
            return Task.CompletedTask;
        }
    }
}
