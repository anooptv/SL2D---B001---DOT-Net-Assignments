using System;

namespace Client.Models
{
    public class PurchaseOrderModel
    {
        public string PONumber { get; set; }
        public DateTime Date { get; set; }
        public string SupplierNumber { get; set; }
        public string ItemCode { get; set; }
        public int Quantity { get; set; }
    }
}