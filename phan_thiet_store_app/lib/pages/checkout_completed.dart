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
        body:
        new Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisSize: MainAxisSize.max,
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Icon(
              Icons.sentiment_satisfied,
              size: 96.0,
              color: Colors.grey,
            ),
            new Container(height: 16.0),
            new Text('Đã hoàn tất đặt hàng với mã: $cartCode',
                style: Theme.of(context).textTheme.display1, textAlign: TextAlign.center),
            new Container(height: 16.0),
            new RaisedButton(
                color: Theme.of(context).accentColor,
                textTheme: ButtonTextTheme.primary,
                child: new Text("Trở về trang chủ"),
                onPressed: () => backToHomePage(context))
          ],
        )
    );
  }
}
