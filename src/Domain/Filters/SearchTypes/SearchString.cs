using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Filters.SearchTypes
{
    public class SearchString
    {
        public SearchString(SearchType type, string value)
        {
            Type = type;
            Value = value;
        }

        public SearchType Type { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}
