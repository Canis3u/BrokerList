import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageBrokerlistComponent } from './page-brokerlist.component';

describe('PageBrokerlistComponent', () => {
  let component: PageBrokerlistComponent;
  let fixture: ComponentFixture<PageBrokerlistComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PageBrokerlistComponent]
    });
    fixture = TestBed.createComponent(PageBrokerlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
