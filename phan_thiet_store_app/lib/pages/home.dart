import 'package:flutter/material.dart';
import '../widgets/homepage_category.dart';

class PageHome extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      appBar: new AppBar(
        title: const Text('Trang chính')
      ),
      body: new Padding(
        padding: const EdgeInsets.all(16.0),
        child: new WidgetHomepageCategory(),
      ),
    );
  }
}
