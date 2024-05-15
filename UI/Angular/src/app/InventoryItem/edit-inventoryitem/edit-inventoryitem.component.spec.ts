import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditInventoryitemComponent } from './edit-inventoryitem.component';

describe('EditInventoryitemComponent', () => {
  let component: EditInventoryitemComponent;
  let fixture: ComponentFixture<EditInventoryitemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditInventoryitemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditInventoryitemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
