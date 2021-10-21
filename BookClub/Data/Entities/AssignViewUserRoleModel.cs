using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public class AssignViewUserRoleModel
    {
        [DisplayName("Role")]
        [Required(ErrorMessage = "Choose Role")]
        public string RoleId { get; set; }
        public List<SelectListItem> ListRole { get; set; }
        [DisplayName("User")]
        [Required(ErrorMessage = "Choose Username")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Choose Username")]
        public string Username { get; set; }
    }
}
