using ChatApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatApp.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        private readonly MessageRepository messageRepo;
        public HistoryController()
        {
            messageRepo = new MessageRepository();
        }
        public ActionResult Index()
        {
            var list1= messageRepo.GetMessagesByField("userNameSender",User?.Identity.Name).Select(m=>m.UserNameReceiver).ToList();
            var list2 = messageRepo.GetMessagesByField("userNameReceiver", User?.Identity.Name).Select(m=>m.UserNameSender).ToList();
            var all=list1.Concat(list2);
            all = all.Distinct().ToList();
            return View(all);
        }

        public ActionResult Details(string userName)
        {
            var list1 = messageRepo.GetMessagesByField("userNameSender", User?.Identity.Name).ToList();
            var list2 = messageRepo.GetMessagesByField("userNameReceiver", User?.Identity.Name).ToList();
            var all = list1.Concat(list2).OrderBy(m=>m.DateSent).ToList();
            ViewBag.UserName = userName;
            return View(all);
        }
        public ActionResult Delete(string userName)
        {
            var list1 = messageRepo.GetMessagesByField("userNameSender", userName).Where(m=>m.UserNameReceiver == User.Identity.Name);
            var list2 = messageRepo.GetMessagesByField("userNameReceiver", userName).Where(m => m.UserNameSender == User.Identity.Name); ;
            var all = list1.Concat(list2).OrderBy(m => m.DateSent).ToList();
            all.ForEach(m =>
            {
                messageRepo.Delete(m.Id);
            });
            return RedirectToAction("Index");
        }
        public ActionResult RenderDetails(string UserName,DateTime? DateFrom,DateTime? DateTo)
        {
            var list1 = messageRepo.GetMessagesByField("userNameSender", User?.Identity.Name).ToList();
            var list2 = messageRepo.GetMessagesByField("userNameReceiver", User?.Identity.Name).ToList();
            var all = list1.Concat(list2).OrderBy(m => m.DateSent).ToList();
            if (DateFrom.HasValue)
            {
                all = all.Where(m => m.DateSent >= DateFrom).ToList();
            }
            if (DateTo.HasValue)
            {
                all = all.Where(m => m.DateSent <= DateTo).ToList();
            }
            return PartialView("_HistoryDetailsPartial", all);
        }

        public ActionResult Stats()
        {
            var result=messageRepo.GetStats(User.Identity.Name);
            return View(result);
        }

    }
}