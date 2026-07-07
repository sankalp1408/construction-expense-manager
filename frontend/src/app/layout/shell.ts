import { Component, inject, signal } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AuthService } from '../core/services/auth.service';
import { Language, LanguageService } from '../core/services/language.service';
import { TranslatePipe } from '../shared/pipes/translate.pipe';

interface NavItem {
  labelKey: string;
  path: string;
  icon: string;
  superAdminOnly?: boolean;
}

@Component({
  selector: 'app-shell',
  imports: [
    RouterOutlet, RouterLink, RouterLinkActive,
    MatToolbarModule, MatSidenavModule, MatListModule, MatIconModule, MatButtonModule, MatMenuModule, MatDividerModule,
    TranslatePipe
  ],
  templateUrl: './shell.html',
  styleUrl: './shell.scss'
})
export class Shell {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly languageService = inject(LanguageService);

  readonly sidenavOpen = signal(false);
  readonly currentUser = this.authService.currentUser;
  readonly isSuperAdmin = this.authService.isSuperAdmin;
  readonly currentLanguage = this.languageService.currentLanguage;

  // Dashboard stands alone; the 3 work-type sections are grouped together
  // under a "Work Types" subheader; Reports/Manage Managers are utilities.
  readonly topItems: NavItem[] = [
    { labelKey: 'nav.dashboard', path: '/dashboard', icon: 'dashboard' }
  ];

  readonly workTypeItems: NavItem[] = [
    { labelKey: 'nav.tenderWorks', path: '/tender-works', icon: 'gavel' },
    { labelKey: 'nav.commissionWorks', path: '/commission-works', icon: 'handshake' },
    { labelKey: 'nav.privateWorks', path: '/private-works', icon: 'home-work' }
  ];

  readonly utilityItems: NavItem[] = [
    { labelKey: 'nav.reports', path: '/reports', icon: 'bar-chart' },
    { labelKey: 'nav.users', path: '/users', icon: 'people', superAdminOnly: true }
  ];

  toggleSidenav(): void {
    this.sidenavOpen.update((v) => !v);
  }

  closeSidenav(): void {
    this.sidenavOpen.set(false);
  }

  setLanguage(lang: Language): void {
    this.languageService.setLanguage(lang);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
