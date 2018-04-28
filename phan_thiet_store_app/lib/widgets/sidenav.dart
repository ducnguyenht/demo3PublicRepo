import 'package:flutter/material.dart';

import '../services/category_service.dart';
import '../models/category.dart';

class WidgetSideNav extends StatefulWidget {
  final Function displayHomePage;
  final Function displayCategoryPage;

  WidgetSideNav(this.displayHomePage, this.displayCategoryPage);

  List<Widget> getSideNavItems(
      BuildContext context, List<Category> categories) {
    List<Widget> ret = new List<Widget>();

    for (num i = 0; i < categories.length; i++) {
      List<Widget> items = new List<Widget>();
      for (num j = 0; j < categories.elementAt(i).childs.length; j++) {
        var name = categories.elementAt(i).childs.elementAt(j).name;
        var id = categories.elementAt(i).childs.elementAt(j).id;
        items.add(new ListTile(
          title: new Container(
              child: new Text(name, style: Theme.of(context).textTheme.body1),
              padding: new EdgeInsets.only(left: 8.0)),
          onTap: () => displayCategoryPage(id, name),
        ));
      }

      var retItem = new ExpansionTile(
          title: new Text(categories.elementAt(i).name,
              style: Theme.of(context).textTheme.body2),
          children: items);
      ret.add(retItem);
    }

    return ret;
  }

  @override
  WidgetSideNavState createState() {
    return new WidgetSideNavState();
  }
}

class WidgetSideNavState extends State<WidgetSideNav> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    var apiCategorySvc = new ApiCategoryService();
    return new Drawer(
        child: new ListView(children: <Widget>[
      new DrawerHeader(
          child: new Column(
        children: <Widget>[
          new Expanded(
              child: new Text(
            'Phụ kiện Phan Thiết',
            style: Theme.of(context).textTheme.title,
          ))
        ],
        crossAxisAlignment: CrossAxisAlignment.start,
      )),
      new ListTile(
        title: new Text('Trang chủ'),
        onTap: () => widget.displayHomePage(),
      ),
      new FutureBuilder<List<Category>>(
        future: apiCategorySvc.getCategories(),
        builder: (context, snapshot) {
          if (snapshot.hasError) print(snapshot.error);

          return snapshot.hasData
              ? new Column(
                  children: widget.getSideNavItems(context, snapshot.data))
              : new Center(child: new CircularProgressIndicator());
        },
      ),
      new Divider(),
      new ListTile(
        title: new Text('Trợ giúp'),
        onTap: () {},
      ),
      new ListTile(
        title: new Text('Liên hệ'),
        onTap: () {},
      )
    ]));
  }
}
