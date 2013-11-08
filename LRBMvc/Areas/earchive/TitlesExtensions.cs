using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRBMvc.Areas.earchive
{
    public class PropertyComparer:IEqualityComparer<Property>
    {
        public bool Equals(Property x, Property y)
        {
            return string.Equals(x.prkno, y.prkno);
        }


        public int GetHashCode(Property obj)
        {
            throw new NotImplementedException();
        }
    }

    public class LandOwnerComparer : IEqualityComparer<LandOwner>
    {
        public bool Equals(LandOwner x, LandOwner y)
        {
            return string.Equals(x.firstname, y.firstname) &&
                string.Equals(x.surname, y.surname)
                && string.Equals(x.middlename, y.middlename);
        }


        public int GetHashCode(LandOwner obj)
        {
            throw new NotImplementedException();
        }
    }

    public partial class LandOwner
    {
        public override string ToString()
        {
            string a = "";
            a += this.surname+ ", ";
            a += this.firstname+ " ";
            a += this.middlename;
            return a;
        }
    }

    public partial class Property
    {
        public override string ToString()
        {
            return this.blockno + " "
            //+ this.plotno + " "
            //+ this.houseno + " "
            + this.location + " "
            + this.lga + " "
            + this.town + " ";
        }
        public string status
        {
            get
            {
                if (this.Transactions.Count != 0)
                {
                    var transaction = this.Transactions.LastOrDefault();
                    return transaction.prkstatus;
                }
                else
                {
                    return "FREE AND CLEAR";
                }
            }
        }
    }
}
