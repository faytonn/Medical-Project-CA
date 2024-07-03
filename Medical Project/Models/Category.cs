namespace Medical_Project.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; }
        private static int _categoryId;

        public Category(string name)
        {
            CategoryId = ++_categoryId;
            Name = name;
        }
    }
}
