import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { GenreChipComponent } from 'src/app/genre-chip/genre-chip.component';
import { RouterModule } from '@angular/router';
import { IonBackButton, IonButton, IonButtons, IonContent, IonHeader, IonToolbar } from '@ionic/angular/standalone';

@Component({
  selector: 'app-genres',
  templateUrl: './genres.page.html',
  styleUrls: ['./genres.page.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule, GenreChipComponent, RouterModule, IonHeader, IonToolbar, IonButtons, IonBackButton, IonButton, IonContent]
})
export class GenresPage {

  genres = [
    'Mystery',
    'Thriller',
    'Science Fiction',
    'Fantasy',
    'Historical Fiction',
    'Adventure',
    'Dystopian',
    'Utopian',
    'Crime',
    'Horror',
    'Memoir',
    'Biography',
    'Autobiography',
    'Satire',
    'Romance',
    'Humor',
    'Political Fiction',
    'Western',
    'Historical Romance',
    'Young Adult',
    'Children\'s Literature',
    'Fairy Tale',
    'Poetry'
  ]

  constructor() { }

}
