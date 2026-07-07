import { Pipe, PipeTransform, inject } from '@angular/core';
import { LanguageService } from '../../core/services/language.service';

// Impure so every usage re-evaluates on the next change-detection cycle after
// the language toggle fires — no need to thread a language signal through
// every component just to make translations react live.
@Pipe({ name: 'translate', pure: false })
export class TranslatePipe implements PipeTransform {
  private readonly languageService = inject(LanguageService);

  transform(key: string, params?: Record<string, string | number>): string {
    return this.languageService.translate(key, params);
  }
}
