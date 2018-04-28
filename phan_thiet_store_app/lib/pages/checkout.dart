import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import '../models/cart.dart';

import '../services/cart_service.dart';

import '../widgets/checkout_box.dart';
import '../widgets/checkout_item.dart';

class PageCheckOut extends StatefulWidget {
  final Cart currentCart;

  PageCheckOut(this.currentCart);

  @override
  PageCheckoutState createState() {
    return new PageCheckoutState(currentCart);
  }
}

class PageCheckoutState extends State<PageCheckOut> {
  Cart currentCart;
  final formatter = new NumberFormat("#,###");

  PageCheckoutState(this.currentCart);

  final GlobalKey<ScaffoldState> _scaffoldKey = new GlobalKey<ScaffoldState>();

  void showInSnackBar(String value) {
    _scaffoldKey.currentState
        .showSnackBar(new SnackBar(content: new Text(value)));
  }

  final GlobalKey<FormState> _formKey = new GlobalKey<FormState>();
  bool _autoValidate = false;

  void _handleSubmitted() {
    final FormState form = _formKey.currentState;
    if (!form.validate()) {
      _autoValidate = true; // Start validating on every change.
      showInSnackBar('Please fix the errors in red before submitting.');
    } else {
      form.save();
    }
  }

  String _validateName(String value) {
    if (value.isEmpty) return 'Name is required.';
    final RegExp nameExp = new RegExp(r'^[A-Za-z ]+$');
    if (!nameExp.hasMatch(value))
      return 'Please enter only alphabetical characters.';
    return null;
  }

  List<Widget> getCartItemsWidgets() {
    var ret = new List<Widget>();
    if (currentCart != null) {
      if (currentCart.items.length > 0) {
        currentCart.items
            .forEach((it) => ret.add(new CheckoutItem(it)));
      } else {
        ret.add(new Text('Giỏ hàng hiện đang trống'));
      }
    }
    return ret;
  }

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        appBar: new AppBar(
          title: new Text('Đặt hàng'),
        ),
        body: new Form(
            key: _formKey,
            autovalidate: _autoValidate,
            child: new ListView(children: <Widget>[
              new CheckoutBox("Thông tin giao hàng", <Widget>[
                new TextFormField(
                  decoration: const InputDecoration(
                    icon: const Icon(Icons.person),
                    hintText: 'Tên bạn là gì?',
                    labelText: 'Tên *',
                  ),
                  onSaved: (String value) {},
                  validator: _validateName,
                ),
                new TextFormField(
                    decoration: const InputDecoration(
                        icon: const Icon(Icons.phone),
                        hintText: 'Số điện thoại để liên lạc với bạn?',
                        labelText: 'Điện thoại *'),
                    keyboardType: TextInputType.phone,
                    onSaved: (String value) {})
              ]),
              new CheckoutBox("Hàng hóa", getCartItemsWidgets()),
              new CheckoutBox("Tổng cộng", <Widget>[
                new Text(
                    '${this.formatter.format(currentCart.amount)} đ',
                    style:
                    Theme.of(context).textTheme.title)
              ]),
              new RaisedButton(
                  child: new Text("Đặt hàng"), onPressed: _handleSubmitted)
            ], padding: new EdgeInsets.all(16.0))));
  }
}
