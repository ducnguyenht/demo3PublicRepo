import 'package:flutter/material.dart';

import './product_card.dart';
import '../services/product_service.dart';
import '../pages/category.dart';
import '../pages/home.dart';
import '../view_models/home_popular_products.dart';

class WidgetHomeCategory extends StatefulWidget {
  WidgetHomeCategory(this.productBlock, this.homeState);

  final HomePopularProductsBlock productBlock;
  final PageHomeState homeState;

  @override
  WidgetHomeCategoryState createState() {
    return new WidgetHomeCategoryState();
  }
}

class WidgetHomeCategoryState extends State<WidgetHomeCategory> {
  void navigateToCategoryPage(String categoryId, String categoryName) {
    var productService = new MockProductService();
    var products = productService.getProductsByCategoryId(categoryId);

    Navigator.push(
        context,
        new MaterialPageRoute(
            builder: (context) => new PageCategory(categoryName, products)));
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
                          onPressed: () => this
                              .widget
                              .homeState
                              .displayProductsFromCategory(
                                  this.widget.productBlock.categoryId,
                                  this.widget.productBlock.name),
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
