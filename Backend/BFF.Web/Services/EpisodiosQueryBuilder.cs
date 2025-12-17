using BFF.Web.Enumerators;
using BFF.Web.Filters;
using BFF.Web.Interfaces.Integrations;

namespace BFF.Web.Services
{
    public class EpisodeQueryBuilder : IQueryBuilder<EpisodiosFilter>
    {
        private string ENDPOINT;

        public EpisodeQueryBuilder()
        {
            ENDPOINT = EQueryItem.episode.ToString();
        }

        public string Build(EpisodiosFilter filter)
        {
            string queryParams = ENDPOINT;

            if (filter.PageIndex > 1)
            {
                queryParams = $"{ENDPOINT}?page={filter.PageIndex}";
                return queryParams;
            }

            if (filter.PageIndex == 1 && filter.Id.HasValue)
            {
                queryParams = $"{ENDPOINT}/{filter.Id.Value}";
                return queryParams;
            }

            if (filter.PageIndex == 1 && !string.IsNullOrEmpty(filter.Name))
            {
                queryParams = $"{ENDPOINT}?name={filter.Name.ToLower()}";
                return queryParams;
            }

            return queryParams;
        }
    }
}