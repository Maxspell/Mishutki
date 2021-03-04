using System;
using System.Collections.Generic;
using Mishutki.Domain.Entities;

namespace Mishutki.Admin.Models
{
    public class PostListViewModel : BaseViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}