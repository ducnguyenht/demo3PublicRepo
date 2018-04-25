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
    return new DefaultTabController(
        length: 2,
        child: new Scaffold(
          appBar: new AppBar(
            bottom: new TabBar(
              tabs: [
                new Tab(icon: new Icon(Icons.perm_contact_calendar), text: 'Thông tin người đặt',),
                new Tab(icon: new Icon(Icons.list), text: 'Hàng cần đặt')
              ],
            ),
            title: new Text('Đặt hàng'),
          ),
          body: new TabBarView(
            children: [
              new Icon(Icons.directions_car),
              new Icon(Icons.directions_transit)
            ],
          ),
        ));
  }
}
