export class FoodOrder {
    constructor(orderno?:string, itemid?:string, price?:number, qty?:number,  totalprice?:number,  description?:string, id?: number) {
      this._id = id;
      this.description = description;
      this.itemid = itemid;
        this.orderno = orderno;
        this.price = price;
        this.qty = qty;
        this.totalprice = totalprice;
    }
  
    _id?: number;
    orderno?:string;
    itemid?:string;
    price?:number;
    qty?:number;
    totalprice?:number;
    description?:string; 
  }



 
  