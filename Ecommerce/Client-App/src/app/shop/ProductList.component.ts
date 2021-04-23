import { Component } from "@angular/core";
import { DataService } from "../shared/dataService";

@Component({
	selector: "product-list",
	templateUrl: "./ProductList.component.html",
	styleUrls: []
})
export class ProductList {
	constructor(private data: DataService) {
		this.Products = data.Products;
	}
	public Products = [] as any;
}