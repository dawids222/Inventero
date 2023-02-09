using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Models.Views;
using LibLite.Inventero.Presentation.Desktop.Resources;
using System.Collections.Generic;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class GroupsViewModel : ItemsViewModel<Group, IGroupStore>
    {
        public GroupsViewModel(
            IGroupStore store,
            IDialogService dialogService,
            IViewService viewService)
            : base(store, dialogService, viewService) { }

        protected override void AddItem()
        {
            _viewService.ShowGroup();
        }

        protected override void EditItem(Group item)
        {
            _viewService.ShowGroup(item);
        }

        protected override List<Column> CreateColumns()
        {
            return new List<Column>()
            {
                new Column(Strings.GroupsNameHeader, nameof(Group.Name)),
            };
        }
    }
}
