import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import '../models/product.dart';
import '../services/cart_service.dart';

class PageProduct extends StatefulWidget {
  final ProductDetail product;
  final formatter = new NumberFormat("#,###");

  PageProduct(this.product);

  @override
  PageProductState createState() {
    return new PageProductState();
  }
}

class PageProductState extends State<PageProduct> {
  void buyNowPressed(BuildContext bContext) {
    var cartSvc = new MockCartService();
    cartSvc.addProductToCart(widget.product.id);
    var snackBar = new SnackBar(
        content: new Text('Đã thêm "${widget.product.name}" vào giỏ hàng.'));
    Scaffold.of(bContext).showSnackBar(snackBar);
  }

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        appBar: new AppBar(title: new Text(this.widget.product.name)),
        body: new Builder(
            // Create an inner BuildContext so that the onPressed methods
            // can refer to the Scaffold with Scaffold.of().
            builder: (BuildContext context) {
          return new Container(
            child: new Column(
              crossAxisAlignment: CrossAxisAlignment.stretch,
              mainAxisSize: MainAxisSize.min,
              children: <Widget>[
                new Center(
                    child: new Image.asset('images/tai_nghe.png',
                        fit: BoxFit.cover)),
                new Text(this.widget.product.name),
                new Text(
                    '${widget.formatter.format(this.widget.product.price)} đ'),
                new Text('Thông số sản phẩm',
                    style: new TextStyle(fontWeight: FontWeight.bold)),
                new Expanded(
                    child: new Text(this.widget.product.description,
                        softWrap: true)),
                new Container(
                    padding: new EdgeInsets.all(10.0),
                    child: new FlatButton.icon(
                        onPressed: () => buyNowPressed(context),
                        icon: new Icon(Icons.add_shopping_cart),
                        label: new Text('Mua ngay'),
                        color: new Color.fromARGB(255, 244, 0, 0),
                        textColor: new Color.fromARGB(255, 255, 255, 255),
                        highlightColor: new Color.fromARGB(255, 200, 0, 0))),
              ],
            ),
            padding: new EdgeInsets.all(10.0),
          );
        }));
  }
}
