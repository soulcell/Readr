import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { BookAddSmallCardComponent } from '../book-add-small-card/book-add-small-card.component';
import BookModel from 'src/models/book-model';


@Component({
  selector: 'app-add-books',
  templateUrl: './add-books.page.html',
  styleUrls: ['./add-books.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule, BookAddSmallCardComponent]
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
    undefined,
    undefined,
    undefined,
    undefined,
    undefined
  ]

  constructor() { }

}
