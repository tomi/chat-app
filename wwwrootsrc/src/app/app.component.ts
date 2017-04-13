import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
<app-chat class="chat"></app-chat>
`,
  styles: [
    `
    .chat {
      display: flex;
      flex-direction: column;
      margin: 40px;
      flex: 1;
    }
    `
  ]
})
export class AppComponent {
  title = 'app works!';
}
