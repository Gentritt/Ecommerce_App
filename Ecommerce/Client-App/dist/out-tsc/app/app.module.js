import { __decorate } from "tslib";
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { DataService } from './shared/dataService';
import { Cart } from './shop/cart.component';
import { ProductList } from './shop/ProductList.component';
import { Shop } from './shop/shop.component';
import { Checkout } from './checkout/checkout.component';
let routes = [
    { path: "", component: Shop },
    { path: "/checkout", component: Checkout }
];
let AppModule = class AppModule {
};
AppModule = __decorate([
    NgModule({
        declarations: [
            AppComponent,
            ProductList,
            Cart,
            Shop,
            Checkout
        ],
        imports: [
            BrowserModule,
            HttpClientModule,
            RouterModule.forRoot(routes, {
                useHash: true,
                enableTracing: false //to debug routes
            })
        ],
        providers: [
            DataService
        ],
        bootstrap: [AppComponent]
    })
], AppModule);
export { AppModule };
//# sourceMappingURL=app.module.js.map