import 'package:uuid/uuid.dart';

import '../models/cart.dart';
import '../services/product_service.dart';

abstract class CartService {
  Cart getCurrentCart();
  void addProductToCart(int productId);
  void removeItemFromCart(String cartItemId);
  void increaseQuantity(String cartItemId);
  void decreaseQuantity(String cartItemId);
  void recalculateCurrentCartAmount();
}

class MockCartService implements CartService {
  static Cart currentCart;

  @override
  void addProductToCart(int productId) {
    var uuid = new Uuid();
    var product = MockProductService.products.firstWhere((it) => it.id == productId);

    var id = uuid.v4();

    var cartItem = new CartItem(id, product.id, product.name, 1, product.price);
    var cart = getCurrentCart();
    cart.items.add(cartItem);
    recalculateCurrentCartAmount();
  }

  @override
  Cart getCurrentCart() {
    if (currentCart == null) {
      currentCart = new Cart();
    }
    return currentCart;
  }

  @override
  void removeItemFromCart(String cartItemId) {
    var cartItem = currentCart.items.firstWhere((e) => e.id == cartItemId);
    currentCart.items.remove(cartItem);
    recalculateCurrentCartAmount();
  }

  @override
  void decreaseQuantity(String cartItemId) {
    var cartItem = currentCart.items.firstWhere((e) => e.id == cartItemId);
    if (cartItem.quantity >= 2) {
      cartItem.quantity -= 1;
    }
    recalculateCurrentCartAmount();
  }

  @override
  void increaseQuantity(String cartItemId) {
    var cartItem = currentCart.items.firstWhere((e) => e.id == cartItemId);
    cartItem.quantity += 1;
    recalculateCurrentCartAmount();
  }

  @override
  void recalculateCurrentCartAmount() {
    num cartAmount = 0;
    for (int i = 0; i < currentCart.items.length; i++) {
      var cartItem = currentCart.items.elementAt(i);
      var product = MockProductService.products.firstWhere((it) => it.id == cartItem.productId);
      cartItem.amount = cartItem.quantity * product.price;
      cartAmount += cartItem.amount;
    }
    currentCart.amount = cartAmount;
  }
}

