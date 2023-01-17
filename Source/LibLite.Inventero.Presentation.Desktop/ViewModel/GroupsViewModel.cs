using CommunityToolkit.Mvvm.ComponentModel;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Core.Models.Pagination;
using System.Threading.Tasks;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class GroupsViewModel : ObservableObject
    {
        private readonly IGroupStore _store;

        [ObservableProperty]
        private PaginatedList<Group> _groups;

        public GroupsViewModel(IGroupStore store)
        {
            _store = store;

            InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            Groups = await _store.GetAsync(new PaginatedListRequest());
        }
    }
}
