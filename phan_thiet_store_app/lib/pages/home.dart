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

  HomePopularProducts popularBlocks;

  @override
  void initState() {
    super.initState();
    var productSvc = new MockProductService();
    popularBlocks = productSvc.getHomePopularProducts();
  }

  Widget displayPopularProducts() {
    List<Widget> widgets = [];
    this.popularBlocks.blocks.forEach((block) => widgets
        .add(new WidgetHomeCategory(block, this.navigateToCategoryPage)));
    return new ListView(children: widgets);
  }

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        appBar: searchBar.build(context),
        drawer: new WidgetSideNav(
            this.navigateToHomePage, this.navigateToCategoryPage),
        body: displayPopularProducts());
  }
}
