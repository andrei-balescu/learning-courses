using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace OdeToFood.ViewComponents
{
    public class ServerIdViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            const string c_serverIdVariable = "ODETOFOOD_SERVERID";
            string serverId = Environment.GetEnvironmentVariable(c_serverIdVariable);

            ViewViewComponentResult result = View<string>(serverId);
            return result;
        }
    }
}