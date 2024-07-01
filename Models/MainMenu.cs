using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CenedexUniversityWebSystem.Models
{
    [Table("MainMenu")]
    [Index(nameof(MenuName), IsUnique = true)]
    public partial class MainMenu: TrackChanges
    {
        public MainMenu()
        {
            SubMenus = new HashSet<SubMenu>();

          // MenuList = new List<MainMenu>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [StringLength(50)]
        public string MenuName { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        public string MenuUrl { get; set; }

        public virtual ICollection<SubMenu> SubMenus { get; set; }
       // public List<MainMenu> MenuList { get; set; }
    }
}
