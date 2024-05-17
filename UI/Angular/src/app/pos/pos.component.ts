import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { InventoryItem } from '../models/InventoryItem';
import { Category } from '../models/category';
import { Supplier } from '../models/supplier';
import { InventoryItemService } from '../services/inventory-item.service';
import { AddcategoryService } from '../services/addcategory.service';
import { SupplierService } from '../services/supplier.service';

@Component({
  selector: 'app-pos',
  standalone: true,
  imports: [RouterOutlet, CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './pos.component.html',
  styleUrls: ['./pos.component.css']
})
export class PosComponent implements OnInit {
  inventoryItems: InventoryItem[] = [];
  inventoryItemsToDisplay: InventoryItem[] = [];
  inventoryItemsToCheckout: InventoryItem[] = [];
  searchTxt: string = "";
  totalPrice: number = 0;
  checkoutText = '';
  

  // inventoryForm: FormGroup = this.formBuilder.group({
  //   name: ['', Validators.required],
  //   description: ['', Validators.required],
  //   qty: [null],
  //   imageUrl: [null],
  //   unitPrice: [0, Validators.required],
  //   category: [null, Validators.required],
  //   supplier: [null, Validators.required]
  // });

  categories: Category[] = [];
  suppliers: Supplier[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private inventoryService: InventoryItemService,
    private categoryService: AddcategoryService,
    private supplierService: SupplierService
  ) {
    this.inventoryItemsToDisplay = this.inventoryItems;
  }

  ngOnInit(): void {
    this.loadCategories();
    this.loadSuppliers();
    this.loadInventoryItems();
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

  loadInventoryItems(): void {
    this.inventoryService.getallinventoryitems().subscribe(items => {
      this.inventoryItems = items;
      this.inventoryItemsToDisplay = items;
    });
  }


  headclick(id: any): void {
    this.inventoryItemsToDisplay = this.inventoryItems.filter(x => x.category?.id == id);
  }

  addToCart(item: InventoryItem): void {
    const existingItem = this.inventoryItemsToCheckout.find(cartItem => cartItem.id === item.id);
    if (existingItem) {
      existingItem.qty = (existingItem.qty ?? 1) + 1;
    } else {
      item.qty = 1;
      this.inventoryItemsToCheckout.push(item);
    }
    this.totalPrice += item.unitPrice ?? 0;
  }

  removeFromCart(item: InventoryItem): void {
    const index = this.inventoryItemsToCheckout.indexOf(item);
    if (index > -1) {
      this.totalPrice -= (item.unitPrice ?? 0) * (item.qty ?? 1);
      this.inventoryItemsToCheckout.splice(index, 1);
    }
  }

  incrementQuantity(item: InventoryItem): void {
    item.qty = (item.qty ?? 1) + 1;
    this.totalPrice += item.unitPrice ?? 0;
  }

  decrementQuantity(item: InventoryItem): void {
    if (item.qty && item.qty > 1) {
      item.qty--;
      this.totalPrice -= item.unitPrice ?? 0;
    }
  }

  searchInventoryItems(): void {
    this.inventoryItemsToDisplay = this.inventoryItems.filter(x => x.name.toLowerCase().includes(this.searchTxt.toLowerCase()));
  }

  onChange(index: number): void {
    if (index.toString() == "1")
      this.checkoutText = "Delivery Address";
    else if (index.toString() == "2")
      this.checkoutText = "Dine In Table No.";
  }

  checkout(): void {
    // Handle checkout logic here
    alert('Checkout functionality to be implemented');
  }
}
