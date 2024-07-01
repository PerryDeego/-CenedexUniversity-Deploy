/******Created by D.Perry | dd.perry @hotmail.com******/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CenedexUniversityWebSystem.Models
{

    public enum Grade
    {
        A, B, C, D, E, F
    }

    public class StudentGrade: TrackChanges
    {
        //Mutataor and accessor for class attributes.
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        public int StudentId { get; set; }
        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Taken")]
        public DateTime DateTaken { get; set; }

        public Course Course { get; set; }

        public Student Student { get; set; }
    }
}


