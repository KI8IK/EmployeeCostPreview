using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCostPreview.Models
{
    /// <summary>
    /// Employee schema model
    /// </summary>
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Per-check gross pay
        /// </summary>
        [Required, Precision(18, 2)]
        public decimal PayRate  { get; set; }

        public ICollection<Dependent> Dependents { get; set; }
    }
}
