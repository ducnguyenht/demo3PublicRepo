import 'package:flutter/material.dart';

import '../pages/product.dart';

class WidgetProductCard extends StatefulWidget {
  @override
  WidgetProductCardState createState() {
    return new WidgetProductCardState();
  }
}

class WidgetProductCardState extends State<WidgetProductCard> {
  void goToProductPage(TapUpDetails tapDetail) {
    Navigator.of(context).push(new MaterialPageRoute<Null>(
      builder: (BuildContext context) {
        return new PageProduct();
      },
    ));
  }

  @override
  Widget build(BuildContext context) {
    return new GestureDetector(child: new Card(
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
        )
    ), onTapUp: goToProductPage);
  }
}
