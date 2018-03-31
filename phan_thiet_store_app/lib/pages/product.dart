import 'package:flutter/material.dart';

class PageProduct extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        appBar: new AppBar(title: new Text('Tai nghe MZ ABC')),
        body: new Column(
          children: <Widget>[
            new Center(child: new Text('Product'),)
          ],
        )
    );
  }
}
