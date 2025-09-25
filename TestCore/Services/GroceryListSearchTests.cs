using Grocery.Core.Models;
using Grocery.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Core.Tests.Services
{
    [TestFixture]
    public class GroceryListSearchTests
    {
        private static List<GroceryList> Seed() => new()
        {
            new GroceryList(1, "Week 1", DateOnly.FromDateTime(DateTime.Today), "red", 0),
            new GroceryList(2, "Boodschappen Zaterdag", DateOnly.FromDateTime(DateTime.Today), "blue", 0),
            new GroceryList(3, "week 2", DateOnly.FromDateTime(DateTime.Today), "green", 0),
        };

        [Test]
        public void EmptyTerm_ReturnsAll()
        {
            var src = Seed();

            var result = GroceryListSearch.FilterByName(src, "");

            Assert.That(result.Count(), Is.EqualTo(src.Count));
        }

        [Test]
        public void TermWithMatch_IsCaseInsensitive_AndFilters()
        {
            var src = Seed();

            var result = GroceryListSearch.FilterByName(src, "week").ToList();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.All(l => l.Name.Contains("week", StringComparison.OrdinalIgnoreCase)));
        }

        [Test]
        public void NoMatch_ReturnsEmpty()
        {
            var src = Seed();

            var result = GroceryListSearch.FilterByName(src, "PIZZA");

            Assert.That(result, Is.Empty);
        }
    }
}
