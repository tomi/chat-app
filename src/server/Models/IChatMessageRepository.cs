using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ChatApi.Models
{
    public interface IChatMessageRepository
    {
        Task<IEnumerable<ChatMessage>> GetAllChatMessages();

        Task<ChatMessage> GetChatMessage(string id);

        Task<ChatMessage> AddChatMessage(ChatMessage item);

        Task<DeleteResult> RemoveChatMessage(string id);

        Task<UpdateResult> UpdateChatMessage(string id, string message);

        Task<DeleteResult> RemoveAllChatMessages();
    }
}