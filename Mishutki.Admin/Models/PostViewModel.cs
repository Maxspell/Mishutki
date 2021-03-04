using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mishutki.Domain.Entities;


namespace Mishutki.Admin.Models
{
    public class PostViewModel
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string CreatedByUser { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<AssignedTagData> Tags { get; set; }
    }
}