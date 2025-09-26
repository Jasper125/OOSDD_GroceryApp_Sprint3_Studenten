using Grocery.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Core.Services
{
    public static class GroceryListSearch
    {
        /// <summary>
        /// Filtert boodschappenlijsten op naam (case-insensitive).
        /// </summary>
        public static IEnumerable<GroceryList> FilterByName(IEnumerable<GroceryList> source, string? term)
        {
            if (source is null) return Enumerable.Empty<GroceryList>();
            if (string.IsNullOrWhiteSpace(term)) return source;

            string t = term.Trim();
            return source.Where(l => l?.Name != null &&
                                     l.Name.Contains(t, StringComparison.OrdinalIgnoreCase));
        }
    }
}
