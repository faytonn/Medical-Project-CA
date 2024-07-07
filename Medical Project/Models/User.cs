using System.Xml.Linq;

namespace Medical_Project.Models
{
    public class User : BaseEntity
    {
        private static int _id;
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        

        public User(string fullName, string email, string password)  
        {
            Id = ++_id;
            FullName = fullName;
            Email = email;
            Password = password;
        }

        public override string ToString()
        {
            return $"User ID: {Id} \nName: {FullName} \tEmail: {Email}";
        }



    }
}
