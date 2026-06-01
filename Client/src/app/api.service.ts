import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetDataInterface } from './interfaces/get-data.interface';
import { ProduktClass } from './classes/produkt.class';
import {FormSubmitInterface} from './interfaces/form-submit.interface';
@Injectable()
export class ApiService implements GetDataInterface, FormSubmitInterface {

  private readonly apiUrl = 'http://localhost:5015/api/Produkty';

  constructor(private http: HttpClient) { }

  Get(filtr?: string, page?: number, pageSize?: number): Observable<ProduktClass[]> {
    let params = new HttpParams();
    if (filtr) {
      params = params.set('filtr', filtr);
    }
    if (page !== undefined) {
      params = params.set('page', page.toString());
    }
    if (pageSize !== undefined) {
      params = params.set('pageSize', pageSize.toString());
    }
    return this.http.get<ProduktClass[]>(this.apiUrl, { params });
  }

  GetByID(id: number): Observable<ProduktClass> {
    return this.http.get<ProduktClass>(`${this.apiUrl}/${id}`);
  }
  Post(nazwa: string, cena: number, dataWaznosci: Date): Observable<boolean> {
    return this.http.post<boolean>(this.apiUrl, {nazwa, cena, dataWaznosci});
  }
  Put(id: number, nazwa: string, cena: number, dataWaznosci: Date): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}/${id}`, {nazwa, cena, dataWaznosci});
  }
  Delete(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${id}`);
  }
}
