using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.ViewModel;
using LibLite.Inventero.Presentation.Desktop.ViewModel.Item;

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

        public void ShowGroups()
        {
            ShowView<GroupsViewModel>();
        }

        public void ShowGroup()
        {
            ShowView<GroupViewModel>();
        }

        public void ShowGroup(Group group)
        {
            var viewModel = ShowView<GroupViewModel>();
            viewModel.LoadItem(group);
        }

        public void ShowProducts()
        {
            ShowView<ProductsViewModel>();
        }

        public void ShowProduct()
        {
            ShowView<ProductViewModel>();
        }

        public void ShowProduct(Product product)
        {
            var viewModel = ShowView<ProductViewModel>();
            viewModel.LoadItem(product);
        }

        public void ShowPurchases()
        {
            ShowView<PurchasesViewModel>();
        }

        public void ShowPurchase()
        {
            ShowView<PurchaseViewModel>();
        }

        public void ShowPurchase(Purchase purchase)
        {
            var viewModel = ShowView<PurchaseViewModel>();
            viewModel.LoadItem(purchase);
        }

        private TViewModel ShowView<TViewModel>()
            where TViewModel : ViewModelBase
        {
            var viewModel = _viewModelService.Get<TViewModel>();
            _mainViewModel.ContentViewModel = viewModel;
            return viewModel;
        }
    }
}
