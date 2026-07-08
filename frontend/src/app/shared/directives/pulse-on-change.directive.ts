import { Directive, ElementRef, Input, OnChanges, SimpleChanges, inject } from '@angular/core';

// Briefly pulses/highlights the host element whenever the bound value changes,
// so summary figures visibly update instead of just snapping to a new number.
// Usage: <span class="figure-value" [pulseOnChange]="w.profit">...</span>
@Directive({
  selector: '[pulseOnChange]',
  standalone: true
})
export class PulseOnChangeDirective implements OnChanges {
  private readonly el = inject(ElementRef<HTMLElement>);

  @Input('pulseOnChange') value: unknown;

  ngOnChanges(changes: SimpleChanges): void {
    const change = changes['value'];
    if (!change || change.isFirstChange() || change.previousValue === change.currentValue) {
      return;
    }

    const target = this.el.nativeElement;
    target.classList.remove('value-pulse', 'value-flash');
    void target.offsetWidth; // restart the animation if it's already mid-flight
    target.classList.add('value-pulse', 'value-flash');
    setTimeout(() => target.classList.remove('value-pulse', 'value-flash'), 700);
  }
}
