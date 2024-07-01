using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CenedexUniversityWebSystem.Models
{
    public class Lecturer: TrackChanges
    {
        //Mutataor and accessor for class attributes.
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Hired")]
        public DateTime DateHired { get; set; }

        //public CourseLecturer CourseLecturer { get; set; }
    }
}
