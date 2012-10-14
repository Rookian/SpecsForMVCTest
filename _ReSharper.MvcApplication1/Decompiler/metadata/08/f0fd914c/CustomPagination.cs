// Type: MvcContrib.Pagination.CustomPagination`1
// Assembly: MvcContrib, Version=3.0.100.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Users\Alex\Documents\Visual Studio 2012\Projects\MvcApplication1\MvcApplication1\bin\MvcContrib.dll

using System.Collections;
using System.Collections.Generic;

namespace MvcContrib.Pagination
{
  public class CustomPagination<T> : IPagination<T>, IPagination, IEnumerable<T>, IEnumerable
  {
    public CustomPagination(IEnumerable<T> dataSource, int pageNumber, int pageSize, int totalItems);
    public IEnumerator<T> GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator();
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalItems { get; }
    public int TotalPages { get; }
    public int FirstItem { get; }
    public int LastItem { get; }
    public bool HasPreviousPage { get; }
    public bool HasNextPage { get; }
  }
}
