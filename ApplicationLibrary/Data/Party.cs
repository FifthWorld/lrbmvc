using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLibrary.Data
{
    public partial class Party
    {
        public String getName
        {
            get
            {
                return Firstname + " " + Surname;
            }
        }


    }
}
