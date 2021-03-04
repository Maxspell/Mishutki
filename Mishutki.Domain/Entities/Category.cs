using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mishutki.Domain.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public int Order { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string CreatedByUser { get; set; }
    }
}
