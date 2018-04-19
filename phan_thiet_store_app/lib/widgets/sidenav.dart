import 'package:flutter/material.dart';

import '../services/category_service.dart';
import '../models/category.dart';

class WidgetSideNav extends StatelessWidget {
  final Function displayHomePage;
  final Function displayCategoryPage;

  WidgetSideNav(this.displayHomePage, this.displayCategoryPage);

  List<Widget> getSideNavItems(BuildContext context, List<Category> categories) {

    List<Widget> ret = new List<Widget>();

    ret.add(new DrawerHeader(
      child: new Text(
        'Phụ kiện Phan Thiết',
        style: Theme.of(context).textTheme.title,
      ),
    ));

    ret.add(new ListTile(
      title: new Text('Trang chủ'),
      onTap: () => displayHomePage(),
    ));

    for (num i = 0; i < categories.length; i++) {
      List<Widget> items = new List<Widget>();
      for (num j = 0; j < categories.elementAt(i).childs.length; j++) {
        var name = categories.elementAt(i).childs.elementAt(j).name;
        var id = categories.elementAt(i).childs.elementAt(j).id;
        items.add(new ListTile(
            title: new Text(name),
            onTap: () => displayCategoryPage(id, name)
        ));
      }

      var retItem = new ExpansionTile(
          title: new Text(categories.elementAt(i).name,
              style: Theme.of(context).textTheme.body2),
          children: items);
      ret.add(retItem);
    }

    ret.add(new Divider());
    ret.add(new ListTile(
      title: new Text('Trợ giúp'),
      onTap: () {},
    ));
    ret.add(new ListTile(
      title: new Text('Liên hệ'),
      onTap: () {},
    ));

    return ret;
  }

  @override
  Widget build(BuildContext context) {
    var apiCategorySvc = new ApiCategoryService();
    return new Drawer(child: new FutureBuilder<List<Category>>(
      future: apiCategorySvc.getCategories(),
      builder: (context, snapshot) {
        if (snapshot.hasError) print(snapshot.error);

        return snapshot.hasData
            ? new ListView(children: getSideNavItems(context, snapshot.data))
            : new Center(child: new CircularProgressIndicator());
      },
    ));
  }
}
