using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.Models
{
    public class Paging
    {
        public bool HasPreviousPage { get { return (PageIndex > 0); } }
        public bool HasNextPage
        {
            get { return (PageIndex < (Count / PageSize)); }
        }
        public int PageSize { get; set; } = ReadOnlyKeys.PageSize;
        public int PageIndex { get; set; }
        public int Count { get; set; }

    }
}
