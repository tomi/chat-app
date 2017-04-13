using System.Net.WebSockets;
using System.Threading.Tasks;

namespace ChatApi.Services
{
    public interface IChatConnections
    {
        Task HandleConnection(WebSocket webSocket);

        Task SendChatMessageAsync(object message);
    }

}