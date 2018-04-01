import 'package:flutter/material.dart';

class PageProduct extends StatelessWidget {
  void buyNowPressed() {}

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      appBar: new AppBar(title: new Text('Tai nghe MZ ABC')),
      body: new Container(
        child: new Column(
          children: <Widget>[
            new Center(
                child:
                    new Image.asset('images/tai_nghe.png', fit: BoxFit.cover)),
            new Text('Tai nghe MZ ABC hàng chính hãng'),
            new Text('5,399,000'),
            new Align(
                child: new Text('Thông số sản phẩm',
                    style: new TextStyle(fontWeight: FontWeight.bold)),
                alignment: Alignment.centerLeft),
            new Align(
                child: new Column(
                  children: <Widget>[
                    new Text('Tai 2 driver'),
                    new Text('Âm thanh chi tiết')
                  ],
                  crossAxisAlignment: CrossAxisAlignment.start,
                ),
                alignment: Alignment.centerLeft),
          ],
        ),
        padding: new EdgeInsets.all(10.0),
      ),
      bottomNavigationBar: new Container(
        padding: new EdgeInsets.all(10.0),
          child: new FlatButton.icon(
        onPressed: buyNowPressed,
        icon: new Icon(Icons.add_shopping_cart),
        label: new Text('Mua ngay'),
        color: new Color.fromARGB(255, 244, 0, 0),
        textColor: new Color.fromARGB(255, 255, 255, 255),
        highlightColor: new Color.fromARGB(255, 200, 0, 0)
      )),
    );
  }
}
