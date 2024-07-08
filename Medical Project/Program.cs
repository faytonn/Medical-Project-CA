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
        bool medicineMenu = true;

        UserService userService = new UserService();
        CategoryService categoryService = new CategoryService();
        MedicineService medicineService = new MedicineService();

        User[] prospectiveUser = new User[0];
        User loggedInUser = new("", "", "");

        while (systemProcess)
        {

            Console.WriteLine("Welcome to the system!");
        restartSystemMenu:
            Console.WriteLine("> > > MAIN MENU < < <");
            Console.WriteLine("[1] Create a new user");
            Console.WriteLine("[2] Log in");
            Console.WriteLine("[3] Exit");

            string command = Console.ReadLine();

            switch (command)
            {
                case "1":
                    CreateUser(userService, prospectiveUser);
                    goto restartSystemMenu;
                case "2":
                    Console.WriteLine("Login");

                    Console.Write("Enter email: ");
                    string loginEmail = Console.ReadLine().Trim();
                    Console.Write("Enter password: ");
                    string loginPassword = Console.ReadLine().Trim();
                    try
                    {
                        loggedInUser = userService.Login(loginEmail, loginPassword);
                        Color.WriteLine($"\nWelcome, {loggedInUser.FullName}!", ConsoleColor.DarkGreen);
                    }
                    catch (NotFoundException ex)
                    {
                        Color.WriteLine(ex.Message, ConsoleColor.Red);
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
                        Console.WriteLine("[8] Find medicine by category ID");
                        Console.WriteLine("[9] List all categories");
                        Console.WriteLine("[0] Go back to User menu");

                        string command2 = Console.ReadLine();

                        switch (command2)
                        {
                            case "1":
                                CreateCategory(categoryService, loggedInUser.Id);
                                goto restartMedicineMenu;
                            case "2":
                                CreateMedicine(medicineService, loggedInUser.Id);
                                goto restartMedicineMenu;
                            case "3":
                                RemoveMedicine(medicineService, loggedInUser.Id);
                                goto restartMedicineMenu;
                            case "4":
                                ShowAllMedicines(medicineService, loggedInUser.Id);
                                goto restartMedicineMenu;
                            case "5":
                                UpdateMedicine(medicineService, loggedInUser.Id);
                                goto restartMedicineMenu;
                            case "6":
                                FindMedicineById(medicineService, loggedInUser.Id);
                                goto restartMedicineMenu;
                            case "7":
                                FindMedicineByName(medicineService, loggedInUser.Id);
                                goto restartMedicineMenu;
                            case "8":
                                FindMedicineByCategoryId(medicineService, loggedInUser.Id);
                                goto restartMedicineMenu;
                            case "9":
                                ShowAllCategories(categoryService, loggedInUser.Id);
                                goto restartMedicineMenu;
                            case "0":
                                medicineMenu = false;
                                goto restartSystemMenu;
                            default:
                                Color.WriteLine("Invalid command entered, please try again.", ConsoleColor.Red);
                                goto restartMedicineMenu;

                        }
                    }
                    break;

                case "3":
                    Console.WriteLine("Terminating the program... \tGoodbye!");
                    systemProcess = false;
                    break;
                default:
                    Color.WriteLine("Invalid option, please try again.", ConsoleColor.Red);
                    break;
            }
        }

    }

    private static void CreateUser(UserService userService, User[] prospectiveUser)
    {
    restartRegistrationProcess:
        try
        {
            Console.Write("Enter full name: ");
            string fullName = Console.ReadLine().Trim();
            Validations.ValidFullName(fullName);

            Console.Write("Enter email: ");
            string email = Console.ReadLine().Trim();
            Validations.ValidEmail(email);
            if (Validations.isEmailDuplicate(email, prospectiveUser))
            {
                throw new DuplicateEmail("This email already exists.");
            }

            Console.Write("Enter password: ");
            string password = Console.ReadLine().Trim();
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
        catch (DuplicateEmail ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
            goto restartRegistrationProcess;
        }
        catch (InvalidPassword ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
            goto restartRegistrationProcess;
        }
        catch (NotFoundException ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
            goto restartRegistrationProcess;
        }
    }

    private static Category CreateCategory(CategoryService categoryService, int userId)
    {
        try
        {
            Console.Write("Name of category: ");
            string categoryCreate = Console.ReadLine().Trim();

            foreach (var prospectiveCategory in DB.Categories)
            {
                if (categoryCreate == prospectiveCategory.Name && userId == prospectiveCategory.UserId)
                {
                    throw new NotFoundException("Category with the given name already exists");
                }
            }


            Category category = new Category(categoryCreate, userId);
            categoryService.CreateCategory(category);
            Color.WriteLine("Category successfully created!", ConsoleColor.Green);
        }
        catch (NotFoundException ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        catch (IsNullException ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }

    private static Medicine CreateMedicine(MedicineService medicineService, int userId)
    {
        try
        {
            Category[] categoryValidation = DB.Categories;
            if (categoryValidation.Length <= 0)
            {
                Color.WriteLine("No category available yet, please try again after creating a category", ConsoleColor.Red);
            }
            else
            {
                Color.WriteLine("Available categories:", ConsoleColor.Yellow);
                DB.CategoriesGetInfo(userId);

                Console.Write("Category ID: ");
                int medicineCategoryId = int.Parse(Console.ReadLine());

                Console.Write("Name of medicine: ");
                string medicineCreate = Console.ReadLine().Trim();

                Console.Write("Price of medicine: ");
                double medicinePrice = double.Parse(Console.ReadLine());


                Medicine medicine = new Medicine(medicineCreate, medicinePrice, medicineCategoryId, userId);
                medicineService.CreateMedicine(medicine);

                Color.WriteLine("Medicine successfully created!", ConsoleColor.Green);
            }
        }
        catch (NotFoundException ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        catch (InvalidPrice ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }


    private static Medicine RemoveMedicine(MedicineService medicineService, int userId)
    {
    restartChangeChoice:
        try
        {
            Medicine[] medicineValidation = DB.Medicines;

            if (medicineValidation.Length <= 0)
            {
                Color.WriteLine("No medicines available yet, please try again after creating a medicine.", ConsoleColor.Red);
            }
            else
            {
                Color.WriteLine("Here are the listed medicines: ", ConsoleColor.Yellow);
                DB.MedicineGetInfo(userId);

                Console.Write("Enter the ID of the medicine you want to remove: ");
                int removeID = int.Parse(Console.ReadLine());

                Medicine findMedicineRemove = medicineService.GetMedicineById(removeID, userId);

                Color.WriteLine($"You want to remove medicine '{findMedicineRemove}'? \t(Say 'yes' or 'no')", ConsoleColor.DarkRed);
                string YesNo = Console.ReadLine().Trim();
                if (YesNo.ToLower() == "yes")
                {
                    Medicine removedMedicine = medicineService.RemoveMedicine(userId, removeID);
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
        }
        catch (NotFoundException ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }

    private static Medicine ShowAllMedicines(MedicineService medicineService, int userId)
    {
        try
        {
            Medicine[] medicineValidation = DB.Medicines;
            if (medicineValidation.Length <= 0)
            {
                Color.WriteLine("No medicines available yet, please try again after creating a medicine.", ConsoleColor.Red);
            }
            else
            {
                Color.WriteLine("Here are all the listed medicines: ", ConsoleColor.Yellow);
                DB.MedicineGetInfo(userId);
            }
        }
        catch (Exception ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }
    private static Medicine UpdateMedicine(MedicineService medicineService, int userId)
    {
    restartUpdateProcess:
        try
        {
            Medicine[] medicineValidation = DB.Medicines;

            if (medicineValidation.Length <= 0)
            {
                Color.WriteLine("No medicines available yet, please try again after creating a medicine.", ConsoleColor.Red);
            }
            else
            {
                Color.WriteLine("Here are all the medicines available for update: ", ConsoleColor.Yellow);
                DB.MedicineGetInfo(userId);

                Console.Write("Enter the ID of the medicine you want to update: ");
                int updateID = int.Parse(Console.ReadLine());

                Medicine findMedicineUpdate = medicineService.GetMedicineById(updateID, userId);/////

                Console.WriteLine($"You want to update medicine '{findMedicineUpdate.Name}'? \t(Say 'yes' or 'no')");
                string YesNoUpdate = Console.ReadLine();
                if (YesNoUpdate.ToLower() == "yes")
                {
                    Console.Write("New name of the medicine: ");
                    string newName = Console.ReadLine();
                    Console.Write("New price of the medicine: ");
                    double newPrice = double.Parse(Console.ReadLine());

                    int categoryId = findMedicineUpdate.CategoryId;
                    userId = findMedicineUpdate.UserId;

                    Medicine newMedicine = new Medicine(newName, newPrice, categoryId, userId);////
                    medicineService.UpdateMedicine(findMedicineUpdate.Id, newMedicine, userId);
                    Color.WriteLine($"Medicine '{findMedicineUpdate}' successfully updated.", ConsoleColor.Green);
                }
                else if (YesNoUpdate.ToLower() == "no")
                {
                    goto restartUpdateProcess;
                }
            }
        }
        catch (NotFoundException ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }

    private static Medicine FindMedicineById(MedicineService medicineService, int userId)
    {
        try
        {
            Medicine[] medicineValidation = DB.Medicines;
            if (medicineValidation.Length <= 0)
            {
                Color.WriteLine("No medicines available yet, please try again after creating a medicine.", ConsoleColor.Red);
            }
            else
            {

                Console.Write("Enter the ID of the medicine you want to find: ");
                int findMedicineByID = int.Parse(Console.ReadLine());

                Medicine medicine = medicineService.GetMedicineById(findMedicineByID, userId);
                if (medicine != null)
                {
                    Color.WriteLine("Medicine found: " + medicine.Name, ConsoleColor.DarkGreen);
                    return medicine;
                }
            }
        }
        catch (Exception ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }

    private static Medicine FindMedicineByName(MedicineService medicineService, int userId)
    {
        try
        {
            Medicine[] medicineValidation = DB.Medicines;
            if (medicineValidation.Length <= 0)
            {
                Color.WriteLine("No medicines available yet, please try again after creating a medicine.", ConsoleColor.Red);
            }
            else
            {
                Console.Write("Enter the name of the medicine you want to find: ");
                string findMedicineByName = Console.ReadLine();

                Medicine medicine = medicineService.GetMedicineByName(findMedicineByName, userId);/////
                if (medicine != null)
                {
                    Color.WriteLine("Medicine found: " + medicine.Name, ConsoleColor.DarkGreen);
                    return medicine;
                }
            }
        }
        catch (Exception ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }

    private static Medicine FindMedicineByCategoryId(MedicineService medicineService, int userId)
    {
        try
        {
            Medicine[] medicineValidation = DB.Medicines;
            if (medicineValidation.Length <= 0)
            {
                Color.WriteLine("No medicines available yet, please try again after creating a medicine.", ConsoleColor.Red);
            }
            else
            {
                Console.Write("Enter the category ID of the medicine you want to find: ");
                int findMedicineByCategoryId = int.Parse(Console.ReadLine());

                Medicine[] medicines = medicineService.GetMedicineByCategory(userId, findMedicineByCategoryId);
                if (medicines != null)
                {
                    foreach (var medicine in medicines)
                    {
                        Color.WriteLine("Medicine found: " + medicine, ConsoleColor.DarkGreen);

                    }

                    if (medicines.Length == 0)
                    {
                        throw new NotFoundException("Medicine is not found");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }

    private static Category ShowAllCategories(CategoryService categoryService, int userId)
    {
        try
        {
            Category[] categoryValidation = DB.Categories;
            if (categoryValidation.Length <= 0)
            {
                Color.WriteLine("No category available yet, please try again after creating a category", ConsoleColor.Red);
            }
            else
            {
                Color.WriteLine("Here are all the categories:", ConsoleColor.Yellow);
                DB.CategoriesGetInfo(userId);
            }
        }
        catch (Exception ex)
        {
            Color.WriteLine(ex.Message, ConsoleColor.Red);
        }
        return null;
    }
}








