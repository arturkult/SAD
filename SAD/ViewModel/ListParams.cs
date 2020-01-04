using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.ViewModel
{
    public class ListParams
    {
        public Dictionary<string, FieldQueryParams> FieldQueryParams { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
