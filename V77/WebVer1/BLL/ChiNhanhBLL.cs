using KiotVietBO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ChiNhanhBLL
/// </summary>
public class ChiNhanhBLL
{
	public ChiNhanhBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public RootChiNhanhBO DanhSachChiNhanh()
    {
        if (MemoryCacheKiot.dsChiNhanh==null)
        {
            var clientRequest = new RestClient("https://public.kiotapi.com//branches?format=json&includeRemoveIds=true&orderBy=modifiedDate&orderDirection=desc&pageSize=30&currentItem=0");
            var requestConfig = new RestRequest(Method.GET);
            requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
            requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
            MemoryCacheKiot.dsChiNhanh = clientRequest.Execute<RootChiNhanhBO>(requestConfig).Data;
        }
        return MemoryCacheKiot.dsChiNhanh;
    }
}