export class FoodItem {
    constructor(
      name: string,
      category: string,
      price: number,
      qty: number,
      imgurl: string,
      id?: number
    ) {
      this._id = id;
      this.name = name;
      this.category = category;
      this.price = price;
      this.qty = qty;
      this.imgurl = imgurl;
    }
  
    _id?: number;
    name: string;
    category: string;
    price: number;
    qty: number;
    imgurl: string;
  }
  