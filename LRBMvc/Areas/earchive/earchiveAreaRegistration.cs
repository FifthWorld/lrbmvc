using System.Web.Mvc;

namespace LRBMvc.Areas.earchive
{
    public class earchiveAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "earchive";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "earchive_default",
                "earchive/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
