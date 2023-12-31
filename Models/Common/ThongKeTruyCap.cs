﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace WebBanHang.Models.Common
{
    public class ThongKeTruyCap
    {
        private static string strConnect = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public static ThongKeViewModel ThongKe()
        {
            using (var connect = new SqlConnection(strConnect))
            {
                
                var item = connect.QueryFirstOrDefault<ThongKeViewModel>("sp_ThongKe");
                return item;
            }
        }
    }
}