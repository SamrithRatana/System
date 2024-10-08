using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMaintenance.Models
{
    public class Article
    {
        public int Id { get; set; } // Primary key property
        public string ArticleHeading { get; set; }
        public string ArticleContent { get; set; } = "Service Checklist Manangement";
        public string Username { get; set; }
        public string ProfilePicture { get; set; }
    }
}
