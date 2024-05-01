import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { GenreChipComponent } from 'src/app/genre-chip/genre-chip.component';
import { Router, RouterModule } from '@angular/router';
import { IonBackButton, IonButton, IonButtons, IonContent, IonHeader, IonToolbar } from '@ionic/angular/standalone';
import { BookService } from 'src/app/services/book.service';
import Genre from 'src/models/genre';

@Component({
  selector: 'app-genres',
  templateUrl: './genres.page.html',
  styleUrls: ['./genres.page.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule, GenreChipComponent, RouterModule, IonHeader, IonToolbar, IonButtons, IonBackButton, IonButton, IonContent]
})
export class GenresPage implements OnInit {

  genres: Array<Genre> = [];

  selectedGenres: Set<Genre> = new Set<Genre>();

  constructor(private bookService: BookService, private router: Router) { }
  
  ngOnInit(): void {
    this.bookService.getGenres().subscribe(res => this.genres = res)
  }

  genreToggled(genre: Genre, value: boolean) {
    if (value) {
      this.selectedGenres.add(genre);
    } else {
      this.selectedGenres.delete(genre);
    }
  }

  continueButton() {
    this.bookService.addPreferedGenres([...this.selectedGenres]).subscribe({
      next: () => this.router.navigate(['/add-books'])
    })
  }

}
