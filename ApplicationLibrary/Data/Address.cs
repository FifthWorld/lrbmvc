using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLibrary.Data
{
    public partial class Address
    {
        public string FullAddress
        {
            get
            {
                return this.Street
                    + " " + this.City
                    + " " + this.Street;
            }
        }
    }
}
