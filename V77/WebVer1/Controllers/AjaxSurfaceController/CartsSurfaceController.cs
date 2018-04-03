﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KiotVietBO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
namespace WebVer1.Controllers.AjaxSurfaceController
{
    public class CartsSurfaceController : Umbraco.Web.Mvc.SurfaceController
    {
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
        public JsonResult SearchProduct(string pk)
        {
            pk=pk.Replace("'", "");
            IEnumerable<object> result = new List<object>();
            JArray arr = new JArray();
            if (pk != null && pk.Length > 2)
            {
                var pr = MemoryCacheKiot.dsHangHoa.data;
                var sdf = pr.Where(o => o.name.Contains(pk));
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
                    return Json(JsonConvert.SerializeObject(new { message = "Error", product = hangHoa.name, stock = tonKhoLHP, count = dsIdHangHoa.Count }),JsonRequestBehavior.AllowGet);
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
            return Json(JsonConvert.SerializeObject(new { message = "", count = dsIdHangHoa.Count }),JsonRequestBehavior.AllowGet);
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
            if (string.IsNullOrEmpty(name) )
            {
                validate["name"]= "Tên không được để trống.";
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
            if (Extension.isNumber(telephone)==false)
            {
                validate["phoneNumber"] = "Điện thoại phải là số.";
                validateB = false;
            }
            if (validateB)
            {
                var session = System.Web.HttpContext.Current.Session;
                var rootCartBO = new CartBLL().GetCartItems(session["dsIdHangHoa"] as List<string>);
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
                var checkCustomer = new CustomerBLL().checkCustomer(khachHang);
                RootOrderBO rootOrderBO = new RootOrderBO();
                rootOrderBO.customerId = checkCustomer.id;
                rootOrderBO.branchId = 13933;
                rootOrderBO.soldById = 29657;
                rootOrderBO.total = rootCartBO.totalPrice;
                rootOrderBO.makeInvoice = false;
                rootOrderBO.status = 1;
                rootOrderBO.statusValue = "Phiếu tạm";
                rootOrderBO.retailerId = 24493;
                rootOrderBO.description = "Đặt hàng qua Website \n"+note;
                rootOrderBO.method = "CASH";
                rootOrderBO.orderDelivery = orderDeliveryBO;
                List<OrderDetailBO> dsHangHoaDuocDat = new List<OrderDetailBO>();
                foreach (var item in items)
                {
                    OrderDetailBO orderDetailBO = new OrderDetailBO()
                    {
                        productId = item.id,
                        productCode = item.code,
                        productName = item.name,
                        quantity = item.quantity,
                        price = item.basePrice
                    };
                    dsHangHoaDuocDat.Add(orderDetailBO);
                }
                rootOrderBO.orderDetails = dsHangHoaDuocDat;
                var result = new CartBLL().PostCart(rootOrderBO);
                System.Web.HttpContext.Current.Session["dsIdHangHoa"] = null;
                return Json("Ok", JsonRequestBehavior.AllowGet);
            }
            return Json( JsonConvert.SerializeObject(validate) , JsonRequestBehavior.AllowGet);
        }
    }
}