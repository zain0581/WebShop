import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { InventoryItem } from '../models/InventoryItem';

@Injectable({
  providedIn: 'root'
})
export class InventoryItemService {

  private apiUrl = 'https://localhost:7280/api'; // Make sure this URL is correct
  // private category$: Subject<Category[]> = new Subject();

  constructor(private httpClient: HttpClient) { }

  //https://localhost:7280/api/InvetoryItem/newcreate





  createInvnentoryItem(data : InventoryItem) : Observable<InventoryItem>{
    // console.log("Service Test: ", data);
    return this.httpClient.post<InventoryItem>(`${this.apiUrl}/InvetoryItem/newcreate`,data);
  }



  getallinventoryitems(): Observable <InventoryItem[]>{
    return this.httpClient.get<InventoryItem[]>(`${this.apiUrl}/InvetoryItem`);
  }


 // https://localhost:7280/api/InvetoryItem/17

  deleteInventory(id: string): Observable<void> {
    return this.httpClient.delete<void>(`${this.apiUrl}/InvetoryItem/${id}`);
  }




  getinventoryitemById(id:string): Observable <InventoryItem>{
    return this.httpClient.get<InventoryItem>(`${this.apiUrl}/Category/id/${id}`);
  }




  updateinventoryitem(id:string,updatecategoryRequest:InventoryItem): Observable <InventoryItem>{
    return this.httpClient.put<InventoryItem>(`${this.apiUrl}/Category/${id}`,updatecategoryRequest);
  }



  // deleteinventoryitem(id:string): Observable <InventoryItem>{
  //   return this.httpClient.delete<InventoryItem>(`${this.apiUrl}/Category/${id}`);
  // }
}
