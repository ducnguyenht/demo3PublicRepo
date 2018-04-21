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
  }

  @override
  Widget build(BuildContext context) {
    var productSvc = new ApiProductService();

    return new Scaffold(
        appBar: searchBar.build(context),
        drawer: new WidgetSideNav(
            this.navigateToHomePage, this.navigateToCategoryPage),
        body: new FutureBuilder<List<HomePopularProductsBlock>>(
          future: productSvc.getHomePopularProducts(),
          builder: (context, snapshot) {
            if (snapshot.hasError) print(snapshot.error);

            return snapshot.hasData
                ? new ListView.builder(
                    itemBuilder: (context, index) => new WidgetHomeCategory(
                        snapshot.data[index], this.navigateToCategoryPage))
                : new Center(child: new CircularProgressIndicator());
          },
        ));
  }
}
