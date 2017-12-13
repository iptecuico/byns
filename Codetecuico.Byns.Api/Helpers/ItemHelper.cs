using Codetecuico.Byns.Api.Models;
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
                searchString = searchString.ToLowerInvariant();
                items = items.Where(x => x.Name.ToLowerInvariant().Contains(searchString) 
                                    || x.Description.ToLowerInvariant().Contains(searchString));
            }

            return items;
        }

        public static IEnumerable<ItemModel> ApplyOrderBy(IEnumerable<ItemModel> items)
        {
            return items.OrderBy(x => x.Name)
                        .Select(x => x);
        }
    }
}
