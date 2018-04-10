import 'package:flutter/material.dart';

import '../widgets/category_products.dart';
import '../models/product.dart';

class PageCategory extends StatelessWidget {

  List<ProductSummary> products;
  String name;

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        appBar: new AppBar(title: new Text('Tai nghe')),
        body: new WidgetCategoryProducts(this.name, this.products)
    );
  }

  PageCategory(this.name, this.products);
}
