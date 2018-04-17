import 'package:flutter/material.dart';

class CommonAppBar extends AppBar {
  final String appBarTitle;
  final Widget searchWidget;

  final ValueChanged<String> searchSubmitted;
  final VoidCallback cartClicked;

  CommonAppBar(this.appBarTitle, this.searchWidget, this.searchSubmitted,
      this.cartClicked)
      : super(title: new Text(appBarTitle), actions: <Widget>[
          searchWidget,
          new IconButton(
              icon: new Icon(Icons.shopping_cart), onPressed: cartClicked)
        ]);
}
