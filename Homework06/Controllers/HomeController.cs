using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Homework06.Models;
using Homework06.Models.ViewModels;
using PagedList;
using Newtonsoft.Json;

namespace Homework06.Controllers
{
    public class HomeController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return PartialView();
        }

        public string GetCatergoryNames()
        {
            object catergoryData = db.categories.Select(p => new { id = p.category_id, name = p.category_name }).ToList();
            return JsonConvert.SerializeObject(catergoryData);
        }

        public string GetProducts(int? i)
        {
            db.Configuration.ProxyCreationEnabled = false;
            object productDatas = db.products.Select(p => new { id = p.product_id, name = p.product_name, brand = p.brand.brand_name, catergory = p.category.category_name, model = p.model_year, price = p.list_price }).ToList().ToPagedList(i ?? 1, 10);
            return JsonConvert.SerializeObject(productDatas);
        }

        public string Search(string text)
        {
            db.Configuration.ProxyCreationEnabled = false;
            object productDatas = db.products.Where(o => o.product_name.Contains(text)).Select(p => new { id = p.product_id, name = p.product_name, brand = p.brand.brand_name, catergory = p.category.category_name, model = p.model_year, price = p.list_price }).ToList();
            return JsonConvert.SerializeObject(productDatas);
        }
        public string GetBrandData()
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<brand> data = db.brands.ToList();

            return JsonConvert.SerializeObject(data);
        }

        public ActionResult Report()
        {

            return View();
        }

        public string ProductDetails(int id)
        {
            //db.Configuration.ProxyCreationEnabled = false;
            object productDetial = db.stocks.Where(y => y.product_id == id).Include(v => v.product).Select(p => new {
                productname = p.product.product_name,
                year = p.product.model_year,
                price = p.product.list_price,
                brand = p.product.brand.brand_name,
                catergory = p.product.category.category_name,
                stores = db.stocks.Where(s => s.product_id == id).Select(n => new { storename = n.store.store_name, quantity = n.quantity })

            }).FirstOrDefault();


            return JsonConvert.SerializeObject(productDetial);

        }

        public string GetReports()
        {
            db.Configuration.ProxyCreationEnabled = false;
            object bikes = db.orders.Select(o => new
            {
                //orderid = o.order_id,

                month = o.order_date.Month,
                bike = db.order_items.Where(x => x.order_id == o.order_id && x.product.category.category_id == 6).ToList(),
            }).ToList();

            return JsonConvert.SerializeObject(bikes);
        }


    }
}