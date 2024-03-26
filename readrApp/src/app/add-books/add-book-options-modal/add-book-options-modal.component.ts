import { Component } from '@angular/core';
import { IonButton, IonButtons, IonContent, IonHeader, IonIcon, IonItem, IonLabel, IonTitle, IonToolbar, ModalController } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { camera, image } from 'ionicons/icons'

@Component({
  selector: 'app-add-book-options-modal',
  templateUrl: './add-book-options-modal.component.html',
  styleUrls: ['./add-book-options-modal.component.scss'],
  standalone: true,
  imports: [IonHeader, IonToolbar, IonButtons, IonButton, IonTitle, IonContent, IonItem, IonIcon, IonLabel],
})
export class AddBookOptionsModalComponent {

  constructor(private modalCtrl: ModalController) 
  {
    addIcons({camera, image})
  }

  cancel() {
    this.modalCtrl.dismiss(null, 'cancel');
  }

  scan() {
    this.modalCtrl.dismiss('scan', 'scan');
  }

  addManually() {
    this.modalCtrl.dismiss(null, 'manually');
  }
}
