using Medical_Project.Exceptions;
using Medical_Project.Models;
using Medical_Project.Services;

bool systemProcess = true;

UserService userService = new UserService();
CategoryService categoryService = new CategoryService();
MedicineService medicineService = new MedicineService();



while (systemProcess)
{
    Console.WriteLine("Welcome to the system!");
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


            User user = new User(fullName, email, password);
            userService.AddUser(user);
            Console.WriteLine("User created successfully!");
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
                Console.WriteLine($"\nWelcome, {loggedInUser.FullName}!");
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine("User not found.");
            }

            Console.WriteLine("> > > MEDICINE MENU < < <");
            Console.WriteLine("[1] Create a new category");
            Console.WriteLine("[2] Create a new medicine");
            Console.WriteLine("[3] Remove a medicine");
            Console.WriteLine("[4] List all medicines");
            Console.WriteLine("[5] Update a medicine");
            Console.WriteLine("[6] Find medicine by ID");
            Console.WriteLine("[7] Find medicine by name");
            Console.WriteLine("[8] Find medicine by category");
            Console.WriteLine("[9] Exit");

            int command2 = int.Parse(Console.ReadLine());
            switch (command2)
            {
                case 1:
                    Console.Write("Name of category: ");
                    string categoryCreate = Console.ReadLine();

                    Category category = new Category(categoryCreate);
                    categoryService.CreateCategory(category);
                    Console.WriteLine("Category successfully created!");
                    break;
                case 2:
                    Console.Write("Name of medicine: ");
                    string medicineCreate = Console.ReadLine();

                    Console.Write("Price of medicine: ");
                    double medicinePrice = double.Parse(Console.ReadLine());

                    Medicine medicine = new Medicine(medicineCreate, medicinePrice);
                    medicineService.CreateMedicine(medicine);
                    Console.WriteLine("Medicine successfully created!");
                    break;
                case 3:
                    Console.Write("Enter the ID of the medicine you want to remove: ");
                    int removeID = int.Parse(Console.ReadLine());
                restarttryAgain:
                    var findMedicineRemove = medicineService.GetMedicineById(removeID);

                    Console.WriteLine($"You want to remove medicine '{findMedicineRemove}'? \t(Say 'yes' or 'no')");
                    string YesNo = Console.ReadLine();
                    if (YesNo == "yes")
                    {
                        medicineService.RemoveMedicine(removeID, findMedicineRemove);
                        Console.WriteLine($"Medicine '{findMedicineRemove}' successfully deleted.");
                    }
                    else if (YesNo == "no")
                    {
                        goto restarttryAgain;
                    }
                    break;
                case 4:
                    Console.WriteLine("Here are all the listed medicines: ");
                    Console.WriteLine(medicineService.GetAllMedicines());
                    break;
                case 5:
                    Console.Write("Enter the ID of the medicine you want to update: ");
                    int updateID = int.Parse(Console.ReadLine());

                restarttryAgain1:
                    var findMedicineUpdate = medicineService.GetMedicineById(updateID);

                    Console.WriteLine($"You want to remove medicine '{findMedicineUpdate}'? \t(Say 'yes' or 'no')");
                    string YesNoUpdate = Console.ReadLine();
                    if (YesNoUpdate == "yes")
                    {
                        medicineService.UpdateMedicine(updateID, findMedicineUpdate);
                        Console.WriteLine($"Medicine '{findMedicineUpdate}' successfully updated.");
                    }
                    else if (YesNoUpdate == "no")
                    {
                        goto restarttryAgain1;
                    }
                    break;
                case 6:
                    Console.WriteLine(medicine);
                    Console.Write("Enter the ID of the medicine you want to find: ");
                    int findMedicineByID = int.Parse(Console.ReadLine());

                    Console.WriteLine(medicineService.GetMedicineById(findMedicineByID));
                    break;
                case 7:
                    Console.Write("Enter the name of the medicine you want to find: ");
                    string findMedicineByName = Console.ReadLine();

                    Console.WriteLine(medicineService.GetMedicineByName(findMedicineByName));
                    break;
                case 8:
                    Console.Write("Enter the category of the medicine you want to find: ");
                    int findMedicineByCategory = int.Parse(Console.ReadLine());

                    Console.WriteLine(medicineService.GetMedicineByCategory(findMedicineByCategory));
                    break;
                case 9:
                    Console.WriteLine("Terminating program...");
                    systemProcess = false;
                    break;
                default:
                    Console.WriteLine("Invalid command entered, please try again.");
                    break;
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



