import { TestBed, inject } from '@angular/core/testing';

import { AuditLogLiveService } from './audit-log-live.service';

describe('AuditLogLiveService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuditLogLiveService]
    });
  });

  it('should be created', inject([AuditLogLiveService], (service: AuditLogLiveService) => {
    expect(service).toBeTruthy();
  }));
});
