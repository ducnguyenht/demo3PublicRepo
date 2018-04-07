import 'package:flutter/material.dart';

import './product_card.dart';
import '../pages/category.dart';
import '../view_models/home_popular_products.dart';

class WidgetHomeCategory extends StatefulWidget {
  WidgetHomeCategory(this.productBlock);

  final HomePopularProductsBlock productBlock;

  @override
  WidgetHomeCategoryState createState() {
    return new WidgetHomeCategoryState();
  }
}

class WidgetHomeCategoryState extends State<WidgetHomeCategory> {
  void navigateToCategoryPage(String categoryId) {
    Navigator.of(context).push(new MaterialPageRoute<Null>(
      builder: (BuildContext context) {
        return new PageCategory();
      },
    ));
  }

  List<Widget> getProductCard() {
    List<Widget> cards = [];
    this
        .widget
        .productBlock
        .products
        .forEach((product) => cards.add(new WidgetProductCard(product)));
    return cards;
  }

  @override
  Widget build(BuildContext context) {
    return new Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisSize: MainAxisSize.min,
        children: <Widget>[
          new Row(
            children: <Widget>[
              new Container(
                  child: new Text(this.widget.productBlock.name,
                      style: new TextStyle(fontWeight: FontWeight.bold)),
                  padding: new EdgeInsets.only(left: 5.0)),
              new Expanded(
                  child: new Align(
                      child: new FlatButton(
                          onPressed: () => navigateToCategoryPage(
                              this.widget.productBlock.categoryId),
                          child: new Text('Xem tất cả',
                              style: new TextStyle(color: Colors.orange)),
                          padding: new EdgeInsets.only(right: 5.0)),
                      alignment: Alignment.centerRight))
            ],
          ),
          new GridView.count(
              primary: false,
              shrinkWrap: true,
              padding: const EdgeInsets.all(5.0),
              crossAxisSpacing: 5.0,
              crossAxisCount: 2,
              children: getProductCard()),
        ]);
  }
}
