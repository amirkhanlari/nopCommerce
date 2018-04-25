using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.CarouselSlider.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.CarouselSlider.Controllers
{
    [Area(AreaNames.Admin)]
    public class WidgetsCarouselSliderController : BasePluginController
    {
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;

        public WidgetsCarouselSliderController(IStoreContext storeContext,
            IStoreService storeService,
            IPermissionService permissionService, 
            IPictureService pictureService,
            ISettingService settingService,
            ICacheManager cacheManager,
            ILocalizationService localizationService)
        {
            this._storeContext = storeContext;
            this._storeService = storeService;
            this._permissionService = permissionService;
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._localizationService = localizationService;
        }

        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var CarouselSliderSettings = _settingService.LoadSetting<CarouselSliderSettings>(storeScope);
            var model = new ConfigurationModel
            {
                Picture1Id = CarouselSliderSettings.Picture1Id,
                Text1 = CarouselSliderSettings.Text1,
                Link1 = CarouselSliderSettings.Link1,
                Picture2Id = CarouselSliderSettings.Picture2Id,
                Text2 = CarouselSliderSettings.Text2,
                Link2 = CarouselSliderSettings.Link2,
                Picture3Id = CarouselSliderSettings.Picture3Id,
                Text3 = CarouselSliderSettings.Text3,
                Link3 = CarouselSliderSettings.Link3,
                Picture4Id = CarouselSliderSettings.Picture4Id,
                Text4 = CarouselSliderSettings.Text4,
                Link4 = CarouselSliderSettings.Link4,
                Picture5Id = CarouselSliderSettings.Picture5Id,
                Text5 = CarouselSliderSettings.Text5,
                Link5 = CarouselSliderSettings.Link5,
                ActiveStoreScopeConfiguration = storeScope
            };

            if (storeScope > 0)
            {
                model.Picture1Id_OverrideForStore = _settingService.SettingExists(CarouselSliderSettings, x => x.Picture1Id, storeScope);
                model.Text1_OverrideForStore = _settingService.SettingExists(CarouselSliderSettings, x => x.Text1, storeScope);
                model.Link1_OverrideForStore = _settingService.SettingExists(CarouselSliderSettings, x => x.Link1, storeScope);
                model.Picture2Id_OverrideForStore = _settingService.SettingExists(CarouselSliderSettings, x => x.Picture2Id, storeScope);
                model.Text2_OverrideForStore = _settingService.SettingExists(CarouselSliderSettings, x => x.Text2, storeScope);
                model.Link2_OverrideForStore = _settingService.SettingExists(CarouselSliderSettings, x => x.Link2, storeScope);
                model.Picture3Id_OverrideForStore = _settingService.SettingExists(CarouselSliderSettings, x => x.Picture3Id, storeScope);
                model.Text3_OverrideForStore = _settingService.SettingExists(CarouselSliderSettings, x => x.Text3, storeScope);
                model.Link3_OverrideForStore = _settingService.SettingExists(CarouselSliderSettings, x => x.Link3, storeScope);
                model.Picture4Id_OverrideForStore = _settingService.SettingExists(CarouselSliderSettings, x => x.Picture4Id, storeScope);
                model.Text4_OverrideForStore = _settingService.SettingExists(CarouselSliderSettings, x => x.Text4, storeScope);
                model.Link4_OverrideForStore = _settingService.SettingExists(CarouselSliderSettings, x => x.Link4, storeScope);
                model.Picture5Id_OverrideForStore = _settingService.SettingExists(CarouselSliderSettings, x => x.Picture5Id, storeScope);
                model.Text5_OverrideForStore = _settingService.SettingExists(CarouselSliderSettings, x => x.Text5, storeScope);
                model.Link5_OverrideForStore = _settingService.SettingExists(CarouselSliderSettings, x => x.Link5, storeScope);
            }

            return View("~/Plugins/Widgets.CarouselSlider/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var CarouselSliderSettings = _settingService.LoadSetting<CarouselSliderSettings>(storeScope);

            //get previous picture identifiers
            var previousPictureIds = new[] 
            {
                CarouselSliderSettings.Picture1Id,
                CarouselSliderSettings.Picture2Id,
                CarouselSliderSettings.Picture3Id,
                CarouselSliderSettings.Picture4Id,
                CarouselSliderSettings.Picture5Id
            };

            CarouselSliderSettings.Picture1Id = model.Picture1Id;
            CarouselSliderSettings.Text1 = model.Text1;
            CarouselSliderSettings.Link1 = model.Link1;
            CarouselSliderSettings.Picture2Id = model.Picture2Id;
            CarouselSliderSettings.Text2 = model.Text2;
            CarouselSliderSettings.Link2 = model.Link2;
            CarouselSliderSettings.Picture3Id = model.Picture3Id;
            CarouselSliderSettings.Text3 = model.Text3;
            CarouselSliderSettings.Link3 = model.Link3;
            CarouselSliderSettings.Picture4Id = model.Picture4Id;
            CarouselSliderSettings.Text4 = model.Text4;
            CarouselSliderSettings.Link4 = model.Link4;
            CarouselSliderSettings.Picture5Id = model.Picture5Id;
            CarouselSliderSettings.Text5 = model.Text5;
            CarouselSliderSettings.Link5 = model.Link5;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            _settingService.SaveSettingOverridablePerStore(CarouselSliderSettings, x => x.Picture1Id, model.Picture1Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(CarouselSliderSettings, x => x.Text1, model.Text1_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(CarouselSliderSettings, x => x.Link1, model.Link1_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(CarouselSliderSettings, x => x.Picture2Id, model.Picture2Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(CarouselSliderSettings, x => x.Text2, model.Text2_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(CarouselSliderSettings, x => x.Link2, model.Link2_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(CarouselSliderSettings, x => x.Picture3Id, model.Picture3Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(CarouselSliderSettings, x => x.Text3, model.Text3_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(CarouselSliderSettings, x => x.Link3, model.Link3_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(CarouselSliderSettings, x => x.Picture4Id, model.Picture4Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(CarouselSliderSettings, x => x.Text4, model.Text4_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(CarouselSliderSettings, x => x.Link4, model.Link4_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(CarouselSliderSettings, x => x.Picture5Id, model.Picture5Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(CarouselSliderSettings, x => x.Text5, model.Text5_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(CarouselSliderSettings, x => x.Link5, model.Link5_OverrideForStore, storeScope, false);
            
            //now clear settings cache
            _settingService.ClearCache();
            
            //get current picture identifiers
            var currentPictureIds = new[]
            {
                CarouselSliderSettings.Picture1Id,
                CarouselSliderSettings.Picture2Id,
                CarouselSliderSettings.Picture3Id,
                CarouselSliderSettings.Picture4Id,
                CarouselSliderSettings.Picture5Id
            };

            //delete an old picture (if deleted or updated)
            foreach (var pictureId in previousPictureIds.Except(currentPictureIds))
            { 
                var previousPicture = _pictureService.GetPictureById(pictureId);
                if (previousPicture != null)
                    _pictureService.DeletePicture(previousPicture);
            }

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
    }
}