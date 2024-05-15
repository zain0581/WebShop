import { SupplierService } from './../../services/supplier.service';
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Supplier } from '../../models/supplier';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-suppliers-list',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule,CommonModule,],
  templateUrl: './suppliers-list.component.html',
  styleUrl: './suppliers-list.component.css'
})
export class SuppliersListComponent {

  constructor(private  router: RouterModule, private supplierservice: SupplierService ) {}


  id : string | null = null;

  categories$?:Observable<Supplier[]>;

  ngOnInit(): void {
    this.categories$ = this.supplierservice.getallsupplier();
  }




  onDelete(id: number ): void {
    if (id !== undefined) {
      this.supplierservice.deletesupplier(id.toString()).subscribe({
        error: (error) => {
          if (error.status === 200) {
            // Treat HTTP status code 200 as a success
            alert("Category deleted successfully");
            this.categories$ = this.supplierservice.getallsupplier();
          }
        }
      });
    }
  }
}
