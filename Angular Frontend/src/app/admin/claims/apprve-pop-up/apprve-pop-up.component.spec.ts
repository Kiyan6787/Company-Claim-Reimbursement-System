import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApprvePopUpComponent } from './apprve-pop-up.component';

describe('ApprvePopUpComponent', () => {
  let component: ApprvePopUpComponent;
  let fixture: ComponentFixture<ApprvePopUpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApprvePopUpComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ApprvePopUpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
