import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:shared_preferences/shared_preferences.dart';

import 'dart:async';

import './checkout_completed.dart';

import '../models/cart.dart';
import '../models/kiot_cart_result.dart';

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
  static const String PREF_ORDER_HISTORY = "PT-APP-ORDER-HISTORY";

  Cart currentCart;
  TextEditingController nameController = new TextEditingController();
  TextEditingController phoneController = new TextEditingController();
  TextEditingController emailController = new TextEditingController();
  TextEditingController addressController = new TextEditingController();

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

    nameController.text = savedName;
    phoneController.text = savedPhone;
    emailController.text = savedEmail;
    addressController.text = savedAddress;
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
      await _saveUserPrefs();
      // Call api here
      var result = await checkOutCart();

      if (!result.isSuccess) {
        showInSnackBar(result.message);
      } else {
        SharedPreferences prefs = await SharedPreferences.getInstance();
        var orderHistory = prefs.getStringList(PREF_ORDER_HISTORY);
        if (orderHistory == null) {
          orderHistory = new List<String>();
        }
        orderHistory.add(result.cartCode);
        await prefs.setStringList(PREF_ORDER_HISTORY, orderHistory);

        Navigator.of(context).pushAndRemoveUntil(new MaterialPageRoute<Null>(
          builder: (BuildContext context) {
            return new PageCheckoutCompleted(result.cartCode);
          },
        ), (Route route) => route.isFirst == true);
      }
    }
  }

  Future<KiotCartResult> checkOutCart() async {
    var cartSvc = new ApiCartService();
    var result =
        await cartSvc.checkOutCurrentCart(nameController.text, phoneController.text, emailController.text, addressController.text);
    return result;
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

  Future _saveUserPrefs() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    await prefs.setString(PREF_NAME, nameController.text);
    await prefs.setString(PREF_PHONE, phoneController.text);
    await prefs.setString(PREF_EMAIL, emailController.text);
    await prefs.setString(PREF_ADDRESS, addressController.text);
  }

  Future<bool> _willPopCallback() async {
    await _saveUserPrefs();
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
                  controller: nameController,
                  onFieldSubmitted: (String value) {
                    nameController.text = value;
                  },
                  onSaved: (String value) {
                    nameController.text = value;
                  },
                ),
                new TextFormField(
                    decoration: const InputDecoration(
                        icon: const Icon(Icons.phone),
                        hintText: 'Số điện thoại để liên lạc với bạn?',
                        labelText: 'Điện thoại *'),
                    validator: _validateNotEmpty,
                    controller: phoneController,
                    keyboardType: TextInputType.phone,
                    onFieldSubmitted: (String value) {
                      phoneController.text = value;
                    },
                    onSaved: (String value) {
                      phoneController.text = value;
                    }),
                new TextFormField(
                    decoration: const InputDecoration(
                        icon: const Icon(Icons.home),
                        hintText: 'Địa chỉ nhận hàng',
                        labelText: 'Địa chỉ *'),
                    keyboardType: TextInputType.text,
                    validator: _validateNotEmpty,
                    controller: addressController,
                    onFieldSubmitted: (String value) {
                      addressController.text = value;
                    },
                    onSaved: (String value) {
                      addressController.text = value;
                    }),
                new TextFormField(
                    decoration: const InputDecoration(
                        icon: const Icon(Icons.mail),
                        hintText: 'Địa chỉ Email nhận thông báo đơn hàng',
                        labelText: 'Email'),
                    controller: emailController,
                    keyboardType: TextInputType.emailAddress,
                    onFieldSubmitted: (String value) {
                      emailController.text = value;
                    },
                    onSaved: (String value) {
                      emailController.text = value;
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
