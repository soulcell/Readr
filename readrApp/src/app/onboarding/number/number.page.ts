import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { 
  IonBackButton, IonButtons, IonCol, IonContent,
  IonGrid, IonHeader, IonInput, IonRow, IonSelect,
  IonSelectOption, IonToolbar, IonButton
} from '@ionic/angular/standalone';
import { CountryPhoneCode, countryPhoneCodes } from 'src/consts/country-phone-codes';
import { AuthService } from 'src/app/services/auth.service';
import { USER } from 'src/consts/local-storage';

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

  constructor(private authService: AuthService, private router: Router) { }

  continueButton() {
    this.authService.getSecurityCode(this.numberPhoneCode + this.localNumber)
      .subscribe(res => {
        if (res) {
          this.router.navigate(['/code']);
        }
      });   
  }

}
