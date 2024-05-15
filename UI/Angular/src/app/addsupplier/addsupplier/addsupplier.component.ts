import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AddcategoryService } from '../../services/addcategory.service';
import { Supplier } from '../../models/supplier';
import { SupplierService } from '../../services/supplier.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-addsupplier',
  standalone: true,
  imports: [ReactiveFormsModule,RouterModule,CommonModule],
  templateUrl: './addsupplier.component.html',
  styleUrl: './addsupplier.component.css'
})
export class AddsupplierComponent {

  supplierForm: FormGroup = this.fb.group({
    name: ['', Validators.required],
    email: [''],
    phone: ['']
  });

  constructor(
    private fb: FormBuilder,
    private supplierService: SupplierService,
    private router: Router
  ) {}

  onItemSave() {
    const supplier: Supplier = {
      id: 0, // You might want to initialize this with a default value or handle it differently
      name: this.supplierForm.value.name,
      email: this.supplierForm.value.email,
      phone: this.supplierForm.value.phone
    };

    this.supplierService.createSupplier(supplier).subscribe(() => {
      alert('Supplier added successfully');
      this.router.navigate(['/supplierlist']);
      // Redirect or perform any other action after adding the supplier
    });
  }
}
