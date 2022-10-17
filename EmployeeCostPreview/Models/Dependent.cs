using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeCostPreview.Models
{
    /// <summary>
    /// Dependent schema model
    /// </summary>
    public class Dependent
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)] 
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
