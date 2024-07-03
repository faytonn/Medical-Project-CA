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
                if (users.Email == email && users.Password == password)
                {
                    return users;
                }

            }
            throw new NotFoundException("User not found.");
        }

        public void AddUser(User user)
        {
            Array.Resize(ref DB.Users, DB.Users.Length + 1);
            DB.Users[DB.Users.Length - 1] = user;
        }


    }

}
