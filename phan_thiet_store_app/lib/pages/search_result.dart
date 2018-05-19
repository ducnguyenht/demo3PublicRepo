import 'package:flutter/material.dart';

import '../models/product.dart';

import '../services/product_service.dart';
import '../widgets/category_products.dart';

class PageSearchResult extends StatefulWidget {
  PageSearchResult({this.searchStr});

  final String searchStr;

  @override
  PageSearchResultState createState() {
    return new PageSearchResultState(searchStr: searchStr);
  }
}

class PageSearchResultState extends State<PageSearchResult> {
  PageSearchResultState({this.searchStr});

  final String searchStr;

  @override
  Widget build(BuildContext context) {
    var productSvc = new ApiProductService();

    return new Scaffold(
        appBar: new AppBar(title: new Text("Kết quả tìm kiếm '$searchStr'")),
        body: new FutureBuilder<List<ProductSummary>>(
          future: productSvc.searchProducts(searchStr),
          builder: (context, snapshot) {
            if (snapshot.hasError) print(snapshot.error);

            return snapshot.hasData
                ? new WidgetCategoryProducts("Kết quả tìm kiếm '$searchStr'", snapshot.data)
                : new Center(child: new CircularProgressIndicator());
          },
        )
    );
  }
}
