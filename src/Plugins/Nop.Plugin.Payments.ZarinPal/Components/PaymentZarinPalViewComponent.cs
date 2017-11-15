using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Payments.ZarinPal.Models;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Payments.ZarinPal.Components
{
    [ViewComponent(Name = "PaymentZarinPal")]
    public class PaymentZarinPalViewComponent : NopViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Plugins/Payments.ZarinPal/Views/PaymentInfo.cshtml");
        }
    }
}
