import { Component, OnInit } from '@angular/core';
import { AddcategoryService } from '../../services/addcategory.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { Category } from '../../models/category';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ToastrModule, ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-category',
  standalone: true,
  imports: [ReactiveFormsModule,RouterModule, CommonModule, ToastrModule,],
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})
export class CategoryComponent implements OnInit{

  constructor(

    private route :ActivatedRoute,
    private categoryService: AddcategoryService,
    private router: Router,
    // private toastr: ToastrService
    // Inject your category service
  ) {}

  id : string | null = null;

  categories$?:Observable<Category[]>;



  ngOnInit(): void {
//subscric a method that returns an observable
   this.categories$ = this.categoryService.getallcategories();
  }


  onDelete(id: number | undefined): void {
    if (id !== undefined) {
      this.categoryService.deleteCategory(id.toString()).subscribe({
        error: (error) => {
          if (error.status === 200) {
            // Treat HTTP status code 200 as a success
            alert("Category deleted successfully");
            this.categories$ = this.categoryService.getallcategories();
          }
        }
      });
    }
  }




}
