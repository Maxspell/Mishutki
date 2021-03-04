using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mishutki.Admin.Models
{
    public class AssignedTagData
    {
        public int TagId { get; set; }
        public string Title { get; set; }
        public bool Assigned { get; set; }
    }
}