using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IseGluteneFree { get; set; } = false;
        public bool? IsDeleted { get; set; } = false;
        public bool? IsModified { get; set; } = false;
    }
}
