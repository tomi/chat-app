export class ChatMessage {
  public id: string;
  public message: string;
  public userName: string;
  public createdOn: Date;
  public updatedOn: Date;

  constructor(options) {
    this.id        = options.Id;
    this.message   = options.Message;
    this.userName  = options.UserName;
    this.createdOn = options.CreatedOn;
    this.updatedOn = options.UpdatedOn;
  }
}
