using Domain.Filters.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Filters
{
    public class ImageFilter
    {
        public List<SearchString> Id { get; set; } = new List<SearchString>();
    }
}
