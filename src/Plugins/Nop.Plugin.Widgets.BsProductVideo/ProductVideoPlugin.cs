using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Plugin.Widgets.BsProductVideo.Data;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.BsProductVideo
{
    /// <summary>
    /// PLugin
    /// </summary>
    public class ProductVideoPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly ProductVideoObjectContext _objectContext;

        public ProductVideoPlugin(IPictureService pictureService,
            ISettingService settingService, IWebHelper webHelper, ProductVideoObjectContext objectContext)
        {
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._webHelper = webHelper;
            this._objectContext = objectContext;
        }



        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { "productdetails_after_pictures"};
        }


        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/ProductVideo/Configure";
        }

        /// <summary>
        /// Gets a view component for displaying plugin in public store
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <param name="viewComponentName">View component name</param>
        public void GetPublicViewComponent(string widgetZone, out string viewComponentName)
        {
            viewComponentName = "WidgetsProductVideo";
        }
       

       
        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //save settings
            
            //_settingService.SaveSetting(settings);


            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Widgets.BsProductVideo.Button.AddVideo", "Add Video/Audio");
            this.AddOrUpdatePluginLocaleResource("Plugin.Widgets.BsProductVideo.ProductId", "Product Id");

            this.AddOrUpdatePluginLocaleResource("Plugin.Widgets.BsProductVideo.ProductName", "Product Name");
            this.AddOrUpdatePluginLocaleResource("Plugin.Widgets.BsProductVideo.EmbedVideoHtmlCode", "Embed Video Audio Html Code");
            this.AddOrUpdatePluginLocaleResource("Plugin.Widgets.BsProductVideo.Picture", "Thumbnail");
            this.AddOrUpdatePluginLocaleResource("Plugin.Widgets.BsProductVideo.DisplayOrder", "Display Order");
            
               //data
            _objectContext.Install();


            base.Install();


           
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            //_settingService.DeleteSetting<SettingsName>();

            //locales
            this.DeletePluginLocaleResource("key");

            this.DeletePluginLocaleResource("Nop.Plugin.Widgets.BsProductVideo.Button.AddVideo");
            this.DeletePluginLocaleResource("Plugin.Widgets.BsProductVideo.ProductId");

            this.DeletePluginLocaleResource("Plugin.Widgets.BsProductVideo.ProductName");
            this.DeletePluginLocaleResource("Plugin.Widgets.BsProductVideo.EmbedVideoHtmlCode");
            this.DeletePluginLocaleResource("Plugin.Widgets.BsProductVideo.Picture");
            this.DeletePluginLocaleResource("Plugin.Widgets.BsProductVideo.DisplayOrder");
            
           //data
            _objectContext.Uninstall();

            base.Uninstall();
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {

           

            var menuItem = new SiteMapNode()
            {
                Visible = true,
                Title = "Product Video/Audio",
                Url = ""
            };

            var menuItemProductList = new SiteMapNode()
            {
                Visible = true,
                Title = "Configure",
                Url = "/Admin/ProductVideo/List"
                
            };

            menuItem.ChildNodes.Add(menuItemProductList);

            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "nopSohel");
            if (pluginNode != null)
                pluginNode.ChildNodes.Add(menuItem);
            else
            {
                var sohel = new SiteMapNode()
                {
                    Visible = true,
                    Title = "SohelPlugins",
                    Url = "",
                    SystemName = "nopSohel",
                    IconClass= "fa fa - bars"
                };
                sohel.ChildNodes.Add(menuItem);

                rootNode.ChildNodes.Add(sohel);
            }

           // rootNode.ChildNodes.Add(menuItemBuilder);
            
        }

        /// <summary>
        /// Gets a view component for displaying plugin in public store
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        public string GetWidgetViewComponentName (string widgetZone)
        {
            return "WidgetsProductVideo";
        }
    }
}
