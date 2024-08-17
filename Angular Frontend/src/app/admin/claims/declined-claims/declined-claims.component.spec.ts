import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeclinedClaimsComponent } from './declined-claims.component';

describe('DeclinedClaimsComponent', () => {
  let component: DeclinedClaimsComponent;
  let fixture: ComponentFixture<DeclinedClaimsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeclinedClaimsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DeclinedClaimsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
