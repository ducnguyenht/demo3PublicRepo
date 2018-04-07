import '../models/category.dart';
import './product_service.dart';

abstract class CategoryService {
  List<Category> getCategories();
}

class MockCategoryService extends CategoryService {
  @override
  List<Category> getCategories() {
    return MockProductService.categories;
  }

}