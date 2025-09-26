using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace Grocery.App.ViewModels
{
    public partial class GroceryListViewModel : BaseViewModel
    {
        public ObservableCollection<GroceryList> GroceryLists { get; set; }
        private readonly IGroceryListService _groceryListService;
        private List<GroceryList> _allGroceryLists = new();
        public IRelayCommand<string> SearchGroceryListsCommand { get; }

        public GroceryListViewModel(IGroceryListService groceryListService) 
        {
            Title = "Boodschappenlijst";
            _groceryListService = groceryListService;

            _allGroceryLists = _groceryListService.GetAll().ToList();
            GroceryLists = new ObservableCollection<GroceryList>(_allGroceryLists);

            SearchGroceryListsCommand = new RelayCommand<string>(SearchGroceryLists);
        }

        [RelayCommand]
        public async Task SelectGroceryList(GroceryList groceryList)
        {
            Dictionary<string, object> paramater = new() { { nameof(GroceryList), groceryList } };
            await Shell.Current.GoToAsync($"{nameof(Views.GroceryListItemsView)}?Titel={groceryList.Name}", true, paramater);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            _allGroceryLists = _groceryListService.GetAll().ToList();
            GroceryLists.Clear();
            foreach (var list in _allGroceryLists)
                GroceryLists.Add(list);
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            GroceryLists.Clear();
        }

        private void SearchGroceryLists(string searchTerm)
        {
            GroceryLists.Clear();

            IEnumerable<GroceryList> query = _allGroceryLists;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(l =>
                    l.Name != null &&
                    l.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            foreach (var list in query)
                GroceryLists.Add(list);
        }

    }
}
