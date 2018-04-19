import 'dart:async';
import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:flutter/foundation.dart' as fo;

import './network_service.dart';
import './product_service.dart';

import '../models/category.dart';

abstract class CategoryService {
  List<Category> getCategories();
}

abstract class AsyncCategoryService {
  Future<List<Category>> getCategories();
}

class MockCategoryService extends CategoryService {
  @override
  List<Category> getCategories() {
    return MockProductService.categories;
  }
}

class ApiCategoryService extends AsyncCategoryService {
  @override
  Future<List<Category>> getCategories() async {
    var networkService = new NetworkService();
    var authToken = await networkService.getAuthToken();
    var response = await http.get(
        'https://nzt.kiotviet.com/api/categories?%24inlinecount=allpages&format=json',
        headers: {"Authorization": "Bearer $authToken"});
    var body = response.body;
    final parsed = json.decode(body);
    var kiotCatList = parsed['Data'];
    List<KiotCategory> retKiot = kiotCatList.map<KiotCategory>((cat) => new KiotCategory.fromJson(cat)).toList();

    var parentKiotCats = retKiot.where((it) => it.parentId == null);
    List<Category> parentCats = parentKiotCats.map<Category>((cat) => new Category.fromKiot(cat)).toList();

    for (var parentCat in parentCats) {
      var childKiotCats = retKiot.where((it) => it.parentId == parentCat.id);
      List<Category> childCats = childKiotCats.map<Category>((cat) => new Category.fromKiot(cat)).toList();
      parentCat.childs = childCats;
    }

    return parentCats;
  }
}
