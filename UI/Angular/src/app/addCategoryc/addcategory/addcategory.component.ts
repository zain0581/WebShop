import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { Category } from '../../models/category';
import { AddcategoryService } from '../../services/addcategory.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-addcategory',
  standalone: true,
  imports: [ReactiveFormsModule,RouterModule, CommonModule],
  templateUrl: './addcategory.component.html',

  styleUrls: ['./addcategory.component.css']
})
export class AddcategoryComponent {

  categoryform: FormGroup = this.fb.group({
    name: ['', Validators.required],
    imageUrl: ['', Validators.required],
    description: [''],
    isActive: [0]
  });

  constructor(
    private fb: FormBuilder,
    private categoryservice : AddcategoryService,
    private router: Router
  ) {}

  onItemSave() {
    const category: Category = {
      name: this.categoryform.value.name,
      imageUrl: this.categoryform.value.imageUrl,
      description: this.categoryform.value.description,
      isActive: this.categoryform.value.isActive
    };

    this.categoryservice.createCategory(category).subscribe(() => {
      alert('Food category Added');
       this.router.navigate(['/category']);
    });
  }
}
