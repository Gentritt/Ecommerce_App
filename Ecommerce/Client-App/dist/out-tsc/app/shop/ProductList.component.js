import { __decorate } from "tslib";
import { Component } from "@angular/core";
let ProductList = class ProductList {
    constructor(data) {
        this.data = data;
        this.Products = [];
        this.Products = data.Products;
    }
};
ProductList = __decorate([
    Component({
        selector: "product-list",
        templateUrl: "./ProductList.component.html",
        styleUrls: []
    })
], ProductList);
export { ProductList };
//# sourceMappingURL=ProductList.component.js.map