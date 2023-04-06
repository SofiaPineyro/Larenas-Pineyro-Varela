using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public class Role
    {
        public RoleCode RoleId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public ICollection<UserRole> Users { get; set; }

    }
}
