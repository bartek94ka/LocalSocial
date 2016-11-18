using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LocalSocial.Models
{
    public class UserFriends
    {
        public UserFriends(string userId)
        {
            Friends = new HashSet<User>();
            UserId = userId;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string UserId { get; private set; }
        public virtual ICollection<User> Friends { get; private set; }
    }
}
