using APPCOVID.BAL.Helpers;
using APPCOVID.Controllers.Core;
using APPCOVID.Entity.ViewModels;
using APPCOVID.Models.Session;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APPCOVID.Controllers
{
    public class TransactionController: CovidController
    {
        // GET: Transaction
        public ActionResult Index()
        {
            Authorize();
            IList<TransactionViewModel> transList = new TransactionHelper().GetAll();
            return View("~/Views/Transaction/Index.cshtml", transList);
        }
        // GET: Transaction/Details/5
        public ActionResult Details(int id)
        {
            Authorize("customer");
            TransactionViewModel transModel = new TransactionHelper().GetAllById(id);
            return View("~/Views/Transaction/ViewDetails.cshtml", transModel);
        }

        // GET: Transaction/Details/5
        public ActionResult SubscriptionDetails()
        {
            Authorize();
            string userId = HttpContext.Session.GetObject("coviduserid");
            IList<TransactionViewModel> transList = new TransactionHelper().GetAllByUserId(Convert.ToInt32(userId));
            return View("~/Views/Transaction/Subscription.cshtml", transList);
        }
        // GET: Transaction
        public ActionResult AllSubscriptionDetails()
        {
            Authorize();
            IList<TransactionViewModel> transList = new TransactionHelper().GetAll().Where(t => t.STATUS == "Active").ToList();
            return View("~/Views/Transaction/Subscription.cshtml", transList);
        }
        // GET: Transaction/Edit/5
        public ActionResult Edit(int id)
        {
            Authorize();
            TransactionViewModel transModel = new TransactionHelper().GetAllById(id);
            return View("~/Views/Transaction/Edit.cshtml", transModel);
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        public ActionResult Edit(TransactionViewModel transModel)
        {
            try
            {
                if(transModel.CUSTOMERID>0)
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
            Authorize("citizen");
            return View("~/Views/Transaction/Create.cshtml");
        }

        // POST: Transaction/Create
        [HttpPost]
        public ActionResult Create(TransactionViewModel transaction)
        {
            try
            {
                string userId = HttpContext.Session.GetObject("coviduserid");
                
                if (transaction.SUBSCRIPTIONTYPE=="Monthly")
                {
                    transaction.VALIDUPTODATE = DateTime.Now.AddMonths(1).ToString("dd-MM-yyyy hh:mm:tt");
                }
                else if (transaction.SUBSCRIPTIONTYPE == "Quarterly")
                {
                    transaction.VALIDUPTODATE = DateTime.Now.AddMonths(4).ToString("dd-MM-yyyy hh:mm:tt");
                }
                else if (transaction.SUBSCRIPTIONTYPE == "Yearly")
                {
                    transaction.VALIDUPTODATE = DateTime.Now.AddYears(1).ToString("dd-MM-yyyy hh:mm:tt");
                }
                else
                {
                    transaction.VALIDUPTODATE = "-------";
                }
                //transaction.PODETAILS="ID23738432477";
                transaction.CUSTOMERID = Convert.ToInt32(userId);
                transaction.CREATEDDATE = DateTime.Now.ToString("dd-MM-yyyy hh:mm:tt");                
                transaction.STATUS = "InActive";

                if (userId!=null || userId!="" || userId!="Unknown")
                    new TransactionHelper().CreateTransaction(transaction);
                return RedirectToAction(nameof(SubscriptionDetails));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return View();
            }
        }
    }
}
