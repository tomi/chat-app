import { Component, OnInit, Input } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ChatMessage } from "app/chat-message";

@Component({
  selector: 'app-chat-message',
  template: `
<div id="{{ message.id }}" class="chat-message-details">
  <div class="chat-message-user">{{ message.userName }}</div>
  <div class="chat-message-time">{{ message.createdOn | date:'HH:mm' }}</div>
</div>
<div class="chat-message-message">{{ message.message }}</div>
`,
  styles: [
    `
    .chat-message-details {
      display: flex;
    }
    .chat-message-user {
      display: block;
      font-weight: bold;
    }
    .chat-message-time {
      margin-left: 0.5em;
      color: rgba(0,0,0,.4)
    }
    .chat-message-message {
      display: block;
    }
    `
  ]
})
export class ChatMessageComponent implements OnInit {
  @Input() message: ChatMessage;

  constructor() { }

  ngOnInit() {
  }

}
