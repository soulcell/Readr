import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BookAddSmallCardComponent } from '../book-add-small-card/book-add-small-card.component';
import BookModel from 'src/models/book';
import { IonBackButton, IonButtons, IonContent, IonHeader, IonToolbar, ModalController, IonButton } from '@ionic/angular/standalone';
import { AddBookOptionsModalComponent } from './add-book-options-modal/add-book-options-modal.component';
import { AddBookManuallyModalComponent } from './add-book-manually-modal/add-book-manually-modal.component';
import { Router, RouterModule } from '@angular/router';
import { BookService } from '../services/book.service';
import { Observable, zip } from 'rxjs';


@Component({
  selector: 'app-add-books',
  templateUrl: './add-books.page.html',
  styleUrls: ['./add-books.page.scss'],
  standalone: true,
  imports: [IonButton, CommonModule, FormsModule, BookAddSmallCardComponent, RouterModule, IonHeader, IonToolbar, IonButtons, IonBackButton, IonContent]
})
export class AddBooksPage {

  books: Array<BookModel|undefined> = [
    {
      id: 0,
      bookTitle: {
        id: 4214,
        title: '20,000 Leaugues Under The Sea',
        author: 'Jules Verne',
        genre: {
          id: 1,
          name: 'Fiction'
        },
      }
    },
    undefined,
    undefined,
    undefined,
    undefined,
    undefined,
  ]

  constructor(private modalCtrl: ModalController, private router: Router, private bookService: BookService) { }


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

  continueButton() {
    const requests: Array<Observable<Object>> = [];
    this.books.forEach(b => {
      b && requests.push(this.bookService.addBook(b));
    });
    zip(...requests).subscribe({
      next: () => {
        this.router.navigate(['/']);
      }
    })
  }

  isTrue(el: any) {
    return !!el;
  }

}
