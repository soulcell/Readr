import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import API_ENDPOINTS from 'src/consts/api-endpoints';
import Genre from 'src/models/genre';
import { tap } from 'rxjs';
import BookModel from 'src/models/book';
import BookLikeStatus from 'src/consts/book-like-status';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private http: HttpClient, private authSrvice: AuthService) { }

  cachedGenres: Array<Genre> = [];

  getGenres() {
    const params = new HttpParams().set('pageNumber', 1).set('pageSize', 10);
    return this.http.get<Array<Genre>>(API_ENDPOINTS.GENRE.GET, { params: params }).pipe(
      tap(res => this.cachedGenres = res)
    );
  }

  addPreferedGenres(genres: Array<Genre>) {
    return this.http.post(API_ENDPOINTS.GENRE.PREFERED_GENRES.ADD, genres.map(g => g.id));
  }

  addBook(book: BookModel) {
    const dto = {
      bookTitleId: book.bookTitle.id,
      bookTitle: book.bookTitle.title,
      bookAuthor: book.bookTitle.author,
      genreId: book.bookTitle.genre!.id,
      // TODO: Add geoposition
    };

    return this.http.post(API_ENDPOINTS.BOOK.MY_BOOKS, dto);
  }

  getSuggestions() {
    return this.http.get<Array<BookModel>>(API_ENDPOINTS.SUGGESTIONS)
  }

  like(book: BookModel) {
    const params = new HttpParams().set('bookId', book.id).set('likeStatus', BookLikeStatus.Like)
    return this.http.post(API_ENDPOINTS.LIKE, null, { params: params });
  }

  dislike(book: BookModel) {
    const params = new HttpParams().set('bookId', book.id).set('likeStatus', BookLikeStatus.Dislike);
    return this.http.post(API_ENDPOINTS.LIKE, null,  { params: params });
  }
}
