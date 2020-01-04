export class AuditLog {
  id: string;
  timestamp: Date;
  userFullName: string;
  roomNumber: string;
  result: boolean;

  public toRequest() {
    return {
      timestamp: this.timestamp ? this.timestamp.toISOString() : null,
      userFullName: this.userFullName,
      roomNumber: this.userFullName,
      result: this.result
    }
  }
}
