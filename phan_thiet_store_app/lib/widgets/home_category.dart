import 'package:flutter/material.dart';

import './product_card.dart';
import '../pages/category.dart';

class WidgetHomeCategory extends StatefulWidget {
  WidgetHomeCategory({this.title});

  final title;

  @override
  WidgetHomeCategoryState createState() {
    return new WidgetHomeCategoryState();
  }
}

class WidgetHomeCategoryState extends State<WidgetHomeCategory> {
  void navigateToCategoryPage() {
    Navigator.of(context).push(new MaterialPageRoute<Null>(
      builder: (BuildContext context) {
        return new Scaffold(
          appBar: new AppBar(title: new Text('Tai nghe')),
          body: new PageCategory(title: 'Tai nghe')
        );
      },
    ));
  }

  @override
  Widget build(BuildContext context) {
    return new Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisSize: MainAxisSize.min,
        children: <Widget>[
          new Text(this.widget.title),
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
          new Align(
              alignment: Alignment.centerRight,
              child: new FlatButton(
                  onPressed: navigateToCategoryPage,
                  child: new Text('Xem tất cả')))
        ]);
  }
}
