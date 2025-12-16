import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-error-page',
  imports: [CommonModule],
  templateUrl: './errorPage.component.html',
  styleUrl: './errorPage.component.css'
})
export class ErrorPageComponent {
  status = '';
  message = '';

  constructor(route: ActivatedRoute) {
    this.status = route.snapshot.queryParamMap.get('status') ?? 'Error';
    this.message = route.snapshot.queryParamMap.get('message')
      ?? 'Ocurrió un error inesperado.';
  }
}
