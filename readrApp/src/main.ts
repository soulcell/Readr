import { enableProdMode, importProvidersFrom } from '@angular/core';
import { HAMMER_GESTURE_CONFIG, HammerGestureConfig, HammerModule, bootstrapApplication } from '@angular/platform-browser';
import { RouteReuseStrategy, provideRouter } from '@angular/router';
import { IonicRouteStrategy, provideIonicAngular } from '@ionic/angular/standalone';

import { routes } from './app/app.routes';
import { AppComponent } from './app/app.component';
import { environment } from './environments/environment';

import 'hammerjs';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './app/interceptors/authInterceptor';


if (environment.production) {
  enableProdMode();
}

bootstrapApplication(AppComponent, {
  providers: [
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    provideIonicAngular({
      scrollAssist: false,
      scrollPadding: false,
    }),
    provideRouter(routes),
    importProvidersFrom(HammerModule),
    provideHttpClient(withInterceptors([authInterceptor]))
  ],
});
