import 'dart:async';
import 'package:flutter/material.dart';

import '../widgets/home_category.dart';
import '../widgets/sidenav.dart';
import '../view_models/home_popular_products.dart';
import '../services/product_service.dart';

import '../states/common_state.dart';

class PageHome extends StatefulWidget {
  @override
  PageHomeState createState() {
    var state = new PageHomeState("Trang chủ");
    return state;
  }
}

class PageHomeState extends CommonState<PageHome> {
  PageHomeState(String title) : super(title);

  List<HomePopularProductsBlock> popularBlocks =
      new List<HomePopularProductsBlock>();

  @override
  void initState() {
    super.initState();
    getData();
  }

  void getData() async {
    var productSvc = new ApiProductService();
    var blocks = await productSvc.getHomePopularProducts();
    this.setState(() {
      popularBlocks = blocks;
    });
  }

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        key: _scaffoldKey,
        appBar: searchBar.build(context),
        drawer: new WidgetSideNav(
            this.navigateToHomePage, this.navigateToCategoryPage),
        body: popularBlocks.length > 0
            ? new ListView.builder(
                itemBuilder: (context, index) => new WidgetHomeCategory(
                    popularBlocks[index], this.navigateToCategoryPage),
                itemCount: popularBlocks.length)
            : new Center(child: new CircularProgressIndicator()));
  }

  bool waitForSnackBar = false;
  final GlobalKey<ScaffoldState> _scaffoldKey = new GlobalKey<ScaffoldState>();
  final int timeout = 3000;

  Future<bool> _requestPop() {
    if (waitForSnackBar == false) {
      waitForSnackBar = true;
      var sb = new SnackBar(
          content: new Text("Bấm Back lần nữa để thoát."),
          duration: new Duration(milliseconds: timeout));
      _scaffoldKey.currentState.showSnackBar(sb);
      new Timer(new Duration(milliseconds: timeout), () {
        waitForSnackBar = false;
      });
    } else {
      return new Future.value(true);
    }
  }
}
