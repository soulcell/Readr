import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { ModalController } from '@ionic/angular/standalone';
import BookModel from 'src/models/book-model';

@Component({
  selector: 'app-add-book-manually-modal',
  templateUrl: './add-book-manually-modal.component.html',
  styleUrls: ['./add-book-manually-modal.component.scss'],
  standalone: true,
  imports: [IonicModule, FormsModule],
})
export class AddBookManuallyModalComponent {


  book: BookModel = {
    id: 0,
    title: '',
    authorName: '',
    imageUrl: '',
    distanceMeters: 0
  }

  constructor(private modalCtrl: ModalController) { }

  cancel() {
    this.modalCtrl.dismiss(null, 'cancel');
  }

  confirm() {
    this.modalCtrl.dismiss(this.book, 'confirm')
  }
}
