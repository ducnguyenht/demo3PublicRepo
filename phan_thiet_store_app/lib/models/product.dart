class ProductSummary {

  String id;
  String name;
  num price;

  int categoryId;

  ProductSummary(this.id, this.name, this.price, this.categoryId);
}

class ProductDetail extends ProductSummary {

  String description;

  ProductDetail(id, name, price, categoryId, this.description) : super(id, name, price, categoryId);
}