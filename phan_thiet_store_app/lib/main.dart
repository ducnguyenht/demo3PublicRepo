import 'package:flutter/material.dart';

import './pages/home.dart';

void main() => runApp(new MyApp());

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return new MaterialApp(
      title: 'Flutter Demo',
      theme: new ThemeData(
          brightness: Brightness.light,
        primaryColor: new Color(0xffffeb3b),
        primaryColorLight: new Color(0xffffff72),
        primaryColorDark: new Color(0xcff8b900),
        accentColor: new Color(0xffffeb3b),
        accentColorBrightness: Brightness.dark
      ),
      home: new PageHome(),
    );
  }
}
