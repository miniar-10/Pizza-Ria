namespace Backend.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string HashedPassword { get; set; }
        public double? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
