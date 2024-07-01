﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CenedexUniversityWebSystem.Models
{
    public class StudentGradeAfterUpdateAuditLog
    {
        public int Id { get; set; }

        public int StudentGradeId { get; set; }

        public int CourseId { get; set; }

        public int StudentId { get; set; }

        public Grade? Grade { get; set; }

        public DateTime DateTaken { get; set; }

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