using BFF.Web.Core.Pagination;

namespace BFF.Web.Interfaces.Integrations
{
    public interface ITranformations<T> where T : class
    {
        ResultPagination<T> TransformSingle(string json);
        ResultPagination<T> TransformPagination(string json, int pageIndex);
    }
}