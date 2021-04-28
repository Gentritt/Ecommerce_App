import { __decorate } from "tslib";
import { Component } from "@angular/core";
let ProductList = class ProductList {
    constructor(data) {
        this.data = data;
        this.Products = [];
    }
    ngOnInit() {
        this.data.loadProducts()
            .subscribe(success => {
            if (success) {
                this.Products = this.data.Products;
            }
        });
    }
    addProduct(product) {
        this.data.AddToOrder(product);
    }
};
ProductList = __decorate([
    Component({
        selector: "product-list",
        templateUrl: "./ProductList.component.html",
        styleUrls: ["ProductList.css"]
    })
], ProductList);
export { ProductList };
//# sourceMappingURL=ProductList.component.js.map