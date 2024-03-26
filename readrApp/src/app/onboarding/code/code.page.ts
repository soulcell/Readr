import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { IonBackButton, IonButtons, IonContent, IonHeader, IonInput, IonToolbar } from '@ionic/angular/standalone';

@Component({
  selector: 'app-code',
  templateUrl: './code.page.html',
  styleUrls: ['./code.page.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, IonHeader, IonToolbar, IonButtons, IonBackButton, IonContent, IonInput]
})
export class CodePage {

  constructor() { }

}
