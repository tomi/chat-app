using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChatApi.Services
{
    public class ChatConnections : IChatConnections
    {
        private readonly List<WebSocket> _sockets = null;

        public ChatConnections()
        {
            _sockets = new List<WebSocket>();
        }

        public async Task HandleConnection(WebSocket webSocket)
        {
            if (webSocket == null)
            {
                throw new ArgumentNullException("socket");
            }

            lock (_sockets)
            {
                _sockets.Add(webSocket);
            }

            await Handle(webSocket);

            lock (_sockets)
            {
                _sockets.Remove(webSocket);
            }
        }

        private async Task Handle(WebSocket webSocket)
        {
            while (webSocket.State == WebSocketState.Open)
            {
                var token = CancellationToken.None;
                var buffer = new ArraySegment<Byte>(new Byte[4096]);
                await webSocket.ReceiveAsync(buffer, token);
            }
        }

        public async Task SendChatMessageAsync(object message)
        {
            var jsonString = JsonConvert.SerializeObject(message);
            var data       = Encoding.UTF8.GetBytes(jsonString);
            var buffer     = new ArraySegment<byte>(data);

            IEnumerable<Task> notifyTasks;

            lock (_sockets)
            {
                notifyTasks = _sockets.Select<WebSocket, Task>(socket =>
                {
                    if (socket != null && socket.State == WebSocketState.Open)
                    {
                        return socket.SendAsync(
                            buffer,
                            WebSocketMessageType.Text,
                            endOfMessage: true,
                            cancellationToken: CancellationToken.None
                        );
                    }
                    else
                    {
                        return Task.CompletedTask;
                    }
                });
            }

            await Task.WhenAll(notifyTasks);
        }
    }
}