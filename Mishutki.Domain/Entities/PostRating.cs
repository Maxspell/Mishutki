using System;
using System.Collections.Generic;

namespace Mishutki.Domain.Entities
{
    public class PostRating
    {
        public int PostRatingId { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string UserId { get; set; }
        public bool Like { get; set; }
        public DateTime Created { get; set; }
    }
}
