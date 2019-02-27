using WEBAPI_Purchase_Order_Processing_System.DataObjects;
using WEBAPI_Purchase_Order_Processing_System.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace WEBAPI_Purchase_Order_Processing_System.Controllers
{
    [RoutePrefix("api/supplier")]
    public class SupplierController : ApiController
    {
        private PODbEntities dbContext = new PODbEntities();

        /// <summary>
        /// Add new supplier
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public string CreateSupplier(SupplierModel supplier)
        {
            var dbSupplier = new SUPPLIER
            {
                SUPLADDR = supplier.Address,
                SUPLNAME = supplier.Name,
                SUPLNO = supplier.Id
            };
            dbContext.SUPPLIER.Add(dbSupplier);
            dbContext.SaveChanges();
            return dbSupplier.SUPLNO;
        }

        [HttpPost]
        [Route("update")]
        public void UpdateSupplier(SupplierModel supplier)
        {
            var dbSupplier = new SUPPLIER
            {
                SUPLADDR = supplier.Address,
                SUPLNAME = supplier.Name,
                SUPLNO = supplier.Id
            };
            dbContext.Entry(dbSupplier).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        [Route("delete/{id}")]
        public void DeleteSupplier(string id)
        {
            var supplier = dbContext.SUPPLIER.FirstOrDefault(x => x.SUPLNO == id);
            if (supplier != null)
            {
                dbContext.SUPPLIER.Remove(supplier);
                dbContext.SaveChanges();
            }
        }

        [Route("get")]
        public List<SupplierModel> GetSuppliers()
        {
            return dbContext.SUPPLIER.Select(x => new SupplierModel
            {
                Address = x.SUPLADDR,
                Id = x.SUPLNO,
                Name = x.SUPLNAME
            }).ToList();
        }

        [Route("get/{id}")]
        public SupplierModel GetSupplier(string id)
        {
            var x = dbContext.SUPPLIER.FirstOrDefault(y => y.SUPLNO == id);
            return x != null ? new SupplierModel
            {
                Address = x.SUPLADDR,
                Id = x.SUPLNO,
                Name = x.SUPLNAME
            } : null;
        }

    }
}
