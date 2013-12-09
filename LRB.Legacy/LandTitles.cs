using LRB.Legacy.Entities;
using LRB.Legacy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRB.Legacy
{    
    public static class LandTitles
    {
        static LTContext context = new LTContext();
        public static  IQueryable<Property> properties;
        public static IQueryable<LandOwner> landOwners;
        public static void initialize()
        {
            properties = context.Properties.Include("LandOwners").AsQueryable();
            landOwners = context.LandOwners.Include("Property").AsQueryable();
        }
        public static Property search_by_prk(string prk_no)
        {
            prk_no = prk_no.Trim();
            if (prk_no != null)
            {
                return properties.Where(p => p.prkno == prk_no).FirstOrDefault();
            }
            else
                return null;
        }

        public static IEnumerable<SearchItem> search_for_individual_owners(string surName = "", string firstName = "", string middleName = "")
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

        public static IEnumerable<SearchItem> search_for_corparate_properties(string industryName)
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


        public static IEnumerable<SearchItem> search_for_property(string town = "", string lga = "", string street = "")
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