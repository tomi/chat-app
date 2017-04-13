import { Component, AfterViewChecked, Input } from '@angular/core';
import { ChatMessage } from "../chat-message";

@Component({
  selector: 'app-chat-list',
  template: `
<app-chat-message class="chat-message" *ngFor="let m of messages" [message]="m"></app-chat-message>
`,
  styles: [
    `
    .chat-message {
      display: flex;
      flex-direction: column;
      flex: none;
      padding: 5px 15px;
      font-family: sans-serif;
      font-size: 14px;
      line-height: 20px;
    }
    .chat-message:hover {
      background-color: #edf2f2;
    }
    `
  ]
})
export class ChatListComponent implements AfterViewChecked {
  @Input() messages: ChatMessage[];

  constructor() {}

  ngAfterViewChecked() {
    const messages = document.querySelectorAll(".chat-message");
    if (!messages) {
      return;
    }

    const latestMessage = messages[messages.length - 1];
    if (!latestMessage) {
      return;
    }

    latestMessage.scrollIntoView();
}
