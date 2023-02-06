using CommunityToolkit.Mvvm.ComponentModel;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.ViewModel;

namespace LibLite.Inventero.Presentation.Desktop.Services
{
    public class ViewService : IViewService
    {
        private readonly MainWindowViewModel _mainViewModel;
        private readonly IViewModelService _viewModelService;

        public ViewService(
            MainWindowViewModel mainViewModel,
            IViewModelService viewModelService)
        {
            _mainViewModel = mainViewModel;
            _viewModelService = viewModelService;
        }

        public void ShowGroupsView()
        {
            var viewModel = ShowView<GroupsViewModel>();
            viewModel.LoadItemsCommand.Execute(null);
        }

        public void ShowGroupView()
        {
            ShowView<GroupViewModel>();
        }

        public void ShowGroupView(Group group)
        {
            var viewModel = ShowView<GroupViewModel>();
            viewModel.LoadItem(group);
        }

        public void ShowProductsView()
        {
            var viewModel = ShowView<ProductsViewModel>();
            viewModel.LoadItemsCommand.Execute(null);
        }

        public void ShowProductView()
        {
            ShowView<ProductViewModel>();
        }

        public void ShowProductView(Product product)
        {
            var viewModel = ShowView<ProductViewModel>();
            viewModel.LoadItem(product);
        }

        public void ShowPurchasesView()
        {
            var viewModel = ShowView<PurchasesViewModel>();
            viewModel.LoadItemsCommand.Execute(null);
        }

        public void ShowPurchaseView()
        {
            ShowView<PurchaseViewModel>();
        }

        public void ShowPurchaseView(Purchase purchase)
        {
            var viewModel = ShowView<PurchaseViewModel>();
            viewModel.LoadItem(purchase);
        }

        private TViewModel ShowView<TViewModel>()
            where TViewModel : ObservableObject
        {
            var viewModel = _viewModelService.Get<TViewModel>();
            _mainViewModel.MainViewModel = viewModel;
            return viewModel;
        }
    }
}
