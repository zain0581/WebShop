import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InventoryitemListComponent } from './inventoryitem-list.component';

describe('InventoryitemListComponent', () => {
  let component: InventoryitemListComponent;
  let fixture: ComponentFixture<InventoryitemListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InventoryitemListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InventoryitemListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
