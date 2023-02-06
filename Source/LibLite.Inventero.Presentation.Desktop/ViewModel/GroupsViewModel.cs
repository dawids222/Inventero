using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Contracts.Tools;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Presentation.Desktop.Enums;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Models.Events;
using LibLite.Inventero.Presentation.Desktop.Models.Views;
using System.Collections.Generic;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class GroupsViewModel : PaginatedListViewModel<Group, IGroupStore>
    {
        private readonly IEventBus _bus;

        public GroupsViewModel(IGroupStore store, IDialogService dialogService, IEventBus bus)
            : base(store, dialogService)
        {
            _bus = bus;
        }

        protected override void AddItem()
        {
            // TODO: Move to a common method?!
            var @event = new ChangeMainViewEvent(MainView.Group);
            _bus.Publish(@event);
        }

        protected override void CreateDataGridColumns(List<Column> columns)
        {
            columns.Add(new Column("Nazwa", nameof(Group.Name)));
        }
    }
}
