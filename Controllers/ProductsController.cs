using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var items = db.Products.ToList();
            return View(items);
        }

        public ActionResult Detail(string alias, int id)
        {
            var items = db.Products.Find(id);
            if (items != null)
            {
                db.Products.Attach(items);
                items.ViewCount = items.ViewCount + 1;
                db.Entry(items).Property(x => x.ViewCount).IsModified = true;
               
                db.SaveChanges();
            }
            return View(items);
        }

        public ActionResult ProductCategory( string alias, int id)
        {
            var items = db.Products.ToList();
            if (id >0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if(cate != null)
            {
                ViewBag.CateName = cate.Title;
            }
            ViewBag.CateId = id;
            return View(items);
        }
        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.Products.Where(x => x.IsHome && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_ProductSales()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
    }
}