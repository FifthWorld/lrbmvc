using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRB.Lib.Domain
{
    public class Optional
    {
        public int Id { get; set; }

        [DisplayName("Home town")]
        public string HomeTown { get; set; }

        [DisplayName("State of Origin")]
        public string StateofOrigin { get; set; }
        [DisplayName("Employer name")]
        public string EmployerName { get; set; }

        public string Occupation { get; set; }
        [DisplayName("Street Name")]
        public string Street { get; set; }
        [DisplayName("State")]
        public string State { get; set; }
        [DisplayName("Town")]
        public string Town { get; set; }
        [DisplayName("LGA")]
        public string LGA { get; set; }

        public Application Application { get; set; }
    }
}
