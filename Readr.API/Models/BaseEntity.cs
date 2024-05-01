namespace Readr.API.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public BaseEntity(int id)
        {
            this.Id = id;
        }

        protected BaseEntity() { }
    }
}
