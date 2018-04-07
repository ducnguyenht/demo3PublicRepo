import 'package:flutter/material.dart';

import '../widgets/home_category.dart';
import './search_result.dart';
import './cart.dart';
import '../view_models/home_popular_products.dart';
import '../services/product_service.dart';

class PageHome extends StatefulWidget {
  HomePopularProducts popularBlocks;

  @override
  PageHomeState createState() {
    return new PageHomeState();
  }

  PageHome() {
    var productSvc = new MockProductService();
    popularBlocks = productSvc.getHomePopularProducts();
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
        return new PageSearchResult(searchStr: searchStr);
      },
    ));
  }

  List<Widget> getPopularProductBlocksWidget() {
    List<Widget> widgets = [];
    this
        .widget
        .popularBlocks
        .blocks
        .forEach((block) => widgets.add(new WidgetHomeCategory(block)));
    return widgets;
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
            new IconButton(
                icon: new Icon(Icons.shopping_cart),
                onPressed: navigateToCartPage)
          ],
        ),
        drawer: new Drawer(
            child: new ListView(
          children: <Widget>[
            new DrawerHeader(
              child: new Text(
                'Phụ kiện Phan Thiết',
                style: Theme.of(context).textTheme.title,
              ),
            ),
            new ListTile(
              title: new Text('Trang chủ'),
              onTap: () {},
            ),
            new ExpansionTile(
              title: new Text('Ốp - bao da',
                  style: Theme.of(context).textTheme.body2),
              children: <Widget>[
                new ListTile(title: new Text('Samsung - Tab Samsung')),
                new ListTile(title: new Text('Oppo')),
                new ListTile(title: new Text('Vivo')),
                new ListTile(title: new Text('Huawei'))
              ],
            ),
            new ExpansionTile(
              title: new Text('Loa - tai nghe - mic',
                  style: Theme.of(context).textTheme.body2),
              children: <Widget>[
                new ListTile(title: new Text('Tai nghe có dây')),
                new ListTile(title: new Text('Loa bluetooth không dây')),
                new ListTile(title: new Text('Loa máy tính - thẻ nhớ')),
                new ListTile(title: new Text('Headphone'))
              ],
            ),
            new ExpansionTile(
              title: new Text('Miếng dán',
                  style: Theme.of(context).textTheme.body2),
              children: <Widget>[
                new ListTile(title: new Text('Dán film trong')),
                new ListTile(title: new Text('Samsung')),
                new ListTile(title: new Text('Hãng XO')),
                new ListTile(title: new Text('Hàng Titan'))
              ],
            ),
            new Divider(),
            new ListTile(
              title: new Text('Trợ giúp'),
              onTap: () {},
            ),
            new ListTile(
              title: new Text('Liên hệ'),
              onTap: () {},
            ),
          ],
        )),
        body: new ListView(children: getPopularProductBlocksWidget()));
  }
}
