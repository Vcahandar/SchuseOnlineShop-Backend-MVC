using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Services.Interfaces;
using SchuseOnlineShop.ViewModels.Layout;

namespace SchuseOnlineShop.ViewComponents
{
    public class FooterViewComponent: ViewComponent
    {
        private readonly ILayoutService _layoutService;
        public FooterViewComponent(ILayoutService layoutService)
        {
           _layoutService = layoutService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            LayoutVM model = new()
            {
                GetSettingDatas = _layoutService.GetSettings()
            };
            return await Task.FromResult(View(model));
        }
    }
}
