// Type: MvcContrib.Pagination.PaginationHelper
// Assembly: MvcContrib, Version=3.0.100.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Users\Alex\Documents\Visual Studio 2012\Projects\MvcApplication1\MvcApplication1\bin\MvcContrib.dll

using System.Collections.Generic;

namespace MvcContrib.Pagination
{
  public static class PaginationHelper
  {
    public static IPagination<T> AsPagination<T>(this IEnumerable<T> source, int pageNumber);
    public static IPagination<T> AsPagination<T>(this IEnumerable<T> source, int pageNumber, int pageSize);
  }
}
