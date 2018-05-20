class ProductSummary {

  int id;
  String name;
  num price;
  String imageUrl;
  num onHand;

  int categoryId;

  ProductSummary(this.id, this.name, this.price, this.categoryId, this.onHand);

  factory ProductSummary.fromJson(Map<String, dynamic> json) {
    var id = json['Id'] as int;
    var fullName = json['FullName'] as String;
    var price = json['BasePrice'] as double;
    var onHand = json['OnHand'] as int;
    var categoryId = json['CategoryId'] as int;

    var ret = new ProductSummary(id, fullName, price, categoryId, onHand);
    ret.imageUrl = json['Image'] as String;
    return ret;
  }
}

class ProductDetail extends ProductSummary {

  String description;

  ProductDetail(id, name, price, categoryId, onHand, this.description) : super(id, name, price, onHand, categoryId);

  factory ProductDetail.fromJson(Map<String, dynamic> json) {
    var id = json['Id'] as int;
    var fullName = json['FullName'] as String;
    var price = json['BasePrice'] as double;
    var categoryId = json['CategoryId'] as int;
    var onHand = json['OnHand'] as int;
    var desc = json['Description'] as String;

    var ret = new ProductDetail(id, fullName, price, categoryId, onHand, desc);
    if (json['ProductImages'].length > 0) {
      ret.imageUrl = json['ProductImages'][0]['Image'];
    }
    return ret;
  }
}