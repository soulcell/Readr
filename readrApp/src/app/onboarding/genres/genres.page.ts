import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { GenreChipComponent } from 'src/app/genre-chip/genre-chip.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-genres',
  templateUrl: './genres.page.html',
  styleUrls: ['./genres.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule, GenreChipComponent, RouterModule]
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
