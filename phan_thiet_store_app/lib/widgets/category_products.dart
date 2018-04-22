import 'package:flutter/material.dart';

import '../widgets/product_card.dart';
import '../models/product.dart';

class WidgetCategoryProducts extends StatelessWidget {
  final String name;
  final List<ProductSummary> products;

  @override
  Widget build(BuildContext context) {
    return
      new GridView.builder(
          itemCount: products.length,
          gridDelegate: new SliverGridDelegateWithFixedCrossAxisCount(crossAxisCount: 2, childAspectRatio: 1.0),
          itemBuilder: (BuildContext context, int index) {
            return new WidgetProductCard(products[index]);
          });
  }

  WidgetCategoryProducts(this.name, this.products);
}
