import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:flutter_advanced_networkimage/flutter_advanced_networkimage.dart';
import 'package:flutter_advanced_networkimage/transition_to_image.dart';

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
  void goToProductPage(int productId, String productName) {
    Navigator.of(context).push(new MaterialPageRoute<Null>(
      builder: (BuildContext context) {
        return new PageProduct(productId, productName);
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
                  child: this.widget.product.imageUrl != null
                      ? new TransitionToImage(new AdvancedNetworkImage(
                          this.widget.product.imageUrl,
                          useDiskCache: true))
                      : new Image.asset('images/image_coming_soon.png',
                          fit: BoxFit.cover),
                ),
                fit: FlexFit.tight,
                flex: 3),
            new Expanded(child: (new Text(this.widget.product.name))),
            new Text('${this.widget.formatter.format(
                    this.widget.product.price)} đ'),
          ],
        )),
        onTapUp: (tapDetail) => goToProductPage(this.widget.product.id, this.widget.product.name));
  }
}
