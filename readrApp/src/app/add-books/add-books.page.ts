import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { BookAddSmallCardComponent } from '../book-add-small-card/book-add-small-card.component';
import BookModel from 'src/models/book-model';
import { ModalController } from '@ionic/angular/standalone';
import { AddBookOptionsModalComponent } from './add-book-options-modal/add-book-options-modal.component';
import { AddBookManuallyModalComponent } from './add-book-manually-modal/add-book-manually-modal.component';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-add-books',
  templateUrl: './add-books.page.html',
  styleUrls: ['./add-books.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule, BookAddSmallCardComponent, RouterModule]
})
export class AddBooksPage {

  books: Array<BookModel|undefined> = [
    {
      id: 4214,
      title: '20,000 Leaugues Under The Sea',
      authorName: 'Jules Verne',
      imageUrl: 'https://images.penguinrandomhouse.com/cover/9780553212525',
      distanceMeters: 0
    },
    {
      id: 4214,
      title: '20,000 Leaugues Under The Sea',
      authorName: 'Jules Verne',
      imageUrl: '',
      distanceMeters: 0
    },
    undefined,
    undefined,
    undefined,
    undefined,
  ]

  constructor(private modalCtrl: ModalController) { }


  async addOrRemoveBookToSlot(slot: number) {
    if (this.books[slot]) {
      this.books[slot] = undefined;
      return;
    }

    const modalOptions = await this.modalCtrl.create({
      component: AddBookOptionsModalComponent,
    });
    modalOptions.present();

    const { data, role } = await modalOptions.onWillDismiss();

    if ( role === 'cancel') {
      return;
    }

    if ( role === 'manually') {
      const modalManually = await this.modalCtrl.create({
        component: AddBookManuallyModalComponent,
      });
      modalManually.present();

      const { data, role } = await modalManually.onWillDismiss();

      if (role === 'cancel') {
        return;
      }

      this.books[slot] = data;
    }
  }

}
