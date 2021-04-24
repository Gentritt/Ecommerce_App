import { Component, OnInit } from "@angular/core";
import { DataService } from "../shared/dataService";

@Component({
	selector: "product-list",
	templateUrl: "./ProductList.component.html",
	styleUrls: []
})
export class ProductList implements OnInit {
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

	public Products = [] as any;
}