using APPCOVID.BAL.Helpers;
using APPCOVID.Entity.ViewModels;
using APPCOVID.Models.Session;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace APPCOVID.Controllers
{
    public class TransactionController:Controller
    {
        // GET: Transaction
        public ActionResult Index()
        {
            // Authorize("customer");
            IList<TransactionViewModel> transList = new TransactionHelper().GetAll();
            return View("~/Views/Transaction/Index.cshtml", transList);
        }
        // GET: Transaction/Details/5
        public ActionResult Details(int id)
        {
            TransactionViewModel transModel = new TransactionHelper().GetAllById(id);
            return View("~/Views/Transaction/ViewDetails.cshtml", transModel);
        }
        // GET: Transaction/Edit/5
        public ActionResult Edit(int id)
        {
            TransactionViewModel transModel = new TransactionHelper().GetAllById(id);
            return View("~/Views/Transaction/Edit.cshtml", transModel);
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        public ActionResult Edit(TransactionViewModel transModel)
        {
            try
            {
                new TransactionHelper().UpdateTransaction(transModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return View();
            }
        }
        // GET: Transaction/Create
        public ActionResult Create()
        {
            return View("~/Views/Transaction/Create.cshtml");
        }

        // POST: Transaction/Create
        [HttpPost]
        public ActionResult Create(TransactionViewModel transaction)
        {
            try
            {
                if (transaction.SUBSCRIPTIONTYPE=="Monthly")
                {
                    transaction.VALIDUPTODATE = DateTime.Now.AddMonths(1).ToString("ddMMyyyyhhmmtt");
                }
                else if (transaction.SUBSCRIPTIONTYPE == "Quarterly")
                {
                    transaction.VALIDUPTODATE = DateTime.Now.AddMonths(4).ToString("ddMMyyyyhhmmtt");
                }
                else if (transaction.SUBSCRIPTIONTYPE == "Yearly")
                {
                    transaction.VALIDUPTODATE = DateTime.Now.AddYears(1).ToString("ddMMyyyyhhmmtt");
                }
                else
                {
                    transaction.VALIDUPTODATE = "-------";
                }
                transaction.PODETAILS="ID23738432477";
                transaction.CREATEDDATE = DateTime.Now.ToString("ddMMyyyyhhmmtt");                
                transaction.STATUS = "Discard";


                new TransactionHelper().CreateTransaction(transaction);
                return View("~/Views/Transaction/Index.cshtml");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return View();
            }
        }
    }
}
