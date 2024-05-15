import { Supplier } from './../../models/supplier';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { SupplierService } from '../../services/supplier.service';

@Component({
  selector: 'app-edit-supplier',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule,FormsModule,RouterModule,],
  templateUrl: './edit-supplier.component.html',
  styleUrl: './edit-supplier.component.css'
})
export class EditSupplierComponent implements OnInit {
  constructor(  private route: ActivatedRoute,
    private supplierService: SupplierService,
    private router: Router) { }


    id: string | null = null;

    supplier?: Supplier;


  ngOnInit(): void {
   this.route.params.subscribe(params => {
    this.id = params['id'];

    if (this.id) {
      this.supplierService.getSupplierById(this.id).subscribe({
        next: (response) => {
          this.supplier = response;
        }
      });
    }
  });

}

onFormSubmit(): void{

  if (this.supplier) {
    const updateSupplierRequest: Supplier = {
      id: Number(this.supplier.id) || 0,
      name: this.supplier.name || '',
      email: this.supplier.email || '',
      phone: this.supplier.phone || '' ,
    };
    this.supplierService.updateSupplier(this.id || '', updateSupplierRequest).subscribe({
      error: (error) => {
        if (error.status === 200) {
          // Treat HTTP status code 200 as a success
          alert("Category updated successfully");
          this.router.navigate(['/supplierlist']);
          // this.categories$ = this.categoryService.getallcategories();
        }
      }
    });
}
}

onCancel(): void {
  this.router.navigate(['/supplierlist']);
}


}


