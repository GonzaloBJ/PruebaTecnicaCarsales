namespace BFF.Web.Interfaces.Integrations
{

    public interface IQueryBuilder<T> where T : class
    {
        string Build(T filter);
    }

}