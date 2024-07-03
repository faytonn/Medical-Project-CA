namespace Medical_Project.Models
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string fullName, string email, string password)  
        {
            FullName = fullName;
            Email = email;
            Password = password;
        }

      

    }
}
