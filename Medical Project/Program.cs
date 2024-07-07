using Medical_Project.Exceptions;
using Medical_Project.Models;
using Medical_Project.Services;
using Medical_Project.Utilities;


namespace Medical_Project;

public class Program
{
    static void Main(string[] args)
    {
        bool systemProcess = true;

        UserService userService = new UserService();
        CategoryService categoryService = new CategoryService();
        MedicineService medicineService = new MedicineService();

        User myUser = new("", "", "");

        while (systemProcess)
        {

            Console.WriteLine("Welcome to the system!");
        restartSystemMenu:
            Console.WriteLine("> > > MAIN MENU < < <");
            Console.WriteLine("[1] Create a new user");
            Console.WriteLine("[2] Log in");
            Console.WriteLine("[3] Exit");

            int command = int.Parse(Console.ReadLine());

            switch (command)
            {
                case 1:
                    CreateUser(userService);
                    goto restartSystemMenu;
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
                restartMedicineMenu:
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
                                CreateCategory(categoryService);
                                goto restartMedicineMenu;
                            case 2:
                                CreateMedicine(medicineService);
                                goto restartMedicineMenu;
                            case 3:
                                RemoveMedicine(medicineService);
                                goto restartMedicineMenu;
                            case 4:
                                ShowAllMedicines(medicineService);
                                goto restartMedicineMenu;
                            case 5:
                                UpdateMedicine(medicineService);
                                goto restartMedicineMenu;
                            case 6:
                                FindMedicineById(medicineService);
                                goto restartMedicineMenu;
                            case 7:
                                FindMedicineByName(medicineService);
                                goto restartMedicineMenu;
                            case 8:
                                FindMedicineByCategoryId(medicineService);
                                goto restartMedicineMenu;
                            case 9:
                                ShowAllCategories(categoryService);
                                goto restartMedicineMenu;
                            case 0:
                                Console.WriteLine("Terminating program...");
                                systemProcess = false;
                                break;
                            default:
                                Color.WriteLine("Invalid command entered, please try again.", ConsoleColor.Red);
                                goto restartMedicineMenu;

                        }
                    }
                    break;
                case 3:
                    Console.WriteLine("Terminating the program... \tGoodbye!");
                    systemProcess = false;
                    break;
                default:
                    Color.WriteLine("Invalid option, please try again.", ConsoleColor.Red);
                    break;
            }
        }

    }

    private static void CreateUser(UserService userService)
    {
    restartRegistrationProcess:
        try
        {
            Console.Write("Enter full name: ");
            string fullName = Console.ReadLine();
            Validations.ValidFullName(fullName);

            Console.Write("Enter email: ");
            string email = Console.ReadLine();
            Validations.ValidEmail(email);

            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            Validations.ValidPassword(password);

            User user = new User(fullName, email, password);
            userService.AddUser(user);
            Color.WriteLine("User created successfully!", ConsoleColor.Green);

        }
        catch (InvalidFullName ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
            goto restartRegistrationProcess;
        }
        catch (InvalidEmail ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
            goto restartRegistrationProcess;
        }
        catch (InvalidPassword ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
            goto restartRegistrationProcess;
        }
    }

    private static Medicine CreateMedicine(MedicineService medicineService)
    {
        User myUser = new("", "", "");
        try
        {
            Color.WriteLine("Available categories:", ConsoleColor.Yellow);
            DB.CategoryGetInfo(myUser.Id);

            Console.Write("Category ID: ");
            int medicineCategoryId = int.Parse(Console.ReadLine());

            Console.Write("Name of medicine: ");
            string medicineCreate = Console.ReadLine();

            Console.Write("Price of medicine: ");
            double medicinePrice = double.Parse(Console.ReadLine());


            Medicine medicine = new Medicine(medicineCreate, medicinePrice, medicineCategoryId, myUser.Id);
            medicineService.CreateMedicine(medicine);

            Color.WriteLine("Medicine successfully created!", ConsoleColor.Green);
        }
        catch (NotFoundException ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }

    private static Category CreateCategory(CategoryService categoryService)
    {
        User myUser = new("", "", "");
        try
        {
            Console.Write("Name of category: ");
            string categoryCreate = Console.ReadLine();

            foreach (var prospectiveCategory in DB.Categories)
            {
                if (categoryCreate == prospectiveCategory.Name && myUser.Id == prospectiveCategory.UserId)
                {
                    throw new NotFoundException("Category with the given name already exists");
                }
            }


            Category category = new Category(categoryCreate, myUser.Id);
            categoryService.CreateCategory(category);
            Color.WriteLine("Category successfully created!", ConsoleColor.Green);
        }
        catch (NotFoundException ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }

    private static Medicine RemoveMedicine(MedicineService medicineService)
    {
        User myUser = new("", "", "");
    restartChangeChoice:
        try
        {
            Color.WriteLine("Here are the listed medicines: ", ConsoleColor.Yellow);
            DB.MedicineGetInfo(myUser.Id);

            Console.Write("Enter the ID of the medicine you want to remove: ");
            int removeID = int.Parse(Console.ReadLine());

            Medicine findMedicineRemove = medicineService.GetMedicineById(removeID, myUser.Id);

            Color.WriteLine($"You want to remove medicine '{findMedicineRemove}'? \t(Say 'yes' or 'no')", ConsoleColor.DarkRed);
            string YesNo = Console.ReadLine();
            if (YesNo.ToLower() == "yes")
            {
                Medicine removedMedicine = medicineService.RemoveMedicine(myUser.Id, removeID);
                Console.WriteLine($"Medicine '{findMedicineRemove}' successfully deleted.");
            }
            else if (YesNo.ToLower() == "no")
            {
                goto restartChangeChoice;
            }
            else
            {
                throw new NotFoundException("Please enter a proper option.");
            }
        }
        catch (NotFoundException ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }

    private static Medicine ShowAllMedicines(MedicineService medicineService)
    {
        User myUser = new("", "", "");
        try
        {
            Color.WriteLine("Here are all the listed medicines: ", ConsoleColor.Yellow);
            DB.MedicineGetInfo(myUser.Id);
        }
        catch (Exception ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }
    private static Medicine UpdateMedicine(MedicineService medicineService)
    {
        User myUser = new("", "", "");
    restartUpdateProcess:
        try
        {
            Color.WriteLine("Here are all the medicines available for update: ", ConsoleColor.Yellow);
            DB.MedicineGetInfo(myUser.Id);

            Console.Write("Enter the ID of the medicine you want to update: ");
            int updateID = int.Parse(Console.ReadLine());

            Medicine findMedicineUpdate = medicineService.GetMedicineById(updateID, myUser.Id);/////

            Console.WriteLine($"You want to update medicine '{findMedicineUpdate}'? \t(Say 'yes' or 'no')");
            string YesNoUpdate = Console.ReadLine();
            if (YesNoUpdate == "yes")
            {
                Console.Write("New name of the medicine: ");
                string newName = Console.ReadLine();
                Console.Write("New price of the medicine: ");
                double newPrice = double.Parse(Console.ReadLine());

                int categoryId = findMedicineUpdate.CategoryId;
                int userId = findMedicineUpdate.UserId;

                Medicine newMedicine = new Medicine(newName, newPrice, categoryId, userId);////
                medicineService.UpdateMedicine(findMedicineUpdate.Id, newMedicine, userId);
                Color.WriteLine($"Medicine '{findMedicineUpdate}' successfully updated.", ConsoleColor.Green);
            }
            else if (YesNoUpdate.ToLower() == "no")
            {
                goto restartUpdateProcess;
            }
        }
        catch (NotFoundException ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }

    private static Medicine FindMedicineById(MedicineService medicineService)
    {
        User myUser = new("", "", "");
        try
        {
            Console.Write("Enter the ID of the medicine you want to find: ");
            int findMedicineByID = int.Parse(Console.ReadLine());

            Console.WriteLine(medicineService.GetMedicineById(findMedicineByID, myUser.Id));////
        }
        catch (Exception ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }

    private static Medicine FindMedicineByName(MedicineService medicineService)
    {
        User myUser = new("", "", "");
        try
        {
            Console.Write("Enter the name of the medicine you want to find: ");
            string findMedicineByName = Console.ReadLine();

            Console.WriteLine(medicineService.GetMedicineByName(findMedicineByName, myUser.Id));/////
        }
        catch (Exception ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }

    private static Medicine FindMedicineByCategoryId(MedicineService medicineService)
    {
        User myUser = new("", "", "");
        try
        {
            Console.Write("Enter the category of the medicine you want to find: ");
            int findMedicineByCategoryId = int.Parse(Console.ReadLine());

            Console.WriteLine(medicineService.GetMedicineByCategory(findMedicineByCategoryId, myUser.Id));
        }
        catch (Exception ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }

    private static Category ShowAllCategories(CategoryService categoryService)
    {
        User myUser = new("", "", "");

        try
        {
            Color.WriteLine("Here are all the categories:", ConsoleColor.Yellow);
            DB.CategoryGetInfo(myUser.Id);
        }
        catch (Exception ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }
}








