using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartStudentQueryAPI.Models
{
    public class Query
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Required]
        public string QueryText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual Student Student { get; set; }
    }
}
