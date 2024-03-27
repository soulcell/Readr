import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { 
  IonBackButton, IonButtons, IonCol, IonContent,
  IonGrid, IonHeader, IonInput, IonRow, IonSelect,
  IonSelectOption, IonToolbar, IonButton
} from '@ionic/angular/standalone';
import { CountryPhoneCode, countryPhoneCodes } from 'src/consts/country-phone-codes';

@Component({
  selector: 'app-number',
  templateUrl: './number.page.html',
  styleUrls: ['./number.page.scss'],
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterModule, IonHeader, 
    IonToolbar, IonButtons, IonBackButton, IonContent, 
    IonGrid, IonRow, IonCol, IonSelect, IonSelectOption, 
    IonInput, IonButton
  ]
})
export class NumberPage {

  countryPhoneCodes: Array<CountryPhoneCode> = countryPhoneCodes;

  localNumber: string = '';

  numberPhoneCode: string = '';

  constructor() { }

}
