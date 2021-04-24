import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
import { Observable } from "rxjs";
import { Product } from "./Product";
@Injectable()
export class DataService {
	constructor(private http: HttpClient) {

	}
	public Products: Product[] = [];
	loadProducts(): Observable<Boolean> {
		return this.http.get("/api/products")
			.pipe(map((data: any) => {
				this.Products = data;
				return true;

			}))
	}
} 