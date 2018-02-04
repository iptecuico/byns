using Codetecuico.Byns.Api.Models;
using Codetecuico.Byns.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Codetecuico.Byns.Api.Helpers
{
    public static class ItemHelper
    {
        public static IEnumerable<ItemModel> ApplySearch(IEnumerable<ItemModel> items, string searchString)
        {
            if (searchString != null)
            {
                return items.Where(x => x.Name.ToLowerInvariant().Contains(searchString.ToLowerInvariant())
                                    || x.Description.ToLowerInvariant().Contains(searchString.ToLowerInvariant()));
            }

            return items;
        }

        public static IEnumerable<ItemModel> ApplyOrderBy(IEnumerable<ItemModel> items)
        {
            return items.OrderBy(x => x.Name)
                        .Select(x => x);
        }

        public static bool IsItemInvalid(int id)
        {
            if (id <= 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsItemInvalid(Item item)
        {
            if (item == null)
            {
                return true;
            }
            return false;
        }

        public static bool IsItemInvalid(ItemForUpdateModel item)
        {
            if (item == null)
            {
                return true;
            }
            return false;
        }

        public static bool IsItemInvalid(ItemForCreationModel item)
        {
            if (item == null)
            {
                return true;
            }
            return false;
        }
    }
}
