import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
// import { HeaderItem } from '../models/headeritem';
// import { FoodItem } from '../models/fooditem';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
// import { FoodserviceService } from '../services/foodservice.service';
import { CommonModule } from '@angular/common';
// import { FoodCategory } from '../models/foodcategory';

// import { FoodItemVM } from '../viewmodels/fooditemvm';
// import { FoodorderService } from '../services/foodorder.service';
// import { FoodOrder } from '../models/foodorder';
import { InventoryItem } from '../models/InventoryItem';
import { Category } from '../models/category';
import { Supplier } from '../models/supplier';
import { InventoryItemService } from '../services/inventory-item.service';
import { AddcategoryService } from '../services/addcategory.service';
import { SupplierService } from '../services/supplier.service';

@Component({
  selector: 'app-pos',
  standalone: true,
  imports: [RouterOutlet,CommonModule,ReactiveFormsModule,FormsModule],
  templateUrl: './pos.component.html',
  styleUrl: './pos.component.css'
})
export class PosComponent implements OnInit{

  // // foodCategories: FoodCategory[] = [];




  // // headdtlist:HeaderItem[]=[]
  // fooditems:FoodItem[]=[];
  // foodtodisplay:FoodItem[]=[];
  // foodtocheckout:FoodItem[]=[];

  // searchtxt:string="";
  // totalprice:number=0;
  // selectedTeam = '';
  // checkouttext='';

  // orderform: FormGroup = this.formbuilder.group({

  //   _id: [0, Validators.required],
  //   price: ['', Validators.required],
  //   qty: ['', Validators.required],
  //   totalprice: ['', Validators.required],
  //   comment: ['', Validators.required]

  // });




  // constructor(private formbuilder: FormBuilder, private _router: Router, private foodorder : FoodorderService, private fooditemservice :FoodserviceService)
  // {


  //     this.foodtodisplay=this.fooditems;
  // }
  // ngOnInit(): void {
  //   throw new Error('Method not implemented.');
  // }



  // // ngOnInit(): void {
  // //   this.foodcategoryService.getFoodCategories().subscribe(categories => {
  // //     this.foodCategories = categories;
  // //     console.log(this.foodCategories);
  // //   });

  // //   this.fooditemservice.getfooditems().subscribe(items => {
  // //     this.fooditems = items;
  // //     console.log(this.fooditems);
  // //   });

  // // }

  // saveOrderForm(){
  //   this.foodtocheckout.forEach((element,index)=>{
  //     const foodorder: FoodOrder = {
  //      itemid  : element._id?.toString(),
  //       price: element.price,
  //       qty: element.qty,
  //       totalprice: this.totalprice,
  //       description: "todo",
  //       orderno: "1"

  //     };
  //     this.foodorder.createFoodOrder(foodorder).subscribe();

  //   });
  //   // alert(JSON .stringify(this.foodtocheckout))
  // }


  // headclick(id:any)
  // {
  //   this.foodtodisplay=this.fooditems.filter(x=>x.category==id);

  // }

  // addtocart(id:any)
  // {

  //   var obj:any=this.foodtodisplay.find(x=>x._id==id);

  //    if(this.foodtocheckout.includes(obj))
  //     {
  //     this.foodtocheckout.forEach((element,index)=>{
  //             if(element._id==obj._id){
  //             this.foodtocheckout[index].qty=this.foodtocheckout[index].qty+1
  //             this.totalprice=this.totalprice+this.foodtocheckout[index].price
  //           }
  //     });
  //     }else
  //     {
  //       this.foodtocheckout.push(obj)
  //       this.totalprice=this.totalprice+obj.price
  //     }
  // }

  // checkoutcart(id:any,flg:number)
  // {
  //   this.foodtocheckout.forEach((element,index)=>{
  //     if(element._id==id && flg==1)
  //     {
  //        this.foodtocheckout[index].qty=this.foodtocheckout[index].qty+1
  //        this.totalprice=this.totalprice+this.foodtocheckout[index].price
  //     }
  //     else  if(element._id==id && flg==0)
  //      {
  //       this.totalprice=this.totalprice-this.foodtocheckout[index].price
  //       if(element.qty>1){

  //         this.foodtocheckout[index].qty=this.foodtocheckout[index].qty-1

  //         }else{
  //         this.foodtocheckout.splice(index,1)
  //         }
  //     }

  //    });
  // }

  // seachfood()
  // {
  //   this.foodtodisplay=this.fooditems.filter(x=>x.name.toLowerCase().includes(this.searchtxt.toLowerCase()))
  // }

  // onChange(index:number) {
  //      if(index.toString()=="1")
  //       this.checkouttext="Delivery Address "
  //     else if(index.toString()=="2")
  //     this.checkouttext="Dine In Table No. "
  // }

  inventoryItems: InventoryItem[] = [];
  inventoryItemsToDisplay: InventoryItem[] = [];
  inventoryItemsToCheckout: InventoryItem[] = [];

  searchTxt: string = "";
  totalPrice: number = 0;
  checkoutText = '';

  inventoryForm: FormGroup = this.formBuilder.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    isAvailable: [null],
    imageUrl: [null],
    category: [null, Validators.required],
    supplier: [null, Validators.required]
  });

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

  saveInventoryItem(): void {
    if (this.inventoryForm.valid) {
      const formData = this.inventoryForm.value;

      if (!formData.category || !formData.supplier) {
        console.error('Category or supplier is undefined');
        return;
      }

      const inventoryItem: InventoryItem = {
        id: 0,
        name: formData.name,
        description: formData.description,
        isAvailable: formData.isAvailable,
        imageUrl: formData.imageUrl,
        category: formData.category,
        suppliers: formData.supplier
      };

      this.inventoryService.createInvnentoryItem(inventoryItem).subscribe({
        next: () => {
          alert("Inventory item added successfully");
          this.loadInventoryItems();
        },
        error: (error) => {
          console.error(error);
        }
      });
    }
  }

  headclick(id: any): void {
    this.inventoryItemsToDisplay = this.inventoryItems.filter(x => x.category?.id == id);
  }

  // addtocart(id: any): void {
  //   const item = this.inventoryItemsToDisplay.find(x => x.id === id);
  //   if (item && !this.inventoryItemsToCheckout.includes(item)) {
  //     this.inventoryItemsToCheckout.push({ ...item, qty: 1 });
  //   } else if (item) {
  //     const index = this.inventoryItemsToCheckout.findIndex(x => x.id === id);
  //     this.inventoryItemsToCheckout[index].qty += 1;
  //   }
  // }

  // checkoutcart(id: any, flg: number): void {
  //   this.inventoryItemsToCheckout.forEach((element, index) => {
  //     if (element.id == id && flg == 1) {
  //       this.inventoryItemsToCheckout[index].qty = this.inventoryItemsToCheckout[index].qty + 1;
  //       this.totalPrice = this.totalPrice + this.inventoryItemsToCheckout[index].price;
  //     } else if (element.id == id && flg == 0) {
  //       this.totalPrice = this.totalPrice - this.inventoryItemsToCheckout[index].price;
  //       if (element.qty > 1) {
  //         this.inventoryItemsToCheckout[index].qty = this.inventoryItemsToCheckout[index].qty - 1;
  //       } else {
  //         this.inventoryItemsToCheckout.splice(index, 1);
  //       }
  //     }
  //   });
  // }

  searchInventoryItems(): void {
    this.inventoryItemsToDisplay = this.inventoryItems.filter(x => x.name.toLowerCase().includes(this.searchTxt.toLowerCase()));
  }

  onChange(index: number): void {
    if (index.toString() == "1")
      this.checkoutText = "Delivery Address";
    else if (index.toString() == "2")
      this.checkoutText = "Dine In Table No.";
  }
}
