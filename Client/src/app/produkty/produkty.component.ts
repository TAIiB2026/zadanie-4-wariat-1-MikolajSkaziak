import { Component, inject } from '@angular/core';
import { GET_DATA_TOKEN } from '../tokens/get-data.token';
import { FORM_SUBMIT_TOKEN } from '../tokens/form-submit.token';
import { BehaviorSubject, combineLatest, switchMap } from 'rxjs';

@Component({
  selector: 'taiib2-produkty',
  standalone: false,
  templateUrl: './produkty.component.html',
  styles: ``
})
export class ProduktyComponent {
  private readonly service = inject(GET_DATA_TOKEN);
  private readonly SubmitService = inject(FORM_SUBMIT_TOKEN);

  public filtr$ = new BehaviorSubject<string>('');
  public page$ = new BehaviorSubject<number>(1);
  public pageSize$ = new BehaviorSubject<number>(5);

  public data$ = combineLatest([this.filtr$, this.page$, this.pageSize$]).pipe(
    switchMap(([filtr, page, pageSize]) => this.service.Get(filtr, page, pageSize))
  );

  onDelete(id: number): void {
    this.SubmitService.Delete(id).subscribe((success: boolean) => {
      if (success) {
        this.refresh();
      } else {
        alert('Nie udało się usunąć produktu');
      }
    });
  }

  onSearch(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    this.filtr$.next(value);
    this.page$.next(1);
  }

  changePage(delta: number): void {
    const current = this.page$.value;
    if (current + delta > 0) {
      this.page$.next(current + delta);
    }
  }

  refresh(): void {
    this.filtr$.next(this.filtr$.value);
  }
}
