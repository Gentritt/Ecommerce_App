import { __decorate } from "tslib";
import { Component } from "@angular/core";
let ProductList = class ProductList {
    constructor() {
        this.Products = [
            {
                title: "First Product",
                price: 19.99
            },
            {
                title: "Second Product",
                price: 19.99
            },
            {
                title: "Third Product",
                price: 19.99
            },
        ];
    }
};
ProductList = __decorate([
    Component({
        selector: "product-List",
        templateUrl: "ProductList.Html",
        styleUrls: []
    })
], ProductList);
export { ProductList };
//# sourceMappingURL=ProductList.js.map