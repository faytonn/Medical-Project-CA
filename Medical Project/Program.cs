using Medical_Project.Exceptions;
using Medical_Project.Models;
using Medical_Project.Services;

bool systemProcess = true;

UserService userService = new UserService();



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
                Console.WriteLine($"Welcome, {loggedInUser.FullName}!");
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

            int command2 = int.Parse(Console.ReadLine());
            switch (command2)
            {

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
    Console.WriteLine();
}


while (true)
{

}
