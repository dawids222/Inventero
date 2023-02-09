using CommunityToolkit.Mvvm.ComponentModel;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Models.Views.Inputs;
using System.Collections.Generic;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class GroupViewModel : ItemViewModel<Group, IGroupStore>
    {
        [ObservableProperty]
        private string _name = string.Empty;

        public GroupViewModel(
            IGroupStore store,
            IViewService viewService,
            IDialogService dialogService)
            : base(store, viewService, dialogService) { }

        protected override IEnumerable<Input> CreateInputs()
        {
            return new Input[]
            {
                new StringInput("Nazwa", nameof(Group.Name)),
            };
        }

        protected override void Load() { }

        protected override Group CreateItem()
        {
            return new Group(Id, Name);
        }

        protected override bool ValidateItem(Group item)
        {
            return !string.IsNullOrWhiteSpace(item.Name);
        }

        public override void LoadItem(Group item)
        {
            Id = item.Id;
            Name = item.Name;
        }

        protected override void GoBack()
        {
            _viewService.ShowGroups();
        }
    }
}
