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
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { PurchaseDialogComponent } from '../Purchase/purchase-dialog/purchase-dialog.component';


@Component({
  selector: 'app-pos',
  standalone: true,
  imports: [RouterOutlet, CommonModule, ReactiveFormsModule, FormsModule, MatButtonModule,MatDialogModule,MatInputModule],
  templateUrl: './pos.component.html',
  styleUrls: ['./pos.component.css']
})
export class PosComponent implements OnInit {
  inventoryItems: InventoryItem[] = [];
  inventoryItemsToDisplay: InventoryItem[] = [];
  inventoryItemsToCheckout: InventoryItem[] = [];
  searchTxt: string = "";
  totalPrice: number = 0;
  // checkoutText = '';
  checkoutText: string = 'Enter your address for delivery';
  step: number = 1;



  onChange(index: number): void {
    if (index === 1) {
      this.checkoutText = 'Enter your address for delivery';
      this.step = 1;
    } else if (index === 2) {
      this.proceedToAddressForm();
    }
  }

  proceedToAddressForm(): void {
    this.step = 2;
    const dialogRef = this.dialog.open(PurchaseDialogComponent, {
      width: '400px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.step = 3;
      }
    });
  }


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
    private supplierService: SupplierService,
    public dialog: MatDialog
  ) {
    this.inventoryItemsToDisplay = this.inventoryItems;
  }

  ngOnInit(): void {
    this.loadCategories();
    // this.loadSuppliers();
    this.loadInventoryItems();
  }

  loadCategories(): void {
    this.categoryService.getallcategories().subscribe(categories => {
      this.categories = categories;
    });
  }

  // loadSuppliers(): void {
  //   this.supplierService.getallsupplier().subscribe(suppliers => {
  //     this.suppliers = suppliers;
  //   });
  // }

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

  // onChange(index: number): void {
  //   if (index.toString() == "1")
  //     this.checkoutText = "Delivery Address";
  //   else if (index.toString() == "2")
  //     this.checkoutText = ".";
  // }

  // checkout(): void {

  //   alert('Checkout functionality to be implemented');
  // }
}
