import 'package:flutter/material.dart';

import '../widgets/product_card.dart';

class WidgetCategoryProducts extends StatelessWidget {
  WidgetCategoryProducts({this.title});

  final title;

  @override
  Widget build(BuildContext context) {
    return new ListView(
        children: <Widget>[
          new Container(
              child: new Text(this.title,
                  style: new TextStyle(fontWeight: FontWeight.bold)),
              padding: new EdgeInsets.only(left: 5.0)),
          new GridView.count(
              primary: false,
              shrinkWrap: true,
              padding: const EdgeInsets.all(5.0),
              crossAxisSpacing: 5.0,
              crossAxisCount: 2,
              children: <Widget>[
                new WidgetProductCard(),
                new WidgetProductCard(),
                new WidgetProductCard(),
                new WidgetProductCard(),
                new WidgetProductCard(),
                new WidgetProductCard(),
                new WidgetProductCard(),
                new WidgetProductCard(),
                new WidgetProductCard()
              ]
          )
        ]);
  }
}
