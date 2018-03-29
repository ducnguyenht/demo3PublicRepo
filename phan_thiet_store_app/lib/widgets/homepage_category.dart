import 'package:flutter/material.dart';

import './product_card.dart';

class WidgetHomepageCategory extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return new Column(
      crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisSize: MainAxisSize.min,
        children: <Widget>[
          new Text('Tai nghe'),
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
                new WidgetProductCard()
              ]),
        ]);
  }
}
