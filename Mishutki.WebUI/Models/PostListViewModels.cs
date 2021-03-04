using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mishutki.Domain.Entities;

namespace Mishutki.WebUI.Models
{
    public class PostListViewModels
    {
        public IEnumerable<Post> Posts { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}