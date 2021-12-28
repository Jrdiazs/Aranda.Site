using Aranda.Services;
using Aranda.Services.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aranda.Site.ViewComponents
{
	public class NavigationMenuViewComponent : ViewComponent
	{
		private readonly IRolMenuServices _services;

		public NavigationMenuViewComponent(IRolMenuServices services)
		{
			_services = services;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var menus = new MenuResponseListItems();
			var claim = HttpContext.User.FindFirst("Id");
			await Task.Run(() => 
			menus = _services.GetMenus(int.Parse(claim.Value))
			);

			return View(menus);
		}
	}
}
