﻿using APPCOVID.BAL.Helpers;
using APPCOVID.Controllers.Core;
using APPCOVID.Entity.ViewModels;
using APPCOVID.Models.Session;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace APPCOVID.Controllers
{
    public class ProductController : CovidController
    {
        // GET: Product
        public ActionResult Index()
        {
            Authorize("customer");
            IList<ProductViewModel> prodList = new ProductHelper().GetAll();
            return View("~/Views/Products/Index.cshtml", prodList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            Authorize();            
            ProductViewModel prodModel = new ProductHelper().GetAllById(id);
            return View("~/Views/Products/ViewDetails.cshtml", prodModel);
        }
        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Authorize();
            ProductViewModel prodModel = new ProductHelper().GetAllById(id);
            return View("~/Views/Products/Edit.cshtml", prodModel);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductViewModel prodModel)
        {
            try
            {
                new ProductHelper().UpdateProduct(prodModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return View();
            }
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            Authorize("customer");
            return View("~/Views/Products/Create.cshtml");
        }
        // GET: Product/Buy
        public ActionResult BuyProduct(int id)
        {
            Authorize("citizen");
            ProductViewModel prodModel = new ProductHelper().GetAllById(id);
            TransactionViewModel transModel = new TransactionViewModel();
            transModel.PRODUCTID = id;
            transModel.CUSTOMERID = prodModel.CUSTOMERID;
            return View("~/Views/Transaction/Create.cshtml", transModel);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductViewModel productInsurance)
        {
            try
            {
                productInsurance.STATUS = "Active";
                productInsurance.CUSTOMERID = Convert.ToInt32(HttpContext.Session.GetObject("coviduserid"));

                new ProductHelper().CreateProduct(productInsurance);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return View();
            }
        }


    }
}