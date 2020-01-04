import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuditLogLiveComponent } from './audit-log-live.component';

describe('AuditLogLiveComponent', () => {
  let component: AuditLogLiveComponent;
  let fixture: ComponentFixture<AuditLogLiveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuditLogLiveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuditLogLiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
