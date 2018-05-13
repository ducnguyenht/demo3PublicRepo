import 'package:flutter/material.dart';

import './home.dart';

class PageCheckoutCompleted extends StatelessWidget {
  final String cartCode;

  PageCheckoutCompleted(this.cartCode);

  backToHomePage(BuildContext context) {
    Navigator.of(context).pop();
  }

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        appBar: new AppBar(title: new Text("Hoàn tất đặt hàng")),
        body: new Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: <Widget>[
              new Text("Đã hoàn tất đặt hàng với mã: $cartCode"),
              new RaisedButton(
                  child: new Text("Trở về trang chủ"),
                  onPressed: () => backToHomePage(context))
            ]));
  }
}
