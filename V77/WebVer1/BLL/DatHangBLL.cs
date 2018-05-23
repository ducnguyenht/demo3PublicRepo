using KiotVietBO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebVer1.BLL
{
    public class DatHangBLL
    {
        public string TimKiemDonHang(string maDonHangOrSoDT,out dynamic ghnDetail)
        {
            ghnDetail = null;
            var checkNumber = System.Text.RegularExpressions.Regex.IsMatch(maDonHangOrSoDT, @"^\d+$");
            string timTheoSoDT = ""; string timTheoMaDH = "";
            if (checkNumber)
            {//neu la so dien thoai
timTheoSoDT = string.Format(@"https://nzt.kiotviet.com/api/Orders?format=json
&Includes=OrderDetails
&Includes=InvoiceDeliveries
&CustomerKey={0}
&%24top=10
&%24filter=(
BranchId+eq+13933+and+
PurchaseDate+eq+%27alltime%27+and+
PurchaseDate+eq+%27alltime%27+and+
(Status+eq+1+or+Status+eq+2+or+Status+eq+3))", maDonHangOrSoDT);
                var clientRequest = new RestClient(timTheoSoDT.Replace(System.Environment.NewLine, ""));
                var requestConfig = new RestRequest(Method.GET);
                requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
                requestConfig.AddHeader("Authorization", "Bearer " + MemoryCashier.Token);
                var ret=clientRequest.Execute(requestConfig).Content;
                dynamic data = JObject.Parse(ret);
                if (data.Data != null && data.Data.Count > 0)
                {
                    foreach (var donHang in data.Data)
                    {
                        foreach (var delivery in donHang.InvoiceDeliveries)
                        {
                            var deliveryCode = delivery.DeliveryCode != null ? delivery.DeliveryCode : "";
                            if (!string.IsNullOrEmpty(deliveryCode.ToString()))
                            {
                                var queryGHNDatHang = @"https://console.ghn.vn/api/v1/apiv3/OrderInfo";
                                clientRequest = new RestClient(queryGHNDatHang);
                                requestConfig = new RestRequest(Method.POST);
                                requestConfig.AddHeader("Content-Type", "application/json");
                                requestConfig.AddParameter("token", "5af966051070b0063166c0c6");
                                requestConfig.AddParameter("OrderCode", deliveryCode.ToString());
                                var ghnDetailString = clientRequest.Execute(requestConfig).Content;
                                ghnDetail= JObject.Parse(ghnDetailString);
                            }
                        }
                    }
                }
                return ret;
            }
            else
            {//neu la ma don hang
timTheoMaDH = string.Format(@"https://nzt.kiotviet.com/api/Orders?format=json
&Includes=OrderDetails
&Includes=InvoiceDeliveries
&CodeKey={0}
&%24top=10
&%24filter=(
BranchId+eq+13933+and+
PurchaseDate+eq+%27alltime%27+and+
PurchaseDate+eq+%27alltime%27+and+
(Status+eq+1+or+Status+eq+2+or+Status+eq+3))", maDonHangOrSoDT);
                var clientRequest = new RestClient(timTheoMaDH.Replace(System.Environment.NewLine, ""));
                var requestConfig = new RestRequest(Method.GET);
                requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
                requestConfig.AddHeader("Authorization", "Bearer " + MemoryCashier.Token);
                var ret = clientRequest.Execute(requestConfig).Content;
                dynamic data = JObject.Parse(ret);
                if (data.Data != null && data.Data.Count > 0)
                {
                    foreach (var donHang in data.Data)
                    {
                        foreach (var delivery in donHang.InvoiceDeliveries)
                        {
                            var deliveryCode = delivery.DeliveryCode!=null? delivery.DeliveryCode:"";
                            if (!string.IsNullOrEmpty(deliveryCode.ToString()))
                            {
                                var queryGHNDatHang = @"https://console.ghn.vn/api/v1/apiv3/OrderInfo";
                                clientRequest = new RestClient(queryGHNDatHang);
                                requestConfig = new RestRequest(Method.POST);
                                requestConfig.AddHeader("Content-Type", "application/json");
                                requestConfig.AddParameter("application/json", JsonConvert.SerializeObject(new { token= "5af966051070b0063166c0c6", OrderCode= deliveryCode.ToString() }), ParameterType.RequestBody);

                                //requestConfig.AddParameter("token", "5af966051070b0063166c0c6");
                                //requestConfig.AddParameter("OrderCode", deliveryCode.ToString());
                                var ghnDetailString = clientRequest.Execute(requestConfig).Content;
                                ghnDetail = JObject.Parse(ghnDetailString);
                            }
                        }
                    }
                }
                return ret;
            }
        }
    }
}
//timTheoSoDT = string.Format(@"https://nzt.kiotviet.com/api/Orders?format=json
//&Includes=Branch
//&Includes=Customer
//&Includes=Payments
//&Includes=Seller
//&Includes=User
//&Includes=InvoiceOrderSurcharges
//&Includes=InvoiceDeliveries
//&ForSummaryRow=true
//&ForManageScreen=true
//&Includes=SaleChannel
//&Includes=Invoices
//&%24inlinecount=allpages
//&CodeKey=
//&CustomerKey={0}
//&ExpectedDeliveryFilterType=alltime
//&%24top=10
//&%24filter=(
//BranchId+eq+13933+and+
//PurchaseDate+eq+%27alltime%27+and+
//PurchaseDate+eq+%27alltime%27+and+
//(Status+eq+1+or+Status+eq+2+or+Status+eq+3))", maDonHangOrSoDT);

//    timTheoMaDH = string.Format(@"https://nzt.kiotviet.com/api/Orders?format=json
//&Includes=Branch
//&Includes=Customer
//&Includes=Payments
//&Includes=Seller
//&Includes=User
//&Includes=InvoiceOrderSurcharges
//&Includes=InvoiceDeliveries
//&ForSummaryRow=true
//&ForManageScreen=true
//&Includes=SaleChannel
//&Includes=Invoices
//&%24inlinecount=allpages
//&CodeKey={0}
//&CustomerKey=
//&UserNameKey=
//&ExpectedDeliveryFilterType=alltime
//&%24top=10
//&%24filter=(
//BranchId+eq+13933+and+
//PurchaseDate+eq+%27alltime%27+and+
//PurchaseDate+eq+%27alltime%27+and+
//(Status+eq+1+or+Status+eq+2+or+Status+eq+3))", maDonHangOrSoDT);

