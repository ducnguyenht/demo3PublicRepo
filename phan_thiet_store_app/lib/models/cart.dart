class Cart {
  num amount;
  List<CartItem> items = new List<CartItem>();
}

class CartItem {
  CartItem(this.id, this.productId, this.quantity, this.amount);

  String id;
  String productId;
  num quantity;
  // Amount = Quantity * Price
  num amount;
}