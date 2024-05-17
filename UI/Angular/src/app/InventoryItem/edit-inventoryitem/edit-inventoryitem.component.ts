import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Supplier } from '../../models/supplier';
import { Category } from '../../models/category';
import { InventoryItemService } from '../../services/inventory-item.service';
import { InventoryItem } from '../../models/InventoryItem';
import { AddcategoryService } from '../../services/addcategory.service';
import { SupplierService } from '../../services/supplier.service';
// import { Inventory } from '../../models/updatteitem';

@Component({
  selector: 'app-edit-inventoryitem',
  standalone: true,
  imports: [ReactiveFormsModule ,RouterModule,CommonModule],
  templateUrl: './edit-inventoryitem.component.html',
  styleUrl: './edit-inventoryitem.component.css'
})
export class EditInventoryitemComponent {
  // inventoryForm: FormGroup;
  // categories: Category[] = [];
  // suppliers: Supplier[] = [];
  // inventoryItemId: string | null = null;

  // constructor(
  //   private fb: FormBuilder,
  //   private route: ActivatedRoute,
  //   private router: Router,
  //   private inventoryService: InventoryItemService,
  //   private categoryService: AddcategoryService,
  //   private supplierService: SupplierService
  // ) {
  //   this.inventoryForm = this.fb.group({
  //     name: ['', Validators.required],
  //     description: ['', Validators.required],
  //     isAvailable: [null],
  //     imageUrl: [null],
  //     category: [null, Validators.required],
  //     supplier: [null, Validators.required]
  //   });
  // }

  // ngOnInit(): void {
  //   this.route.params.subscribe(params => {
  //     this.inventoryItemId = params['id'];

  //     if (this.inventoryItemId) {
  //       this.loadInventoryItem();
  //     }
  //   });

  //   this.loadCategories();
  //   this.loadSuppliers();
  // }

  // loadInventoryItem(): void {
  //   this.inventoryService.getinventoryitemById(this.inventoryItemId).subscribe(item => {
  //     if (item) {
  //       this.inventoryForm.patchValue({
  //         name: item.name,
  //         description: item.description,
  //         isAvailable: item.isAvailable,
  //         imageUrl: item.imageUrl,
  //         category: item.category,
  //         supplier: item.suppliers,
  //       });
  //     }
  //   });
  // }

  // loadCategories(): void {
  //   this.categoryService.getallcategories().subscribe(categories => {
  //     this.categories = categories;
  //   });
  // }

  // loadSuppliers(): void {
  //   this.supplierService.getallsupplier().subscribe(suppliers => {
  //     this.suppliers = suppliers;
  //   });
  // }

  // onItemSave(): void {
  //   if (this.inventoryForm.valid) {
  //     const formData = this.inventoryForm.value;
  //     const inventoryItem: InventoryItem = {
  //       id: this.inventoryItemId,
  //       name: formData.name,
  //       description: formData.description,
  //       isAvailable: formData.isAvailable,
  //       imageUrl: formData.imageUrl,
  //       category: formData.category,
  //       supplier: formData.supplier
  //     };

  //     this.inventoryService.updateinventoryitem(this.inventoryItemId, inventoryItem).subscribe(() => {
  //       alert("Inventory item updated successfully");
  //       this.router.navigate(['/inventory-items']);
  //     }, error => {
  //       alert("Failed to update inventory item");
  //       console.error(error);
  //     });
  //   }
  // }

}
