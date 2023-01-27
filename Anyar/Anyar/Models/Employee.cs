using System.ComponentModel.DataAnnotations.Schema;

namespace Anyar.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Professia { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string? Img { get; set; }

        [NotMapped]
        public IFormFile FormFile { get; set; }
    }
}
