import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Subscription } from 'rxjs';
import { Category } from '../../models/category';
import { AddcategoryService } from '../../services/addcategory.service';


@Component({
  selector: 'app-edit-categoryl',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule, RouterModule,],
  templateUrl: './edit-categoryl.component.html',
  styleUrl: './edit-categoryl.component.css'
})

  export class EditCategorylComponent implements OnInit {

    id: string | null = null;

    category?: Category;

    constructor(
      private route: ActivatedRoute,
      private categoryService: AddcategoryService,
      private router: Router
    ) {}

    ngOnInit(): void {
       this.route.params.subscribe(params => {
        this.id = params['id'];

        if (this.id) {
          this.categoryService.getCategoryById(this.id).subscribe({
            next: (response) => {
              this.category = response;
            }
          });
        }
      });
    }


    onFormSubmit(): void {
      if (this.category) {
        const updateCategoryRequest: Category = {
          id: Number(this.category.id) || 0,
          name: this.category.name || '',
          imageUrl: this.category.imageUrl || '',
          description: this.category.description || '',
          isActive: this.category.isActive || 0
        };

        this.categoryService.updateCategory(this.id || '', updateCategoryRequest).subscribe({
          error: (error) => {
            if (error.status === 200) {
              // Treat HTTP status code 200 as a success
              alert("Category updated successfully");
              this.router.navigate(['/category']);
              // this.categories$ = this.categoryService.getallcategories();
            }
          }
        });
      }
    }

    onCancel(): void {
      this.router.navigate(['/category']);
    }


  }
