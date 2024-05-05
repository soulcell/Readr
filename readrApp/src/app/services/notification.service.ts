import { Injectable } from '@angular/core';
import { HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import API_ENDPOINTS from 'src/consts/api-endpoints';
import BookModel from 'src/models/book';
import { AuthService } from './auth.service';
import { Observable, Subject } from 'rxjs';
import MutualLike from 'src/models/mutualLike';
import User from 'src/models/user';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private hubConnection?: HubConnection;

  private mutualLikes: Subject<MutualLike> = new Subject<MutualLike>();

  constructor(private authService: AuthService) { }

  public startConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(API_ENDPOINTS.NOTIFICATION, {
        accessTokenFactory: () => this.authService.getToken(),
        skipNegotiation: true, 
        transport: HttpTransportType.WebSockets
      })
      .build();

    this.hubConnection.start()
      .then(() => console.log('Notification connection started'))
      .catch(err => console.log('Error while starting a notification connection: ' + err));

    this.hubConnection.on('ReceiveMutualLikeNotification', (user: User, hisBook: BookModel, myBook: BookModel) => {
      const mutualLike: MutualLike = {
        user: user,
        hisBook: hisBook,
        myBook: myBook,
      }
  
      this.mutualLikes.next(mutualLike);
    });
  }

  public startConnectionIfNotStarted() {
    this.hubConnection || this.startConnection();
  }

  public getMutualLikes() {
    return this.mutualLikes.asObservable();
  }

}
