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
            Console.WriteLine("> > CATEGORIES LIST < <");
            foreach (var category in Categories)
            {
                if(userId == category.userId)
                Console.WriteLine(category);

            }
        }

        public static void UserGetInfo()
        {
            Console.WriteLine("> > USERS LIST < <");
            foreach ( var user in Users)
            {
                if(userId == user.Id)
                {
                Console.WriteLine(user);
                }
            }
        }

        
    }
}
