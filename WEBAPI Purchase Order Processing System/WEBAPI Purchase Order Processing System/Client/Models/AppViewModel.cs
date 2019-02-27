using System.Collections.Generic;

namespace Client.Models
{
    public class AppViewModel
    {
        public List<SupplierModel> Suppliers { get; set; } = new List<SupplierModel>();
        public List<ItemModel> Items { get; set; } = new List<ItemModel>();
        public List<PurchaseOrderModel> PurchaseOrders { get; set; } = new List<PurchaseOrderModel>();

    }
}