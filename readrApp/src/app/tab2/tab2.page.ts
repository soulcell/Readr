import { Component, ElementRef, HostBinding, QueryList, ViewChildren } from '@angular/core';
import { ExploreContainerComponent } from '../explore-container/explore-container.component';
import { BookCardComponent } from '../book-card/book-card.component';
import { HammerModule } from '@angular/platform-browser';
import BookModel from 'src/models/book-model';
import { CommonModule } from '@angular/common';
import { addIcons } from 'ionicons';
import { refreshCircleOutline } from 'ionicons/icons';
import { RouterModule } from '@angular/router';
import { IonButton, IonContent, IonHeader, IonIcon, IonToolbar } from '@ionic/angular/standalone';


@Component({
  selector: 'app-tab2',
  templateUrl: 'tab2.page.html',
  styleUrls: ['tab2.page.scss'],
  standalone: true,
  imports: [
    CommonModule, RouterModule, ExploreContainerComponent, BookCardComponent, 
    HammerModule, IonButton, IonIcon, IonButton,
    IonHeader, IonToolbar, IonContent
  ],
})
export class Tab2Page {


  @HostBinding('style.--card-translation-x')
  cardTranslationX = 0;

  @HostBinding('style.--card-translation-y')
  cardTranslationY = 0;

  @HostBinding('style.--card-rotation')
  cardRotation = 0;

  @HostBinding('style.--card-gradient-opacity')
  cardGradientOpacity = 0;

  @HostBinding('style.--card-gradient')
  cardGradient = '';

  @HostBinding('class.panning')
  isPanning = false;

  @ViewChildren('app-book-card')
  bookCards!: QueryList<ElementRef>;


  cards: Array<BookModel> = [
    {
      id: 0,
      title: "The Picture of Dorian Gray",
      authorName: "Oscar Wilde",
      imageUrl: "https://d28hgpri8am2if.cloudfront.net/book_images/onix/cvr9781625587534/the-picture-of-dorian-gray-9781625587534_hr.jpg",
      distanceMeters: 1000
    },
    {
      id: 2,
      title: "The Great Gatsby",
      authorName: "F. Scott Fitzgerald",
      imageUrl: "https://i0.wp.com/americanwritersmuseum.org/wp-content/uploads/2018/02/CK-3.jpg?resize=267%2C400&ssl=1",
      distanceMeters: 500
    },
    {
      id: 3,
      title: "The Glass Bead Game",
      authorName: "Hermann Hesse",
      imageUrl: "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1386922806i/16634.jpg",
      distanceMeters: 1400
    },
  ];

  handlePan(event: any) {
    if (event.deltaX === 0 || (event.center.x === 0 && event.center.y === 0)) return;


    const xMulti = event.deltaX * 0.03;
    const yMulti = event.deltaY / 80;
    const rotate = xMulti * yMulti;

    this.cardTranslationX = event.deltaX;
    this.cardTranslationY = event.deltaY;
    this.cardRotation = rotate;

    this.cardGradientOpacity = Math.abs(event.deltaX * 0.005);

    this.cardGradient = (event.deltaX >= 0) 
    ? 'linear-gradient(0deg, rgba(12, 153, 26, 1), rgba(20, 255, 44, 1))'
    : 'linear-gradient(0deg, rgba(64, 0, 0, 1), rgba(166, 0, 44, 1))'

    this.isPanning = true;
  }

  handlePanEnd(event: any) {
    this.isPanning = false;

    if (event.deltaX > 100)
    {
      this.cards.shift();
      return;
    }

    if (event.deltaX < -100)
    {
      this.cards.shift();
      return;
    }
  }

  constructor() {
    addIcons({refreshCircleOutline})
  }

}


