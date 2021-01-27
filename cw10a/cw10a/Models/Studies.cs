using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw10.Models
{
    public class Studies
    {
        public Studies()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int IdStudy { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
