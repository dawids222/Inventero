using LibLite.Inventero.Core.Models.Domain;

namespace LibLite.Inventero.Presentation.Desktop.Interfaces
{
    public interface IViewService
    {
        void ShowPurchases();
        void ShowProducts();
        void ShowGroups();

        void ShowPurchase();
        void ShowProduct();
        void ShowGroup();

        void ShowPurchase(Purchase purchase);
        void ShowProduct(Product product);
        void ShowGroup(Group group);
    }
}
