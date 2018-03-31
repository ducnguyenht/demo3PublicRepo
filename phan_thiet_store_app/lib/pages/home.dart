import 'package:flutter/material.dart';
import '../widgets/home_category.dart';

class PageHome extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      appBar: new AppBar(
        title: const Text('Trang chính')
      ),
      body: new ListView(children: <Widget>[
          new WidgetHomeCategory(title: 'Tai nghe'),
          new WidgetHomeCategory(title: 'Ốp lưng'),
          new WidgetHomeCategory(title: 'Bao da')
        ])
    );
  }
}
