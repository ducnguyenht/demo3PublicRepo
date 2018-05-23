using KiotVietBO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Models;
using WebVer1.BLL;
#region BO
#region Cupon
public class Cupon
{
    public DateTime tuNgay { get; set; }
    public DateTime denNgay { get; set; }
    public bool suDung1Lan { get; set; }
    public bool suDungNhieuLan { get; set; }
    public bool coHieuLuc { get; set; }
    public int giamGia { get; set; }
    public string name { get; set; }
    public int maHang_NhomHang { get; set; }
    public string hangHoa { get; set; }
    public string suDungCho { get; set; }
}
#endregion

#region Thu ngan
public static class MemoryCashier
{
    public static string Token = "eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkExMjhDQkMtSFMyNTYiLCJraWQiOiJ3WGEifQ.BXbKLbxdjGBO1p72MuSp6kApa232i7_Fz4aCBaISUOOMXAaN0kN0GhGrxvp9rrJ-OKhsqs0zojLd5G_dMmdp1A0rWy018Laoq8FoIgUT-4xdwr_5nwcBOdNsLIiudJV13rcXTFfP167B5n2_sehN80p7cgfn42uGkfUFEpvoRULDu9dkb4gwZXXtD_kZx-9o6trH9h5zJlNtpOdqQdJjrD-mKEwieG_UxotUtFIlTIig9RgCKQdZd4MkC_iCjoNC1iwd3rUoZuE0bRtJ_WkkZyVZZfpWjyTQ09FiuvvQofOzc1TNX3uPcOxUxN6aVX4RycwgjAbGSdcT-AO1mkCmCQ.iWgeibl_nCxk7o9yklySHQ.Z-N94ZMr0DQl1I7P8eWwENNoDZbnOx7GEd0S6NPWteKDgg9bN_UlOKQkrpOMk4eb39rW_p9IWdYp5iQQFaNNjIBSX2xFi8K14vgp1GsvbFjY7e_urjSeGiJDVvW-HxGLTJsRZLmix-yIydjTxtOLjduwL_NeqT-b67WkkKV5GA7yPoTgA578Bt6s21if9C423h5xowK1VLyP5DwOZKDEh26Ot4T6Uy3iFA6IunUk2ut5laSUTCot7i2w8UGvDRBToK7MhObHYPPRz-s5pzvywTjYNMGnGwlFFCefMJKy7CCSw0-WIxIxOtlNiYsKz8leD66Rwtu0tZcHGOTkhSzpbEZDU81G_CgRNJqa05w3eU63jn4pTrdOltF9HJPrSgOKzV6RETi-ffmg7cHFU7KGVtfTs82VIzBqjf5hwxzSjTYrDAUbQOwS7X4vKXGsnKzGhbVLekwFNOzxFKaioxDPbzoWSiW86dX65qF5Vq9Mun2EWdvNRwztZyLw_VmMrKIH.mKYJm-xobKT1jJY_cp684tnqhwKJ0bBoBEp9drTrSf0";
    public static List<Bill> bills;
}
public class Bill
{
    [Key]
    public string Code { get; set; }
    public DateTime CreatedDate { get; set; }
    public string strCreateDate { get; set; }
    public string CustomerName { get; set; }
    public string SoldByGivenName { get; set; }
    public string CreatedByGivenName { get; set; }
    public string SaleChannel { get; set; }
    public double PaidAmount { get; set; }
    public bool IsPaid { get; set; }
    public bool IsDelete { get; set; }
}

public class Filter
{
    public string __type { get; set; }
    public bool ForSale { get; set; }
    public bool TotalValueOnly { get; set; }
    public bool ForSummaryRow { get; set; }
    public bool ForReturn { get; set; }
    public bool ForExportDetail { get; set; }
    public string ExpectedDeliveryFilterType { get; set; }
    public bool UsingStoreProcedure { get; set; }
    public bool IsCheckTransaction { get; set; }
    public int Top { get; set; }
}

public class RootBill
{
    public int Total1Value { get; set; }
    public double Total2Value { get; set; }
    public int Total3Value { get; set; }
    public int Total4Value { get; set; }
    public double Total5Value { get; set; }
    public int Total { get; set; }
    public List<Bill> Data { get; set; }
    public Filter Filter { get; set; }
    public DateTime Timestamp { get; set; }
}
public class RootBillV1
{
    public int UserId { get; set; }
    public string Action { get; set; }
    public int SubjectId { get; set; }
    public string SubjectCode { get; set; }
    public string SubjectLabel { get; set; }
    public string SubjectType { get; set; }
    public DateTime Timestamp { get; set; }
    public string UserName { get; set; }
    public int Value { get; set; }
    //public bool IsPaid { get; set; }
}
#endregion

#region Token
public class Meta
{
    public string MobileApi { get; set; }
    public string RetailerType { get; set; }
}
public class RootToken
{
    public string UserId { get; set; }
    public string SessionId { get; set; }
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string BearerToken { get; set; }
    public ResponseStatus ResponseStatus { get; set; }
    public Meta Meta { get; set; }
}
public class CheckBill
{
    public int UserId { get; set; }
    public string Action { get; set; }
    public int SubjectId { get; set; }
    public string SubjectCode { get; set; }
    public string SubjectLabel { get; set; }
    public string SubjectType { get; set; }
    public DateTime Timestamp { get; set; }
    public string UserName { get; set; }
    public int Value { get; set; }
    public bool IsPaid { get; set; }
}
#endregion

#endregion


namespace WebVer1.Controllers.AjaxSurfaceController
{
    public class CartsSurfaceController : Umbraco.Web.Mvc.SurfaceController
    {
        public static DataContext db = new DataContext(new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase().Options);

        [HttpPost]
        public ActionResult SaveWebhookData()
        {
            var json = "";
            using (var inputStream = new System.IO.StreamReader(Request.InputStream))
            {
                json = inputStream.ReadToEnd();
            }
            var rootBill = JsonConvert.DeserializeObject<WebVer1.BLL.ParseInvoice.RootInvoice>(json);
            var notification = rootBill.Notifications[0];
            var datas = notification.Data;
            string[] keys = new string[] {"invoice.update",  "order.update"  };
            string sKeyResult = keys.FirstOrDefault<string>(s=> notification.Action.Contains(s));
            switch (sKeyResult)
            {
                case "invoice.update":
                    foreach (var data in datas)
                    {
                   
                        if (db.Bills.Count() <= 0)
                        {
                            Bill bill = new Bill();
                            bill.Code = data.Code;
                            bill.SoldByGivenName = data.SoldByName;
                            bill.PaidAmount = data.TotalPayment;
                            bill.CreatedDate = data.PurchaseDate;
                            bill.strCreateDate = data.PurchaseDate.ToString("HH:mm");
                            db.Bills.Add(bill);
                            db.SaveChanges(true);
                            BLL.PkptHub.Static_Send(bill);
                       
                        }
                        else
                        {
                            var checkBill = db.Bills.Where(o => o.Code.Contains(data.Code)).FirstOrDefault();
                            if (checkBill == null)
                            {
                                Bill bill = new Bill();
                                bill.Code = data.Code;
                                bill.SoldByGivenName = data.SoldByName;
                                bill.PaidAmount = data.TotalPayment;
                                bill.CreatedDate = data.PurchaseDate;
                                bill.strCreateDate = data.PurchaseDate.ToString("HH:mm");
                                db.Bills.Add(bill);
                                
                                db.SaveChanges(true);
                                BLL.PkptHub.Static_Send(bill);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }           
            return Json(new { message = "success!" });
        }

        #region Thu Ngan Check bill
        [HttpGet]
        public JsonResult CheckPaid(string maHoaDon)
        {
            var contentService = ApplicationContext.Current.Services.ContentService;
            var content = contentService.GetById(int.Parse(System.Configuration.ConfigurationManager.AppSettings["ListBillId"].ToString()));
            var bill = content.Children().Where(o => o.Name == maHoaDon).FirstOrDefault();
            if (bill != null)
            {
                if (bill.GetValue<bool>("trangThai") == false)
                {
                    bill.SetValue("trangThai", true);
                    contentService.SaveAndPublishWithStatus(bill);
                    return Json(bill.GetValue<bool>("trangThai"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    bill.SetValue("trangThai", false);
                    contentService.SaveAndPublishWithStatus(bill, 0);
                    return Json(bill.GetValue<bool>("trangThai"), JsonRequestBehavior.AllowGet);
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        private string RequestToken()
        {
            var url = @"https://nzt.kiotviet.com/api/auth/credentials?format=json";
            var clientRequestRestSharp = new RestClient(url);
            var requestRequestRestSharp = new RestRequest(Method.POST);
            //requestRequestRestSharp.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            requestRequestRestSharp.AddParameter("provider", "credentials");
            requestRequestRestSharp.AddParameter("UserName", "0944505039");
            requestRequestRestSharp.AddParameter("Password", "@vanlinh123");
            var responseRestSharp = clientRequestRestSharp.Execute<RootToken>(requestRequestRestSharp);
            return responseRestSharp.Data.BearerToken;
        }
        private List<CheckBill> RequestBill(string token)
        {
            var url = @"https://nzt.kiotviet.com/api/activities";
            var clientRequestRestSharp = new RestClient(url);
            var requestRequestRestSharp = new RestRequest(Method.GET);
            requestRequestRestSharp.AddHeader("Authorization", string.Format(@"Bearer {0}", token));
            requestRequestRestSharp.AddCookie("LatestBranch_244993_29657", "13933");
            var responseRestSharp = clientRequestRestSharp.Execute<List<CheckBill>>(requestRequestRestSharp);
            return responseRestSharp.Data;
        }
      
        public JsonResult CheckBill(string maChiNhanh)
        {
            //if (User.Identity.IsAuthenticated)
            var url = String.Format(@"https://nzt.kiotviet.com/api/invoices?format=json&Includes=BranchName&Includes=Branch&Includes=InvoiceDeliveries&Includes=TableAndRoom&Includes=Customer&Includes=Payments&Includes=SoldBy&Includes=User&Includes=InvoiceOrderSurcharges&Includes=Order&ForSummaryRow=true&Includes=SaleChannel&UsingStoreProcedure=false&%24inlinecount=allpages&ExpectedDeliveryFilterType=alltime&%24top=10&%24filter=(BranchId+eq+{0}+and+PurchaseDate+eq+%27alltime%27+and+(Status+eq+4+or+Status+eq+3+or+Status+eq+1))", maChiNhanh);
            List<CheckBill> checkBills = RequestBill(MemoryCashier.Token);
            var response = checkBills;
            if (checkBills == null || checkBills.Count == 1)
            {
                MemoryCashier.Token = RequestToken();
                checkBills = RequestBill(MemoryCashier.Token);
            }
            var respBills = checkBills.OrderByDescending(o => o.Timestamp).ToList();
            var contentService = ApplicationContext.Current.Services.ContentService;
            var content = contentService.GetById(int.Parse(System.Configuration.ConfigurationManager.AppSettings["ListBillId"].ToString()));
            var childs = content.Children().Where(o => o.GetValue<DateTime>("ngayTao") >= DateTime.Today);
            if (respBills != null && respBills.Count > 0)
            {
                foreach (var item in respBills)
                {
                    var checkHd = childs.Where(o => o.Name.Contains(item.SubjectCode)).FirstOrDefault();
                    if ((checkHd == null && item.SubjectType == "Orders") || (checkHd == null && item.SubjectType.Equals("Invoices")))
                    {
                        //https://nzt.kiotviet.com/#/Invoices?Code=HD020342
                        //https://nzt.kiotviet.com/#/Orders?Code=DH000141

                        var newContent = contentService.CreateContent(item.SubjectCode, content.Id, "checkBill", 0);
                        switch (item.SubjectType)
                        {
                            case "Orders":
                                newContent.SetValue("trangThai", true);
                                break;
                            //case "Invoices":break;
                            default:
                                newContent.SetValue("trangThai", item.IsPaid);
                                break;
                        }
                        newContent.SetValue("ngayTao", item.Timestamp);
                        newContent.SetValue("khachHang", item.SubjectCode);
                        newContent.SetValue("nguoiBan", item.UserName);
                        newContent.SetValue("tienPhaiThu", item.Value);
                        contentService.SaveAndPublishWithStatus(newContent);
                    }
                }
            }
            var filter = childs.Where(o => o.GetValue<bool>("trangThai") == false).OrderByDescending(o => o.GetValue<DateTime>("ngayTao"));
            var filterDeleted = childs.Where(o => o.GetValue<bool>("trangThai") == true).OrderByDescending(o => o.GetValue<DateTime>("ngayTao")).Take(4);
            List<Bill> lst = new List<Bill>();
            foreach (var item in filter)
            {
                Bill b = new Bill()
                {
                    Code = item.Name,
                    CreatedDate = item.GetValue<DateTime>("ngayTao"),
                    strCreateDate = item.GetValue<DateTime>("ngayTao").ToString("hh:mm"),
                    SoldByGivenName = item.GetValue<string>("nguoiBan"),
                    CustomerName = item.GetValue<string>("khachHang"),
                    PaidAmount = item.GetValue<int>("tienPhaiThu"),
                    IsPaid = item.GetValue<bool>("trangThai"),
                };
                lst.Add(b);
            }
            foreach (var item in filterDeleted)
            {
                Bill b = new Bill()
                {
                    Code = item.Name,
                    CreatedDate = item.GetValue<DateTime>("ngayTao"),
                    strCreateDate = item.GetValue<DateTime>("ngayTao").ToString("hh:mm"),
                    SoldByGivenName = item.GetValue<string>("nguoiBan"),
                    CustomerName = item.GetValue<string>("khachHang"),
                    PaidAmount = item.GetValue<int>("tienPhaiThu"),
                    IsPaid = item.GetValue<bool>("trangThai"),
                };
                lst.Add(b);
            }
            return Json(lst.OrderBy(o => o.IsPaid).ThenByDescending(o => o.CreatedDate), JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpGet]
        public JsonResult CheckCupon(string cupon)
        {
            var contentService = ApplicationContext.Current.Services.ContentService;
            var content = contentService.GetById(2116);
            var query = content.Children().Where(o => o.Name == cupon).FirstOrDefault();
            var session = System.Web.HttpContext.Current.Session;
            if (query != null)
            {
                var tuNgay = query.GetValue<DateTime>("tuNgay");
                var denNgay = query.GetValue<DateTime>("denNgay");
                var giamGia = query.GetValue<int>("giamGia");
                var suDung1Lan = query.GetValue<bool>("suDung1Lan");
                var suDungNhieuLan = query.GetValue<bool>("suDungNhieuLan");
                var coHieuLuc = query.GetValue<bool>("coHieuLuc");
                var maNhomHangHoa = query.GetValue<int>("maNhomHangHoa");
                var suDungCho = query.GetValue<string>("suDungCho");
                Cupon cp = new Cupon()
                {
                    coHieuLuc = coHieuLuc,
                    denNgay = denNgay,
                    giamGia = giamGia,
                    name = query.Name,
                    suDung1Lan = suDung1Lan,
                    suDungNhieuLan = suDungNhieuLan,
                    tuNgay = tuNgay,
                    maHang_NhomHang = maNhomHangHoa,
                    suDungCho = suDungCho
                };
                if (tuNgay <= DateTime.Now && DateTime.Now <= denNgay)
                {
                    if (coHieuLuc)
                    {
                        string message = "Mã giảm giá " + cupon + " sẽ được áp dụng khi hoàn tất đơn hàng.<br/>";//denNgay.ToString("dd/MM/yyyy")
                        message += "Mã giảm giá có thể sử dụng từ ngày " + tuNgay.ToString("dd/MM/yyyy") + " đến ngày " + denNgay.ToString("dd/MM/yyyy") + ".<br/>";
                        message += "Mã giảm giá sử dung cho " + suDungCho + ".";

                        if (session["dsCupon"] == null && session != null)
                        {
                            Dictionary<string, Cupon> dsCupon = new Dictionary<string, Cupon>();
                            dsCupon.Add(query.Name, cp);
                            session["dsCupon"] = dsCupon;
                            session["CuponMessage"] = new KeyValuePair<int, string>(1, message);
                            return Json(new { message = message, count = 1 }, JsonRequestBehavior.AllowGet);
                        }
                        else if (session["dsCupon"] != null && session != null)
                        {
                            session["CuponMessage"] = new KeyValuePair<int, string>(2, message + "<br/>Chỉ được sử dụng 1 mã giảm giá.");
                            return Json(new { message = message + "<br/>Chỉ được sử dụng 1 mã giảm giá.", count = 1 }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        session["CuponMessage"] = new KeyValuePair<int, string>(3, "Mã giảm giá đã được sử dụng.Vui lòng nhập mã khác.");
                        return Json(new { message = "Mã giảm giá đã được sử dụng.Vui lòng nhập mã khác.", count = 1 }, JsonRequestBehavior.AllowGet);
                    }
                    session["CuponMessage"] = new KeyValuePair<int, string>(4, "Mã giảm giá đã quá hạn sử dụng.");
                    return Json(new { message = "Mã giảm giá đã quá hạn sử dụng.", count = 1 }, JsonRequestBehavior.AllowGet);

                }
            }
            session["CuponMessage"] = new KeyValuePair<int, string>(5, "Mã giảm giá không đúng.Vui lòng nhập mã khác.");
            return Json(new { message = "Mã giảm giá không đúng.Vui lòng nhập mã khác.", count = 1 }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult CartCount()
        {
            List<string> dsIdHangHoa = null;
            var session = System.Web.HttpContext.Current.Session;
            if (session != null && session["dsIdHangHoa"] != null)
            {
                dsIdHangHoa = session["dsIdHangHoa"] as List<string>;
            }
            else
            {
                dsIdHangHoa = new List<string>();
                session["dsIdHangHoa"] = dsIdHangHoa;
            }
            return Json(dsIdHangHoa.Count, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AddToCart(string pk)
        {
            List<string> dsIdHangHoa = null;
            var session = System.Web.HttpContext.Current.Session;
            if (session != null && session["dsIdHangHoa"] != null)
            {
                dsIdHangHoa = session["dsIdHangHoa"] as List<string>;
                dsIdHangHoa.Add(pk);
            }
            else
            {
                dsIdHangHoa = new List<string>();
                dsIdHangHoa.Add(pk);
            }
            session["dsIdHangHoa"] = dsIdHangHoa;
            return Json(dsIdHangHoa.Count, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AddToCartInOpLung(string pk, string arr)
        {
            List<string> dsIdHangHoa = null;
            JArray cartOpLung = new JArray();
            JObject jo = new JObject();
            var session = System.Web.HttpContext.Current.Session;
            if (session != null && session["dsIdHangHoa"] != null)
            {
                dsIdHangHoa = session["dsIdHangHoa"] as List<string>;
                dsIdHangHoa.Add(pk);
            }
            else
            {
                dsIdHangHoa = new List<string>();
                dsIdHangHoa.Add(pk);
            }
            if (session != null && session["dsIdOpLung"] != null)
            {
                cartOpLung = session["dsIdOpLung"] as JArray;
                jo.Add("pk", pk);
                jo.Add("arr", JArray.Parse(arr));
                cartOpLung.Add(jo);
            }
            else
            {
                cartOpLung = new JArray();
                cartOpLung.Add(new JObject(
                    new JProperty("pk", pk),
                    new JProperty("arr", JArray.Parse(arr)
                 )));
            }
            session["dsIdOpLung"] = cartOpLung;
            session["dsIdHangHoa"] = dsIdHangHoa;
            return Json(dsIdHangHoa.Count, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult SearchProduct(string pk)
        {
            pk = pk.Replace("'", "");
            IEnumerable<object> result = new List<object>();
            JArray arr = new JArray();
            if (pk != null && pk.Length > 2)
            {
                List<KiotVietBO.ChiTietHangHoaBO> pr = new List<KiotVietBO.ChiTietHangHoaBO>();
                pr = MemoryCacheKiot.dsHangHoa.data;
                pr = pr.DistinctBy(o => o.id).ToList();
                string[] searchTerms = Extension.RejectMarks(pk.ToString()).Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < searchTerms.Length; i++)
                {
                    pr = (from comp in pr
                          where comp.name.RejectMarksStr().ToLower().Contains(searchTerms[i].ToLower())
                          select comp).ToList();
                }
                pr = pr.OrderBy(comp => comp.name.RejectMarksStr().ToLower().StartsWith(searchTerms[0].RejectMarksStr().ToLower())).Take(10).ToList();
                for (int i = 0; i < searchTerms.Length; i++)
                {
                    arr = new JArray(
                             from comp in pr
                             where comp.name.RejectMarksStr().ToLower().Contains(searchTerms[i].ToLower())
                             orderby comp.name
                             select JObject.FromObject(new
                             {
                                 Image = String.Format("<a title=\"{0}\" href=\"{1}\"><img src=\"{2}\" alt=\"{3}\" width=\"50\" height=\"50\" /></a>", comp.name, @"/danh-muc/san-pham?pt=" + comp.id, comp.images != null ? comp.images[0] : @"Images/noimageb.png", comp.name),
                                 Product = String.Format("<a title=\"{0}\" href=\"{1}\">{2}</a>", comp.fullName, @"/danh-muc/san-pham?pt=" + comp.id, comp.name),
                                 Status = comp.ConHang > 0 ? "Còn hàng" : "Hết Hàng",
                             }));
                }
            }
            var asdf = arr.AsJEnumerable().Take(10);
            return Json(JsonConvert.SerializeObject(asdf), JsonRequestBehavior.AllowGet);
        }
        [System.Web.Http.HttpGet]
        public JsonResult RemoveFromCart(string pk)
        {
            List<string> dsIdHangHoa = null;
            var session = System.Web.HttpContext.Current.Session;
            if (session != null && session["dsIdHangHoa"] != null)
            {
                dsIdHangHoa = session["dsIdHangHoa"] as List<string>;
                while (dsIdHangHoa.Contains(pk))
                {
                    dsIdHangHoa.Remove(pk);
                }
                session["dsIdHangHoa"] = dsIdHangHoa;
            }
            else
            {
                dsIdHangHoa = new List<string>();
            }
            return Json(dsIdHangHoa.Count, JsonRequestBehavior.AllowGet);
        }
        [System.Web.Http.HttpGet]
        public JsonResult UpdateQuantity(string pk, string sl)
        {
            List<string> dsIdHangHoa = null;
            var session = System.Web.HttpContext.Current.Session;
            if (session["dsIdHangHoa"] != null)
            {
                dsIdHangHoa = session["dsIdHangHoa"] as List<string>;
                var clientRequest = new RestClient("https://public.kiotapi.com/products/" + pk);
                var requestConfig = new RestRequest(Method.GET);
                requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
                requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
                var hangHoa = clientRequest.Execute<ChiTietHangHoaBO>(requestConfig).Data;
                var tonKhoLHP = hangHoa.inventories.Where(o => o.branchId == 13933).FirstOrDefault().onHand;
                if (tonKhoLHP < Convert.ToInt32(sl))
                {
                    return Json(JsonConvert.SerializeObject(new { message = "Error", product = hangHoa.name, stock = tonKhoLHP, count = dsIdHangHoa.Count }), JsonRequestBehavior.AllowGet);
                }
                while (dsIdHangHoa.Contains(pk))
                {
                    dsIdHangHoa.Remove(pk);
                }
                var slsp = Convert.ToInt32(sl);
                for (int i = 1; i <= slsp; i++)
                {
                    dsIdHangHoa.Add(pk);
                }
                session["dsIdHangHoa"] = dsIdHangHoa;
            }
            else
            {
                dsIdHangHoa = new List<string>();
                for (int i = 1; i <= Convert.ToInt32(sl); i++)
                {
                    dsIdHangHoa.Add(pk);
                }
                session["dsIdHangHoa"] = dsIdHangHoa;
            }
            return Json(JsonConvert.SerializeObject(new { message = "", count = dsIdHangHoa.Count }), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult UpdateQuantityTempp(string pk, string sl)
        {
            List<string> dsIdHangHoa = null;
            var session = System.Web.HttpContext.Current.Session;
            if (session != null && session["dsIdHangHoa"] != null)
            {
                dsIdHangHoa = session["dsIdHangHoa"] as List<string>;
                var hangHoa = MemoryCacheKiot.dsHangHoa.data.Where(o => o.id == Convert.ToInt32(pk)).FirstOrDefault();
                var tonKhoLHP = hangHoa.inventories.Where(o => o.branchId == 13933).FirstOrDefault().onHand;
                if (tonKhoLHP < Convert.ToInt32(sl))
                {
                    JObject jo = new JObject();
                    jo.Add("message", "Error");
                    jo.Add("product", hangHoa.name);
                    jo.Add("stock", tonKhoLHP);
                    jo.Add("count", dsIdHangHoa.Count);
                    return Json(JsonConvert.SerializeObject(jo), JsonRequestBehavior.AllowGet);
                }
                while (dsIdHangHoa.Contains(pk))
                {
                    dsIdHangHoa.Remove(pk);
                }
                var slsp = Convert.ToInt32(sl);
                for (int i = 1; i <= slsp; i++)
                {
                    dsIdHangHoa.Add(pk);
                }
                session["dsIdHangHoa"] = dsIdHangHoa;
            }
            else
            {
                dsIdHangHoa = new List<string>();
                var hangHoa = MemoryCacheKiot.dsHangHoa.data.Where(o => o.id == Convert.ToInt32(pk)).FirstOrDefault();
                var tonKhoLHP = hangHoa.inventories.Where(o => o.branchId == 13933).FirstOrDefault().onHand;
                if (tonKhoLHP < Convert.ToInt32(sl))
                {
                    JObject jo = new JObject();
                    jo.Add("message", "Error");
                    jo.Add("product", hangHoa.name);
                    jo.Add("stock", tonKhoLHP);
                    jo.Add("count", dsIdHangHoa.Count);
                    return Json(JsonConvert.SerializeObject(jo), JsonRequestBehavior.AllowGet);
                }
                for (int i = 1; i <= Convert.ToInt32(sl); i++)
                {
                    dsIdHangHoa.Add(pk);
                }
                session["dsIdHangHoa"] = dsIdHangHoa;
            }
            return Json(JsonConvert.SerializeObject(new { message = "", count = dsIdHangHoa.Count }), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DatHang(string name, string phone, string email, string address, string note)
        {
            String lastname = name;
            String telephone = phone;
            String ghichudonhang = note;
            String diachi = address;
            JObject validate = new JObject();
            validate.Add("name", ""); validate.Add("phone", ""); validate.Add("phoneNumber", ""); validate.Add("address", "");
            bool validateB = true;
            if (string.IsNullOrEmpty(name))
            {
                validate["name"] = "Tên không được để trống.";
                validateB = false;
            }
            if (string.IsNullOrEmpty(phone))
            {
                validate["phone"] = "Điện thoại không được để trống.";
                validateB = false;
            }
            if (string.IsNullOrEmpty(address))
            {
                validate["address"] = "Địa chỉ không được để trống.";
                validateB = false;
            }
            if (Extension.isNumber(telephone) == false)
            {
                validate["phoneNumber"] = "Điện thoại phải là số.";
                validateB = false;
            }
            if (validateB)
            {
                var session = System.Web.HttpContext.Current.Session;
                var rootCartBO = new CartBLL().GetCartItems(session["dsIdHangHoa"] as List<string>);
                Dictionary<string, Cupon> cupon = session["dsCupon"] != null ? session["dsCupon"] as Dictionary<string, Cupon> : null;
                var items = rootCartBO.carts;
                OrderDeliveryBO orderDeliveryBO = new OrderDeliveryBO();
                orderDeliveryBO.type = 0;
                orderDeliveryBO.price = 0;
                orderDeliveryBO.receiver = lastname;
                orderDeliveryBO.contactNumber = telephone;
                orderDeliveryBO.address = diachi;
                orderDeliveryBO.partnerDeliveryId = 1970;
                PartnerDeliveryBO partnerDeliveryBO = new PartnerDeliveryBO();
                partnerDeliveryBO.code = "DT000001";
                partnerDeliveryBO.name = "nhân viên shop";
                partnerDeliveryBO.contactNumber = "0971670168";
                orderDeliveryBO.partnerDelivery = partnerDeliveryBO;
                KhachHangBO khachHang = new KhachHangBO()
                {
                    name = lastname,
                    contactNumber = telephone,
                };
                if (session["dsIdOpLung"] != null)
                {
                    var sdOplung = session["dsIdOpLung"] as JArray;
                    foreach (dynamic item in sdOplung)
                    {
                        var t = item.arr.ToString();
                        t = t.Replace("[", "").Replace("{", "").Replace("]", "").Replace("}", "").Replace(",", "\n");
                        note += t;
                    }
                }
                var checkCustomer = new CustomerBLL().checkCustomer(khachHang);
                RootOrderBO rootOrderBO = new RootOrderBO();
                rootOrderBO.customerId = checkCustomer.id;
                rootOrderBO.branchId = 13933;
                rootOrderBO.soldById = 29657;
                rootOrderBO.makeInvoice = false;
                rootOrderBO.status = 1;
                rootOrderBO.statusValue = "Phiếu tạm";
                rootOrderBO.retailerId = 24493;
                rootOrderBO.description = "Đặt hàng qua Website \n" + note;
                rootOrderBO.method = "CASH";
                rootOrderBO.orderDelivery = orderDeliveryBO;
                if (cupon != null)
                {
                    var Cupon = cupon.Values.FirstOrDefault();
                    var cuponAvailable = false;//ap dung giam gia
                    if (Cupon.suDung1Lan && Cupon.coHieuLuc)
                    {
                        var contentService = ApplicationContext.Current.Services.ContentService;
                        var content = contentService.GetById(2116);
                        var query = content.Children().Where(o => o.Name == Cupon.name).FirstOrDefault();
                        var coHieuLuc = query.GetValue<bool>("coHieuLuc");
                        if (coHieuLuc)
                        {
                            //query.SetValue("coHieuLuc", false);
                            //contentService.SaveAndPublishWithStatus(query);
                            cuponAvailable = true;
                        }
                    }
                    else if (Cupon.suDung1Lan == false && Cupon.coHieuLuc)
                    {
                        cuponAvailable = true;
                    }
                    else
                    {
                        cuponAvailable = false;
                    }
                    if (cuponAvailable)
                    {
                        var maHang_NhomHang = Cupon.maHang_NhomHang;
                        //neu cupon la ma hang hoa
                        var hangHoa = MemoryCacheKiot.dsHangHoa.data.Where(o => o.id == maHang_NhomHang).FirstOrDefault();
                        if (hangHoa != null)
                        {
                            foreach (var item in items.Where(o => o.id == hangHoa.id))
                            {
                                item.disCount = Cupon.giamGia;
                            }
                        }
                        else
                        {//nguoc lai cupon la ma nhom hang
                            var nhomHang = new NhomHangBLL().DanhNhomHangPlane().Where(o => o.categoryId == maHang_NhomHang).FirstOrDefault();
                            var nhomHangPlane = new NhomHangBLL().DanhNhomHangPlane();
                            var parents =
                                        nhomHangPlane
                                            .Where(x => x.parentId != 0)
                                            .ToDictionary(x => x.categoryId, x => x.parentId);

                            Func<int, IEnumerable<int>> getParents = null;
                            getParents = i =>
                                parents.ContainsKey(i)
                                    ? new[] { parents[i] }.Concat(getParents(parents[i]))
                                    : Enumerable.Empty<int>();
                            foreach (var cart in items)
                            {
                                var hanghoa = MemoryCacheKiot.dsHangHoa.data.Where(o => o.id == cart.id).FirstOrDefault();
                                var sdf = getParents(hanghoa.categoryId);
                                var find = sdf.ToList();
                                find.Add(hanghoa.categoryId);
                                if (find.Contains(Cupon.maHang_NhomHang))
                                {
                                    cart.disCount = Cupon.giamGia;
                                }
                            }
                        }
                    }
                    rootOrderBO.total = rootCartBO.totalAfterPriceDisCount;
                }
                else
                {
                    rootOrderBO.total = rootCartBO.totalPrice;
                }

                List<OrderDetailBO> dsHangHoaDuocDat = new List<OrderDetailBO>();
                foreach (var item in items)
                {
                    OrderDetailBO orderDetailBO = new OrderDetailBO()
                    {
                        productId = item.id,
                        productCode = item.code,
                        productName = item.name,
                        quantity = item.quantity,
                        price = item.basePrice,
                        discountRatio = item.disCount

                    };
                    dsHangHoaDuocDat.Add(orderDetailBO);
                }
                rootOrderBO.orderDetails = dsHangHoaDuocDat;
                var result = new CartBLL().PostCart(rootOrderBO);
                session["dsCupon"] = null;
                session["dsIdOpLung"] = null;
                session["dsIdHangHoa"] = null;
                return Json("Ok", JsonRequestBehavior.AllowGet);
            }
            return Json(JsonConvert.SerializeObject(validate), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DatHangMain(string name, string phone, string email, string address, string note)
        {
            String lastname = name;
            String telephone = phone;
            String ghichudonhang = note;
            String diachi = address;
            JObject validate = new JObject();
            validate.Add("name", ""); validate.Add("phone", ""); validate.Add("phoneNumber", ""); validate.Add("address", "");
            bool validateB = true;
            if (string.IsNullOrEmpty(name))
            {
                validate["name"] = "Tên không được để trống.";
                validateB = false;
            }
            if (string.IsNullOrEmpty(phone))
            {
                validate["phone"] = "Điện thoại không được để trống.";
                validateB = false;
            }
            if (string.IsNullOrEmpty(address))
            {
                validate["address"] = "Địa chỉ không được để trống.";
                validateB = false;
            }
            if (Extension.isNumber(telephone) == false)
            {
                validate["phoneNumber"] = "Điện thoại phải là số.";
                validateB = false;
            }
            if (validateB)
            {
                var session = System.Web.HttpContext.Current.Session;
                var rootCartBO = new CartBLL().GetCartItems(session["dsIdHangHoa"] as List<string>);
                Dictionary<string, Cupon> cupon = session["dsCupon"] != null ? session["dsCupon"] as Dictionary<string, Cupon> : null;
                var items = rootCartBO.carts;
                KhachHangBO khachHang = new KhachHangBO()
                {
                    name = lastname,
                    contactNumber = telephone,
                };
                if (session["dsIdOpLung"] != null)
                {
                    var sdOplung = session["dsIdOpLung"] as JArray;
                    foreach (dynamic item in sdOplung)
                    {
                        var t = item.arr.ToString();
                        t = t.Replace("[", "").Replace("{", "").Replace("]", "").Replace("}", "").Replace(",", "\n");
                        note += t;
                    }
                }
                var checkCustomer = new CustomerBLL().checkCustomer(khachHang);
                RootDatHangBO rootDatHangBO = new RootDatHangBO();

                DatHangOrder order = new DatHangOrder();
                order.BranchId = 13933;
                order.RetailerId = 244993;
                order.CustomerId = checkCustomer.id;
                order.SoldById = 29657;
                order.SoldBy = new DatHangSoldBy()
                {
                    CreatedBy = 0,
                    CreatedDate = DateTime.Now,
                    Email = "",
                    GivenName = "anh Linh",
                    Id = 29657,
                    IsActive = true,
                    IsAdmin = true,
                    Type = 0,
                    UserName = "0944505039",
                    isDeleted = false
                };
                order.SaleChannelId = 10273;
                order.Seller = new DatHangSeller()
                {
                    CreatedBy = 0,
                    CreatedDate = DateTime.Now,
                    Email = "",
                    GivenName = "anh Linh",
                    Id = 29657,
                    IsActive = true,
                    IsAdmin = true,
                    Type = 0,
                    UserName = "0944505039",
                    isDeleted = false
                };
                var codeDH = DateTime.Now;
                order.Code = "DH" + codeDH.Month + codeDH.Day + codeDH.Hour + codeDH.Minute + codeDH.Second;

                order.InvoiceOrderSurcharges = new List<object>();
                order.Payments = new List<object>();
                order.DeliveryDetail = new DatHangDeliveryDetail()
                {
                    Type = 0,
                    TypeName = "",
                    Status = 3,
                    Address = address,
                    ContactNumber = telephone,
                    Receiver = name,
                    DeliveryBy = 1970,
                    UsingPriceCod = 1
                };
                order.UsingCod = 1;
                order.Status = 1;
                //order.Total
                order.Description = note;
                JObject jo = new JObject(); JObject jo1 = new JObject();
                jo1.Add("Id", "Cash");
                jo1.Add("Label", "Tiền mặt");
                jo.Add("Amount", 0);
                jo.Add("Method", jo1);
                order.Extra = JsonConvert.SerializeObject(jo);
                order.Surcharge = 0;
                order.Type = 2;
                order.addToAccount = "0";
                order.PayingAmount = 0;
                if (cupon != null)
                {
                    var Cupon = cupon.Values.FirstOrDefault();
                    var cuponAvailable = false;//ap dung giam gia
                    if (Cupon.suDung1Lan && Cupon.coHieuLuc)
                    {
                        var contentService = ApplicationContext.Current.Services.ContentService;
                        var content = contentService.GetById(2116);
                        var query = content.Children().Where(o => o.Name == Cupon.name).FirstOrDefault();
                        var coHieuLuc = query.GetValue<bool>("coHieuLuc");
                        if (coHieuLuc)
                        {
                            //query.SetValue("coHieuLuc", false);
                            //contentService.SaveAndPublishWithStatus(query);
                            cuponAvailable = true;
                        }
                    }
                    else if (Cupon.suDung1Lan == false && Cupon.coHieuLuc)
                    {
                        cuponAvailable = true;
                    }
                    else
                    {
                        cuponAvailable = false;
                    }
                    if (cuponAvailable)
                    {
                        var maHang_NhomHang = Cupon.maHang_NhomHang;
                        //neu cupon la ma hang hoa
                        var hangHoa = MemoryCacheKiot.dsHangHoa.data.Where(o => o.id == maHang_NhomHang).FirstOrDefault();
                        if (hangHoa != null)
                        {
                            foreach (var item in items.Where(o => o.id == hangHoa.id))
                            {
                                item.disCount = Cupon.giamGia;
                            }
                        }
                        else
                        {//nguoc lai cupon la ma nhom hang
                            var nhomHang = new NhomHangBLL().DanhNhomHangPlane().Where(o => o.categoryId == maHang_NhomHang).FirstOrDefault();
                            var nhomHangPlane = new NhomHangBLL().DanhNhomHangPlane();
                            var parents =
                                        nhomHangPlane
                                            .Where(x => x.parentId != 0)
                                            .ToDictionary(x => x.categoryId, x => x.parentId);

                            Func<int, IEnumerable<int>> getParents = null;
                            getParents = i =>
                                parents.ContainsKey(i)
                                    ? new[] { parents[i] }.Concat(getParents(parents[i]))
                                    : Enumerable.Empty<int>();
                            foreach (var cart in items)
                            {
                                var hanghoa = MemoryCacheKiot.dsHangHoa.data.Where(o => o.id == cart.id).FirstOrDefault();
                                var sdf = getParents(hanghoa.categoryId);
                                var find = sdf.ToList();
                                find.Add(hanghoa.categoryId);
                                if (find.Contains(Cupon.maHang_NhomHang))
                                {
                                    cart.disCount = Cupon.giamGia;
                                }
                            }
                        }
                    }
                    order.ProductDiscount = rootCartBO.totalPrice - rootCartBO.totalAfterPriceDisCount;
                    order.Total = rootCartBO.totalAfterPriceDisCount;
                    order.TotalBeforeDiscount = rootCartBO.totalPrice;
                }
                else
                {
                    order.Total = rootCartBO.totalPrice;
                    order.TotalBeforeDiscount = rootCartBO.totalPrice;
                    order.ProductDiscount = 0;

                }

                List<DatHangOrderDetail> dsHangHoaDuocDat = new List<DatHangOrderDetail>();
                var billdetail = "<table style=\"width: 100%;border: 1px solid #ddd;\"><tr style=\"background-color: #4CAF50;height:35px;font-size: 15px;font-weight: bold;\"><td>Tên Sản Phẩm</td><td>Số lượng</td><td>Đơn Giá</td><td>Hình ảnh</td></tr>";

                foreach (var item in items)
                {
                    var discount = item.basePrice * item.disCount / 100;
                    DatHangOrderDetail orderDetailBO = new DatHangOrderDetail()
                    {
                        BasePrice = item.basePrice,
                        IsLotSerialControl = false,
                        IsRewardPoint = true,
                        Price = item.basePrice,
                        ProductId = item.id,
                        ProductCode = item.code,
                        Quantity = item.quantity,
                        DiscountRatio = item.disCount,
                        Discount = discount
                    };
                    dsHangHoaDuocDat.Add(orderDetailBO);
                    var url = @"phukienphanthiet.com/danh-muc/san-pham?pt=";
                    billdetail += "<tr><td  style=\"border: 1px solid #ddd;\"><a href=\"" + url + item.id + "\" target=\"_Blank\" title=\"" + item.name + "\">" + item.name + "</a></td>";
                    billdetail += "<td  style=\"border: 1px solid #ddd;\">" + item.quantity + "</td>";
                    billdetail += "<td  style=\"border: 1px solid #ddd;\">" + string.Format("{0:#,##0 đ}", item.totalAfterDiscount) + "</td>";
                    billdetail += "<td  style=\"border: 1px solid #ddd;\"><img src=\"" + item.image + "\" height=\"42\" width=\"42\"></td></tr>";

                }
                order.OrderDetails = dsHangHoaDuocDat;
                var result = new CartBLL().PostCartAdmin(new RootDatHangBO() { Order = order });
                if (!string.IsNullOrEmpty(email))
                {
                    SmtpClient smtp = new SmtpClient();
                    smtp.Credentials = new System.Net.NetworkCredential("giacongweb.com@gmail.com", "tyurpbvpzdtalflz");
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    try
                    {
                        MailMessage mail = new MailMessage();

                        mail.From = new MailAddress("giacongweb.com@gmail.com", "Phukienphanthiet.Com", System.Text.Encoding.UTF8);

                        mail.To.Add("banhang@phukienphanthiet.com");
                        mail.To.Add(email);
                        mail.Subject = "Đơn hàng PHỤ KIỆN PHAN THIẾT " + telephone;
                        mail.Body = "Xin Chào," + lastname + " ( " + telephone + " )" +
                            "<p>Cảm ơn bạn đã đặt hàng tại PHỤ KIỆN PHAN THIẾT</p>" +
                            "<p>Bộ phận bán hàng đã tiếp nhận đơn hàng của bạn với thông tin:</p>" +
                            "<p>Địa chỉ :" + diachi + "</p>" +
                            "<p style=\"font-weight: bold;\">Đơn hàng :" + session["maDonHang"] + " <a href=\"http://phukienphanthiet.com/don-hang?q=" + session["maDonHang"] + "\" target=\"_Blank\" title=\"Kiểm tra đơn hàng\"> (Kiểm tra trạng thái đơn hàng) </a> </p>" +
                            "<br/>" + billdetail.ToString() +
                            "<p>Tổng tiền : " + string.Format("{0:#,##0 đ}", rootCartBO.totalAfterPriceDisCount) + "</p>" +
                            "<p> PHỤ KIỆN PHAN THIẾT sẽ liên hệ với " + lastname + " để xác nhận lại </p>" +
                            "<br/><p> Chào trân trọng và hợp tác</p>" +
                        "<p>📡 Fb: https://fb.com/pkpt01 </p>" +
                        "<p>🌎 Web: http://phukienphanthiet.com</p>" +
                        "<p>🎥 Youtube: http://goo.gl/jioD9o </p>" +
                        "<p>🏭 số 02 Lê Hồng Phong, phan thiết</p>" +
                        "<p>🏢 số 12 Phạm Ngọc Thạch, phan thiết</p>";

                        mail.IsBodyHtml = true;
                        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                        smtp.Send(mail);

                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json("Ok", JsonRequestBehavior.AllowGet);
            }
            return Json(JsonConvert.SerializeObject(validate), JsonRequestBehavior.AllowGet);
        }
    }
}