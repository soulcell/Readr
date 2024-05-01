namespace Readr.API.Models
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }

        public Genre(int id, string name) : base(id)
        {
            Name = name;
        }

        private Genre() { }
    }
}
