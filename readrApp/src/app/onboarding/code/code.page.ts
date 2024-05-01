import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { IonBackButton, IonButton, IonButtons, IonContent, IonHeader, IonInput, IonToolbar } from '@ionic/angular/standalone';
import { AuthService } from 'src/app/services/auth.service';
import { catchError, tap } from 'rxjs';

@Component({
  selector: 'app-code',
  templateUrl: './code.page.html',
  styleUrls: ['./code.page.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, IonHeader, IonToolbar, IonButtons, IonBackButton, IonContent, IonInput, IonButton]
})
export class CodePage {

  code: string = ''

  constructor(private authService: AuthService, private router: Router) { }

  continueButton() {
    this.authService.checkSecurityCode(this.code)
      .subscribe({
        next: _ => this.router.navigate(['/myname']),
        error: err => console.error(err)
      });
  }

}
