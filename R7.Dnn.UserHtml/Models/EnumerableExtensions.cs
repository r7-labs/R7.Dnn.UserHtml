using System.Collections.Generic;
using System.Linq;

namespace R7.Dnn.UserHtml.Models
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T> (this IEnumerable<T> source) => source == null || !source.Any ();
    }
}
