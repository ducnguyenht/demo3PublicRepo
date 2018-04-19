import '../models/product.dart';

class HomePopularProducts {
  List<HomePopularProductsBlock> blocks = new List<HomePopularProductsBlock>();
}

class HomePopularProductsBlock {
  int categoryId;
  String name;
  List<ProductSummary> products = new List<ProductSummary>();

  HomePopularProductsBlock(this.categoryId, this.name);
}