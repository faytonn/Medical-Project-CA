using Medical_Project.Models;

namespace Medical_Project.Services
{
    public class CategoryService
    { 
        public void CreateCategory(Category category)
        {
            Array.Resize(ref DB.Categories, DB.Categories.Length + 1);
            DB.Categories[DB.Categories.Length - 1] = category;
        }

        public Category[] GetAllCategories()
        {
            return DB.Categories;
        }
        
    }
}
