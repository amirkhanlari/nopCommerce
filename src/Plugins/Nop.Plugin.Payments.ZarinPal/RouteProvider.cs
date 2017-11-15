using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Payments.ZarinPal
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(IRouteBuilder routes)
        {
            //PDT
            routes.MapRoute("Plugin.Payments.ZarinPal.PDTHandler",
                 "Plugins/PaymentZarinPal/PDTHandler",
                 new { controller = "PaymentZarinPal", action = "PDTHandler" }
            );
            //Cancel
            routes.MapRoute("Plugin.Payments.ZarinPal.CancelOrder",
                 "Plugins/PaymentZarinPal/CancelOrder",
                 new { controller = "PaymentZarinPal", action = "CancelOrder" }
            );
        }
        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
