using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLibrary.Data
{
    public partial class Application
    {
        public Party ContactPerson
        {
            get
            {
                var ctcPerson = Parties.Where(p => p.PartyType == "ContactPerson").FirstOrDefault();
                if (ctcPerson == null)
                {
                    this.Parties.Add(new Party() { PartyType = "ContactPerson" });
                }
                ctcPerson = Parties.Where(p => p.PartyType == "ContactPerson").FirstOrDefault();
                return ctcPerson;
            }
            set
            {
                var ctcPerson = Parties.Where(p => p.PartyType == "ContactPerson").FirstOrDefault();
                if (ctcPerson !=null)
                {
                    ctcPerson = value;
                }
                else
                {
                    this.Parties.Add(value);
                }
            }

        }

    }
}
