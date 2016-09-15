using System.Web.Mvc;

namespace ConnApsWebAPI.Areas.BuildingManager
{
    public class BuildingManagerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BuildingManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BuildingManager_default",
                "api/BuildingManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}