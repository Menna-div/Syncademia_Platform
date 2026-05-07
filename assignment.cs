using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace syncademia
{
    public class assignment
    {
       public int Id { get; set; }
        public int MentorId { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public string DriveLink { get; set; }
    }
}