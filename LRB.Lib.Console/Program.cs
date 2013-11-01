using LRB.Lib.Domain;
using LRB.Lib.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRB.Lib.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            //var app = new Application()
            //{
            //    StartDate = DateTime.Now,
            //    UserId = "smokes",//user.UserName,
            //    ApplicationType = "Certificate of Occupancy"
            //};
            //UnitOfWork uow = new UnitOfWork();
            //uow.LandApplicationRepository.Insert(app);
            //uow.Save();

            var r = new Requirement()
            {
                applicationType = "Individual",
                landSize = 10000,
                landSizeUnit = "Hectares",
                landUse = "Commercial"
            };

            var app = LandRecords.CreateApplication(r);

            var app2 = LandRecords.GetApplication(app.Id);
            var prop = app.PrimaryProperty;

            prop.PropertyAddress = prop.getPropertyAddress();
            prop.PropertyAddress.Street = "Sohomo Were Avnue";
            prop.PropertyAddress.Town = "Ondo Town";
            prop.PropertyAddress.State = "Ondo State";
            prop.PropertyAddress.LGA = "AKure South";

            LandRecords.SaveApplication(5, prop);

            System.Console.WriteLine("Adresses= " + app.PrimaryProperty.Addresses.Count);
            System.Console.WriteLine("Properties= " + app.Properties.Count);
            System.Console.WriteLine("Parties= " + app.Parties.Count);
            System.Console.ReadKey();
            

            //UnitOfWork uow2 = new UnitOfWork();

            //Party p = uow2.Context.Parties.Where(a => a.PartyType == "ContactPerson" && a.Application.Id == 2).FirstOrDefault();
            //if (p == null)
            //{
                
            //    uow2.Context.Parties.Add(p);
            //}
            //var p = LandRecords.GetContactPerson(2);
            //if (p==null)
            //{
            //    p = new Party();
            //}
            //p.Firstname = "Ijaware";
            //p.Middlename = "Wisdom";
            //p.Surname = "Oluwatomiwa";
            //p.MobileNo = "07037290250";
            //p.Email = "e911miri@gmail.com";
            //p.Firstname = "Tomiwa";
            //p.PartyType = "ContactPerson";

            //LandRecords.SaveApplication(2, p);
            //uow2.Context.SaveChanges();

            //var app2 = uow2.LandApplicationRepository.GetByID(5);

            //app2.Parties.Add(p);

            //uow2.Save();


            //uow.Context.Parties.Add(p);
            //uow.Save();

            //LandRecords.SaveApplication(app.Id, p);
        }
    }
}
