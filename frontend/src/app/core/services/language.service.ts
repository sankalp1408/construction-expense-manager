import { Injectable, signal } from '@angular/core';
import { EN } from '../i18n/en';
import { MR } from '../i18n/mr';

export type Language = 'en' | 'mr';

const LANG_KEY = 'cem_lang';
const DICTS: Record<Language, unknown> = { en: EN, mr: MR };

@Injectable({ providedIn: 'root' })
export class LanguageService {
  private readonly languageSignal = signal<Language>(this.readStored());

  readonly currentLanguage = this.languageSignal.asReadonly();

  setLanguage(lang: Language): void {
    localStorage.setItem(LANG_KEY, lang);
    this.languageSignal.set(lang);
  }

  translate(key: string, params?: Record<string, string | number>): string {
    const value = this.lookup(DICTS[this.languageSignal()], key) ?? this.lookup(EN, key) ?? key;
    return params ? this.interpolate(value, params) : value;
  }

  private lookup(dict: unknown, key: string): string | undefined {
    const result = key.split('.').reduce<unknown>((acc, part) => {
      if (acc && typeof acc === 'object' && part in acc) {
        return (acc as Record<string, unknown>)[part];
      }
      return undefined;
    }, dict);
    return typeof result === 'string' ? result : undefined;
  }

  private interpolate(value: string, params: Record<string, string | number>): string {
    return Object.entries(params).reduce(
      (acc, [key, val]) => acc.replace(`{${key}}`, String(val)),
      value
    );
  }

  private readStored(): Language {
    const stored = localStorage.getItem(LANG_KEY);
    return stored === 'mr' ? 'mr' : 'en';
  }
}
