using System.Collections.Generic;

namespace Bat.Core
{
    public static class MenuExtension
    {
        public static IEnumerable<MenuModel> GetAllMenu(this IEnumerable<MenuModel> userMenus)
        {
            if (userMenus.IsNull()) return null;

            var result = new List<MenuModel>();
            foreach (var menu in userMenus)
            {
                result.Add(menu);
                if (menu.HasChild) result.AddRange(menu.ChildMenus);
            }

            return result;
        }

        public static List<MenuModel> GetAllMenu(this List<MenuModel> userMenus)
        {
            if (userMenus.IsNull()) return null;

            var result = new List<MenuModel>();
            foreach (var menu in userMenus)
            {
                result.Add(menu);
                if (menu.HasChild) result.AddRange(menu.ChildMenus);
            }

            return result;
        }
    }
}