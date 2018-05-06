import 'dart:async';
import 'dart:convert';
import 'package:flutter/foundation.dart' as fo;

import 'package:faker/faker.dart';
import 'package:http/http.dart' as http;

import '../services/category_service.dart';
import '../services/network_service.dart';

import '../view_models/home_popular_products.dart';

import '../models/config.dart';
import '../models/product.dart';
import '../models/category.dart';

abstract class ProductService {
  HomePopularProducts getHomePopularProducts();
  ProductDetail getProductById(int id);
  List<ProductSummary> getProductsByCategoryId(int categoryId);
}

abstract class AsyncProductService {
  Future<List<HomePopularProductsBlock>> getHomePopularProducts();
  Future<ProductDetail> getProductById(int id);
  Future<List<ProductSummary>> getProductsByCategoryId(int categoryId);
  Future<List<ProductSummary>> getLimitedProductsByCategoryId(
      int numberOfProducts, int categoryId);
}

class MockProductService extends ProductService {
  static List<Category> categories;
  static List<ProductDetail> products;
  static HomePopularProducts homePopularProducts;

  @override
  HomePopularProducts getHomePopularProducts() {
    _generateProducts();
    return homePopularProducts;
  }

  @override
  ProductDetail getProductById(int id) {
    var product = products.firstWhere((it) => it.id == id);
    return product;
  }

  @override
  List<ProductSummary> getProductsByCategoryId(int categoryId) {
    return products.where((it) => it.categoryId == categoryId).toList();
  }

  void _generateProducts() {
    if (homePopularProducts == null) {
      categories = new List<Category>();
      products = new List<ProductDetail>();
      homePopularProducts = new HomePopularProducts();

      num numOfCats = 4;
      num numOfChildCats = 4;
      num numOfProductsPerCats = 8;

      for (num i = 0; i < numOfCats; i++) {
        var categoryId = faker.randomGenerator.integer(20000, min: 10000);
        var categoryName = faker.food.restaurant();

        var category = new Category(categoryId, categoryId, categoryName);
        categories.add(category);

        for (num ii = 0; ii < numOfChildCats; ii++) {
          var childCatId = faker.randomGenerator.integer(40000, min: 30000);
          var childCatName = faker.food.restaurant();

          var childCat = new Category(childCatId, categoryId, childCatName);
          category.childs.add(childCat);
        }

        var categoryBlock =
            new HomePopularProductsBlock(categoryId, categoryName);
        homePopularProducts.blocks.add(categoryBlock);

        for (num j = 0; j < numOfProductsPerCats; j++) {
          var productId = faker.randomGenerator.integer(4000000, min: 3000000);
          var productName = faker.food.dish();
          var productPrice =
              faker.randomGenerator.integer(2000000, min: 1000000);
          var productDesc = faker.food.cuisine();

          var product = new ProductDetail(
              productId, productName, productPrice, categoryId, productDesc);

          products.add(product);

          if (j <= 3) {
            categoryBlock.products.add(product);
          }
        }
      }
    }
  }
}

class ApiProductService extends AsyncProductService {
  @override
  Future<List<HomePopularProductsBlock>> getHomePopularProducts() async {
    fo.debugPrint('get home popular products');
    var ret = new HomePopularProducts();

    var catSvc = new ApiCategoryService();
    var parentCats = await catSvc.getCategories();

    for (var parentCat in parentCats) {
      final parentCatId = parentCat.id;
      fo.debugPrint('get home popular products cat $parentCatId');
      var categoryBlock =
          new HomePopularProductsBlock(parentCat.id, parentCat.name);
      ret.blocks.add(categoryBlock);

      var productSummaries =
          await getLimitedProductsByCategoryId(4, parentCat.id);
      categoryBlock.products.addAll(productSummaries);
    }

    return ret.blocks;
  }

  @override
  Future<ProductDetail> getProductById(int id) async {
    var networkSvc = new NetworkService();
    var config = new Config.getConfig();
    var authToken = await networkSvc.getAuthToken();
    fo.debugPrint('token $authToken');

    var response = await http.get(
        config.getFullUrl("/api/branchs/${config.branchId}/masterproducts/$id"),
        headers: {"Authorization": "Bearer $authToken"});
    var body = response.body;
    final parsed = json.decode(body);
    var ret = new ProductDetail.fromJson(parsed);

    return ret;
  }

  @override
  Future<List<ProductSummary>> getProductsByCategoryId(int categoryId) async {
    var networkSvc = new NetworkService();
    var config = new Config.getConfig();
    var authToken = await networkSvc.getAuthToken();
    fo.debugPrint('token $authToken');
    fo.debugPrint('get products by cat $categoryId');
    var response = await http.get(
      config.getFullUrl("/api/branchs/${config.branchId}/masterproducts?format=json&ForSummaryRow=false&CategoryId=$categoryId&IsActive=true"),
        headers: {"Authorization": "Bearer $authToken"});
    var body = response.body;
    final parsed = json.decode(body);

    fo.debugPrint('got products by cat $categoryId');

    var kiotProductLists = parsed['Data'];
    List<ProductSummary> productList = kiotProductLists
        .map<ProductSummary>((it) => new ProductSummary.fromJson(it))
        .toList();

    return productList;
  }

  @override
  Future<List<ProductSummary>> getLimitedProductsByCategoryId(
      int numberOfProducts, int categoryId) async {
    var networkSvc = new NetworkService();
    var config = new Config.getConfig();
    var authToken = await networkSvc.getAuthToken();

    fo.debugPrint('Get limited products by cat $categoryId');

    var response = await http.get(
      config.getFullUrl("/api/branchs/${config.branchId}/masterproducts?format=json&ForSummaryRow=false&CategoryId=$categoryId&IsActive=true&pageSize=$numberOfProducts&skip=0&take=4"),
        headers: {"Authorization": "Bearer $authToken"});
    var body = response.body;
    final parsed = json.decode(body);

    fo.debugPrint('Got limited products by cat $categoryId');

    var kiotProductLists = parsed['Data'];
    List<ProductSummary> productList = kiotProductLists
        .map<ProductSummary>((it) => new ProductSummary.fromJson(it))
        .toList();

    return productList;
  }
}
