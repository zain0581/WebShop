import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { FoodItem } from '../models/fooditem';

@Injectable({
  providedIn: 'root'
})
export class FoodserviceService {

  private url = 'http://localhost:5200';
  private fooditems$: Subject<FoodItem[]> = new Subject();
  
  constructor(private httpClient: HttpClient) { }
  
  private refreshfooditems() {
    this.httpClient.get<FoodItem[]>(`${this.url}/fooditems`)
      .subscribe(fooditems => {
        this.fooditems$.next(fooditems);
      });
  }
  
  getfooditems(): Subject<FoodItem[]> {
    this.refreshfooditems();
    return this.fooditems$;
  }
  
  getfooditem(id: string): Observable<FoodItem> {
    return this.httpClient.get<FoodItem>(`${this.url}/fooditems/${id}`);
  }
  
  createFoodItem(foodcategory: FoodItem): Observable<string> {
    alert(JSON.stringify(foodcategory))
    return this.httpClient.post(`${this.url}/fooditems`, foodcategory, { responseType: 'text' });
  }
  
  updateFoodItem(id: string, fooditem: FoodItem): Observable<string> {
    return this.httpClient.put(`${this.url}/fooditems/${id}`, fooditem, { responseType: 'text' });
  }
  
  deleteFoodItem(id: string): Observable<string> {
    return this.httpClient.delete(`${this.url}/fooditems/${id}`, { responseType: 'text' });
  }
}
