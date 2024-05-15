import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddInventoryitemComponent } from './add-inventoryitem.component';

describe('AddInventoryitemComponent', () => {
  let component: AddInventoryitemComponent;
  let fixture: ComponentFixture<AddInventoryitemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddInventoryitemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddInventoryitemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
