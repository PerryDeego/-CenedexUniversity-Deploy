using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CenedexUniversityWebSystem.Models
{
    
    [Table("SubMenu")]
    public partial class SubMenu: TrackChanges
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [StringLength(50)]
        public string SubMenuName { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        public string SubMenuUrl { get; set; }


        [Required(ErrorMessage = "This Field is required.")]
        public int? MainMenuId { get; set; }

        public virtual MainMenu MainMenu { get; set; }
    }
}
