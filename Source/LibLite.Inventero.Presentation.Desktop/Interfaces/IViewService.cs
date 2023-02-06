using LibLite.Inventero.Core.Models.Domain;

namespace LibLite.Inventero.Presentation.Desktop.Interfaces
{
    public interface IViewService
    {
        void ShowPurchasesView();
        void ShowProductsView();
        void ShowGroupsView();

        void ShowPurchaseView();
        void ShowProductView();
        void ShowGroupView();

        void ShowPurchaseView(Purchase purchase);
        void ShowProductView(Product product);
        void ShowGroupView(Group group);
    }
}
