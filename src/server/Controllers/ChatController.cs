using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ChatApi.Models;
using ChatApi.Services;

namespace ChatApi.Controllers
{
    public class UpdateParam
    {
        public string Message { get; set; } = String.Empty;
    }

    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IChatConnections _chatConnections = null;

        public ChatController(
            IChatMessageRepository chatMessageRepository,
            IChatConnections chatConnections)
        {
            _chatMessageRepository = chatMessageRepository;
            _chatConnections = chatConnections;
        }

        // GET: ChatMessages/ChatMessages
        [HttpGet]
        public Task<string> Get()
        {
            return GetChatMessageInternal();
        }

        private async Task<string> GetChatMessageInternal()
        {
            var ChatMessages = await _chatMessageRepository.GetAllChatMessages();
            return JsonConvert.SerializeObject(ChatMessages);
        }

        // GET api/ChatMessages/5
        [HttpGet("{id}", Name = "GetMessage")]
        public Task<string> Get(string id)
        {
            return GetChatMessageByIdInternal(id);
        }

        private async Task<string> GetChatMessageByIdInternal(string id)
        {
            var ChatMessage = await _chatMessageRepository.GetChatMessage(id) ?? new ChatMessage();
            return JsonConvert.SerializeObject(ChatMessage);
        }

        // POST api/ChatMessages
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ChatMessage value)
        {
            if (value == null) {
                return BadRequest();
            }
            var message = await _chatMessageRepository.AddChatMessage(value);

            _chatConnections.SendChatMessageAsync(message);

            return Created("GetMessage", new { Id = message.Id });
        }

        // PUT api/ChatMessages/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]UpdateParam param)
        {
            _chatMessageRepository.UpdateChatMessage(id, param.Message);
        }

        // DELETE api/ChatMessages/5
        public void Delete(string id)
        {
            _chatMessageRepository.RemoveChatMessage(id);
        }
    }
}
