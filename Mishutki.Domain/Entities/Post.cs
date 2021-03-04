using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mishutki.Domain.Entities
{
    public class Post
    {
        public Post()
        {
            Tags = new List<Tag>();
        }

        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public int Rating { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int StatusTypeId { get; set; }
        public string CreatedByUser { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<PostRating> PostRatings { get; set; }
    }
}
