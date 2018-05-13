import 'package:flutter/material.dart';

class PageOrderHistory extends StatelessWidget {
  final List<String> orderCodes;

  PageOrderHistory(this.orderCodes);

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        appBar: new AppBar(
          title: new Text('Đặt hàng'),
        ),
        body: new ListView.builder(
            itemBuilder: (BuildContext context, int index) {
              return new ListTile(
                  title: new Text(orderCodes[index]),
                  trailing: new RaisedButton(
                      child: new Text("Xem"), onPressed: () => {}));
            },
            itemCount: orderCodes.length));
  }
}
