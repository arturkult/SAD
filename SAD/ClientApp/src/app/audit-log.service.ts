import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { AuditLog } from './audit-log';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuditLogService {
  private url = "api/audit-logs";
  private auditLogs: AuditLog[] = [];
  private auditLogs$ = new BehaviorSubject<AuditLog[]>(this.auditLogs);
  constructor(private http: HttpClient) {
    this.refresh();
  }

  refresh() {
    this.http.post<AuditLog[]>(this.url, new AuditLog()).subscribe(
      (result) => {
        this.auditLogs = result;
        this.auditLogs$.next(this.auditLogs);
      })
  }

  filter(params: AuditLog) {
    console.log(params);
    this.http.post<AuditLog[]>(this.url, params).subscribe(
      (result) => {
        this.auditLogs = result;
        this.auditLogs$.next(this.auditLogs);
      })
  }

  getAll = () => this.auditLogs$;
}
