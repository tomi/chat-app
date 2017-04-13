import { Component, OnInit } from '@angular/core';
import { ChatService } from "../chat.service";
import { ChatMessage } from "../chat-message";

@Component({
  selector: 'app-chat',
  template: `
<app-chat-list class="chat-list" [messages]="messages"></app-chat-list>
<app-chat-input class="chat-input"></app-chat-input>
`,
  styles: [
    `
    .chat-list {
      border: 2px solid rgba(34,36,38,.20);
      border-bottom: 0;
      border-radius: .28571429rem .28571429rem 0 0;
      flex: 1;
      flex-direction: column;
      justify-content: flex-end;
      background-color: rgba(255,255,255, 0.9);
      overflow-y: scroll;
      padding: 10px 0;
    }
    .chat-input {
      border: 2px solid rgba(34,36,38,.20);
      border-top: 0;
      border-radius: 0 0 .28571429rem .28571429rem;
      background-color: rgba(255,255,255, 0.9);
      padding: 10px;
      margin: 0;
      display: flex;
      line-height: 1.4285em;
    }
    `
  ]
})
export class ChatComponent implements OnInit {
  private messages: ChatMessage[];

  constructor(private chatService: ChatService) { }

  ngOnInit() {
    this.chatService.getMessages({})
      .then(messages => this.messages = messages);
    this.chatService.newMessageStream
      .subscribe(msg => this.messages.push(msg));
  }

}
