import 'package:flutter/material.dart';

import '../widgets/category_products.dart';

class PageCategory extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        appBar: new AppBar(title: new Text('Tai nghe')),
        body: new WidgetCategoryProducts(title: 'Tai nghe')
    );
  }
}
