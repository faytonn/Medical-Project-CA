namespace Medical_Project.Models
{
    public class Medicine : BaseEntity
    {
        private static int _id;
        public string Name { get; set; }
        public double Price { get; set; }
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
