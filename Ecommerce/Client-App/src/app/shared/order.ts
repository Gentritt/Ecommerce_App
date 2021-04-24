﻿export class OrderItem {
    id!: number;
    quantity!: number;
    unitPrice!: number;
    productId!: number;
    productCategory!: string;
    productSize!: string;
    productTitle!: string;
    productArtist!: string;
    productArtId!: string;
}

export class Order {
    orderId!: number;
    orderDate: Date = new Date();
    orderNumber!: string;
    items: Array<OrderItem> = new Array<OrderItem>();
}1