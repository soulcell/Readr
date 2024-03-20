import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./tabs/tabs.routes').then((m) => m.routes),
  },
  {
    path: 'sign-in',
    loadComponent: () => import('./onboarding/sign-in/sign-in.page').then( m => m.SignInPage)
  },
  {
    path: 'number',
    loadComponent: () => import('./onboarding/number/number.page').then( m => m.NumberPage)
  },
  {
    path: 'code',
    loadComponent: () => import('./onboarding/code/code.page').then( m => m.CodePage)
  },
];
