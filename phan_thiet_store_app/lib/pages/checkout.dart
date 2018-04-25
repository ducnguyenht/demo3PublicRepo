import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import '../models/cart.dart';
import '../services/cart_service.dart';
import '../widgets/cart_item.dart';

class PageCheckOut extends StatefulWidget {
  void checkoutCart() {}

  @override
  PageCheckoutState createState() {
    return new PageCheckoutState();
  }
}

class PageCheckoutState extends State<PageCheckOut> {
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      appBar: new AppBar(title: new Text('Đặt hàng')),
      body: new Text('Trang đặt hàng')
    );
  }
}
