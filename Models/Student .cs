using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CenedexUniversityWebSystem.Models
{
    public class Student : TrackChanges
    {
        //Mutataor and accessor for class attributes.
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [StringLength(50)]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "This Field is required.")]
        [StringLength(50)]
        public string LastName { get; set; }


        [Required(ErrorMessage = "This Field is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]

        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + "  " + LastName;
            }
        }

        public ICollection<StudentGrade> StudentGrades { get; set; }
    }
}
