import { Component, OnInit } from '@angular/core';
import { AuditLogService } from '../audit-log.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { AuditLog } from '../audit-log';
import { User } from '../user.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { map } from 'rxjs/operators';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-audit-log-list',
  templateUrl: './audit-log-list.component.html',
  styleUrls: ['./audit-log-list.component.css']
})
export class AuditLogListComponent implements OnInit {

  filterParams = new AuditLog();
  pageSizeOptions = [10, 25, 50, 100];
  pageParams = {
    pageSize: 10,
    pageNumber: 0,
    totalSize: 0,
  }
  list = new Observable<AuditLog[]>();

  constructor(private service: AuditLogService) {
    this.refresh();
  }
  refresh() {
    this.list = this.service.getAll().pipe(
      map(result => {
        this.pageParams.totalSize = result.length;
        return result.slice(
          this.pageParams.pageNumber * this.pageParams.pageSize,
          (this.pageParams.pageNumber + 1) * this.pageParams.pageSize
        );
      })
    );
  }
  ngOnInit() {
  }

  change(e) {
    this.filterParams[e.target.name] = e.target.value;
    this.service.filter(this.filterParams);
  }

  changePage(e: PageEvent) {
    this.pageParams.pageNumber = e.pageIndex;
    this.pageParams.pageSize = e.pageSize;
    this.refresh();
  }

}
