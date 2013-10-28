using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLibrary.Data
{
    public partial class Property
    {
        public String getName
        {
            get
            {
                return this.EntityKey.ToString();
            }
        }

        public string MainAddress
        {
            get
            {
                var _address = Addresses.FirstOrDefault();
                if (_address != null)
                {
                    return _address.Street + " "
                        + _address.City + " "
                        + _address.State + " ";
                }
                return "";
            }
        }
    }
}
