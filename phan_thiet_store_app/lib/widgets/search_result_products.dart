import 'package:flutter/material.dart';

class WidgetSearchResultProducts extends StatelessWidget {
  WidgetSearchResultProducts({this.searchStr});

  final searchStr;

  @override
  Widget build(BuildContext context) {
    return new ListView(
        children: <Widget>[
          new Container(
              child: new Text("Kết quả tìm kiếm cho '${this.searchStr}'",
                  style: new TextStyle(fontWeight: FontWeight.bold)),
              padding: new EdgeInsets.only(left: 5.0)),
          new GridView.count(
              primary: false,
              shrinkWrap: true,
              padding: const EdgeInsets.all(5.0),
              crossAxisSpacing: 5.0,
              crossAxisCount: 2,
              children: <Widget>[
              ]
          )
        ]);
  }
}
