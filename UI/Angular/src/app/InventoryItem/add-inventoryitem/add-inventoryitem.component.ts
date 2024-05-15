import { Supplier } from './../../models/supplier';
import { Category } from './../../models/category';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { InventoryItemService } from '../../services/inventory-item.service';
import { Router, RouterModule } from '@angular/router';
// import { Supplier } from '../../models/supplier';
// import { Category } from '../../models/category';
import { AddcategoryService } from '../../services/addcategory.service';
import { SupplierService } from '../../services/supplier.service';
import { InventoryItem } from '../../models/InventoryItem';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-inventoryitem',
  standalone: true,
  imports: [ReactiveFormsModule,RouterModule,CommonModule],
  templateUrl: './add-inventoryitem.component.html',
  styleUrl: './add-inventoryitem.component.css'
})
export class AddInventoryitemComponent {





  inventoryForm: FormGroup;
  categories: Category[] = [];
  suppliers: Supplier[] = [];

  constructor(
    private fb: FormBuilder,
    private inventoryService: InventoryItemService,
    private categoryService: AddcategoryService,
    private supplierService: SupplierService,
    private router: Router
  ) {
    this.inventoryForm = this.fb.group({

      name: ['', Validators.required],
      description: ['', Validators.required],
      isAvailable: [null],
      imageUrl: [null],
      category: [null, Validators.required],
      supplier: [null, Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadCategories();
    this.loadSuppliers();
  }

  loadCategories(): void {
    this.categoryService.getallcategories().subscribe(categories => {
      this.categories = categories;
    });
  }

  loadSuppliers(): void {
    this.supplierService.getallsupplier().subscribe(suppliers => {
      this.suppliers = suppliers;
    });
  }

  onItemSave(): void {
    if (this.inventoryForm.valid) {
      const formData = this.inventoryForm.value;

      // Check if formData.Category and formData.Supplier are defined
      if (!formData.category || !formData.supplier) {
        console.error('Category or supplier is undefined');
        return;
      }

       const TestData = {
      "id": 0,
      "name": "Test",
      "description": "Test",
      "isAvailable": 0,
      "imageUrl": "string",
      "category": {
        "id": 2,
        "name": "string",
        "description": "string",
        "imageUrl": "string",
        "isActive": 0
      },
      "suppliers": {
        "id": 1,
        "name": "string",
        "email": "string",
        "phone": "string"
      }
    }

      const inventoryItem: InventoryItem = {
        id: 0, // Assuming it will be generated by the server
        name: formData.name,
        description: formData.description,
        isAvailable: formData.isAvailable,
        imageUrl: formData.imageUrl,
        category: {
          id: formData.category.id, // Accessing id property of category
          name: formData.category.name,
          description: formData.category.description,
          imageUrl: formData.category.imageUrl,
          isActive: formData.category.isActive
        },
        suppliers: {
          id: formData.supplier.id, // Accessing id property of supplier
          name: formData.supplier.name,
          email: formData.supplier.email,
          phone: formData.supplier.phone
        }
      };
  console.log("tesdata", TestData);
      console.log("inventoryItem", inventoryItem);

      console.log("inventoryItem", inventoryItem);

      // Call the service method to create inventory item
      this.inventoryService.createInvnentoryItem(inventoryItem).subscribe({
        next: () => {
          alert("success added");
          // Redirect to the inventory page if needed
          // this.router.navigate(['/inventory']);
        },
        error: (error) => {
          alert("got error");
          console.error(error); // Log the error for debugging
        }
      });
    }

  // onItemSave(): void {
  //   if (this.inventoryForm.valid) {
  //     const formData = this.inventoryForm.value;
  //   alert(JSON.stringify(formData));

  //   // const TestData = {
  //   //   "id": 0,
  //   //   "name": "Test",
  //   //   "description": "Test",
  //   //   "isAvailable": 0,
  //   //   "imageUrl": "string",
  //   //   "category": {
  //   //     "id": 2,
  //   //     "name": "string",
  //   //     "description": "string",
  //   //     "imageUrl": "string",
  //   //     "isActive": 0
  //   //   },
  //   //   "suppliers": {
  //   //     "id": 1,
  //   //     "name": "string",
  //   //     "email": "string",
  //   //     "phone": "string"
  //   //   }
  //   // }

  //   if (!formData.category || !formData.supplier) {
  //     console.error('Category or supplier is undefined');
  //     return;
  //   }
  //     const inventoryItem: InventoryItem = {


  //       id: 0, // Assuming it will be generated by the server

  //       name: formData.name,
  //       description: formData.description,
  //       isAvailable: formData.isAvailable,
  //       imageUrl: formData.imageUrl,
  //       category:{
  //         id: formData.Category.id,
  //         name: formData.name,
  //         description: formData.description,
  //         imageUrl: formData.imageUrl,
  //         isActive: formData.isActive
  //         },

  //     supplier:{
  //       id: formData.Supplier.id,
  //       name: formData.name,
  //       email: formData.email,
  //       phone: formData.phone
  //     }


  //     };
  //     // console.log("tesdata", TestData);
  //     console.log("inventoryItem", inventoryItem);





  //     this.inventoryService.createInvnentoryItem(inventoryItem).subscribe({
  //       next: () => {
  //         alert("succde added ")
  //         // this.router.navigate(['/inventory']); // Navigate to the inventory page
  //       },
  //       error: (error) => {
  //        alert("got error")
  //         console.error(error); // Log the error for debugging
  //       }
  //     });
  //   }}
}}