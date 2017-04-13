using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ChatApi.Models
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly ChatMessageContext _context = null;

        public ChatMessageRepository(IOptions<Settings> settings)
        {
            _context = new ChatMessageContext(settings);
        }

        public async Task<IEnumerable<ChatMessage>> GetAllChatMessages()
        {
            return await _context.ChatMessages
                .Find(_ => true)
                .Limit(20)
                .SortByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<ChatMessage> GetChatMessage(string id)
        {
            var filter = Builders<ChatMessage>.Filter.Eq("Id", id);
            return await _context.ChatMessages
                  .Find(filter)
                  .FirstOrDefaultAsync();
        }

        public async Task<ChatMessage> AddChatMessage(ChatMessage item)
        {
            item.CreatedOn = DateTime.Now;
            item.UpdatedOn = DateTime.Now;

            await _context.ChatMessages.InsertOneAsync(item);

            return item;
        }

        public async Task<DeleteResult> RemoveChatMessage(string id)
        {
            return await _context.ChatMessages.DeleteOneAsync(
                Builders<ChatMessage>.Filter.Eq("Id", id));
        }

        public async Task<UpdateResult> UpdateChatMessage(string id, string message)
        {
            var filter = Builders<ChatMessage>.Filter.Eq(s => s.Id, id);
            var updateBuilder = Builders<ChatMessage>.Update;
            var update = updateBuilder
                .Set(s => s.Message, message)
                .Set(s => s.UpdatedOn, DateTime.Now);

            return await _context.ChatMessages.UpdateOneAsync(filter, update);
        }

        public async Task<DeleteResult> RemoveAllChatMessages()
        {
            return await _context.ChatMessages.DeleteManyAsync(new BsonDocument());
        }
    }
}