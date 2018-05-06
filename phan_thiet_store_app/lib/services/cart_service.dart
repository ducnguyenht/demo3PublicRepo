import 'dart:async';
import 'package:uuid/uuid.dart';
import 'package:http/http.dart' as http;

import '../models/kiot_cart.dart';
import '../models/cart.dart';
import '../models/config.dart';

import '../services/product_service.dart';

abstract class CartService {
  Cart getCurrentCart();
  void addProductToCart(int productId);
  void removeItemFromCart(String cartItemId);
  void increaseQuantity(String cartItemId);
  void decreaseQuantity(String cartItemId);
  void recalculateCurrentCartAmount();
}

abstract class AsyncCartService {
  Cart getCurrentCart();
  Future addProductToCart(int productId);
  void removeItemFromCart(String cartItemId);
  void increaseQuantity(String cartItemId);
  void decreaseQuantity(String cartItemId);
  Future recalculateCurrentCartAmount();
  Future<String> checkOutCurrentCart(String name, String phone, String email, String address);
}

class ApiCartService implements AsyncCartService {
  static Cart currentCart;

  @override
  Future addProductToCart(int productId) async {
    var uuid = new Uuid();
    var productSvc = new ApiProductService();

    var product = await productSvc.getProductById(productId);

    var id = uuid.v4();

    var cartItem = new CartItem(id, product.id, product.name, product.imageUrl, 1, product.price);
    var cart = getCurrentCart();
    cart.items.add(cartItem);
  }

  @override
  void decreaseQuantity(String cartItemId) {
    var cartItem = currentCart.items.firstWhere((e) => e.id == cartItemId);
    if (cartItem.quantity >= 2) {
      cartItem.quantity -= 1;
    }
  }

  @override
  void increaseQuantity(String cartItemId) {
    var cartItem = currentCart.items.firstWhere((e) => e.id == cartItemId);
    cartItem.quantity += 1;
  }

  @override
  Cart getCurrentCart() {
    if (currentCart == null) {
      currentCart = new Cart();
    }
    return currentCart;
  }

  @override
  Future recalculateCurrentCartAmount() async {
    num cartAmount = 0;
    var productSvc = new ApiProductService();
    for (int i = 0; i < currentCart.items.length; i++) {
      var cartItem = currentCart.items.elementAt(i);
      var product = await productSvc.getProductById(cartItem.productId);
      cartItem.amount = cartItem.quantity * product.price;
      cartAmount += cartItem.amount;
    }
    currentCart.amount = cartAmount;
  }

  @override
  Future removeItemFromCart(String cartItemId) async {
    var cartItem = currentCart.items.firstWhere((e) => e.id == cartItemId);
    currentCart.items.remove(cartItem);
  }

  @override
  Future<String> checkOutCurrentCart(String name, String phone, String email, String address) async {
    var config = new Config.getConfig();
    if (config.isSupportCheckout && currentCart != null) {
      var kiotCart = new KiotCart.fromCart(name, phone, email, address, currentCart);
      var kiotCartJson = kiotCart.toJson();

      var config = new Config.getConfig();

      var response = await http.post(
          config.checkOutPath,
          body: kiotCartJson,
          headers: {"Content-Type": "application/json"});

      var cartCode = response.body;
      return cartCode;
    }
    return "";
  }
}

class MockCartService implements CartService {
  static Cart currentCart;

  @override
  void addProductToCart(int productId) {
    var uuid = new Uuid();
    var product = MockProductService.products.firstWhere((it) => it.id == productId);

    var id = uuid.v4();

    var cartItem = new CartItem(id, product.id, product.name, product.imageUrl, 1, product.price);
    var cart = getCurrentCart();
    cart.items.add(cartItem);
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
  }

  @override
  void decreaseQuantity(String cartItemId) {
    var cartItem = currentCart.items.firstWhere((e) => e.id == cartItemId);
    if (cartItem.quantity >= 2) {
      cartItem.quantity -= 1;
    }
  }

  @override
  void increaseQuantity(String cartItemId) {
    var cartItem = currentCart.items.firstWhere((e) => e.id == cartItemId);
    cartItem.quantity += 1;
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

