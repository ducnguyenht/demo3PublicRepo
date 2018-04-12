class Category {
  String id;
  String name;
  List<Category> childs = new List<Category>();

  Category(this.id, this.name);

  factory Category.fromJson(Map<String, dynamic> json) {
    var catId = json['categoryId'] as int;
    var catName = json['categoryName'] as String;
    var ret = new Category(catId.toString(), catName);

    var childCatsJson = json['children'];
    if (childCatsJson != null) {
      List<Category> childCats = childCatsJson.map<Category>((childCat) => new Category.fromJson(childCat)).toList();
      ret.childs = childCats;
    }

    return ret;
  }
}