using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LocalSocial.Models
{
    public class Post
    {
        public Post()
        {
            //this.Tags = new HashSet<Tag>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descryption { get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AddDate { get; set; }
        public string UserId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
    public class PostsBindingModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Descryption { get; set; }

        [Required]
        public float Latitude { get; set; }
        [Required]
        public float Longitude { get; set; }
    }
    }
}
