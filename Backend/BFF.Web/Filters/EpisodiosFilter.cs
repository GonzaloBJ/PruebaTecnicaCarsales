namespace BFF.Web.Filters
{
    public class EpisodiosFilter
    {
        public int PageIndex { get; set; } = 1;
        public int? Id { get; set; }
        public string? Name { get; set; }    
    }
}