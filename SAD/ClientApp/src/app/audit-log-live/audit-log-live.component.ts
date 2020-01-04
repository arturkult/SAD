import { Component, OnInit } from '@angular/core';
import { AuditLogLiveService } from '../audit-log-live.service';
import { AuditLog } from '../audit-log';

@Component({
  selector: 'app-audit-log-live',
  templateUrl: './audit-log-live.component.html',
  styleUrls: ['./audit-log-live.component.css']
})
export class AuditLogLiveComponent implements OnInit {

  list: AuditLog[] = [];

  constructor(private hub: AuditLogLiveService) { }

  ngOnInit() {
    this.hub.getNewAuditLog().subscribe((auditLog) => {
      this.list.unshift(auditLog);
      this.list = this.list.slice(0, 10);
    });
  }

}
