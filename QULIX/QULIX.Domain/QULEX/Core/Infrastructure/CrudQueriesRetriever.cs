namespace QULIX.Domain.QULEX.Core.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Newtonsoft.Json;

    using QULIX.Domain.QULEX.Core.Constants;
    using QULIX.Domain.QULEX.Core.Enums;

    public static class CrudQueriesRetriever
    {
        /// <summary>
        /// The registered queries mappers.
        /// </summary>
        private static readonly IDictionary<string, IReadOnlyDictionary<CrudQueriesEnum, string>> RegisteredDataRetrieverQueries 
            = new Dictionary<string, IReadOnlyDictionary<CrudQueriesEnum, string>>();

        public static string PathPrefix = null;

        /// <summary>
        /// The get queries.
        /// </summary>
        /// <param name="path"> The path.  </param>
        /// <typeparam name="TDataRetriever"> </typeparam>
        /// <returns> The <see cref="IReadOnlyDictionary"/>. </returns>
        internal static IReadOnlyDictionary<CrudQueriesEnum, string> GetQueriesDictionary<TDataRetriever>(string path = PathsToSourceFiles.DataRetrieversQueriesFiles)
            where TDataRetriever : IDisposable
        {
            string dataRetrieverClassName = typeof(TDataRetriever).Name;
            var newPath = string.Concat(PathPrefix ?? string.Empty, path);
            string sourceFile = $"{newPath}/{dataRetrieverClassName}Queries.json";
            if (RegisteredDataRetrieverQueries.ContainsKey(sourceFile))
            {
                return RegisteredDataRetrieverQueries[sourceFile];
            }

            var result = JsonConvert.DeserializeObject<IReadOnlyDictionary<CrudQueriesEnum, string>>(File.ReadAllText(sourceFile));
            RegisteredDataRetrieverQueries.Add(sourceFile, result);

            return result;
        }
    }
}
