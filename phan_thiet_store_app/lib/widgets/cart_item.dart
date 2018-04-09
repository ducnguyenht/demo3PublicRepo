import 'package:flutter/material.dart';

import '../models/cart.dart';

class WidgetCartItem extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return new Card(
      child: new Container(
        child: new Row(children: <Widget>[
          new Container(
            child: new Image.asset('images/tai_nghe.png', fit: BoxFit.cover),
            padding: new EdgeInsets.all(5.0),
            height: 64.0,
          ),
          new Expanded(
              child: new Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: <Widget>[
              new Container(
                  child: new Text('Tai nghe ANZ'),
                  padding: new EdgeInsets.all(5.0)),
              new Container(
                  child: new Text('đ 280.000',
                      style: Theme.of(context).textTheme.caption),
                  padding: new EdgeInsets.all(5.0))
            ],
          )),
          new Row(
            children: <Widget>[
              new IconButton(
                  icon: new Icon(Icons.remove_circle), onPressed: null),
              new Text('2'),
              new IconButton(icon: new Icon(Icons.add_circle), onPressed: null)
            ],
          ),
        ]),
        padding: new EdgeInsets.all(12.0),
        color: Theme.of(context).cardColor,
      ),
    );
  }
}
