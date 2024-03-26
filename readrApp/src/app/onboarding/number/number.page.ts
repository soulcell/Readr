import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { 
  IonBackButton, IonButtons, IonCol, IonContent,
  IonGrid, IonHeader, IonInput, IonRow, IonSelect,
  IonSelectOption, IonToolbar 
} from '@ionic/angular/standalone';

@Component({
  selector: 'app-number',
  templateUrl: './number.page.html',
  styleUrls: ['./number.page.scss'],
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterModule, IonHeader, 
    IonToolbar, IonButtons, IonBackButton, IonContent, 
    IonGrid, IonRow, IonCol, IonSelect, IonSelectOption, 
    IonInput
  ]
})
export class NumberPage {

  constructor() { }

}
