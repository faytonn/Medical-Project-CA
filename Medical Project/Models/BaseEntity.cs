namespace Medical_Project.Models
{
    public abstract class BaseEntity
    {
        private static int _id;
        public int Id { get; }

        protected BaseEntity()
        {
            Id = ++_id;
        }
    }
}
