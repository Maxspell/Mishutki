using System;
using System.Collections.Generic;
using Mishutki.Domain.Entities;

namespace Mishutki.Admin.Models
{
    public class CategoryListViewModel : BaseViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}