import 'dart:async';

import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';
import 'package:shared_preferences/shared_preferences.dart';

class PageOrderHistory extends StatefulWidget {
  final List<String> orderCodes;

  PageOrderHistory(this.orderCodes);

  @override
  PageOrderHistoryState createState() {
    return new PageOrderHistoryState(orderCodes);
  }
}

class PageOrderHistoryState extends State<PageOrderHistory> {
  PageOrderHistoryState(this.orderCodes);

  List<String> orderCodes;

  _launchURL(String cartCode) async {
    if (cartCode == null) return;
    var url = 'http://phukienphanthiet.com/don-hang?q=$cartCode';
    if (await canLaunch(url)) {
      await launch(url);
    }
  }

  static const String PREF_ORDER_HISTORY = "PT-APP-ORDER-HISTORY";

  _removeOrderCode(int itemIndex) async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    var orderHistory = prefs.getStringList(PREF_ORDER_HISTORY);
    orderHistory.removeAt(itemIndex);
    await prefs.setStringList(PREF_ORDER_HISTORY, orderHistory);
    setState(() {
      orderCodes = orderHistory;
    });
  }

  Future<Null> _confirmDelete(int itemIndex, String itemCode) async {
    return showDialog<Null>(
      context: context,
      barrierDismissible: false, // user must tap button!
      builder: (BuildContext context) {
        return new AlertDialog(
          title: new Text('Xóa theo dõi đơn hàng'),
          content: new SingleChildScrollView(
            child: new ListBody(
              children: <Widget>[
                new Text('Bạn muốn bỏ theo dõi đơn hàng \'$itemCode\' ?')
              ],
            ),
          ),
          actions: <Widget>[
            new FlatButton(
              child: new Text('Có', style: Theme.of(context).textTheme.button),
              onPressed: () async {
                await _removeOrderCode(itemIndex);
                Navigator.of(context).pop();
              },
            ),
            new FlatButton(
              child: new Text('Không'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            )
          ],
        );
      },
    );
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
                    title: new Text(
                        orderCodes[index] != null ? orderCodes[index] : "N/A"),
                    trailing: new Row(
                      children: <Widget>[
                        new RaisedButton(
                            color: Theme.of(context).accentColor,
                            textTheme: ButtonTextTheme.primary,
                            child: new Text("Xem"),
                            onPressed: () => _launchURL(orderCodes[index])),
                        new Container(width: 16.0),
                        new RaisedButton(
                            color: Theme.of(context).buttonColor,
                            textTheme: ButtonTextTheme.primary,
                            child: new Text("Xóa theo dõi"),
                            onPressed: () =>
                                _confirmDelete(index, orderCodes[index]))
                      ],
                    ),
                  );
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
