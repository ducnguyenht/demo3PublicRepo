class Cart {
  num amount;
  List<CartItem> items = new List<CartItem>();
}

class CartItem {
  CartItem(this.id, this.productId, this.productName, this.quantity, this.amount);

  String id;
  int productId;
  String productName;
  num quantity;
  // Amount = Quantity * Price
  num amount;
}