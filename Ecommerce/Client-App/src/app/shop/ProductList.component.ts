import { Component, OnInit } from "@angular/core";
import { DataService } from "../shared/dataService";
import { Product } from "../shared/Product";
@Component({
	selector: "product-list",
	templateUrl: "./ProductList.component.html",
	styleUrls: ["ProductList.css"]
})
export class ProductList implements OnInit {

	public Products : Product[] = [];

	constructor(private data: DataService) {	
	}


	ngOnInit(): void {
		this.data.loadProducts()
			.subscribe(success => {
				if (success) {
					this.Products = this.data.Products;
				}
			})
	}

	addProduct(product: Product) {
		this.data.AddToOrder(product);
	}

	
}