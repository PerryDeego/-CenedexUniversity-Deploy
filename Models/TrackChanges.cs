using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CenedexUniversityWebSystem.Models
{
    public  abstract class TrackChanges
    {
        [StringLength(256)]
       public string CreatedBy { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        [StringLength(256)]
        public string ModifiedBy { get; set; }

        public DateTimeOffset ModifiedAt { get; set; }

        [StringLength(450)]
        public String UserId { get; set; }

        public virtual IdentityUser User { get; set; }
        //public virtual List<IdentityUser> Users { get; set; }
    }
}
