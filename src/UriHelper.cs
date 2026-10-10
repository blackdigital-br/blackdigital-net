
using System;

namespace BlackDigital
{
    /// <summary>
    /// Provides extension methods for building URIs and parsing query strings.
    /// </summary>
    public static class UriHelper
    {
        /// <summary>
        /// Appends path segments to a <see cref="Uri"/>, preserving the original query string.
        /// </summary>
        /// <param name="uri">The base URI.</param>
        /// <param name="paths">The path segments to append.</param>
        /// <returns>A new <see cref="Uri"/> with the appended paths.</returns>
        public static Uri Append(this Uri uri, params string[] paths)
        {
            string path = String.Format("{0}{1}{2}{3}", uri.Scheme,
                Uri.SchemeDelimiter, uri.Authority, uri.AbsolutePath);

            Uri newUri = new(path);

            newUri = new Uri(paths.Aggregate(newUri.AbsoluteUri, (current, path) => string.Format("{0}/{1}", current.TrimEnd('/'), path.TrimStart('/'))));

            UriBuilder builder = new(newUri);
            builder.Query = uri.Query;

            return builder.Uri;
        }

        /// <summary>
        /// Parses the query string of a <see cref="Uri"/> into a dictionary of key-value pairs.
        /// </summary>
        /// <param name="uri">The URI whose query string should be parsed.</param>
        /// <returns>A dictionary of query parameters.</returns>
        public static Dictionary<string, string> GetQueryString(this Uri uri)
        {
            if (uri == null || string.IsNullOrWhiteSpace(uri.Query))
                return new();

            var query = uri.Query.Substring(1);

            return query.Split('&')
                            .ToDictionary(q => q.Split('=')[0], q => q.Split('=')[1]);
        }
    }
}
