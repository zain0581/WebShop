import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Supplier } from '../models/supplier';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {

  //https://localhost:7280/api/Supplier
  private url = 'https://localhost:7280/api';
  private category$: Subject<Supplier[]> = new Subject();

  constructor(private httpClient: HttpClient) { }

  createSupplier(category: Supplier): Observable<string> {
    return this.httpClient.post(`${this.url}/Supplier`, category, { responseType: 'text' });
  }


  //https://localhost:7280/api/Supplier/GettingSuppliersOnly
  getallsupplier(): Observable <Supplier[]>{
    return this.httpClient.get<Supplier[]>(`${this.url}/Supplier/GettingSuppliersOnly`);
  }



  //https://localhost:7280/api/Supplier/1002

  deletesupplier(id:string): Observable <Supplier>{
    return this.httpClient.delete<Supplier>(`${this.url}/Supplier/${id}`);
  }


  updateSupplier(id:string,updatecategoryRequest:Supplier): Observable <Supplier>{
    return this.httpClient.put<Supplier>(`${this.url}/Supplier/${id}`,updatecategoryRequest);
  }



  //https://localhost:7280/api/Supplier/10
  getSupplierById(id:string): Observable <Supplier>{
    return this.httpClient.get<Supplier>(`${this.url}/Supplier/${id}`);
  }



}
