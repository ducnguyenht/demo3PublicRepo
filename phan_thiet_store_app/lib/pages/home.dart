import 'package:flutter/material.dart';

import '../widgets/home_category.dart';
import './search_result.dart';
import './cart.dart';

class PageHome extends StatefulWidget {
  @override
  PageHomeState createState() {
    return new PageHomeState();
  }
}

class PageHomeState extends State<PageHome> {
  void navigateToCartPage() {
    Navigator.of(context).push(new MaterialPageRoute<Null>(
      builder: (BuildContext context) {
        return new PageCart();
      },
    ));
  }

  void navigateToSearchResultPage(searchStr) {
    Navigator.of(context).push(new MaterialPageRoute<Null>(
      builder: (BuildContext context) {
        return new PageSearchResult();
      },
    ));
  }

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        appBar: new AppBar(
            title: new TextFormField(
                decoration: new InputDecoration(
                    hintText: 'Tìm kiếm',
                    fillColor: new Color.fromARGB(255, 250, 250, 250),
                    isDense: true,
                    prefixIcon: new Icon(Icons.search),
                    filled: true),
                autofocus: false,
            onFieldSubmitted: navigateToSearchResultPage),
          actions: <Widget>[
            new IconButton(icon: new Icon(Icons.shopping_cart), onPressed: navigateToCartPage)
          ],
        ),
        body: new ListView(children: <Widget>[
          new WidgetHomeCategory(title: 'Tai nghe'),
          new WidgetHomeCategory(title: 'Ốp lưng'),
          new WidgetHomeCategory(title: 'Bao da')
        ]));
  }
}
