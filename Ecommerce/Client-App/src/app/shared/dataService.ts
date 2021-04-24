import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
@Injectable()
export class DataService {
	constructor(private http: HttpClient) {

	}
	public Products = [];
	loadProducts() {
		return this.http.get("/api/products")
			.pipe(map((data:any) => {
				this.Products = data;
				return true;
			}));
		
	}
} 