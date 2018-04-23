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

  List<HomePopularProductsBlock> popularBlocks = new List<HomePopularProductsBlock>();

  @override
  void initState() {
    super.initState();
    getData();
  }

  void getData() async {
    var productSvc = new ApiProductService();
    var blocks = await productSvc.getHomePopularProducts();
    this.setState(() { popularBlocks = blocks; });
  }

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        appBar: searchBar.build(context),
        drawer: new WidgetSideNav(
            this.navigateToHomePage, this.navigateToCategoryPage),
        body: popularBlocks.length > 0
            ? new ListView.builder(
            itemBuilder: (context, index) => new WidgetHomeCategory(
                popularBlocks[index], this.navigateToCategoryPage),
            itemCount: popularBlocks.length)
            : new Center(child: new CircularProgressIndicator())
    );
  }
}
