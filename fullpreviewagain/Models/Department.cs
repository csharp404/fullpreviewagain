using System.Reflection.Metadata.Ecma335;

namespace fullpreviewagain.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual Manager? manager { get; set; }
    }
}
