using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application
{
    public class ItemListDTO<T>
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages
        { 
            get
            {
                var totalPages = (int)Math.Ceiling(FilteredCount / (double)PageSize);
                if (totalPages == 0)
                    totalPages = 1;

                return totalPages;
            }
        }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public long TotalCount { get; set; }
        public long FilteredCount { get; set; }

        public ItemListDTO(List<T> items, int pageIndex, int totalPages)
        {
            Items = items;
            PageIndex = pageIndex;
        }

        public ItemListDTO()
        {
            
        }
    }
}
