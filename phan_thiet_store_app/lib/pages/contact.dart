import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';

class PageContact extends StatelessWidget {
  _callPhone(String phone) async {
    if (phone == null) return;
    var url = 'tel:$phone';
    if (await canLaunch(url)) {
      await launch(url);
    }
  }

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        appBar: new AppBar(
          title: new Text('Liên hệ'),
        ),
        body: new Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          mainAxisAlignment: MainAxisAlignment.center,
          mainAxisSize: MainAxisSize.max,
          children: <Widget>[
            new Container(
                child: new Image.asset('images/logo.png', fit: BoxFit.cover),
                padding: new EdgeInsets.fromLTRB(16.0, 0.0, 16.0, 0.0)),
            new Container(height: 16.0),
            new Row(
                children: <Widget>[
                  new Icon(Icons.navigation,
                      color: Theme.of(context).accentColor),
                  new Text("Địa chỉ 1",
                      style: Theme.of(context).textTheme.body2),
                ],
                mainAxisSize: MainAxisSize.max,
                mainAxisAlignment: MainAxisAlignment.center),
            new Text("12 Phạm Ngọc Thạch, Phú Trinh, Phan Thiết, Bình Thuận",
                textAlign: TextAlign.center),
            new Container(height: 16.0),
            new Row(
                children: <Widget>[
                  new Icon(Icons.navigation,
                      color: Theme.of(context).accentColor),
                  new Text("Địa chỉ 2",
                      style: Theme.of(context).textTheme.body2),
                ],
                mainAxisSize: MainAxisSize.max,
                mainAxisAlignment: MainAxisAlignment.center),
            new Text("02 Lê Hồng Phong, Phan Thiết, Bình Thuận",
                textAlign: TextAlign.center),
            new Container(height: 16.0),
            new Row(
                children: <Widget>[
                  new Icon(Icons.phone, color: Theme.of(context).accentColor),
                  new Text("Điện thoại",
                      style: Theme.of(context).textTheme.body2),
                ],
                mainAxisSize: MainAxisSize.max,
                mainAxisAlignment: MainAxisAlignment.center),
            new Row(
                children: <Widget>[
                  new OutlineButton.icon(
                      icon: new Icon(Icons.phone_android),
                      label: new Text("0971 670 168", textAlign: TextAlign.center),
                      onPressed: () => _callPhone("0971670168")
                  ),
                  new Container(width: 16.0),
                  new OutlineButton.icon(
                      icon: new Icon(Icons.phone_android),
                      label: new Text("0944 505 039", textAlign: TextAlign.center),
                      onPressed: () => _callPhone("0944505039"),
                  )
                ],
                mainAxisSize: MainAxisSize.max,
                mainAxisAlignment: MainAxisAlignment.center)
          ],
        ));
  }
}
