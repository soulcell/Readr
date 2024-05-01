import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IonButton, IonButtons, IonContent, IonHeader, IonInput, IonToolbar, IonSelect, IonSelectOption, ModalController } from '@ionic/angular/standalone';
import { BookService } from 'src/app/services/book.service';
import BookModel from 'src/models/book';
import BookTitle from 'src/models/bookTitle';
import Genre from 'src/models/genre';

@Component({
  selector: 'app-add-book-manually-modal',
  templateUrl: './add-book-manually-modal.component.html',
  styleUrls: ['./add-book-manually-modal.component.scss'],
  standalone: true,
  imports: [FormsModule, IonHeader, IonToolbar, IonButtons, IonButton, IonContent, IonInput, IonSelect, IonSelectOption],
})
export class AddBookManuallyModalComponent implements OnInit {

  bookTitle: BookTitle = {
    id: 0,
    title: '',
    author: '',
  }

  genres: Array<Genre> = []

  constructor(private modalCtrl: ModalController, private bookService: BookService) { }

  ngOnInit(): void {
    if (this.bookService.cachedGenres.length > 0) {
      this.genres = this.bookService.cachedGenres;
    } else {
      this.bookService.getGenres().subscribe({ next: res => this.genres = res }); 
    }
  }

  cancel() {
    this.modalCtrl.dismiss(null, 'cancel');
  }

  confirm() {
    const book: BookModel = {
      id: 0,
      bookTitle: this.bookTitle
    }
    this.modalCtrl.dismiss(book, 'confirm')
  }
}
