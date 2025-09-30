using System.ComponentModel.DataAnnotations;

namespace SmartStudentQueryAPI.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string EnrollmentNo { get; set; }
    }
}
