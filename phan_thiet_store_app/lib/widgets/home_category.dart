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
        return new PageCategory();
      },
    ));
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
                  child: new Text(this.widget.title,
                      style: new TextStyle(fontWeight: FontWeight.bold)),
                  padding: new EdgeInsets.only(left: 5.0)),
              new Expanded(
                  child: new Align(
                      child: new FlatButton(
                          onPressed: navigateToCategoryPage,
                          child: new Text('Xem tất cả', style: new TextStyle(color: Colors.orange)),
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
              children: <Widget>[
                new WidgetProductCard(),
                new WidgetProductCard(),
                new WidgetProductCard(),
                new WidgetProductCard()
              ]),
        ]);
  }
}
