import { CommonModule } from '@angular/common';
import { Component, HostBinding, Input } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { addOutline } from 'ionicons/icons';
import BookModel from 'src/models/book-model';

@Component({
  selector: 'app-book-add-small-card',
  templateUrl: './book-add-small-card.component.html',
  styleUrls: ['./book-add-small-card.component.scss'],
  standalone: true,
  imports: [CommonModule, IonicModule]
})
export class BookAddSmallCardComponent {


  @Input()
  book?: BookModel

  @HostBinding('class.has-book')
  get has_book() { return this.book || undefined; }

  constructor() { 
    addIcons({addOutline})
  }

}
