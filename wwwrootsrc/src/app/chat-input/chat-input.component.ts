import { Component, OnInit } from '@angular/core';
import { FormControl } from "@angular/forms";
import 'rxjs/add/operator/do';
import { ChatService } from "app/chat.service";

@Component({
  selector: 'app-chat-input',
  template: `
<input
  type="text"
  placeholder="Send a message"
  class="chat-input-message"
  #message
  [formControl]="messageField">
<button
  type="button"
  class="chat-input-button"
  (click)="sendMessage(message.value)">Send</button>
`,
  styles: [
    `
    .chat-input-message {
      flex: 1;
      border: 1px solid rgba(34,36,38,.15);
      border-radius: .28571429rem 0 0 .28571429rem;
      line-height: 1.21428571em;
      padding: 10px 5px 10px 5px;
      font-family: sans-serif;
      font-size: 14px;
    }
    .chat-input-button {
      margin: 0;
      border: 1px solid rgba(34,36,38,.15);
      border-radius: 0 .28571429rem .28571429rem 0;
      font-family: sans-serif;
      font-size: 14px;
      background-color: #3D9970;
    }
    `
  ]
})
export class ChatInputComponent implements OnInit {
  private messageField: FormControl;

  constructor(private chatService: ChatService) { }

  ngOnInit() {
    this.messageField = new FormControl();
  }

  sendMessage() {
    if (!this.messageField.valid) {
      return;
    }

    this.chatService.postMessage({
      message: this.messageField.value,
      userName: "Pertsa"
    });
    this.messageField.reset();
  }
}
