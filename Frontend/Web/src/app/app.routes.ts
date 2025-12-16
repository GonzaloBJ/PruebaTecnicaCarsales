import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./features/home/home.rutes')
        .then(m => m.HOME_ROUTES)
  },
  {
    path: 'episodios',
    loadChildren: () =>
      import('./features/episodios/episodios.routes')
        .then(m => m.EPISODIOS_ROUTES)
  },
  {
    path: 'error',
    loadChildren: () =>
      import('./features/error/error.routes')
        .then(m => m.ERROR_ROUTES)
  },
  {
    path: '**',
    redirectTo: 'error'
  }
];
