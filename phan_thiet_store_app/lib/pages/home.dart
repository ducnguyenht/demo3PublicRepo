import 'package:flutter/material.dart';

import '../widgets/home_category.dart';
import '../widgets/home_sidenav.dart';
import '../widgets/category_products.dart';
import './search_result.dart';
import './cart.dart';
import '../view_models/home_popular_products.dart';
import '../services/product_service.dart';

class PageHome extends StatefulWidget {
  HomePopularProducts popularBlocks;

  @override
  PageHomeState createState() {
    var state =  new PageHomeState();
    return state;
  }

  PageHome() {
    var productSvc = new MockProductService();
    popularBlocks = productSvc.getHomePopularProducts();
  }
}

class PageHomeState extends State<PageHome> with TickerProviderStateMixin  {
  Widget currentBodyWidget;

  @override
  void initState() {
    super.initState();
    this.displayPopularProducts(isInit: true);
  }

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

  void displayPopularProducts({isInit = false}) {
    List<Widget> widgets = [];
    this
        .widget
        .popularBlocks
        .blocks
        .forEach((block) => widgets.add(new WidgetHomeCategory(block, this)));
    var ret = new ListView(children: widgets);

    this.setState(() { currentBodyWidget = ret; });

    if (!isInit) {
      Navigator.of(context).pop();
    }
  }

  void displayProductsFromCategory(String catId, String catName, {isOpenFromNav = false}) {
    var productSvc = new MockProductService();
    var products = productSvc.getProductsByCategoryId(catId);
    var homeWidget = new WidgetCategoryProducts(catName, products);
    this.setState(() { currentBodyWidget = homeWidget; });
    if (isOpenFromNav) {
      Navigator.of(context).pop();
    }
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
        drawer: new WidgetHomeSideNav(this),
        body: currentBodyWidget);
  }
}
