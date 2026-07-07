import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { superAdminGuard } from './core/guards/super-admin.guard';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./features/login/login').then((m) => m.Login)
  },
  {
    path: '',
    loadComponent: () => import('./layout/shell').then((m) => m.Shell),
    canActivate: [authGuard],
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'dashboard' },
      {
        path: 'dashboard',
        loadComponent: () => import('./features/dashboard/dashboard').then((m) => m.Dashboard)
      },
      {
        path: 'tender-works',
        loadComponent: () => import('./features/tender-works/tender-work-list/tender-work-list').then((m) => m.TenderWorkList)
      },
      {
        path: 'tender-works/:id',
        loadComponent: () => import('./features/tender-works/tender-work-detail/tender-work-detail').then((m) => m.TenderWorkDetail)
      },
      {
        path: 'commission-works',
        loadComponent: () => import('./features/commission-works/commission-work-list/commission-work-list').then((m) => m.CommissionWorkList)
      },
      {
        path: 'commission-works/:id',
        loadComponent: () => import('./features/commission-works/commission-work-detail/commission-work-detail').then((m) => m.CommissionWorkDetail)
      },
      {
        path: 'private-works',
        loadComponent: () => import('./features/private-works/private-work-list/private-work-list').then((m) => m.PrivateWorkList)
      },
      {
        path: 'private-works/:id',
        loadComponent: () => import('./features/private-works/private-work-detail/private-work-detail').then((m) => m.PrivateWorkDetail)
      },
      {
        path: 'reports',
        loadComponent: () => import('./features/reports/reports').then((m) => m.Reports)
      },
      {
        path: 'users',
        loadComponent: () => import('./features/users/user-list').then((m) => m.UserList),
        canActivate: [superAdminGuard]
      },
      { path: '**', redirectTo: 'dashboard' }
    ]
  }
];
