import 'package:flutter/material.dart';

import '../widgets/search_result_products.dart';

class PageSearchResult extends StatelessWidget {
  PageSearchResult({this.searchStr});

  final searchStr;

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        appBar: new AppBar(title: new Text('Kết quả tìm kiếm')),
        body: new WidgetSearchResultProducts(searchStr: this.searchStr)
    );
  }
}
