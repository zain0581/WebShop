import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'foodo';

  constructor(private router: Router) {}
  onAdminButtonClick(): void {
    // Navigate to the admin component with a parameter indicating button click
    this.router.navigate(['/admin'], { queryParams: { adminButton: 'true' } });
  }
}
