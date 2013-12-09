using LRB.Legacy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRB.Legacy.Entities
{
    public class SearchItem
    {
        public IEnumerable<LandOwner> owners { get; set; }
        public string address { get; set; }
        public string prkNo { get; set; }
        public string permalink
        {
            get
            {
                return "~/Title.aspx?prkno=" + prkNo;
            }
        }
        public string owner
        {
            get
            {
                if (owners.Count() == 1)
                {
                    return owners.FirstOrDefault().ToString();
                }
                else
                {
                    string t = "";
                    foreach (var o in owners)
                    {
                        if (o != owners.Last())
                        {
                            t += o.ToString() + " AND ";
                        }
                        else
                        {
                            t += o.ToString();
                        }

                    }
                    return t;
                }
            }
        }


        /*
         * This static method returns a list of search items useful to the Title Search Page
         * When given a Property Enumeration
         */
        public static IEnumerable<SearchItem> getSearchItems(IEnumerable<Property> properties)
        {
            if (properties != null)
            {
                List<SearchItem> items = new List<SearchItem>();
                foreach (var p in properties)
                {
                    items.Add(new SearchItem()
                    {
                        address = p.ToString(),
                        prkNo = p.prkno,
                        owners = p.LandOwners
                    });
                }
                return items;
            }
            return null;

        }
    }
}
