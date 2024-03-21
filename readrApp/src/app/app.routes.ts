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
  {
    path: 'myname',
    loadComponent: () => import('./onboarding/myname/myname.page').then( m => m.MynamePage)
  },
  {
    path: 'genres',
    loadComponent: () => import('./onboarding/genres/genres.page').then( m => m.GenresPage)
  },
  {
    path: 'add-books',
    loadComponent: () => import('./add-books/add-books.page').then( m => m.AddBooksPage)
  },
];
