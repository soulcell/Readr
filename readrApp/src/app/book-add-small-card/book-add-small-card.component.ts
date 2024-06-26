import { CommonModule } from '@angular/common';
import { Component, EventEmitter, HostBinding, Input, Output } from '@angular/core';
import { IonButton, IonIcon } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { addOutline } from 'ionicons/icons';
import BookModel from 'src/models/book';

@Component({
  selector: 'app-book-add-small-card',
  templateUrl: './book-add-small-card.component.html',
  styleUrls: ['./book-add-small-card.component.scss'],
  standalone: true,
  imports: [CommonModule, IonButton, IonIcon]
})
export class BookAddSmallCardComponent {


  @Input()
  book?: BookModel

  @Output()
  buttonClick = new EventEmitter();


  @HostBinding('class.has-book')
  get has_book() { return this.book || undefined; }

  constructor() { 
    addIcons({addOutline})
  }

}
