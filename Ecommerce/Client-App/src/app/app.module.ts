import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { DataService } from './shared/dataService';
import { Cart } from './shop/cart.component';
import { ProductList } from './shop/ProductList.component';

@NgModule({
  declarations: [
        AppComponent,
        ProductList,
        Cart
  ],
  imports: [
      BrowserModule,
      HttpClientModule
  ],
    providers: [
        DataService
],
  bootstrap: [AppComponent]
})
export class AppModule { }
