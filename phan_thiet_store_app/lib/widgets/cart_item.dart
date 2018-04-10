import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import '../models/cart.dart';
import '../services/cart_service.dart';

class WidgetCartItem extends StatefulWidget {
  final CartItem cartItem;
  final Function refreshCart;

  @override
  WidgetCartItemState createState() {
    return new WidgetCartItemState(cartItem);
  }

  WidgetCartItem(this.cartItem, this.refreshCart);
}

class WidgetCartItemState extends State<WidgetCartItem> {
  CartItem cartItem;
  final formatter = new NumberFormat("#,###");

  void increaseQuantity() {
    var cartSvc = new MockCartService();
    cartSvc.increaseQuantity(cartItem.id);
    this.setState(() { cartItem: cartItem;});
    this.widget.refreshCart();
  }

  void decreaseQuantity() {
    var cartSvc = new MockCartService();
    cartSvc.decreaseQuantity(cartItem.id);
    this.widget.refreshCart();
  }

  @override
  Widget build(BuildContext context) {
    return new Card(
      child: new Container(
        child: new Row(children: <Widget>[
          new Container(
            child: new Image.asset('images/tai_nghe.png', fit: BoxFit.cover),
            padding: new EdgeInsets.all(5.0),
            height: 64.0,
          ),
          new Expanded(
              child: new Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: <Widget>[
              new Container(
                  child: new Text(widget.cartItem.productName),
                  padding: new EdgeInsets.all(5.0)),
              new Container(
                  child: new Text('${this.formatter.format(widget.cartItem.amount)} đ',
                      style: Theme.of(context).textTheme.caption),
                  padding: new EdgeInsets.all(5.0))
            ],
          )),
          new Row(
            children: <Widget>[
              new IconButton(
                  icon: new Icon(Icons.remove_circle),
                  onPressed: decreaseQuantity),
              new Text('${widget.cartItem.quantity}'),
              new IconButton(
                  icon: new Icon(Icons.add_circle), onPressed: increaseQuantity)
            ],
          ),
        ]),
        padding: new EdgeInsets.all(12.0),
        color: Theme.of(context).cardColor,
      ),
    );
  }

  WidgetCartItemState(this.cartItem);
}
