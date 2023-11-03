using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(100)]
        public string userName { get; set; }
        [Required]
        [MaxLength(100)]
        public string passWord { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        public string phoneNumber { get; set; }

        [MaxLength(50)]
        public string fullName { get; set; }
    }
}
