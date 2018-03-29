import 'package:flutter/material.dart';

class WidgetProductCard extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return new Card(
        child: new Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      mainAxisSize: MainAxisSize.min,
      children: <Widget>[
        new Flexible(
            child: new Center(
                child:
                    new Image.asset('images/tai_nghe.png', fit: BoxFit.cover)),
            fit: FlexFit.tight,
            flex: 3),
        new Expanded(child: (new Text('Tai nghe MZ S567'))),
        new Text('78.000')
      ],
    ));
  }
}
