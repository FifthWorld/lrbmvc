using ApplicationLibrary;
using LRB.Lib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRB.Lib.Console
{
    class LandRecordsTest
    {
        public void CreateApplication_WithRequirement_And_SubmitToSola()
        {
            var r = new Requirement()
            {
                applicationType = "Individual",
                landSize = 10000,
                landSizeUnit = "Hectares",
                landUse = "Commercial"
            };
            System.Console.WriteLine("Application Requirement Configured");
            var app = LandRecords.CreateApplication(r, "tomcat");

            var app2 = LandRecords.GetApplication(app.Id);
            var prop = app.PrimaryProperty;

            prop.PropertyAddress = prop.getPropertyAddress();
            prop.PropertyAddress.Street = "Sohomo Were Avnue";
            prop.PropertyAddress.Town = "Ondo Town";
            prop.PropertyAddress.State = "Ondo State";
            prop.PropertyAddress.LGA = "AKure South";
            LandRecords.SaveApplication(app.Id, prop);

            System.Console.WriteLine("Property Information Added");

            Party p = app2.ContactPerson;
            if (p==null)
            {
                p = new Party();
                System.Console.WriteLine("Creating new Party because app.ContactPerson returned null");
            }

            p.Firstname = "Ijaware";
            p.Middlename = "Wisdom";
            p.Surname = "Oluwatomiwa";
            p.MobileNo = "07037290250";
            p.Email = "e911miri@gmail.com";
            p.DOB = DateTime.Now;
            p.Firstname = "Tomiwa";
            p.PartyType = "ContactPerson";
            p.Street = "no 1. Fatai Kadiri Street";
            p.Town = "Alagbaka";
            p.ILGA = "AKURE SOUTH";

            System.Console.WriteLine("Contact Information Added");
            LandRecords.SaveApplication(app.Id, p);


            app2 = LandRecords.GetApplication(app.Id);
            System.Console.WriteLine("Adresses= " + app2.PrimaryProperty.Addresses.Count);
            System.Console.WriteLine("Properties= " + app2.Properties.Count);
            System.Console.WriteLine("Parties= " + app2.Parties.Count);

            SolaService solaSvc = new SolaService(app2.Id);
            var appTO = solaSvc.SubmitToSola();

            LandRecords.UpdateStatus(app.Id, appTO.statusCode, appTO.id, appTO.nr);
            System.Console.WriteLine("Application Submitted to Sola");

            app2 = LandRecords.GetApplication(app.Id);
            System.Console.WriteLine("Application NR: " + app2.SolaNR);
            System.Console.WriteLine("Application Id: " + app2.SolaId);
            System.Console.WriteLine("Application Status: " + app2.Status);
        }
    }
}
