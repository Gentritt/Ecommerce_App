import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
import { Observable } from "rxjs";
import { Product } from "./Product";
import * as orders from "./order";
@Injectable()
export class DataService {
	constructor(private http: HttpClient) {

	}
	public order: orders.Order = new orders.Order();
	public Products: Product[] = [];
	loadProducts(): Observable<Boolean> {
		return this.http.get("/api/products")
			.pipe(map((data: any) => {
				this.Products = data;
				return true;

			}))
	}

	public AddToOrder(product: Product) {


		let item: orders.OrderItem = this.order.items.find(i => i.productId == product.id)!;
	

		if (item) {
			item.quantity++;
		}
		else {

			item = new orders.OrderItem();
			item.productId= product.id;
			item.productArtist = product.artist;
			item.productCategory = product.category;
			item.productArtId = product.artId;
			item.productTitle = product.title;
			item.productSize = product.size;
			item.unitPrice = product.price;
			item.quantity = 1;

			this.order.items.push(item);
		}

	}
} 