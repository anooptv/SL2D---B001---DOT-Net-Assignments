using WEBAPI_Purchase_Order_Processing_System.DataObjects;
using WEBAPI_Purchase_Order_Processing_System.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace WEBAPI_Purchase_Order_Processing_System.Controllers
{
    [RoutePrefix("api/item")]
    public class ItemController : ApiController
    {
        private PODbEntities dbContext = new PODbEntities();

        [Route("create")]
        [HttpPost]
        public string CreateItem(ItemModel item)
        {
            var dbItem = new ITEM
            {
                ITCODE = item.Code,
                ITDESC = item.Desc,
                ITRATE = item.Rate
            };
            dbContext.ITEM.Add(dbItem);
            dbContext.SaveChanges();
            return dbItem.ITCODE;
        }

        [Route("update")]
        [HttpPost]
        public void UpdateItem(ItemModel item)
        {
            var dbItem = new ITEM
            {
                ITCODE = item.Code,
                ITDESC = item.Desc,
                ITRATE = item.Rate
            };
            dbContext.Entry(dbItem).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        [Route("delete/{code}")]
        public void DeleteItem(string code)
        {
            var item = dbContext.ITEM.FirstOrDefault(x => x.ITCODE == code);
            if (item != null)
            {
                dbContext.ITEM.Remove(item);
                dbContext.SaveChanges();
            }
        }

        [Route("get")]
        public List<ItemModel> GetItems()
        {
            return dbContext.ITEM.Select(x => new ItemModel
            {
                Code = x.ITCODE,
                Desc = x.ITDESC,
                Rate = x.ITRATE.Value
            }).ToList();
        }

        [Route("get/{code}")]
        public ItemModel GetItem(string code)
        {
            var x = dbContext.ITEM.FirstOrDefault(y => y.ITCODE == code);
            return x != null ? new ItemModel
            {
                Code = x.ITCODE,
                Desc = x.ITDESC,
                Rate = x.ITRATE.Value
            } : null;
        }
    }
}
