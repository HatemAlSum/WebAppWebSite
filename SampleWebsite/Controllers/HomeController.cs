﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleWebsite.Models;
using System.IO;
using System.Text;
using ExcelDataReader;

namespace SampleWebsite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            var Colors = new List<string>(){
                "red","blue","indigo","green","black","cyan"
            };
            ViewData["Color"] =Colors[SampleWebsite.Startup.GetRandomNumber(0,5)];
            ViewData["Message"] = string.Format("Your contact page with {0} color from {1}.",
                                                ViewData["Color"],
                                                System.Net.Dns.GetHostName());

            return View();
        }


        public IActionResult ExcelReader()
        {
            string filePath = "test.xlsx";
            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                {
                    var results = reader.AsDataSet();
                    results.Tables.Count.ToString();
                    ViewData["excel"] = results.Tables[0].DefaultView.Table.Rows;
                    ViewData.Model = results.Tables[0].DefaultView.Table.Rows;
                }
            }
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
