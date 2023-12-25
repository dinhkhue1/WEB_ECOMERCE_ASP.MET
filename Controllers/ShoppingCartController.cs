﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.EF;

namespace WebBanHang.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }

        


        public ActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.Items);
            }
            
            return PartialView();
        }

        public ActionResult Partial_Item_ThanhToan()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }

            return PartialView();
        }

        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if(cart != null)
            {
                return Json(new { Count = cart.Items.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Partial_CheckOut()
        {
            return PartialView();
        }

        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
            }

            return View();
        }
        [HttpGet]
        public ActionResult CheckOutSuccess()
        {
            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult CheckOut(OrderViewModel req)
        {
            var code = new { Success = false, code = -1 };
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null)
                {
                    /*return RedirectToAction("CheckOutSuccess");*/
                    Order or = new Order();
                    or.CustomerName = req.CustomerName;
                    or.Phone = req.Phone;
                    or.Address = req.Address;
                    or.Email = req.Email;
                    cart.Items.ForEach(x => or.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price
                    }));
                    or.TotalAmount = cart.Items.Sum(x => (x.Price * x.Quantity));
                    or.TypePayment = req.TypePayment;
                    or.CreatedDate = DateTime.Now;
                    or.ModifiedDate = DateTime.Now;
                    or.CreatedBy = req.Phone;
                    Random rd = new Random();
                    or.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    db.Orders.Add(or);
                    db.SaveChanges();

                    //send mail 

                    var strSanPham = "";
                    var ThanhTien = decimal.Zero;
                    var TongTien = decimal.Zero;
                    foreach(var sp in cart.Items)
                    {
                        strSanPham += "<tr>";
                        strSanPham += "<td>" + sp.ProductName + "</td>";
                        strSanPham += "<td>" + sp.Quantity + "</td>";
                        strSanPham += "<td>" + sp.TotalPrice + "</td>";
                        strSanPham += "</tr>";
                        ThanhTien += sp.Price * sp.Quantity;
                    }
                    TongTien = ThanhTien;
                    string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/send2.html"));
                    contentCustomer = contentCustomer.Replace("{{MaDon}}", or.Code);
                    contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
                    contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", or.CustomerName);
                    contentCustomer = contentCustomer.Replace("{{Phone}}", or.Phone);
                    contentCustomer = contentCustomer.Replace("{{Email}}", req.Email);
                    contentCustomer = contentCustomer.Replace("{{DiaChiGiaoHang}}", or.Address);
                    contentCustomer = contentCustomer.Replace("{{ThanhTien}}", ThanhTien.ToString());
                    contentCustomer = contentCustomer.Replace("{{TongTien}}", TongTien.ToString());
                    WebBanHang.Common.Common.SendEmail("MyShop", "Đơn hàng #" + or.Code, contentCustomer.ToString(), req.Email);

                    string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/send1.html"));
                    contentAdmin = contentAdmin.Replace("{{MaDon}}", or.Code);
                    contentAdmin = contentAdmin.Replace("{{SanPham}}", strSanPham);
                    contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", or.CustomerName);
                    contentAdmin = contentAdmin.Replace("{{Phone}}", or.Phone);
                    contentAdmin = contentAdmin.Replace("{{Email}}", req.Email);
                    contentAdmin = contentAdmin.Replace("{{DiaChiGiaoHang}}", or.Address);
                    contentAdmin = contentAdmin.Replace("{{ThanhTien}}", ThanhTien.ToString());
                    contentAdmin = contentAdmin.Replace("{{TongTien}}", TongTien.ToString());
                    WebBanHang.Common.Common.SendEmail("MyShop", "Đơn hàng mới #" + or.Code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);
                    code = new { Success = true, code = 1 };
                    cart.ClearCart();
                    return RedirectToAction("CheckOutSuccess");
                }
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult AddToCart(int id , int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            var db = new ApplicationDbContext();
            var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);
            if(checkProduct != null)
            {
                
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if(cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    CategoryName = checkProduct.ProductCategory.Title,
                    Alias = checkProduct.Alias,
                    Quantity = quantity,
                };
                if (checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                {
                    item.ProductImg = checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault).Image;
                }

                /*gán giá bán hiện tại cho thằng ShoppingCartItem để có thể tính thống kê và sử lý giá tiền*/
                item.Price = checkProduct.Price;
                if (checkProduct.PriceSale > 0)
                {
                    item.Price = (decimal)checkProduct.PriceSale;
                }
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Thêm sản phẩm vào giỏ hàng thành công", code = 1, Count = cart.Items.Count };
            }
            return Json(code);
        }

       

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var checkProduct = cart.Items.FirstOrDefault(x => x.ProductId == id);
                if(checkProduct!= null)
                {
                    cart.Remove(id);
                    code = new { Success = true, msg = "", code = 1, Count = cart.Items.Count };
                }
            }

            return Json(code);
        }


        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id, quantity);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }


        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.ClearCart();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

    }
}