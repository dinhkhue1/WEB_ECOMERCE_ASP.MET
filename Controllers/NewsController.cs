using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.EF;

namespace WebBanHang.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Posts
        public ActionResult Index(int? page)
        {
            var pageSize = 1;
            if (page == null)
            {
                page = 1;
            }
         /*   - IEnumerable là một giao diện được triển khai bởi nhiều kiểu dữ liệu, bao gồm cả danh sách(List), mảng(Array), tập hợp(Set), và nhiều loại dữ liệu khác 
            -giúp thực hiện các truy vấn phức tạp trên dữ liệu mà không cần phải lấy toàn bộ dữ liệu từ cơ sở dữ liệu ngay từ đầu*/
            IEnumerable<News> items = db.News.OrderByDescending(x => x.CreatedDate);
            /*HasValue - kiểm tra xem có null không , nếu không thì chuyển về int*/ 
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageIndex;
            return View(items);
        }

        public ActionResult Detail(int id)
        {
            var item = db.News.Find(id);
            return View(item);
        }

        public ActionResult Partial_News_Home()
        {
            var items = db.News.Take(3).ToList();
            return PartialView(items);
        }
    }
}