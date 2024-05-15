import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { InventoryItemService } from '../../services/inventory-item.service';
import { Observable } from 'rxjs';
import { InventoryItem } from '../../models/InventoryItem';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-inventoryitem-list',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule,CommonModule,],
  templateUrl: './inventoryitem-list.component.html',
  styleUrl: './inventoryitem-list.component.css'
})
export class InventoryitemListComponent {


  constructor(private  router: RouterModule, private inventoryservice: InventoryItemService ) {}


  id : string | null = null;

  inventory$?:Observable<InventoryItem[]>;

  ngOnInit(): void {
    this.inventory$ = this.inventoryservice.getallinventoryitems();
  }



  // onDelete(id: number ): void {
  //   if (id !== undefined) {
  //     this.inventoryservice.deleteInventory(id.toString()).subscribe({
  //       error: (error) => {
  //         if (error.status === 200) {
  //           // Treat HTTP status code 200 as a success
  //           alert("Category deleted successfully");
  //           this.inventory$ = this.inventoryservice.getallinventoryitems();
  //         }
  //       }
  //     });
  //   }
  // }
  onDelete(id: number): void {
    if (id !== undefined) {
      this.inventoryservice.deleteInventory(id.toString()).subscribe({
        next: () => {
          alert("Inventory item deleted successfully");
          this.refreshInventory();
        },
        error: (eror) => {
          console.error('Error deleting inventory item:', eror);
          // Handle error appropriately (e.g., display error message)
        }
      });
    }
  }

  private refreshInventory(): void {
    this.inventory$ = this.inventoryservice.getallinventoryitems();
  }
  }

