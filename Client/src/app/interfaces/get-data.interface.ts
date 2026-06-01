import { Observable } from "rxjs";
import { ProduktClass } from "../classes/produkt.class";

export interface GetDataInterface {
    Get(filtr?: string, page?: number, pageSize?: number): Observable<ProduktClass[]>;
    GetByID(id: number): Observable<ProduktClass>;
    
}