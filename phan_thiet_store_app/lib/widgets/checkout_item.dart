import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:flutter_advanced_networkimage/flutter_advanced_networkimage.dart';
import 'package:flutter_advanced_networkimage/transition_to_image.dart';

import '../models/cart.dart';

class CheckoutItem extends StatelessWidget {
  final CartItem cartItem;
  final formatter = new NumberFormat("#,###");

  CheckoutItem(this.cartItem);

  @override
  Widget build(BuildContext context) {
    return new Card(
        child: new Column(
          children: <Widget>[
            new Container(
              child: new Row(children: <Widget>[
                new Container(
                  child: this.cartItem.imageUrl != null
                      ? new TransitionToImage(new AdvancedNetworkImage(
                      this.cartItem.imageUrl,
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
                            child: new Text(cartItem.productName),
                            padding: new EdgeInsets.all(5.0)),
                        new Container(
                            child: new Text(
                                '${this.formatter.format(cartItem.amount)} đ',
                                style: Theme.of(context).textTheme.caption),
                            padding: new EdgeInsets.all(5.0))
                      ],
                    )),
                new Text('SL: ${cartItem.quantity}'),
              ]),
              padding: new EdgeInsets.all(4.0),
              color: Theme.of(context).cardColor,
            )
          ],
          crossAxisAlignment: CrossAxisAlignment.stretch,
        ));
  }
}