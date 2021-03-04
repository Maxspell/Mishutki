using System;
using System.Collections.Generic;
using Mishutki.Domain.Entities;

namespace Mishutki.Admin.Models
{
    public class TagListViewModel : BaseViewModel
    {
        public IEnumerable<Tag> Tags { get; set; }
    }
}