using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mishutki.Domain.Entities
{
    public class BannerPlace
    {
        public int BannerPlaceId { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string CreatedByUser { get; set; }
        public virtual ICollection<Banner> Banners { get; set; }
    }
}
