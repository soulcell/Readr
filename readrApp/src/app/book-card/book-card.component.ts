import { Component, Input } from '@angular/core';
import BookModel from '../../models/book-model';
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

    this.book = {
      id: 0,
      title: "The Picture of Dorian Gray",
      authorName: "Oscar Wilde",
      imageUrl: "/assets/test/the-picture-of-dorian-gray-9781625587534_hr.jpg",
      distanceMeters: 1000
    }
  }

  @Input()
  book!: BookModel;

}
