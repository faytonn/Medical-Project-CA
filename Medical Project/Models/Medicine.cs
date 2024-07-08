using Medical_Project.Exceptions;

namespace Medical_Project.Models
{
    public class Medicine : BaseEntity
    {
        private static int _id;
        public string Name { get; set; }
        private double _price; 
        public double Price 
        {
            get
            {
                return _price;
            }
            set
            {
                if(value < 0 )
                {
                    throw new InvalidPrice("Price cannot be less than 0");
                }
                _price = value;
            }
        }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }


        public Medicine(string name, double price, int categoryId, int userId) 
        {
            CategoryId = categoryId;
            UserId = userId;
            Name = name;
            Price = price;
            CreatedDate = DateTime.Now;
            Id = ++_id;
        }

        public override string ToString()
        {
            return $"User ID: {UserId} \nCategory ID: {CategoryId} \tName: {Name} \tPrice: {Price} AZN \tID: {Id} \t{CreatedDate}";
        }



    }
}
