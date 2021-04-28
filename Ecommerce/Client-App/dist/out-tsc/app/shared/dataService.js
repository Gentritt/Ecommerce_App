import { __decorate } from "tslib";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
import * as orders from "./order";
let DataService = class DataService {
    constructor(http) {
        this.http = http;
        this.order = new orders.Order();
        this.Products = [];
    }
    loadProducts() {
        return this.http.get("/api/products")
            .pipe(map((data) => {
            this.Products = data;
            return true;
        }));
    }
    AddToOrder(product) {
        let item = this.order.items.find(i => i.productId == product.id);
        if (item) {
            item.quantity++;
        }
        else {
            item = new orders.OrderItem();
            item.productId = product.id;
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
};
DataService = __decorate([
    Injectable()
], DataService);
export { DataService };
//# sourceMappingURL=dataService.js.map