import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:shared_preferences/shared_preferences.dart';

import 'dart:async';

import './checkout_completed.dart';

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
  static const String PREF_NAME = "PT-APP-USER-NAME";
  static const String PREF_PHONE = "PT-APP-USER-PHONE";
  static const String PREF_EMAIL = "PT-APP-USER-EMAIL";
  static const String PREF_ADDRESS = "PT-APP-USER-ADDRESS";

  Cart currentCart;
  String name;
  String phone;
  String email;
  String address;

  final formatter = new NumberFormat("#,###");

  @override
  void initState() {
    super.initState();
    getUserPref();
  }

  void getUserPref() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    var savedName = prefs.getString(PREF_NAME) ?? "";
    var savedPhone = prefs.getString(PREF_PHONE) ?? "";
    var savedEmail = prefs.getString(PREF_EMAIL) ?? "";
    var savedAddress = prefs.getString(PREF_ADDRESS) ?? "";

    setState(() {
      this.name = savedName;
      this.phone = savedPhone;
      this.email = savedEmail;
      this.address = savedAddress;
    });
  }

  PageCheckoutState(this.currentCart);

  final GlobalKey<ScaffoldState> _scaffoldKey = new GlobalKey<ScaffoldState>();

  void showInSnackBar(String value) {
    _scaffoldKey.currentState
        .showSnackBar(new SnackBar(content: new Text(value)));
  }

  final GlobalKey<FormState> _formKey = new GlobalKey<FormState>();
  bool _autoValidate = false;

  void _handleSubmitted() async {
    final FormState form = _formKey.currentState;
    if (!form.validate()) {
      _autoValidate = true; // Start validating on every change.
      showInSnackBar('Vui lòng điền đủ thông tin trước khi đặt hàng.');
    } else {
      form.save();
      // Call api here
      var apiCartCode = await checkOutCart();

      if (apiCartCode == "") {
        showInSnackBar('Không thể tạo đơn đặt hàng.');
      } else {
        Navigator.of(context).push(new MaterialPageRoute<Null>(
          builder: (BuildContext context) {
            return new PageCheckoutCompleted(apiCartCode);
          },
        ));
      }
    }
  }

  Future<String> checkOutCart() async {
    var cartSvc = new ApiCartService();
    var cartCode =
        await cartSvc.checkOutCurrentCart(name, phone, email, address);
    return cartCode;
  }

  String _validateName(String value) {
    if (value.isEmpty) return 'Không được để trống tên.';
    final RegExp nameExp = new RegExp(r'^[A-Za-z ]+$');
    if (!nameExp.hasMatch(value))
      return 'Please enter only alphabetical characters.';
    return null;
  }

  String _validateNotEmpty(String value) {
    if (value.isEmpty) return 'Không được bỏ trống trường này';
    return null;
  }

  List<Widget> getCartItemsWidgets() {
    var ret = new List<Widget>();
    if (currentCart != null) {
      if (currentCart.items.length > 0) {
        currentCart.items.forEach((it) => ret.add(new CheckoutItem(it)));
      } else {
        ret.add(new Text('Giỏ hàng hiện đang trống'));
      }
    }
    return ret;
  }

  Future<bool> _willPopCallback() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    await prefs.setString(PREF_NAME, this.name);
    await prefs.setString(PREF_PHONE, this.phone);
    await prefs.setString(PREF_EMAIL, this.email);
    await prefs.setString(PREF_ADDRESS, this.address);
    return true;
  }

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        key: _scaffoldKey,
        appBar: new AppBar(
          title: new Text('Đặt hàng'),
        ),
        body: new Form(
            key: _formKey,
            autovalidate: _autoValidate,
            onWillPop: _willPopCallback,
            child: new ListView(children: <Widget>[
              new CheckoutBox("Thông tin giao hàng", <Widget>[
                new TextFormField(
                  decoration: const InputDecoration(
                    icon: const Icon(Icons.person),
                    hintText: 'Tên bạn là gì?',
                    labelText: 'Tên *',
                  ),
                  validator: _validateName,
                  initialValue: this.name,
                  onFieldSubmitted: (String value) {
                    this.name = value;
                  },
                  onSaved: (String value) {
                    this.name = value;
                  },
                ),
                new TextFormField(
                    decoration: const InputDecoration(
                        icon: const Icon(Icons.phone),
                        hintText: 'Số điện thoại để liên lạc với bạn?',
                        labelText: 'Điện thoại *'),
                    validator: _validateNotEmpty,
                    initialValue: this.phone,
                    keyboardType: TextInputType.phone,
                    onFieldSubmitted: (String value) {
                      this.phone = value;
                    },
                    onSaved: (String value) {
                      this.phone = value;
                    }),
                new TextFormField(
                    decoration: const InputDecoration(
                        icon: const Icon(Icons.mail),
                        hintText: 'Địa chỉ Email nhận thông báo đơn hàng',
                        labelText: 'Email'),
                    initialValue: this.email,
                    keyboardType: TextInputType.emailAddress,
                    onFieldSubmitted: (String value) {
                      this.email = value;
                    },
                    onSaved: (String value) {
                      this.email = value;
                    }),
                new TextFormField(
                    decoration: const InputDecoration(
                        icon: const Icon(Icons.home),
                        hintText: 'Địa chỉ nhận hàng',
                        labelText: 'Địa chỉ'),
                    keyboardType: TextInputType.text,
                    initialValue: this.address,
                    onFieldSubmitted: (String value) {
                      this.address = value;
                    },
                    onSaved: (String value) {
                      this.address = value;
                    }),
              ]),
              new CheckoutBox("Hàng hóa", getCartItemsWidgets()),
              new CheckoutBox("Tổng cộng", <Widget>[
                new Text('${this.formatter.format(currentCart.amount)} đ',
                    style: Theme.of(context).textTheme.title)
              ]),
              new RaisedButton(
                  color: Theme.of(context).accentColor,
                  textTheme: ButtonTextTheme.primary,
                  child: new Text("Hoàn tất đặt hàng"),
                  onPressed: _handleSubmitted)
            ], padding: new EdgeInsets.all(16.0))));
  }
}
