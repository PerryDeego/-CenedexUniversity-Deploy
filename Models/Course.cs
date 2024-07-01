using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CenedexUniversityWebSystem.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Course: TrackChanges
    {

        //Mutataor and accessor for class attributes.
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [Range(0, 8, ErrorMessage = "Enter valid credit from 0 to 8.")]
        public short Credit { get; set; }

        //public ICollection<StudentGrade> StudentGrades { get; set; }

       // public CourseLecturer CourseLecturer { get; set; }
    }
}

