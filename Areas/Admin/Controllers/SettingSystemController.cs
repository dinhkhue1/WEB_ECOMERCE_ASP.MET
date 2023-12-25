﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.EF;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class SettingSystemController : Controller
    {
        // GET: Admin/SettingSystem
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Partial_Setting()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddSetting(SettingSystemViewModel req)
        {
            SystemSetting set = null;
            var checkTitle = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingTitle"));
            /*check xem nếu chưa có thì tạo mới , còn nếu có rồi thì chuyển trạng thái của Entity thành Modified để biết rằng đối tượng cần được thay đổi trong DB*/
            if (checkTitle == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingTitle";
                set.SettingValue = req.SettingTitle;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkTitle.SettingValue = req.SettingTitle;
                db.Entry(checkTitle).State = System.Data.Entity.EntityState.Modified;
            }
            //logo
            var checkLogo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingLogo"));
            if (checkLogo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingLogo";
                set.SettingValue = req.SettingLogo;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkLogo.SettingValue = req.SettingLogo;
                db.Entry(checkLogo).State = System.Data.Entity.EntityState.Modified;
            }
            //Email
            var email = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingEmail"));
            if (email == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingEmail";
                set.SettingValue = req.SettingEmail;
                db.SystemSettings.Add(set);
            }
            else
            {
                email.SettingValue = req.SettingEmail;
                db.Entry(email).State = System.Data.Entity.EntityState.Modified;
            }
            //Hotline
            var Hotline = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingHotline"));
            if (Hotline == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingHotline";
                set.SettingValue = req.SettingHotline;
                db.SystemSettings.Add(set);
            }
            else
            {
                Hotline.SettingValue = req.SettingHotline;
                db.Entry(Hotline).State = System.Data.Entity.EntityState.Modified;
            }
            //TitleSeo
            var TitleSeo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingTitleSeo"));
            if (TitleSeo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingTitleSeo";
                set.SettingValue = req.SettingTitleSeo;
                db.SystemSettings.Add(set);
            }
            else
            {
                TitleSeo.SettingValue = req.SettingTitleSeo;
                db.Entry(TitleSeo).State = System.Data.Entity.EntityState.Modified;
            }
            //DessSeo
            var DessSeo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingDesSeo"));
            if (DessSeo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingDesSeo";
                set.SettingValue = req.SettingDesSeo;
                db.SystemSettings.Add(set);
            }
            else
            {
                DessSeo.SettingValue = req.SettingDesSeo;
                db.Entry(DessSeo).State = System.Data.Entity.EntityState.Modified;
            }
            //KeySeo
            var KeySeo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingKeySeo"));
            if (KeySeo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingKeySeo";
                set.SettingValue = req.SettingKeySeo;
                db.SystemSettings.Add(set);
            }
            else
            {
                KeySeo.SettingValue = req.SettingKeySeo;
                db.Entry(KeySeo).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();

            return View("Partial_Setting");
        }
    }
}