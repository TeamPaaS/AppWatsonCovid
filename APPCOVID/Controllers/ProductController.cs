using APPCOVID.BAL.Helpers;
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
            Authorize();
            IList<ProductViewModel> prodList = new ProductHelper().GetAll();
            int stage = Convert.ToInt32(HttpContext.Session.GetObject("OST_Result_Stage") ?? "0");
            ViewBag.activeProductsByStage = stage > 0 ? new ProductHelper().GetAll(stage) : new ProductHelper().GetAll();
            return View("~/Views/Products/Index.cshtml", prodList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            Authorize("customer");
            ProductViewModel prodModel = new ProductHelper().GetAllById(id);
            return View("~/Views/Products/ViewDetails.cshtml", prodModel);
        }
        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Authorize("customer");
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
            Authorize("customer");
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
            if (productInsurance != null && productInsurance.SHORTDESCRIPTION != "" && productInsurance.PRODUCTURL != "" && productInsurance.DESCRIPTION != "" && productInsurance.IMAGEURL != "")
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
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }


    }
}