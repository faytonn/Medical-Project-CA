using System.Runtime.CompilerServices;

namespace Medical_Project.Models
{
    public class Medicine : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        private static int _categoryId;
        public int CategoryId { get; }
        private static int _userId;
        public int UserId { get; }
        public DateTime CreatedDate { get; set; }


        public Medicine(string name, double price)
        {
            CategoryId = ++_categoryId;
            UserId = _userId;

            Name = name;
            Price = price;
            CreatedDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Category ID: {CategoryId} \tName: {Name} \tPrice: {Price} ID: \t{Id} \t{CreatedDate}";
        }

    }
}
