using Medical_Project.Exceptions;
using Medical_Project.Models;

namespace Medical_Project.Utilities;

public static class Validations
{
    public static void ValidPassword(string password)
    {
        bool isUpper = false;
        bool isLower = false;
        bool isDigit = false;
        bool isPunct = false;

        for (int i = 0; i < password.Length; i++)
        {
            if (char.IsUpper(password[i]))
            {
                isUpper = true;
            }
            else if (char.IsLower(password[i]))
            {
                isLower = true;
            }
            else if (char.IsDigit(password[i]))
            {
                isDigit = true;
            }
            else if (char.IsPunctuation(password[i]))
            {
                isPunct = true;
            }
            if (isUpper && isLower && isDigit || isPunct && password.Length >= 8)
            {
                return;
            }

        }
        throw new InvalidPassword("Password should contain uppercase & lowercase letters and digits. Length of password should minimum be 8 characters.");
    }

    public static bool ValidFullName(string fullName)
    {
        foreach (char c in fullName)
        {
            if (!char.IsLetter(c) && c != ' ')
            {
                throw new InvalidFullName("Full Name should consist of letters and spaces only.");
            }
        }
        return true;
    }


    public static bool ValidEmail(string email)
    {

        if (email.Contains('@') && email.Contains('.'))
        {
            return true;
        }
        throw new InvalidEmail("Email should consist of '@' and '.'");
    }

    public static bool isEmailDuplicate(string email, User[] prospectiveUsers)
    {
        foreach(User user in prospectiveUsers)
        {
            if (email == user.Email)
            {
                throw new DuplicateEmail("This email already exists.");
            }
        }
        return false;
    }
}


