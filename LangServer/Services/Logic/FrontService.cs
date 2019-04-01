using LangServer.Services.Interfaces;
using Nest;

namespace LangServer.Services.Logic
{
    public class FrontService : IFrontService
    {
        ElasticClient _client;
        public FrontService()
        {
           _client= State.State.Client;
        }
        public void CreateIndex(string indexName)
        {
            if (!_client.IndexExists(indexName).Exists)
            {
                _client.CreateIndex(indexName);
            }
        }

    }
}
