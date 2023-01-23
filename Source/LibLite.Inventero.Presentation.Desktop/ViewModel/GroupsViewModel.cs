using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Models.Views;
using System.Collections.Generic;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class GroupsViewModel : PaginatedListViewModel<Group, IGroupStore>
    {
        public GroupsViewModel(IGroupStore store, IDialogService dialogService)
            : base(store, dialogService) { }

        protected override void AddItem()
        {
            return;
        }

        protected override void CreateDataGridColumns(List<Column> columns)
        {
            columns.Add(new Column("Nazwa", nameof(Group.Name)));
        }
    }
}
