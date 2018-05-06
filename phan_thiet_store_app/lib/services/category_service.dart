import 'dart:async';
import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:flutter/foundation.dart' as fo;

import './network_service.dart';
import './product_service.dart';

import '../models/category.dart';
import '../models/config.dart';

abstract class CategoryService {
  List<Category> getCategories();
}

abstract class AsyncCategoryService {
  Future<List<Category>> getCategories();
  Future<List<Category>> getChildCategories(int parentCatId);
}

class MockCategoryService extends CategoryService {
  @override
  List<Category> getCategories() {
    return MockProductService.categories;
  }
}

class ApiCategoryService extends AsyncCategoryService {
  static List<Category> _parentCats;
  static List<Category> _allCats;

  @override
  Future<List<Category>> getCategories() async {
    if (_parentCats == null) {
      await _populateCategories();
    }
    return _parentCats;
  }

  Future _populateCategories() async {
    var networkService = new NetworkService();
    fo.debugPrint('getting token');
    var authToken = await networkService.getAuthToken();
    fo.debugPrint('got token');
    fo.debugPrint('getting cats');
    var config = new Config.getConfig();
    var response = await http.get(
        config
            .getFullUrl("/api/categories?%24inlinecount=allpages&format=json"),
        headers: {"Authorization": "Bearer $authToken"});
    var body = response.body;
    final parsed = json.decode(body);
    fo.debugPrint('got cats');
    var kiotCatList = parsed['Data'];
    List<KiotCategory> retKiot = kiotCatList
        .map<KiotCategory>((cat) => new KiotCategory.fromJson(cat))
        .toList();

    var parentKiotCats = retKiot.where((it) => it.parentId == null);
    List<Category> parentCats = parentKiotCats
        .map<Category>((cat) => new Category.fromKiot(cat))
        .toList();

    for (var parentCat in parentCats) {
      var childKiotCats = retKiot.where((it) => it.parentId == parentCat.id);
      List<Category> childCats = childKiotCats
          .map<Category>((cat) => new Category.fromKiot(cat))
          .toList();
      parentCat.childs = childCats;
    }

    var allCats =
        retKiot.map<Category>((cat) => new Category.fromKiot(cat)).toList();

    _allCats = allCats;
    _parentCats = parentCats;
  }

  @override
  Future<List<Category>> getChildCategories(int parentCatId) async {
    if (_allCats != null) {
      await _populateCategories();
    }

    var childCats = _allCats.where((it) => it.parentId == parentCatId).toList();
    return childCats;
  }
}
