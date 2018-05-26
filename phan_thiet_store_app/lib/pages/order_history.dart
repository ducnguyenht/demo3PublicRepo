import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';

class PageOrderHistory extends StatelessWidget {
  final List<String> orderCodes;

  PageOrderHistory(this.orderCodes);

  _launchURL(String cartCode) async {
    if (cartCode == null) return;
    var url = 'http://phukienphanthiet.com/don-hang?q=$cartCode';
    if (await canLaunch(url)) {
      await launch(url);
    }
  }

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        appBar: new AppBar(
          title: new Text('Lịch sử đặt hàng'),
        ),
        body: (orderCodes != null && orderCodes.length > 0)
            ? new ListView.builder(
                itemBuilder: (BuildContext context, int index) {
                  return new ListTile(
                      title: new Text(orderCodes[index] != null
                          ? orderCodes[index]
                          : "N/A"),
                      trailing: new RaisedButton(
                          color: Theme.of(context).accentColor,
                          textTheme: ButtonTextTheme.primary,
                          child: new Text("Xem"),
                          onPressed: () => _launchURL(orderCodes[index])));
                },
                itemCount: orderCodes.length)
            : new Column(
                crossAxisAlignment: CrossAxisAlignment.center,
                mainAxisSize: MainAxisSize.max,
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  Icon(
                    Icons.sentiment_dissatisfied,
                    size: 96.0,
                    color: Colors.grey,
                  ),
                  new Container(height: 16.0),
                  new Text('Lịch sử đặt hàng hiện đang trống',
                      style: Theme.of(context).textTheme.display1,
                      textAlign: TextAlign.center)
                ],
              ));
  }
}
