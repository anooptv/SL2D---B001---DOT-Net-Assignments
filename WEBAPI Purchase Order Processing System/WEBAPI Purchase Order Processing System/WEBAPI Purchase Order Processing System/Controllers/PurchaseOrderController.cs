using WEBAPI_Purchase_Order_Processing_System.DataObjects;
using WEBAPI_Purchase_Order_Processing_System.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace WEBAPI_Purchase_Order_Processing_System.Controllers
{
    [RoutePrefix("api/purchase")]
    public class PurchaseOrderController : ApiController
    {
        private PODbEntities dbContext = new PODbEntities();

        [Route("create")]
        [HttpPost]
        public void CreatePurchaseOrder(PurchaseOrderModel POModel)
        {
            var dbMaster = new POMASTER
            {
                PONO = POModel.PONumber,
                PODATE = POModel.Date,
                SUPLNO = POModel.SupplierNumber
            };

            var dbDetail = new PODETAIL
            {
                PONO = POModel.PONumber,
                ITCODE = POModel.ItemCode,
                QTY = POModel.Quantity
            };

            dbContext.POMASTER.Add(dbMaster);
            dbContext.PODETAIL.Add(dbDetail);
            dbContext.SaveChanges();
        }

        [Route("update")]
        [HttpPost]
        public void UpdatePurchaseOrder(PurchaseOrderModel POModel)
        {
            var dbMaster = new POMASTER
            {
                PONO = POModel.PONumber,
                PODATE = POModel.Date,
                SUPLNO = POModel.SupplierNumber
            };

            var dbDetail = new PODETAIL
            {
                PONO = POModel.PONumber,
                ITCODE = POModel.ItemCode,
                QTY = POModel.Quantity
            };

            dbContext.Entry(dbMaster).State = EntityState.Modified;
            dbContext.Entry(dbDetail).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        [Route("delete/{orderNumber}")]
        public void DeletePurchaseOrder(string orderNumber)
        {
            var dbMaster = dbContext.POMASTER.FirstOrDefault(x => x.PONO == orderNumber);
            var dbDetail = dbContext.PODETAIL.FirstOrDefault(x => x.PONO == orderNumber);
            if (dbMaster != null)
            {
                dbContext.POMASTER.Remove(dbMaster);
                dbContext.SaveChanges();
            }
            if (dbDetail != null)
            {
                dbContext.PODETAIL.Remove(dbDetail);
                dbContext.SaveChanges();
            }
        }

        [Route("delete")]
        public List<PurchaseOrderModel> GetPurchaseOrders()
        {
            return dbContext.PODETAIL.Select(x => new PurchaseOrderModel
            {
                PONumber = x.PONO,
                Date = x.POMASTER.PODATE.Value,
                ItemCode = x.ITCODE,
                Quantity = x.QTY.Value,
                SupplierNumber = x.POMASTER.SUPLNO
            }).ToList();
        }

        [Route("get/{orderNumber}")]
        public PurchaseOrderModel GetPurchaseOrder(string code)
        {
            var x = dbContext.PODETAIL.Include(y => y.POMASTER).FirstOrDefault(y => y.PONO == code);
            return x != null ? new PurchaseOrderModel
            {
                PONumber = x.PONO,
                Date = x.POMASTER.PODATE.Value,
                ItemCode = x.ITCODE,
                Quantity = x.QTY.Value,
                SupplierNumber = x.POMASTER.SUPLNO
            } : null;
        }
    }
}
