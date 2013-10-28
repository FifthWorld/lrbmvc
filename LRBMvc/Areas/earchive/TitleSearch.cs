using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRBMvc.Areas.earchive
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
    public class TitleSearch
    {
        LandTitles context = new LandTitles();
        public IQueryable<Property> properties;
        public IQueryable<LandOwner> landOwners;
        public TitleSearch()
        {
            properties = context.Properties.Include("LandOwners");
            landOwners = context.LandOwners.Include("Property");
        }
        public Property search_by_prk(string prk_no)
        {
            prk_no = prk_no.Trim();
            if (prk_no != null)
            {
                return properties.Where(p => p.prkno == prk_no).FirstOrDefault();
            }
            else
                return null;
        }

        /*
         * This method is used to search for ptoperties properties that belong to a certain set of individuals
         */
        public IEnumerable<SearchItem> search_for_individual_owners(string surName = "", string firstName = "", string middleName = "")
        {
            surName = surName.Trim();
            firstName = firstName.Trim();
            middleName = middleName.Trim();
            IQueryable<LandOwner> owners = landOwners;
            List<Property> temp = new List<Property>();
            if (surName.Length != 0)
            {
                owners = owners.Where(p => p.surname.Contains(surName));
            }
            if (firstName.Length != 0)
            {
                owners = owners.Where(p => p.firstname.Contains(firstName));
            }
            if (middleName.Length != 0)
            {
                owners = owners.Where(p => p.middlename.Contains(middleName));
            }
            foreach (var owner in owners)
            {
                temp.Add(owner.Property);
            }
            return SearchItem.getSearchItems(temp);
        }

        public IEnumerable<SearchItem> search_for_corparate_properties(string industryName)
        {
            industryName = industryName.Trim();
            IQueryable<LandOwner> owners = landOwners;
            List<Property> temp = new List<Property>();
            if (industryName.Length != 0)
            {
                owners = owners.Where(p => p.industryname.Contains(industryName));
            }
            foreach (var owner in owners)
            {
                temp.Add(owner.Property);
            }
            return SearchItem.getSearchItems(temp);
        }


        public IEnumerable<SearchItem> search_for_property(string town = "", string lga = "", string street = "")
        {
            town = town.Trim();
            lga = lga.Trim();
            street = street.Trim();
            IQueryable<Property> temp = properties;

            temp = (town.Length != 0) ? temp.Where(p => p.town.Contains(town)) : temp;
            temp = (lga.Length != 0) ? temp.Where(p => p.lga.Contains(lga)) : temp;
            temp = (street.Length != 0) ? temp.Where(p => p.location.Contains(street)) : temp;
            
            return SearchItem.getSearchItems(temp.ToList());
        }
    }
}