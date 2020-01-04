import { Injectable } from '@angular/core';
import * as signalr from '@aspnet/signalr';
import { AuditLog } from './audit-log';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuditLogLiveService {
  hub: signalr.HubConnection;
  live$ = new Subject<AuditLog>();

  constructor() {
    this.hub = new signalr.HubConnectionBuilder()
      .withUrl("hub/audit-log")
      .build();
    this.hub.start().then(() => {
      this.hub.on("live", (auditLog: AuditLog) => {
        this.live$.next(auditLog);
      })
    });
  }

  getNewAuditLog = () => this.live$;

}
