using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CenedexUniversityWebSystem.Models
{
    public class MainMenuAfterDeleteAuditLog
    {
        public int Id { get; set; }

        public int MainMenuId { get; set; }

        [StringLength(50)]
        public string MenuName { get; set; }

        public string MenuUrl { get; set; }

        [StringLength(256)]
        public string CreatedBy { get; set; }

        public DateTimeOffset CreatedAt { get; set; }


        [StringLength(256)]
        public string ModifiedBy { get; set; }

        public DateTimeOffset ModifiedAt { get; set; }

        [StringLength(256)]
        public string DeletedBy { get; set; }

        public DateTimeOffset DeletedAt { get; set; }

        [StringLength(450)]
        public String UserId { get; set; }

        [StringLength(10)]
        public String Action { get; set; }
    }
}