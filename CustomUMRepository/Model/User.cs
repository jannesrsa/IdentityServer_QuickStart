using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CustomUserManagerRepository.Model
{
    [Table("User", Schema = "CustomUM")]
    public class User
    {
        [Key]
        [Required]
        public int UserID { get; set; }

        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }

        public byte[] UserPassword { get; set; }

        public byte[] PasswordHash { get; set; }

        [MaxLength(200)]
        public string UserDescription { get; set; }

        [MaxLength(200)]
        public string UserEmail { get; set; }
        public int? ManagerID { get; set; }

        [MaxLength(200)]
        public string DisplayName { get; set; }
    }
}
