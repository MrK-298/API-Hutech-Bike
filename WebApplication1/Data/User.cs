using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string userName { get; set; }
        [Required]
        [MaxLength(100)]
        public string passWord { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        //[Required]
        //[MaxLength(10)]
        //public string phoneNumber { get; set; }

        //[MaxLength(50)]
        //public string fullName { get; set; }

    }
}
