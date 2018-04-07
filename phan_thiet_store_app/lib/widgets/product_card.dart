import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import '../services/product_service.dart';
import '../pages/product.dart';
import '../models/product.dart';

class WidgetProductCard extends StatefulWidget {

  final ProductSummary product;
  final formatter = new NumberFormat("#,###");

  @override
  WidgetProductCardState createState() {
    return new WidgetProductCardState();
  }

  WidgetProductCard(this.product);
}

class WidgetProductCardState extends State<WidgetProductCard> {
  void goToProductPage(String productId) {
    var productSvc = new MockProductService();
    var product = productSvc.getProductById(productId);

    Navigator.of(context).push(new MaterialPageRoute<Null>(
      builder: (BuildContext context) {
        return new PageProduct(product);
      },
    ));
  }

  @override
  Widget build(BuildContext context) {
    return new GestureDetector(
        child: new Card(
            child: new Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisSize: MainAxisSize.min,
          children: <Widget>[
            new Flexible(
                child: new Center(
                    child: new Image.asset('images/tai_nghe.png',
                        fit: BoxFit.cover)),
                fit: FlexFit.tight,
                flex: 3),
            new Expanded(child: (new Text(this.widget.product.name))),
            new Text('${this.widget.formatter.format(this.widget.product.price)} đ'),
          ],
        )),
        onTapUp: (tapDetail) => goToProductPage(this.widget.product.id));
  }
}
