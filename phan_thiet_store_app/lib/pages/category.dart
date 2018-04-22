import 'package:flutter/material.dart';

import '../models/product.dart';

import '../states/common_state.dart';

import '../services/product_service.dart';

import '../widgets/sidenav.dart';
import '../widgets/category_products.dart';

class PageCategory extends StatefulWidget {
  final int id;
  final String name;

  @override
  PageCategoryState createState() {
    return new PageCategoryState(this.id, this.name);
  }

  PageCategory(this.id, this.name);
}

class PageCategoryState extends CommonState<PageCategory> {
  final int id;
  final String name;

  PageCategoryState(this.id, this.name) : super(name);

  @override
  Widget build(BuildContext context) {
    var productSvc = new ApiProductService();

/*
    return new Scaffold(
        appBar: searchBar.build(context),
        body:
            new WidgetCategoryProducts(this.widget.name, this.widget.products),
        drawer: new WidgetSideNav(
            this.navigateToHomePage, this.navigateToCategoryPage));
*/

    return new Scaffold(
        appBar: searchBar.build(context),
        drawer: new WidgetSideNav(
            this.navigateToHomePage, this.navigateToCategoryPage),
        body: new FutureBuilder<List<ProductDetail>>(
          future: productSvc.getProductsByCategoryId(id),
          builder: (context, snapshot) {
            if (snapshot.hasError) print(snapshot.error);

            return snapshot.hasData
                ? new WidgetCategoryProducts(name, snapshot.data)
                : new Center(child: new CircularProgressIndicator());
          },
        ));
  }
}
