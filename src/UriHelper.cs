
using System;

namespace BlackDigital
{
    public static class UriHelper
    {
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
