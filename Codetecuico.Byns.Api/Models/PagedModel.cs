using System.Collections.Generic;

namespace Codetecuico.Byns.Api.Models
{
    public class PagedModel<T> where T : class
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public int RecordCount { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}