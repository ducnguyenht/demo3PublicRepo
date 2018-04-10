import 'package:flutter/material.dart';

import '../widgets/product_card.dart';
import '../models/product.dart';

class WidgetCategoryProducts extends StatelessWidget {
  String name;
  List<ProductSummary> products;

  List<Widget> getProductCard() {
    List<Widget> cards = [];
    this.products
        .forEach((product) => cards.add(new WidgetProductCard(product)));
    return cards;
  }

  @override
  Widget build(BuildContext context) {
    return new ListView(
        children: <Widget>[
          new Container(
              child: new Text(this.name,
                  style: new TextStyle(fontWeight: FontWeight.bold)),
              padding: new EdgeInsets.only(left: 5.0)),
          new GridView.count(
              primary: false,
              shrinkWrap: true,
              padding: const EdgeInsets.all(5.0),
              crossAxisSpacing: 5.0,
              crossAxisCount: 2,
              children: getProductCard()
          )
        ]);
  }

  WidgetCategoryProducts(this.name, this.products);
}
