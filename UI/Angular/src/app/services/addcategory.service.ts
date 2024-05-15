import { Injectable } from '@angular/core';
import { Category } from '../models/category';
import { Observable, Subject, catchError } from 'rxjs';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class AddcategoryService {
  private apiUrl = 'https://localhost:7280/api'; // Make sure this URL is correct
  // private category$: Subject<Category[]> = new Subject();

  constructor(private httpClient: HttpClient) { }

  createCategory(category: Category): Observable<string> {
    return this.httpClient.post(`${this.apiUrl}/Category`, category, { responseType: 'text' });
  }
  //https://localhost:7280/api/Category/OnlyCategory

  getallcategories(): Observable <Category[]>{
    return this.httpClient.get<Category[]>(`${this.apiUrl}/Category/OnlyCategory`);
  }


  //https://localhost:7280/api/Category/id/1013


  getCategoryById(id:string): Observable <Category>{
    return this.httpClient.get<Category>(`${this.apiUrl}/Category/id/${id}`);
  }




  //https://localhost:7280/api/Category/2
  updateCategory(id:string,updatecategoryRequest:Category): Observable <Category>{
    return this.httpClient.put<Category>(`${this.apiUrl}/Category/${id}`,updatecategoryRequest);
  }

  //https://localhost:7280/api/Category/22

  deleteCategory(id:string): Observable <Category>{
    return this.httpClient.delete<Category>(`${this.apiUrl}/Category/${id}`);
  }


}
