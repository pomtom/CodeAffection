using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeAffection.Models
{
    public class Employee
    {
        [Key()]
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee name is required.")]
        [DisplayName("Emloyee Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Employee city is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Employee phone is required.")]
        [DisplayName("Phone")]
        [MaxLength(10, ErrorMessage = "Maximum 10 characters only.")]
        public int Phone { get; set; }

        [Required(ErrorMessage = "Employee BirthDate is required.")]
        [Column("BirthDate")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateOnly BirthDate { get; set; }
    }
}
