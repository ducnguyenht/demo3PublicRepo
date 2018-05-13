import 'package:flutter/material.dart';
import 'package:flutter_search_bar/flutter_search_bar.dart';

import '../widgets/common/common_app_bar.dart';

import '../pages/home.dart';
import '../pages/cart.dart';
import '../pages/search_result.dart';
import '../pages/category.dart';

import '../services/product_service.dart';

abstract class CommonState<T extends StatefulWidget> extends State<T> {

  CommonState(this.appBarTitle);

  SearchBar searchBar;
  String appBarTitle;

  @override
  void initState() {
    super.initState();

    searchBar = new SearchBar(
        hintText: "Tìm kiếm",
        inBar: false,
        setState: setState,
        onSubmitted: print,
        buildDefaultAppBar: buildAppBar);
  }

  AppBar buildAppBar(BuildContext context) {
    return new CommonAppBar(
        this.appBarTitle,
        searchBar.getSearchAction(context),
            (searchStr) => this.navigateToSearchResultPage(searchStr),
            () => this.navigateToCartPage());
  }

  void navigateToSearchResultPage(searchStr) {
    Navigator.of(context).push(new MaterialPageRoute<Null>(
      builder: (BuildContext context) {
        return new PageSearchResult(searchStr: searchStr);
      },
    ));
  }

  void navigateToCartPage() {
    Navigator.of(context).push(new MaterialPageRoute<Null>(
      builder: (BuildContext context) {
        return new PageCart();
      },
    ));
  }

  void navigateToHomePage() {
    Navigator.pop(context);
    Navigator.of(context).pushReplacement(new MaterialPageRoute<Null>(
      builder: (BuildContext context) {
        return new PageHome();
      },
    ));
  }

  void navigateToCategoryPage(int catId, String catName) {
    Navigator.pop(context);
    Navigator.of(context).pushAndRemoveUntil(new MaterialPageRoute<Null>(
      builder: (BuildContext context) {
        return new PageCategory(catId, catName);
      },
    ), (Route route) => route.isFirst == true);
  }
}