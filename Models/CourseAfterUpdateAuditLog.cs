using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CenedexUniversityWebSystem.Models
{
    public class CourseAfterUpdateAuditLog
    {
        //Mutataor and accessor for class attributes.
        public int Id { get; set; }

        public int CourseId { get; set; }


        [StringLength(50)]
        public string Name { get; set; }

        public short Credit { get; set; }

        [StringLength(256)]
        public string CreatedBy { get; set; }

        public DateTimeOffset CreatedAt { get; set; }


        [StringLength(256)]
        public string ModifiedBy { get; set; }

        public DateTimeOffset ModifiedAt { get; set; }

        [StringLength(450)]
        public String UserId { get; set; }

        [StringLength(10)]
        public String Action { get; set; }
    }
}
