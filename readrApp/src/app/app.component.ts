import { Component, OnInit } from '@angular/core';
import { IonApp, IonRouterOutlet, AlertController } from '@ionic/angular/standalone';
import { NotificationService } from './services/notification.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  standalone: true,
  imports: [IonApp, IonRouterOutlet],
})
export class AppComponent implements OnInit {
  constructor(
    private notificationService: NotificationService, 
    private alertController: AlertController, 
    private router: Router) {  }

  ngOnInit(): void {
    this.notificationService.getMutualLikes().subscribe({
      next: async (res) => {
        console.log('test');
        const alert = await this.alertController.create({
          message: `${res.user.name} wants to trade your book "${res.myBook.bookTitle.title}" for his book "${res.hisBook.bookTitle.title}"`,
          buttons: [
            { text: 'Call', role: 'call' }, 
            { text: 'Cancel', role: 'cancel' },
          ]
        });
        alert.present()

        const { role } = await alert.onWillDismiss()


        if (role === 'call') {
            window.location.href = 'tel:' + res.user.phone;
          }
      }
    });
  }
}
