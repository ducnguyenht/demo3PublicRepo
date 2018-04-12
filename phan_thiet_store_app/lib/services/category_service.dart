import 'dart:async';
import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:flutter/foundation.dart' as fo;

import '../models/category.dart';
import './product_service.dart';

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
    var response = await http.get('http://phukienphanthiet.com/umbraco/api/CategoriesApi/Get');
    var body = response.body;
    final parsed = json.decode(body);
    var catList = parsed['data'];
    List<Category> ret = catList.map<Category>((cat) => new Category.fromJson(cat)).toList();
    return ret;
  }
}