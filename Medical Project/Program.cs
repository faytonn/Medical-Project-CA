using Medical_Project.Exceptions;
using Medical_Project.Models;
using Medical_Project.Services;
using Medical_Project.Utilities;

bool systemProcess = true;

UserService userService = new UserService();
CategoryService categoryService = new CategoryService();
MedicineService medicineService = new MedicineService();



while (systemProcess)
{

    Console.WriteLine("Welcome to the system!");
restartSystemMenu:
    Console.Clear();
    Console.WriteLine("> > > MENU < < <");
    Console.WriteLine("[1] Create a new user");
    Console.WriteLine("[2] Log in");
    Console.WriteLine("[3] Exit");

    int command = int.Parse(Console.ReadLine());

    switch (command)
    {
        case 1:
                Console.Write("Enter full name: ");
                string fullName = Console.ReadLine();

                Console.Write("Enter email: ");
                string email = Console.ReadLine();

                Console.Write("Enter password: ");
                string password = Console.ReadLine();
            try
            { 
                User user = new User(fullName, email, password);
                userService.AddUser(user);
                Color.WriteLine("User created successfully!", ConsoleColor.Green);
            
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine("Error...");
            }
            break;
        case 2:
                Console.WriteLine("Login");

                Console.Write("Enter email: ");
                string loginEmail = Console.ReadLine();
                Console.Write("Enter password: ");
                string loginPassword = Console.ReadLine();
            try
            {
                User loggedInUser = userService.Login(loginEmail, loginPassword);
                Color.WriteLine($"\nWelcome, {loggedInUser.FullName}!", ConsoleColor.DarkGreen);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                goto restartSystemMenu;
            }
           
            

            bool medicineMenuSystem = true;

            while (medicineMenuSystem)
            {
                Console.WriteLine("> > > MEDICINE MENU < < <");
                Console.WriteLine("[1] Create a new category");
                Console.WriteLine("[2] Create a new medicine");
                Console.WriteLine("[3] Remove a medicine");
                Console.WriteLine("[4] List all medicines");
                Console.WriteLine("[5] Update a medicine");
                Console.WriteLine("[6] Find medicine by ID");
                Console.WriteLine("[7] Find medicine by name");
                Console.WriteLine("[8] Find medicine by category");
                Console.WriteLine("[9] List all categories");
                Console.WriteLine("[0] Exit");

                int command2 = int.Parse(Console.ReadLine());
                switch (command2)
                {
                    case 1:
                        try
                        {
                            Console.Write("Name of category: ");
                            string categoryCreate = Console.ReadLine();
                            
                            foreach(var prospectiveCategory in DB.Categories) 
                            {
                                if(categoryCreate == prospectiveCategory.Name && loggedInUser.Id = prospectiveCategory.UserId)
                                {
                                    throw new NotFoundException("Category with the given name already exists");
                                }
                            }


                            Category category = new Category(categoryCreate, );
                            categoryService.CreateCategory(category);
                            Console.WriteLine("Category successfully created!");
                        }
                        catch (NotFoundException ex)
                        {
                            Color.WriteLine(ex.Message, ConsoleColor.Red);
                        }
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Available categories:");
                            DB.CategoryGetInfo();

                            Console.Write("Category ID: ");
                            int medicineCategoryId = int.Parse(Console.ReadLine());

                            Console.Write("Name of medicine: ");
                            string medicineCreate = Console.ReadLine();

                            Console.Write("Price of medicine: ");
                            double medicinePrice = double.Parse(Console.ReadLine());


                            Medicine medicine = new Medicine(medicineCreate, medicinePrice,medicineCategoryId,5);
                            medicineService.CreateMedicine(medicine);
                        }
                        catch (Exception ex)
                        {
                            Color.WriteLine(ex.Message, ConsoleColor.Red);
                        }
                        break;
                    case 3:
                        try
                        {
                            Console.Write("Enter the ID of the medicine you want to remove: ");
                            int removeID = int.Parse(Console.ReadLine());
                        restarttryAgain:
                            var findMedicineRemove = medicineService.GetMedicineById(removeID);

                            int userId = 1;

                            Console.WriteLine($"You want to remove medicine '{findMedicineRemove}'? \t(Say 'yes' or 'no')");
                            string YesNo = Console.ReadLine();
                            if (YesNo == "yes")
                            {
                                medicineService.RemoveMedicine(userId, removeID);
                                Console.WriteLine($"Medicine '{findMedicineRemove}' successfully deleted.");
                            }
                            else if (YesNo == "no")
                            {
                                goto restarttryAgain;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 4:
                        try
                        {
                            Console.WriteLine("Here are all the listed medicines: ");
                            DB.MedicineGetInfo();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 5:
                        try
                        {
                            Console.WriteLine("Here are all the medicines available for update: ");
                            DB.MedicineGetInfo();

                            Console.Write("Enter the ID of the medicine you want to update: ");
                            int updateID = int.Parse(Console.ReadLine());

                        restarttryAgain1:
                            var findMedicineUpdate = medicineService.GetMedicineById(updateID);

                            Console.WriteLine($"You want to update medicine '{findMedicineUpdate}'? \t(Say 'yes' or 'no')");
                            string YesNoUpdate = Console.ReadLine();
                            if (YesNoUpdate == "yes")
                            {
                                Console.Write("New name of the medicine: ");
                                string newName = Console.ReadLine();
                                Console.Write("New price of the medicine: ");
                                double newPrice = double.Parse(Console.ReadLine());

                                int userId = 1;


                                Medicine newMedicine = new Medicine(newName, newPrice);
                                medicineService.UpdateMedicine(findMedicineUpdate.Id, newMedicine, userId);
                                Console.WriteLine($"Medicine '{findMedicineUpdate}' successfully updated.");
                            }
                            else if (YesNoUpdate.ToLower() == "no")
                            {
                                goto restarttryAgain1;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 6:
                        try
                        {
                            Console.Write("Enter the ID of the medicine you want to find: ");
                            int findMedicineByID = int.Parse(Console.ReadLine());

                            Console.WriteLine(medicineService.GetMedicineById(findMedicineByID));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 7:
                        try
                        {
                            Console.Write("Enter the name of the medicine you want to find: ");
                            string findMedicineByName = Console.ReadLine();

                            Console.WriteLine(medicineService.GetMedicineByName(findMedicineByName));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 8:
                        try
                        {
                            Console.Write("Enter the category of the medicine you want to find: ");
                            int findMedicineByCategory = int.Parse(Console.ReadLine());

                            Console.WriteLine(medicineService.GetMedicineByCategory(findMedicineByCategory));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 9:

                    case 0:
                        try
                        {
                            Console.WriteLine("Terminating program...");
                            systemProcess = false;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid command entered, please try again.");
                        break;

                }
            }
            break;
        case 3:
            Console.WriteLine("Terminating the program... \tGoodbye!");
            systemProcess = false;
            break;
        default:
            Console.WriteLine("Invalid option, please try again.");
            break;
    }

}



