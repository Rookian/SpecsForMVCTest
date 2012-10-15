using System.Collections;
using System.Collections.Generic;
using MvcContrib.Pagination;

namespace MvcApplication1.Models
{
    public class Data
    {
        public int Id { get; set; }
        public string Name { get; set; } 
    }

    public class ViewModel
    {
        public IPagination<Data> List { get; set; }
        public SearchFilter SearchFilter { get; set; }
        public string Test { get; set; }
    }

    public class SearchFilter
    {
        public string Name { get; set; }
    }

    class PagedList<T> : IPagination<T>
    {
        private readonly IEnumerable<T> _enumerable;

        public PagedList(IEnumerable<T>enumerable)
        {
            _enumerable = enumerable;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _enumerable.GetEnumerator();
        }

        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalItems { get; private set; }
        public int TotalPages { get; private set; }
        public int FirstItem { get; private set; }
        public int LastItem { get; private set; }
        public bool HasPreviousPage { get; private set; }
        public bool HasNextPage { get; private set; }
    }
}