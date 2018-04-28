import 'package:flutter/material.dart';

class CheckoutBox extends StatelessWidget {
  final List<Widget> children;
  final String title;

  CheckoutBox(this.title, this.children);

  Widget getContainerChild(BuildContext context) {
    List<Widget> items = new List<Widget>();
    if (title != null) {
      items.add(new Text(title,
          style: Theme
              .of(context)
              .textTheme
              .subhead));
      items.add(new Divider());
      items.addAll(children);
    }

    return new Column(children: items, crossAxisAlignment: CrossAxisAlignment.start);
  }

  @override
  Widget build(BuildContext context) {
    return new Container(
      child: getContainerChild(context),
      padding: new EdgeInsets.all(16.0),
      margin: new EdgeInsets.only(bottom: 8.0),
      decoration: new BoxDecoration(
          border: new Border.all(color: Colors.grey, width: 0.5), color: Colors.white),
    );
  }
}