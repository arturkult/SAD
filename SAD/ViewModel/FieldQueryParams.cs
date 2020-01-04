using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.ViewModel
{
    public enum OrderDirection
    {
        Ascending,
        Descending,
        None
    }
    public class FieldQueryParams
    {
        public string FilterOperator { get; set; }
        public string FilterValue { get; set; }
        public OrderDirection Order { get; set; }
    }
}
