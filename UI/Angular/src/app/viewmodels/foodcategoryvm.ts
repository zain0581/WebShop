export class FoodCategoryVM {
    
    constructor(id:number,name:string,imgurl:string) {
        this.id=id;
        this.name=name;
        this.imgurl  =imgurl;
    }
    id:number;
    name:string;
    imgurl:string; 
}