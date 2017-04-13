import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import { ChatMessage } from "./chat-message";
import { Observable } from 'rxjs';
import "rxjs/add/operator/toPromise";

@Injectable()
export class ChatService {
  apiRoot: string = "http://localhost:5000/api/chat";
  wsRoot: string = "ws://localhost:5000/ws";
  webSocket: WebSocket;
  newMessageStream: Observable<ChatMessage>;

  constructor(private http: Http) {
    this.webSocket = new WebSocket(this.wsRoot);
    this.newMessageStream = Observable.fromEvent(this.webSocket, "message")
      .map(event => JSON.parse((event as MessageEvent).data))
      .map(msg => new ChatMessage(msg));
  }

  getMessages(options): Promise<ChatMessage[]> {
    return this.http.get(`${ this.apiRoot }`)
      .toPromise()
      .then(res => res.json().map(msg => new ChatMessage(msg)))
      .then(msgs => msgs.reverse());
  }

  postMessage(messagePayload: object) {
    return this.http.post(`${ this.apiRoot }`, messagePayload)
      .toPromise();
  }
}
