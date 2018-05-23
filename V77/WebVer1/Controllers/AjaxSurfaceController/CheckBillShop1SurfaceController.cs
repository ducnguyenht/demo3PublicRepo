using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KiotVietBO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

public static class MemoryCheckBillShopCashier
{
    public static string Token = "";
    public static List<Bill> bills;
}
namespace WebVer1.Controllers.AjaxSurfaceController
{
    public class CheckBillShop1SurfaceController : Umbraco.Web.Mvc.SurfaceController
    {
        #region Thu Ngan Check bill
        [HttpGet]
        public JsonResult CheckPaid(string maHoaDon)
        {
            var bill = MemoryCheckBillShopCashier.bills.Where(o => o.Code == maHoaDon).FirstOrDefault();
            if (bill != null)
            {
                if (bill.IsPaid == false)
                {
                    bill.IsPaid = true;
                    return Json(bill.IsPaid, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    bill.IsPaid = false;
                    return Json(bill.IsPaid, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        private string RequestToken()
        {
            var url = @"https://shopyenngan.kiotviet.com/api/auth/credentials?format=json";
            var clientRequestRestSharp = new RestClient(url);
            var requestRequestRestSharp = new RestRequest(Method.POST);
            requestRequestRestSharp.AddParameter("provider", "credentials");
            requestRequestRestSharp.AddParameter("UserName", "0975929191");
            requestRequestRestSharp.AddParameter("Password", "MINHVUONG010613");
            var responseRestSharp = clientRequestRestSharp.Execute<RootToken>(requestRequestRestSharp);
            return responseRestSharp.Data.BearerToken;
        }
        [HttpGet]
        public JsonResult CheckBill(string maChiNhanh)
        {
            if (User.Identity.IsAuthenticated)
            {

                //var url = String.Format(@"https://shopyenngan.kiotviet.com/api/invoices?&Includes=Customer&Includes=SoldBy&%24top=10&%24filter=(BranchId+eq+{0}+and+PurchaseDate+eq+%27alltime%27+and+(Status+eq+4+or+Status+eq+3+or+Status+eq+1))", maChiNhanh);
                var url = String.Format(@"https://shopyenngan.kiotviet.com/api/invoices?format=json&Includes=BranchName&Includes=Branch&Includes=InvoiceDeliveries&Includes=TableAndRoom&Includes=Customer&Includes=Payments&Includes=SoldBy&Includes=User&Includes=InvoiceOrderSurcharges&Includes=Order&ForSummaryRow=true&Includes=SaleChannel&UsingStoreProcedure=false&%24inlinecount=allpages&ExpectedDeliveryFilterType=alltime&%24top=10&%24filter=(BranchId+eq+{0}+and+PurchaseDate+eq+%27alltime%27+and+(Status+eq+4+or+Status+eq+3+or+Status+eq+1))", maChiNhanh);

                var clientRequest = new RestClient(url);
                var requestConfig = new RestRequest(Method.GET);
                requestConfig.AddHeader("Authorization", "Bearer " + MemoryCheckBillShopCashier.Token);
                var response = clientRequest.Execute<RootBill>(requestConfig).Data;
                if (response.Data == null)
                {
                    MemoryCheckBillShopCashier.Token = RequestToken();
                    requestConfig.AddHeader("Authorization", "Bearer " + MemoryCheckBillShopCashier.Token);
                    response = clientRequest.Execute<RootBill>(requestConfig).Data;
                }
                var respBills = response.Data.OrderByDescending(o => o.CreatedDate).ToList();
                if (MemoryCheckBillShopCashier.bills == null)
                {
                    MemoryCheckBillShopCashier.bills = new List<Bill>();
                }
                var memoryBills = MemoryCheckBillShopCashier.bills;
                respBills.RemoveAt(respBills.Count - 1);
                if (respBills != null && respBills.Count > 0)
                {
                    if (memoryBills == null && memoryBills.Count == 0)
                    {
                        memoryBills = respBills;
                    }
                    else
                    {
                        foreach (var item in respBills)
                        {
                            var check = memoryBills.Where(o => o.Code == item.Code).FirstOrDefault();
                            if (check == null)
                            {
                                memoryBills.Add(item);
                            }
                        }
                    }
                }

                var filter = memoryBills.Where(o => o.IsPaid).OrderByDescending(o => o.CreatedDate).ToList();
                if (filter.Count() > 4)
                {
                    foreach (var item in filter.Skip(4).Take(filter.Count))
                    {
                        item.IsDelete = true;
                    }
                }
                var filterDelete = memoryBills.Where(o => o.IsDelete).OrderByDescending(o => o.CreatedDate).ToList();
                if (filterDelete.Count > 20)
                {
                    filterDelete.RemoveRange(20, filterDelete.Count - 20);
                    memoryBills.RemoveAll(o => o.IsDelete == true);
                    memoryBills.AddRange(filterDelete);
                }

                return Json(memoryBills.Where(o => o.IsDelete == false).OrderBy(o => o.IsPaid).ThenByDescending(o => o.CreatedDate), JsonRequestBehavior.AllowGet);
            }
            return Json(new List<Bill>(), JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}