export class FoodItemVM {
    
    constructor(id:number,name:string,category:number,price:number,qty:number,imgurl:string) {
    
        this.id=id;
        this.name=name;
        this.category  =category;
        this.price=price;
        this.qty=qty;
        this.imgurl  =imgurl;
    }

    id:number;
    name:string;
    category:number;
    price:number;
    qty:number;
    imgurl:string; 
}