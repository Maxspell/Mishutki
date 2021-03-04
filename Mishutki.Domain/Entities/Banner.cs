using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mishutki.Domain.Entities
{
    public class Banner
    {
        public int BannerId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
        public virtual BannerPlace BannerPlace { get; set; }
        public int BannerPlaceId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string CreatedByUser { get; set; }
    }
}
