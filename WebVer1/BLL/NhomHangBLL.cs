using KiotVietBO;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web;
/// <summary>
/// Summary description for NhomHangBLL
/// </summary>
public class NhomHangBLL
{

    public NhomHangBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public RootNhomHangBO DanhSachNhomHangCache()
    {
        if (MemoryCacheKiot.dsNhomHang == null)
        {
            var clientRequest = new RestClient("https://public.kiotapi.com/categories?format=json&includeRemoveIds=true&pageSize=30&currentItem=0&orderBy=modifiedDate&orderDirection=desc&hierachicalData=true");
            var requestConfig = new RestRequest(Method.GET);
            requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
            requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
            MemoryCacheKiot.dsNhomHang = clientRequest.Execute<RootNhomHangBO>(requestConfig).Data;
            foreach (var item in MemoryCacheKiot.dsNhomHang.data)
            {
                ApplyNestingLevel(item, 0);
            }
        }
        return MemoryCacheKiot.dsNhomHang;
    }
    public void ApplyNestingLevel(NhomHangBO bo, int level)
    {
        if (bo == null) { return; }
        bo.level = level;
        if (bo.hasChild)
        {
            foreach (var child in bo.children)
            {
                ApplyNestingLevel(child, level + 1);
            }
        }
    }

    public List<NhomHangBO> DanhNhomHangPlane()
    {
        var ds = new List<NhomHangBO>();
        foreach (var item in MemoryCacheKiot.dsNhomHang.data)
        {
            NhomHangBO nh = new NhomHangBO()
            {
                categoryId = item.categoryId,
                categoryName = item.categoryName,
                hasChild = item.hasChild,
                level=0
            };
            ds.Add(nh);
            if (item.hasChild)
            {
                foreach (var item1 in item.children)
                {
                    item1.level = 1;
                    ds.Add(item1);
                    if (item1.hasChild)
                    {
                        foreach (var item2 in item1.children)
                        {
                            item2.level = 2;
                            ds.Add(item2);
                        }
                    }
                }
            }
        }
        return ds;
    }
    public async Task<RootNhomHangBO> GetDotNetCountAsync()
    {
        RootNhomHangBO ret = new RootNhomHangBO();
        var clientRequest = new RestClient("https://public.kiotapi.com/categories?format=json&includeRemoveIds=true&pageSize=30&currentItem=0&orderBy=modifiedDate&orderDirection=desc&hierachicalData=true");
        var taskCompletionSource = new TaskCompletionSource<RootNhomHangBO>();
        var requestConfig = new RestRequest(Method.GET);
        requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
        requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
        //var asyncHandler = clientRequest.ExecuteAsync<RootNhomHangBO>(requestConfig, (response) => taskCompletionSource.SetResult(response.Data));
        //var client = new RestClient("http://www.YOUR SITE.com/api/");
        //var request = new RestRequest("Products", Method.GET);
        var response = await clientRequest.ExecuteTaskAsync<RootNhomHangBO>(requestConfig);
        var t = response.Data;
        //RootNhomHangBO dsNhomHang =await  clientRequest.ExecuteAsync<RootNhomHangBO>()(requestConfig).Data;
        return response.Data;
    }

    public RootNhomHangBO DanhSachNhomHang()
    {

        var clientRequest = new RestClient("https://public.kiotapi.com/categories?format=json&includeRemoveIds=true&pageSize=30&currentItem=0&orderBy=modifiedDate&orderDirection=desc&hierachicalData=true");
        var requestConfig = new RestRequest(Method.GET);
        requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
        requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
        RootNhomHangBO dsNhomHang = clientRequest.Execute<RootNhomHangBO>(requestConfig).Data;

        //HttpContext.Current.Cache.Insert(
        //               "DanhSachNhomHang",
        //               KiotVietConst.kiot_token,
        //               null,
        //               DateTime.UtcNow.AddDays(1),
        //               System.Web.Caching.Cache.NoSlidingExpiration
        //           );

        return dsNhomHang;
    }
    public NhomHangBO ChiTietNhomHang(string id)
    {
        var clientRequest = new RestClient("https://public.kiotapi.com/categories/" + id);
        var requestConfig = new RestRequest(Method.GET);
        requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
        requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
        return clientRequest.Execute<NhomHangBO>(requestConfig).Data;
    }
    public DSNhomHangBo ChiTietNhomHangCon(string id)
    {
        var clientRequest = new RestClient("https://public.kiotapi.com/categories/" + id);
        var requestConfig = new RestRequest(Method.GET);
        requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
        requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
        return clientRequest.Execute<DSNhomHangBo>(requestConfig).Data;
    }

    public void ThemNhomHang(NhomHangBO nhomHang)
    {
        var clientRequest = new RestClient("https://public.kiotapi.com/categories");
        var requestConfig = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
        requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
        requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token + "");
        requestConfig.AddHeader("Content-Type", "application/json");
        requestConfig.AddParameter("application/json", JsonConvert.SerializeObject(nhomHang), ParameterType.RequestBody);

        var result = clientRequest.Execute(requestConfig);
        if (result.StatusCode != HttpStatusCode.OK)
        {
            throw result.ErrorException;
        }
    }
}

