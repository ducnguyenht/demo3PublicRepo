using KiotVietBO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web.WebApi;
public class MessageResult
{
    public int Status { get; set; }
    public string Message { get; set; }
    public Cupon Cupon { get; set; }
}
public class PostDatHangMain
{
    public string name { get; set; }
    public string phone { get; set; }
    public string email { get; set; }
    public string address { get; set; }
    public string note { get; set; }
    public string strCupon { get; set; }
    public List<string> dsIdHangHoa { get; set; }
}

namespace WebVer1.Controllers.Api
{
    public class PkPTApiController : UmbracoApiController
    {
     
        private RootCardBO GetCartItems(List<string> dsIdHangHoaDuocDat)
        {
            List<CartBO> cartBOs = new List<CartBO>();
            if (dsIdHangHoaDuocDat!=null)
            {
                var grouped = dsIdHangHoaDuocDat
                    .GroupBy(s => s)
                    .Select(g => new { Id = g.Key, Count = g.Count() });
                foreach (var item in grouped)
                {
                    var hangHoa = MemoryCacheKiot.dsHangHoa.data.Where(o => o.id ==Convert.ToInt32(item.Id)).FirstOrDefault();            
                    var cartBO = new CartBO()
                    {
                        basePrice = hangHoa.basePrice,
                        code=hangHoa.code,
                        description = hangHoa.description,
                        id = hangHoa.id,
                        name = hangHoa.name,
                        image = hangHoa.images != null ? hangHoa.images[0] : "",
                        quantity = item.Count,
                    
                    };
                    cartBOs.Add(cartBO);
                }
                cartBOs = cartBOs.OrderBy(o => o.name).ToList();
            }
            RootCardBO rootCardBO = new RootCardBO() { carts = cartBOs };
            return rootCardBO;
        }
        private string PostCartAdmin(RootDatHangBO rootOrderBO)
        {
            var clientRequest = new RestClient("https://nzt.kiotviet.com/api/orders");
            var requestConfig = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            requestConfig.AddHeader("BranchId", "13933");
            requestConfig.AddHeader("Authorization", "Bearer " + MemoryCashier.Token+ "");
            requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
            requestConfig.AddParameter("application/json", JsonConvert.SerializeObject(rootOrderBO), ParameterType.RequestBody);
            var result = clientRequest.Execute(requestConfig);
            //if (result.StatusCode == HttpStatusCode.OK)
            //{
            //     var dh = Newtonsoft.Json.Linq.JObject.Parse(result.Content);
            //    return result.Content;//(string)dh["Code"];
            //}
            return result.Content;
        }
        [HttpGet]
        public RootCardBO GetCarts(string strDsIdHangHoa, string cupon = null)
        {
            List<string> dsIdHangHoa = new List<string>();
            RootCardBO rootCardBO = new RootCardBO();
            if (string.IsNullOrEmpty(cupon))
            {
                dsIdHangHoa = strDsIdHangHoa.Split(',').ToList();
                return this.GetCartItems(dsIdHangHoa);
            }
            else
            {
                dsIdHangHoa = strDsIdHangHoa.Split(',').ToList();
                var rootCartBO = this.GetCartItems(dsIdHangHoa);
                var items = rootCartBO.carts;
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
                    Cupon Cupon = new Cupon()
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
                        var cuponAvailable = false;
                        if (Cupon.suDung1Lan && Cupon.coHieuLuc)
                        {
                            cuponAvailable = Cupon.coHieuLuc;
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
                        return rootCartBO; 
                    }
                    else
                    {
                        return this.GetCartItems(dsIdHangHoa);
                    }
                }
            }
            return null;
        }

        [HttpGet]
        public MessageResult GetCheckCupon(string cupon)
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
                        return new MessageResult() { Status = 1, Message = "Ok", Cupon = cp };
                    }
                    else
                    {
                        return new MessageResult() { Status = 3, Message = "Mã giảm giá đã được sử dụng.Vui lòng nhập mã khác." };
                    }
                }
                else
                {
                    return new MessageResult() { Status = 4, Message = "Mã giảm giá đã quá hạn sử dụng." };
                }
            }
            return new MessageResult() { Status = 5, Message = "Mã giảm giá không đúng.Vui lòng nhập mã khác." };
        }
        [HttpPost]
        public HttpResponseMessage PostDatHangMainResponse(PostDatHangMain datHang)
        {
            JavaScriptSerializer serialiser = new JavaScriptSerializer();
            HttpContext.Current.Response.ContentType = "application/json";
         
            String lastname = datHang.name;
            String telephone = datHang.phone;
            String ghichudonhang = datHang.note;
            String diachi = datHang.address;
            JObject validate = new JObject();
            validate.Add("name", ""); validate.Add("phone", ""); validate.Add("phoneNumber", ""); validate.Add("address", "");
            bool validateB = true;
            if (string.IsNullOrEmpty(datHang.name))
            {
                validate["name"] = "Tên không được để trống.";
                validateB = false;
            }
            if (string.IsNullOrEmpty(datHang.phone))
            {
                validate["phone"] = "Điện thoại không được để trống.";
                validateB = false;
            }
            if (string.IsNullOrEmpty(datHang.address))
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
                var rootCartBO = this.GetCartItems(datHang.dsIdHangHoa);
                Cupon cp = null;
                if (!string.IsNullOrEmpty(datHang.strCupon))
                {
                    var contentService = ApplicationContext.Current.Services.ContentService;
                    var content = contentService.GetById(2116);
                    var query = content.Children().Where(o => o.Name == datHang.strCupon).FirstOrDefault();
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
                        if (tuNgay <= DateTime.Now && DateTime.Now <= denNgay)
                        {
                            if (coHieuLuc)
                            {
                                cp = new Cupon()
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
                            }
                        }
                    }
                }

                var items = rootCartBO.carts;
                KhachHangBO khachHang = new KhachHangBO()
                {
                    name = lastname,
                    contactNumber = telephone,
                };
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
                    Address = datHang.address,
                    ContactNumber = telephone,
                    Receiver = datHang.name,
                    DeliveryBy = 1970,
                    UsingPriceCod = 1
                };
                order.UsingCod = 1;
                order.Status = 1;
                //order.Total
                order.Description = datHang.note;
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
                if (cp != null)
                {
                    var Cupon = cp;
                    var cuponAvailable = false;
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
                var result = this.PostCartAdmin(new RootDatHangBO() { Order = order });
                if (result.Contains("KvValidateInvoiceException") || result.Contains("ErrorCode"))
                {
                    HttpContext.Current.Response.StatusCode = 400;
                    HttpContext.Current.Response.Write(result);
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
                else
                {
                    if (!string.IsNullOrEmpty(datHang.email))
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
                            mail.To.Add(datHang.email);
                            mail.Subject = "Đơn hàng PHỤ KIỆN PHAN THIẾT " + telephone;
                            mail.Body = "Xin Chào," + lastname + " ( " + telephone + " )" +
                            "<p>Cảm ơn bạn đã đặt hàng tại PHỤ KIỆN PHAN THIẾT</p>" +
                            "<p>Bộ phận bán hàng đã tiếp nhận đơn hàng của bạn với thông tin:</p>" +
                            "<p>Địa chỉ :" + diachi + "</p>" +
                            "<p style=\"font-weight: bold;\">Đơn hàng :" + result + " <a href=\"http://phukienphanthiet.com/don-hang?q=" + result + "\" target=\"_Blank\" title=\"Kiểm tra đơn hàng\"> (Kiểm tra trạng thái đơn hàng) </a> </p>" +
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
                        catch (Exception ex) { }
                    }
                }
                HttpContext.Current.Response.Write(result);//serialiser.Serialize(JObject.Parse(result))//do'
             
                //return result;
            }
            //return "false";
          
            return new HttpResponseMessage();
        }

        [HttpPost]
        public string PostDatHangMain(PostDatHangMain datHang)//string name, string phone, string email, string address, string note, string strCupon = null)
        {
            String lastname = datHang.name;
            String telephone = datHang.phone;
            String ghichudonhang = datHang.note;
            String diachi = datHang.address;
            JObject validate = new JObject();
            validate.Add("name", ""); validate.Add("phone", ""); validate.Add("phoneNumber", ""); validate.Add("address", "");
            bool validateB = true;
            if (string.IsNullOrEmpty(datHang.name))
            {
                validate["name"] = "Tên không được để trống.";
                validateB = false;
            }
            if (string.IsNullOrEmpty(datHang.phone))
            {
                validate["phone"] = "Điện thoại không được để trống.";
                validateB = false;
            }
            if (string.IsNullOrEmpty(datHang.address))
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
                var rootCartBO = this.GetCartItems(datHang.dsIdHangHoa);
                Cupon cp = null;
                if (!string.IsNullOrEmpty(datHang.strCupon))
                {
                    var contentService = ApplicationContext.Current.Services.ContentService;
                    var content = contentService.GetById(2116);
                    var query = content.Children().Where(o => o.Name == datHang.strCupon).FirstOrDefault();
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
                        if (tuNgay <= DateTime.Now && DateTime.Now <= denNgay)
                        {
                            if (coHieuLuc)
                            {
                                cp = new Cupon()
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
                            }
                        }
                    }
                }

                var items = rootCartBO.carts;
                KhachHangBO khachHang = new KhachHangBO()
                {
                    name = lastname,
                    contactNumber = telephone,
                };
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
                    Address = datHang.address,
                    ContactNumber = telephone,
                    Receiver = datHang.name,
                    DeliveryBy = 1970,
                    UsingPriceCod = 1
                };
                order.UsingCod = 1;
                order.Status = 1;
                //order.Total
                order.Description = datHang.note;
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
                if (cp != null)
                {
                    var Cupon = cp;
                    var cuponAvailable = false;
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
                var result = this.PostCartAdmin(new RootDatHangBO() { Order = order });
                if (!string.IsNullOrEmpty(datHang.email))
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
                        mail.To.Add(datHang.email);
                        mail.Subject = "Đơn hàng PHỤ KIỆN PHAN THIẾT " + telephone;
                        mail.Body = "Xin Chào," + lastname + " ( " + telephone + " )" +
                        "<p>Cảm ơn bạn đã đặt hàng tại PHỤ KIỆN PHAN THIẾT</p>" +
                        "<p>Bộ phận bán hàng đã tiếp nhận đơn hàng của bạn với thông tin:</p>" +
                        "<p>Địa chỉ :" + diachi + "</p>" +
                        "<p style=\"font-weight: bold;\">Đơn hàng :" + result + " <a href=\"http://phukienphanthiet.com/don-hang?q=" + result + "\" target=\"_Blank\" title=\"Kiểm tra đơn hàng\"> (Kiểm tra trạng thái đơn hàng) </a> </p>" +
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
                    catch (Exception ex) { }
                }
                return result;
            }
            return "false";
        }
    }
}