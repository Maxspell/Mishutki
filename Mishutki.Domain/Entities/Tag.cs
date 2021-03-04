using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mishutki.Domain.Entities
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string CreatedByUser { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
