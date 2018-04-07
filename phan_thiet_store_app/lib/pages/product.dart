import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import '../models/product.dart';

class PageProduct extends StatelessWidget {
  void buyNowPressed() {
  }

  final ProductDetail product;
  final formatter = new NumberFormat("#,###");

  PageProduct(this.product);

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      appBar: new AppBar(title: new Text(this.product.name)),
      body: new Container(
        child: new Column(
          children: <Widget>[
            new Center(
                child:
                    new Image.asset('images/tai_nghe.png', fit: BoxFit.cover)),
            new Text(this.product.name),
            new Text('${formatter.format(this.product.price)} đ'),
            new Align(
                child: new Text('Thông số sản phẩm',
                    style: new TextStyle(fontWeight: FontWeight.bold)),
                alignment: Alignment.centerLeft),
            new Align(
                child: new Column(
                  children: <Widget>[
                    new Text(this.product.description, softWrap: true)
                  ],
                  crossAxisAlignment: CrossAxisAlignment.start,
                ),
                alignment: Alignment.centerLeft),
          ],
        ),
        padding: new EdgeInsets.all(10.0),
      ),
      bottomNavigationBar: new Container(
        padding: new EdgeInsets.all(10.0),
          child: new FlatButton.icon(
        onPressed: buyNowPressed,
        icon: new Icon(Icons.add_shopping_cart),
        label: new Text('Mua ngay'),
        color: new Color.fromARGB(255, 244, 0, 0),
        textColor: new Color.fromARGB(255, 255, 255, 255),
        highlightColor: new Color.fromARGB(255, 200, 0, 0)
      )),
    );
  }
}
