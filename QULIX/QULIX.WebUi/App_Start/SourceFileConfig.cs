using QULIX.Domain.QULEX.Core.Infrastructure;

namespace QULIX.WebUi.App_Start
{
    public class SourceFileConfig
    {
        public static void RegisterSources(string pathPrefix)
        {
            CrudQueriesRetriever.PathPrefix = pathPrefix;
        }
    }
}