using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Remastered.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public Authors Author { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Deleted { get; set; }

        [NotMapped]
        public int? PostIdFromQuery { get; set; }
    }
}
