using System.Collections.Generic;
using System.IO;
using Nop.Core;
using Nop.Core.Infrastructure;

using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Infrastructure;


namespace Nop.Plugin.Widgets.CarouselSlider
{
    /// <summary>
    /// PLugin
    /// </summary>
    public class CarouselSliderPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly INopFileProvider _fileProvider;


        public CarouselSliderPlugin (IPictureService pictureService,
            ISettingService settingService, IWebHelper webHelper, INopFileProvider fileProvider)
        {
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._webHelper = webHelper;
            this._fileProvider = fileProvider;

        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { "home_page_top" };
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsCarouselSlider/Configure";
        }

        /// <summary>
        /// Gets a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <param name="viewComponentName">View component name</param>
        public void GetWidgetViewComponent(string widgetZone, out string viewComponentName)
        {
            viewComponentName = "WidgetsCarouselSlider";
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //pictures
            var sampleImagesPath = _fileProvider.MapPath("~/Plugins/Widgets.CarouselSlider/Content/CarouselSlider/sample-images/");
            
            //settings
            var settings = new CarouselSliderSettings
            {
                Picture1Id = _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "banner1.jpg"), MimeTypes.ImagePJpeg, "banner_1").Id,
                Text1 = "",
                Link1 = _webHelper.GetStoreLocation(false),
                Picture2Id = _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "banner2.jpg"), MimeTypes.ImagePJpeg, "banner_2").Id,
                Text2 = "",
                Link2 = _webHelper.GetStoreLocation(false),
                //Picture3Id = _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "banner3.jpg"), MimeTypes.ImagePJpeg, "banner_3").Id,
                //Text3 = "",
                //Link3 = _webHelper.GetStoreLocation(false),
            };
            _settingService.SaveSetting(settings);


            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CarouselSlider.Picture1", "Picture 1");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CarouselSlider.Picture2", "Picture 2");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CarouselSlider.Picture3", "Picture 3");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CarouselSlider.Picture4", "Picture 4");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CarouselSlider.Picture5", "Picture 5");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CarouselSlider.Picture", "Picture");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CarouselSlider.Picture.Hint", "Upload picture.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CarouselSlider.Text", "Comment");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CarouselSlider.Text.Hint", "Enter comment for picture. Leave empty if you don't want to display any text.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CarouselSlider.Link", "URL");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CarouselSlider.Link.Hint", "Enter URL. Leave empty if you don't want this picture to be clickable.");

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<CarouselSliderSettings>();

            //locales
            this.DeletePluginLocaleResource("Plugins.Widgets.CarouselSlider.Picture1");
            this.DeletePluginLocaleResource("Plugins.Widgets.CarouselSlider.Picture2");
            this.DeletePluginLocaleResource("Plugins.Widgets.CarouselSlider.Picture3");
            this.DeletePluginLocaleResource("Plugins.Widgets.CarouselSlider.Picture4");
            this.DeletePluginLocaleResource("Plugins.Widgets.CarouselSlider.Picture5");
            this.DeletePluginLocaleResource("Plugins.Widgets.CarouselSlider.Picture");
            this.DeletePluginLocaleResource("Plugins.Widgets.CarouselSlider.Picture.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.CarouselSlider.Text");
            this.DeletePluginLocaleResource("Plugins.Widgets.CarouselSlider.Text.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.CarouselSlider.Link");
            this.DeletePluginLocaleResource("Plugins.Widgets.CarouselSlider.Link.Hint");

            base.Uninstall();
        }

        public string GetWidgetViewComponentName (string widgetZone)
        {
            return "WidgetsCarouselSlider";
        }
    }
}