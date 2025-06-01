namespace Backend.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HashedPassword { get; set; }
        public double PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
