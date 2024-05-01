import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { IonBackButton, IonButton, IonButtons, IonContent, IonHeader, IonInput, IonToolbar } from '@ionic/angular/standalone';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-myname',
  templateUrl: './myname.page.html',
  styleUrls: ['./myname.page.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, IonHeader, IonToolbar, IonButtons, IonBackButton, IonContent, IonInput, IonButton]
})
export class MynamePage {

  userName: string = '';

  constructor(private authService: AuthService, private router: Router) { }

  continueButton() {
    this.authService.setUserName(this.userName).subscribe({
      next: _ => this.router.navigate(['/genres'])
    })
  }

}
