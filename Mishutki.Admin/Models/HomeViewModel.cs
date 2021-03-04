using System;
using System.Collections.Generic;
using Mishutki.Domain.Entities;

namespace Mishutki.Admin.Models
{
    public class HomeViewModel
    {
        public int PostCount { get; set; }
        public int CategoryCount { get; set; }
        public int TagCount { get; set; }
        public int UserCount { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}