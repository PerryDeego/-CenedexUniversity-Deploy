using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CenedexUniversityWebSystem.Models
{
    public class LecturerAfterUpdateAuditLog
    {
         public int Id { get; set; }

        public int LecturerId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public DateTime DateHired { get; set; }

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
