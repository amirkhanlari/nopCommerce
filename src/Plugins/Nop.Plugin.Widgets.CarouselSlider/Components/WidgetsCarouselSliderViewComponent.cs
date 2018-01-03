using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.CarouselSlider.Infrastructure.Cache;
using Nop.Plugin.Widgets.CarouselSlider.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.CarouselSlider.Components
{
    [ViewComponent(Name = "WidgetsCarouselSlider")]
    public class WidgetsCarouselSliderViewComponent : NopViewComponent
    {
        private readonly IStoreContext _storeContext;
        private readonly IStaticCacheManager _cacheManager;
        private readonly ISettingService _settingService;
        private readonly IPictureService _pictureService;

        public WidgetsCarouselSliderViewComponent(IStoreContext storeContext, 
            IStaticCacheManager cacheManager, 
            ISettingService settingService, 
            IPictureService pictureService)
        {
            this._storeContext = storeContext;
            this._cacheManager = cacheManager;
            this._settingService = settingService;
            this._pictureService = pictureService;
        }

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var CarouselSliderSettings = _settingService.LoadSetting<CarouselSliderSettings>(_storeContext.CurrentStore.Id);

            var model = new PublicInfoModel
            {
                Picture1Url = GetPictureUrl(CarouselSliderSettings.Picture1Id),
                Text1 = CarouselSliderSettings.Text1,
                Link1 = CarouselSliderSettings.Link1,

                Picture2Url = GetPictureUrl(CarouselSliderSettings.Picture2Id),
                Text2 = CarouselSliderSettings.Text2,
                Link2 = CarouselSliderSettings.Link2,

                Picture3Url = GetPictureUrl(CarouselSliderSettings.Picture3Id),
                Text3 = CarouselSliderSettings.Text3,
                Link3 = CarouselSliderSettings.Link3,

                Picture4Url = GetPictureUrl(CarouselSliderSettings.Picture4Id),
                Text4 = CarouselSliderSettings.Text4,
                Link4 = CarouselSliderSettings.Link4,

                Picture5Url = GetPictureUrl(CarouselSliderSettings.Picture5Id),
                Text5 = CarouselSliderSettings.Text5,
                Link5 = CarouselSliderSettings.Link5
            };

            if (string.IsNullOrEmpty(model.Picture1Url) && string.IsNullOrEmpty(model.Picture2Url) &&
                string.IsNullOrEmpty(model.Picture3Url) && string.IsNullOrEmpty(model.Picture4Url) &&
                string.IsNullOrEmpty(model.Picture5Url))
                //no pictures uploaded
                return Content("");

            return View("~/Plugins/Widgets.CarouselSlider/Views/PublicInfo.cshtml", model);
        }

        protected string GetPictureUrl(int pictureId)
        {
            var cacheKey = string.Format(ModelCacheEventConsumer.PICTURE_URL_MODEL_KEY, pictureId);
            return _cacheManager.Get(cacheKey, () =>
            {
                //little hack here. nulls aren't cacheable so set it to ""
                var url = _pictureService.GetPictureUrl(pictureId, showDefaultPicture: false) ?? "";
                return url;
            });
        }
    }
}
