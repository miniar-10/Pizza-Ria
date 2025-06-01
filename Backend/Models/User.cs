using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string HashedPassword {  get; set; }
        public double PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
