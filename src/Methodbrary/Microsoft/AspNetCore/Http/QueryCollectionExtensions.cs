using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Methodbrary.Microsoft.AspNetCore.Http
{
    public static class QueryCollectionExtensions
    {
        public static string ByAlias(this IQueryCollection query, params string[] aliases) => 
            query.Keys
                .SingleOrDefault(k 
                    => aliases
                        .Select(a => a.ToLowerInvariant())
                        .Contains(k.ToLowerInvariant())
                ) != null
                ? (string) query[query.Keys.SingleOrDefault(k => aliases.Select(a => a.ToLowerInvariant()).Contains(k.ToLowerInvariant()))]
                : string.Empty;
    }
}