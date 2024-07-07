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

        public static void MedicineGetInfo(int userId)
        {
            foreach (var medicine in Medicines)
            {
                if (medicine.UserId == userId)
                {
                    Console.WriteLine(medicine);
                }
            }
        }

        public static void CategoriesGetInfo(int userId)
        {
            Console.WriteLine("> > CATEGORIES LIST < <");
            foreach (var category in Categories)
            {
                if (userId == category.UserId)
                {
                    Console.WriteLine(category);
                }

            }
        }

        public static void UserGetInfo(int userId)
        {
            Console.WriteLine("> > USERS LIST < <");
            foreach (var user in Users)
            {
                if (userId == user.Id)
                {
                    Console.WriteLine(user);
                }
            }
        }


    }
}
