import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-purchase-dialog',
  standalone: true,
  imports: [MatButtonModule,MatDialogModule,MatInputModule,CommonModule, FormsModule],
  templateUrl: './purchase-dialog.component.html',
  styleUrl: './purchase-dialog.component.css'
})
export class PurchaseDialogComponent {


  cardNumber: string = '';
  cardExpiry: string = '';
  cardHolderName: string = '';
  userName: string = '';
  address: string = '';

  city: string = '';

  constructor(public dialogRef: MatDialogRef<PurchaseDialogComponent>) {}

  confirmPurchase(): void {
    if (this.cardNumber && this.cardExpiry && this.cardHolderName && this.userName && this.address &&  this.city) {
      alert('Purchase confirmed! Your order is on its way!');
      this.dialogRef.close();
    } else {
      alert('Please fill in all required fields.');
    }
  }
}
