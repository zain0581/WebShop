import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FoodserviceService } from '../services/foodservice.service';
import { FoodItem } from '../models/fooditem';
// import { FoodCategory } from '../models/foodcategory';

import { CommonModule } from '@angular/common';
import { FormsModule,  } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Category } from '../models/category';
import { AddcategoryService } from '../services/addcategory.service';
import { SupplierService } from '../services/supplier.service';
import { Supplier } from '../models/supplier';


@Component({
  selector: 'app-addfooditem',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule, RouterModule],
  templateUrl: './addfooditem.component.html',
  styleUrls: ['./addfooditem.component.css']
})
export class AddfooditemComponent implements OnInit{

  _id?: number
 foodCategories: Category[] = [];
  _suppliers: Supplier[]= [];



  itemform: FormGroup = this.fb.group({
    name: ['', Validators.required],
    imgurl: ['', Validators.required],
    price: [0, Validators.required],
    category: [0, Validators.required],
    qty: [0, Validators.required]
  });

  constructor(private fb: FormBuilder, private fooditemService: FoodserviceService, private categoryService: AddcategoryService,private suppllier :SupplierService) {}

  ngOnInit(): void {
    this.categoryService.getallcategories().subscribe(categories => {

       this.foodCategories = categories;
       console.log(this.foodCategories); // Check the console for the array
    });
    this.suppllier.getallsupplier().subscribe(suppliers => {
      this._suppliers= suppliers;
      console.log(suppliers);
    });

  }

  onItemSave() {
    const fooitem: FoodItem = {
      name: this.itemform.value.name,
      imgurl: this.itemform.value.imgurl,
      price: this.itemform.value.price,
      category: this.itemform.value.category,
      qty: this.itemform.value.qty
    };

    this.fooditemService.createFoodItem(fooitem).subscribe();
  }

}
