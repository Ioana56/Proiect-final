using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blog_Remastered.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class Authors
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string About { get; set; } = string.Empty;
        public string ApplicationUserId { get; set; }
    }
}
