using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Databases.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Username { get; set; } = null!;

        [StringLength(256)]
        public string Email { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;

        public byte[] PasswordSalt { get; set; } = null!;

        [StringLength(100)]
        public string KnownAs { get; set; } = null!;

        public DateTime Birthday { get; set; }

        [StringLength(10)]
        public string Gender { get; set; } = null!;

        [StringLength(500)]
        public string Avatar { get; set; } = null!;

        [StringLength(100)]
        public string City { get; set; } = null!;
    }
}