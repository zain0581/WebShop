import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCategorylComponent } from './edit-categoryl.component';

describe('EditCategorylComponent', () => {
  let component: EditCategorylComponent;
  let fixture: ComponentFixture<EditCategorylComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditCategorylComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditCategorylComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
