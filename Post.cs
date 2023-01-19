using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Remastered.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Authors Author { get; set; }
        public int PageViews { get; set; } = 0;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Deleted { get; set; } = null;
        public ICollection<Comment> Comments { get; set; }
    }
}
