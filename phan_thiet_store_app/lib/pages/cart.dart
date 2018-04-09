import 'package:flutter/material.dart';

import '../models/cart.dart';
import '../services/cart_service.dart';
import '../widgets/cart_item.dart';

class PageCart extends StatefulWidget {
  void checkoutCart() {

  }

  @override
  PageCartState createState() {
    return new PageCartState();
  }
}

class PageCartState extends State<PageCart> {
  Cart currentCart;
  
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        appBar: new AppBar(title: new Text('Giỏ hàng')),
        body: new Column(
          children: <Widget>[
            new Expanded(
                child: new ListView(children: <Widget>[
              new WidgetCartItem(),
              new WidgetCartItem(),
              new WidgetCartItem(),
              new WidgetCartItem(),
              new WidgetCartItem(),
              new WidgetCartItem(),
              new WidgetCartItem()
            ], padding: new EdgeInsets.all(8.0))),
            new Card(
                child: new Container(
                    child: new Row(
                      children: <Widget>[
                        new Expanded(
                            child: new Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: <Widget>[
                            new Container(
                                child: new Text('Tổng cộng',
                                    style: Theme.of(context).textTheme.caption),
                                padding: new EdgeInsets.all(4.0)),
                            new Container(
                                child: new Text('đ 800.000',
                                    style: Theme.of(context).textTheme.title),
                                padding: new EdgeInsets.all(4.0))
                          ],
                        )),
                        new RaisedButton(
                            color: Theme.of(context).accentColor,
                            textTheme: ButtonTextTheme.primary,
                            child: new Text('Đặt hàng'), onPressed: widget.checkoutCart
                        )
                      ],
                    ),
                    padding: new EdgeInsets.all(16.0)))
          ],
        ));
  }

  @override
  void initState() {
    var cartSvc = new MockCartService();
    var cart = cartSvc.getCurrentCart();
    setState(() { currentCart = cart; });
  }
}
