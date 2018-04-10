class Category {
  String id;
  String name;
  List<Category> childs = new List<Category>();
  Category(this.id, this.name);
}