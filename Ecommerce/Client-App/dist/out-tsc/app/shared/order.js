export class OrderItem {
}
export class Order {
    constructor() {
        this.orderDate = new Date();
        this.items = new Array();
        //get subtotal(): number {
        //    return _.sum(_.map(this.items, i => i.unitPrice * i.quantity));
        //};
    }
}
//# sourceMappingURL=order.js.map