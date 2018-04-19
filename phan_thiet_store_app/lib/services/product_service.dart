import 'package:uuid/uuid.dart';
import 'package:faker/faker.dart';

import '../view_models/home_popular_products.dart';
import '../models/product.dart';
import '../models/category.dart';

abstract class ProductService {
  HomePopularProducts getHomePopularProducts();
  ProductDetail getProductById(String id);
  List<ProductSummary> getProductsByCategoryId(int categoryId);
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
  ProductDetail getProductById(String id) {
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

      var uuid = new Uuid();

      num numOfCats = 4;
      num numOfChildCats = 4;
      num numOfProductsPerCats = 8;

      for (num i = 0; i < numOfCats; i++) {
        var categoryId = faker.randomGenerator.integer(20000, min: 10000);
        var categoryName = faker.food.restaurant();

        var category = new Category(categoryId, categoryName);
        categories.add(category);

        for (num ii = 0; ii < numOfChildCats; ii++) {
          var childCatId = faker.randomGenerator.integer(40000, min: 30000);
          var childCatName = faker.food.restaurant();

          var childCat = new Category(childCatId, childCatName);
          category.childs.add(childCat);
        }

        var categoryBlock =
            new HomePopularProductsBlock(categoryId, categoryName);
        homePopularProducts.blocks.add(categoryBlock);

        for (num j = 0; j < numOfProductsPerCats; j++) {
          var productId = uuid.v4();
          var productName = faker.food.dish();
          var productPrice = faker.randomGenerator.integer(2000000, min: 1000000);
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
