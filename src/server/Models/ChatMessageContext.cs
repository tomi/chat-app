using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatApi.Models
{
    public class ChatMessageContext
    {
        private readonly IMongoDatabase _database = null;

        public ChatMessageContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
            }
        }

        public IMongoCollection<ChatMessage> ChatMessages
        {
            get
            {
                return _database.GetCollection<ChatMessage>("ChatMessage");
            }
        }
    }
}