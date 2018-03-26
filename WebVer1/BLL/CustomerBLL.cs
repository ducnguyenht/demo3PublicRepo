using KiotVietBO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net;
/// <summary>
/// Summary description for CustomerBLL
/// </summary>
public class CustomerBLL
{
	public CustomerBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public RetKhachHangBO checkCustomer(KhachHangBO khachHangBO)
    {
        RetKhachHangBO ret = new RetKhachHangBO();
        var clientRequest = new RestClient("https://public.kiotapi.com/customers?format=json&contactNumber=" + khachHangBO.contactNumber);
        var requestConfig = new RestRequest(Method.GET);
        requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
        requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
        var khachHang = clientRequest.Execute<RootRetKhachHangBO>(requestConfig).Data;
        if (khachHang.data!=null && khachHang.data.Count>0)
        {
            ret = khachHang.data[0];
            return ret;
        }
        else
        {
            clientRequest = new RestClient("https://public.kiotapi.com/customers");
            requestConfig = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
            requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token + "");
            requestConfig.AddHeader("Content-Type", "application/json");
            requestConfig.AddParameter("application/json", JsonConvert.SerializeObject(khachHangBO), ParameterType.RequestBody);
            var result = clientRequest.Execute<RootRetKhachHangBO>(requestConfig);           

            if (result.StatusCode != HttpStatusCode.OK)
            {
                ret= result.Data.data[0];
            }
        }
        return ret;
    }
}