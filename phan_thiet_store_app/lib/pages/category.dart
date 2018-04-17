import 'package:flutter/material.dart';

import '../models/product.dart';

import '../states/common_state.dart';

import '../widgets/sidenav.dart';
import '../widgets/category_products.dart';

class PageCategory extends StatefulWidget {
  final List<ProductSummary> products;
  final String name;

  @override
  PageCategoryState createState() {
    return new PageCategoryState(this.name, this.products);
  }

  PageCategory(this.name, this.products);
}

class PageCategoryState extends CommonState<PageCategory> {
  final List<ProductSummary> products;
  final String name;

  PageCategoryState(this.name, this.products) : super(name);

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        appBar: searchBar.build(context),
        body:
            new WidgetCategoryProducts(this.widget.name, this.widget.products),
        drawer: new WidgetSideNav(
            this.navigateToHomePage, this.navigateToCategoryPage));
  }
}
