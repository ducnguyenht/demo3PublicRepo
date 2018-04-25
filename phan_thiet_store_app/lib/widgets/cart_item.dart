import 'dart:async';

import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:flutter_advanced_networkimage/flutter_advanced_networkimage.dart';
import 'package:flutter_advanced_networkimage/transition_to_image.dart';

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
    var cartSvc = new ApiCartService();
    cartSvc.increaseQuantity(cartItem.id);
    this.widget.refreshCart();
  }

  void decreaseQuantity() {
    var cartSvc = new ApiCartService();
    cartSvc.decreaseQuantity(cartItem.id);
    this.widget.refreshCart();
  }

  Future<Null> _askRemoveCartItem() async {
    return showDialog<Null>(
        context: context,
        barrierDismissible: false, // user must tap button!
        child: new AlertDialog(
          title: new Text('Vui lòng xác nhận'),
          content: new SingleChildScrollView(
            child: new ListBody(
              children: <Widget>[
                new Text(
                    "Bạn muốn xóa '${cartItem.productName}' khỏi giỏ hàng?")
              ],
            ),
          ),
          actions: <Widget>[
            new FlatButton(
              child: new Text('Đồng ý',
                  style: new TextStyle(color: new Color(Colors.red.value))),
              onPressed: () async {
                var cartSvc = new ApiCartService();
                await cartSvc.removeItemFromCart(cartItem.id);
                this.widget.refreshCart();
                Navigator.pop(context);
              },
            ),
            new FlatButton(
              child: new Text('Hủy bỏ'),
              onPressed: () {
                Navigator.pop(context);
              },
            ),
          ],
        ));
  }

  @override
  Widget build(BuildContext context) {
    return new Card(
        child: new Column(
      children: <Widget>[
        new Container(
          child: new Row(children: <Widget>[
            new Container(
              child: this.widget.cartItem.imageUrl != null
                  ? new TransitionToImage(new AdvancedNetworkImage(
                      this.widget.cartItem.imageUrl,
                      useDiskCache: true))
                  : new Image.asset('images/image_coming_soon.png',
                      fit: BoxFit.cover),
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
                    child: new Text(
                        '${this.formatter.format(widget.cartItem.amount)} đ',
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
                    icon: new Icon(Icons.add_circle),
                    onPressed: increaseQuantity)
              ],
            ),
          ]),
          padding: new EdgeInsets.all(12.0),
          color: Theme.of(context).cardColor,
        ),
        new Divider(height: 1.0),
        new FlatButton(
          child: new Text('Xóa',
              style: new TextStyle(color: new Color(Colors.red.value))),
          onPressed: _askRemoveCartItem,
          padding: new EdgeInsets.all(12.0),
        ),
      ],
      crossAxisAlignment: CrossAxisAlignment.stretch,
    ));
  }

  WidgetCartItemState(this.cartItem);
}
