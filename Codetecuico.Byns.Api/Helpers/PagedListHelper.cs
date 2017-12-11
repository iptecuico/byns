using Codetecuico.Byns.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codetecuico.Byns.Api.Helpers
{
    public static class PagedListHelper<T> where T : class
    {
        public static PagedModel<T> CreatePagedList(IEnumerable<T> list, int pageNumber, int pageSize)
        {
            var skipCount = (pageNumber - 1) * pageSize;

            var pagedModel = new PagedModel<T>();
            pagedModel.PageNumber = pageNumber;
            pagedModel.PageSize = pageSize;
            pagedModel.RecordCount = list.Count();
            pagedModel.PageCount = (int)Math.Ceiling((double)pagedModel.RecordCount / pageSize);
            pagedModel.Data = list.Skip(skipCount)
                                    .Take(pageSize);

            return pagedModel;
        }
    }
}
