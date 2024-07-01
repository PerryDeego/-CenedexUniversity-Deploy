using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CenedexUniversityWebSystem.Models
{
    public class CourseReportViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public Grade? Grade { get; set; }

        public DateTime DateTaken { get; set; }

    }
}
