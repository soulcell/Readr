import { Component, Input } from '@angular/core';
import BookModel from '../../models/book';
import { IonIcon } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { locationOutline } from 'ionicons/icons';

@Component({
  selector: 'app-book-card',
  templateUrl: './book-card.component.html',
  styleUrls: ['./book-card.component.scss'],
  imports: [IonIcon],
  standalone: true,
})
export class BookCardComponent {

  constructor() 
  {
    addIcons({ locationOutline })
  }

  @Input()
  book!: BookModel;

}
