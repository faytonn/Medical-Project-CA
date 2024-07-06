using Medical_Project.Exceptions;
using Medical_Project.Models;

namespace Medical_Project.Services
{
    public class UserService
    {
        public User Login(string email, string password)
        {
            foreach (var users in DB.Users)
            {
                if (users.Email.ToLower() == email.ToLower() && users.Password == password)
                {
                    return users;
                }

            }
            throw new NotFoundException("User not found.");
        }

        public void AddUser(User user)
        {
            foreach(var prospectiveUser in DB.Users)
            {
                if(prospectiveUser.Email == user.Email)
                {
                    throw new NotFoundException("User with given email already exists.");
                }
            }
            Array.Resize(ref DB.Users, DB.Users.Length + 1);
            DB.Users[DB.Users.Length - 1] = user;

        }


    }

}
