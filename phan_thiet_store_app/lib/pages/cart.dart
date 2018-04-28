import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import './checkout.dart';

import '../models/cart.dart';
import '../services/cart_service.dart';
import '../widgets/cart_item.dart';

class PageCart extends StatefulWidget {
  @override
  PageCartState createState() {
    return new PageCartState();
  }
}

class PageCartState extends State<PageCart> {
  Cart currentCart;
  final formatter = new NumberFormat("#,###");
  bool isLoading = false;

  void checkoutCart() {
    Navigator.of(context).push(new MaterialPageRoute<Null>(
      builder: (BuildContext context) {
        return new PageCheckOut(currentCart);
      },
    ));
  }

  @override
  void initState() {
    super.initState();
    refreshCart();
  }

  void refreshCart() async {
    setState(() { isLoading = true; });
    var cartSvc = new ApiCartService();
    await cartSvc.recalculateCurrentCartAmount();
    var cart = cartSvc.getCurrentCart();
    setState(() {
      currentCart = cart;
      isLoading = false;
    });
  }

  List<Widget> getCartItemsWidgets() {
    var ret = new List<Widget>();
    if (currentCart != null) {
      if (currentCart.items.length > 0) {
        currentCart.items
            .forEach((it) => ret.add(new WidgetCartItem(it, refreshCart)));
      } else {
        ret.add(new Text('Giỏ hàng hiện đang trống'));
      }
    }
    return ret;
  }

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        appBar: new AppBar(title: new Text('Giỏ hàng')),
        body: new Column(
          children: <Widget>[
            new Expanded(
                child: (currentCart != null && isLoading == false)
                    ? new ListView(
                        children: getCartItemsWidgets(),
                        padding: new EdgeInsets.all(8.0))
                    : new Center(child: new CircularProgressIndicator())),
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
                                child: currentCart != null &&
                                        currentCart.amount != null
                                    ? new Text(
                                        '${this.formatter.format(currentCart.amount)}  đ',
                                        style:
                                            Theme.of(context).textTheme.title)
                                    : new Text('0 đ',
                                        style:
                                            Theme.of(context).textTheme.title),
                                padding: new EdgeInsets.all(4.0))
                          ],
                        )),
                        new RaisedButton(
                            color: Theme.of(context).accentColor,
                            textTheme: ButtonTextTheme.primary,
                            child: new Text('Đặt hàng'),
                            onPressed: checkoutCart)
                      ],
                    ),
                    padding: new EdgeInsets.all(16.0)))
          ],
        ));
  }
}
