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

}
namespace WebVer1.Controllers.AjaxSurfaceController
{
    public class CartsSurfaceController : Umbraco.Web.Mvc.SurfaceController
    {
        [System.Web.Http.HttpGet]
        public JsonResult CheckCupon(string cupon)
        {
            var contentService = ApplicationContext.Current.Services.ContentService;
            var content = contentService.GetById(2116);
            var query = content.Children().Where(o => o.Name == cupon).FirstOrDefault();
            if (query!=null)
            {
                var tuNgay = query.GetValue<DateTime>("tuNgay");
                var denNgay = query.GetValue<DateTime>("denNgay");
                var giamGia = query.GetValue<int>("giamGia");
                var suDung1Lan = query.GetValue<bool>("suDung1Lan");
                var suDungNhieuLan = query.GetValue<bool>("suDungNhieuLan");
                var coHieuLuc = query.GetValue<bool>("coHieuLuc");
                var maNhomHangHoa = query.GetValue<int>("maNhomHangHoa");
                Cupon cp = new Cupon() {
                    coHieuLuc = coHieuLuc,
                    denNgay=denNgay,
                    giamGia=giamGia,
                    name=query.Name,
                    suDung1Lan=suDung1Lan,
                    suDungNhieuLan=suDungNhieuLan,
                    tuNgay=tuNgay,
                    maHang_NhomHang = maNhomHangHoa
                };
                if (tuNgay <= DateTime.Now && DateTime.Now <= denNgay)
                {
                    if (coHieuLuc)
                    {
                        var session = System.Web.HttpContext.Current.Session;
                        if (session["dsCupon"] == null && session != null)
                        {
                            Dictionary<string, Cupon> dsCupon = new Dictionary<string, Cupon>();
                            dsCupon.Add(query.Name, cp);
                            session["dsCupon"] = dsCupon;
                            return Json(new { message = "Mã giảm giá này sẽ được áp dụng khi hoàn tất đơn hàng.", count = 1 }, JsonRequestBehavior.AllowGet);
                        }else if(session["dsCupon"] != null && session != null)
                        {
                            return Json(new { message = "Mã giảm giá này sẽ được áp dụng khi hoàn tất đơn hàng.", count = 1 }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { message = "Mã giảm giá đã được sử dụng.Vui lòng nhập mã khác.", count = 1 }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { message = "Mã giảm giá đã quá hạn sử dụng.", count = 1 }, JsonRequestBehavior.AllowGet);

                }
            }
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
            if (session != null && session["dsIdOpLung"]!=null)
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
                    new JProperty("pk",pk),
                    new JProperty("arr",JArray.Parse(arr)
                 )));
            }
            session["dsIdOpLung"] = cartOpLung;
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
                Dictionary<string, Cupon> cupon = session["dsCupon"]!=null? session["dsCupon"] as Dictionary<string, Cupon> : null;
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
                if (session["dsIdOpLung"]!=null)
                {
                    var sdOplung = session["dsIdOpLung"] as JArray;
                    foreach (dynamic item in sdOplung)
                    {
                        var t = item.arr.ToString();
                        t= t.Replace("[", "").Replace("{","").Replace("]","").Replace("}","").Replace(",","\n");
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
                rootOrderBO.description = "Đặt hàng qua Website \n"+note;
                rootOrderBO.method = "CASH";
                rootOrderBO.orderDelivery = orderDeliveryBO;
                if (cupon!=null)
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
                            query.SetValue("coHieuLuc", false);
                            contentService.SaveAndPublishWithStatus(query);
                            cuponAvailable = true;
                        }
                    }
                    else if (Cupon.suDung1Lan==false && Cupon.coHieuLuc)
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
                        discountRatio=item.disCount
                    };
                    dsHangHoaDuocDat.Add(orderDetailBO);
                }
                rootOrderBO.orderDetails = dsHangHoaDuocDat;
                //var result = new CartBLL().PostCart(rootOrderBO);
                session["dsCupon"] = null;
                session["dsIdOpLung"] = null;
                session["dsIdHangHoa"] = null;
                return Json("Ok", JsonRequestBehavior.AllowGet);
            }
            return Json( JsonConvert.SerializeObject(validate) , JsonRequestBehavior.AllowGet);
        }
    }
}