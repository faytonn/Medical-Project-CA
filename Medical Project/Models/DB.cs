namespace Medical_Project.Models
{
    public static class DB
    {
        public static User[] Users;
        public static Category[] Categories;
        public static Medicine[] Medicines;

        static DB()
        {
            Users = new User[0];
            Categories = new Category[0];
            Medicines = new Medicine[0];
        }

        public static void MedicineGetInfo()
        {
            foreach (var medicine in Medicines) 
            {
                Console.WriteLine(medicine);
            }
        }

        public static void CategoryGetInfo()
        {
            foreach(var category in Categories)
            {
                Console.WriteLine(category);
            }
        }

        
    }
}
