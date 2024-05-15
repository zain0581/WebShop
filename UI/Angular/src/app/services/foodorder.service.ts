import { Injectable } from '@angular/core';
import { FoodOrder } from '../models/foodorder';
import { Observable, Subject } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FoodorderService {

  private url = 'http://localhost:5200';
  private foodorder$: Subject<FoodOrder[]> = new Subject();
  
  constructor(private httpClient: HttpClient) { }
  
  private refreshFoodOrder() {
    this.httpClient.get<FoodOrder[]>(`${this.url}/foodcategories`)
      .subscribe(foodcategories => {
        this.foodorder$.next(foodcategories);
      });
  }
  
  getFoodOrders(): Subject<FoodOrder[]> {
    this.refreshFoodOrder();
    return this.foodorder$;
  }
  
  getFoodOrder(id: string): Observable<FoodOrder> {
    return this.httpClient.get<FoodOrder>(`${this.url}/foodorders/${id}`);
  }
  
  createFoodOrder(foodcategory: FoodOrder): Observable<string> {
    return this.httpClient.post(`${this.url}/foodorders`, foodcategory, { responseType: 'text' });
  }
  
  updateFoodOrder(id: string, foodcategory: FoodOrder): Observable<string> {
    return this.httpClient.put(`${this.url}/foodorders/${id}`, foodcategory, { responseType: 'text' });
  }
  
  deleteFoodOrder(id: string): Observable<string> {
    return this.httpClient.delete(`${this.url}/foodorders/${id}`, { responseType: 'text' });
  }
   
}
