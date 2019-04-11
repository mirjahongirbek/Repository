using LangEntity.Language;
using LangEntity.Project;
using MongoRepository.MongoRepository;

namespace LangServer.Services.Interfaces
{
    public interface IProjectService : IMongoRepository<LangProject>
    {
    }
    public interface ILanguageService : IMongoRepository<Language>
    {

    }

}
