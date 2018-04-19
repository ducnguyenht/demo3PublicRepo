class Category {
  int id;
  String name;
  List<Category> childs = new List<Category>();

  Category(this.id, this.name);

  factory Category.fromJson(Map<String, dynamic> json) {
    var catId = json['id'] as int;
    var catName = json['name'] as String;
    var ret = new Category(catId, catName);
    return ret;
  }

  factory Category.fromKiot(KiotCategory kiotCat) {
    var ret = new Category(kiotCat.id, kiotCat.name);
    return ret;
  }
}

class KiotCategory {
  int id;
  String name;
  bool hasChild;
  int parentId;

  KiotCategory(this.id, this.name, this.hasChild, this.parentId);

  factory KiotCategory.fromJson(Map<String, dynamic> json) {
    var catId = json['Id'] as int;
    var catName = json['Name'] as String;
    var hasChild = json['HasChild'] as bool;
    var parentId = json['ParentId'] as int;
    var ret = new KiotCategory(catId, catName, hasChild, parentId);
    return ret;
  }
}