using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityControl.Models
{
    public class StudentDTO
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        public string Login { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов")]
        public string LastName { get; set; }
        public string Role { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Password { get; set; }
        public List<ScienceCheckModel> Sciences { get; set; }

        public StudentDTO()
        {
            Sciences = new List<ScienceCheckModel>();
        }
    }
}