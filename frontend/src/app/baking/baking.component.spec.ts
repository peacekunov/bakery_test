import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BakingComponent } from './baking.component';

describe('BakingComponent', () => {
  let component: BakingComponent;
  let fixture: ComponentFixture<BakingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BakingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BakingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
