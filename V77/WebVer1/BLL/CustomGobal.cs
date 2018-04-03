using KiotVietBO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using umbraco.cms.businesslogic.web;
using Umbraco.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Core;
public class CustomGlobal : UmbracoApplication
{
    public void Init(HttpApplication application)
    {
        application.PreRequestHandlerExecute += application_PreRequestHandlerExecute;
        application.BeginRequest += this.Application_BeginRequest;
        application.EndRequest += this.Application_EndRequest;
        application.Error += Application_Error;
    }
    protected override void OnApplicationStarted(object sender, EventArgs e)
    {
        base.OnApplicationStarted(sender, e);
        KiotVietConst.client_id = ConfigurationManager.AppSettings["client_id"];
        KiotVietConst.client_secret = ConfigurationManager.AppSettings["client_secret"];
        KiotVietConst.scopes = ConfigurationManager.AppSettings["scopes"];
        KiotVietConst.grant_type = ConfigurationManager.AppSettings["grant_type"];
        KiotVietConst.TokenEndPoint = ConfigurationManager.AppSettings["TokenEndPoint"];
        KiotVietConst.AuthorizationEndPoint = ConfigurationManager.AppSettings["AuthorizationEndPoint"];
        KiotVietConst.Retailer = ConfigurationManager.AppSettings["Retailer"];
    }
    private void application_PreRequestHandlerExecute(object sender, EventArgs e)
    {
        try
        {
            if (Session != null && Session.IsNewSession)
            {
                KiotVietConst.kiot_token = (string)HttpContext.Current.Cache["ApiAuthenticationToken"];
                if (string.IsNullOrWhiteSpace(KiotVietConst.kiot_token))
                {
                    var clientRequestRestSharp = new RestClient(KiotVietConst.TokenEndPoint);
                    var requestRequestRestSharp = new RestRequest(Method.POST);
                    requestRequestRestSharp.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    requestRequestRestSharp.AddParameter(KiotVietConst.propNameScopes, KiotVietConst.scopes);
                    requestRequestRestSharp.AddParameter(KiotVietConst.propNameGrantType, KiotVietConst.grant_type);
                    requestRequestRestSharp.AddParameter(KiotVietConst.propNameClientId, KiotVietConst.client_id);
                    requestRequestRestSharp.AddParameter(KiotVietConst.propNameClientSecret, KiotVietConst.client_secret);
                    var responseRestSharp = clientRequestRestSharp.Execute<AccessTokenBO>(requestRequestRestSharp);
                    switch (responseRestSharp.StatusCode)
                    {
                        case 0:
                            var contentService = ApplicationContext.Current.Services.ContentService;
                            var content = contentService.GetById(2057);
                            KiotVietConst.kiot_token = content.GetValue("token").ToString();
                            break;
                        case HttpStatusCode.OK:
                            KiotVietConst.kiot_token = responseRestSharp.Data.access_token;
                            contentService = ApplicationContext.Current.Services.ContentService;
                            content = contentService.GetById(2057);
                            content.SetValue("token", KiotVietConst.kiot_token);
                            contentService.SaveAndPublishWithStatus(content);
                            HttpContext.Current.Cache.Insert(
                                "ApiAuthenticationToken",
                                KiotVietConst.kiot_token,
                                null,
                                DateTime.UtcNow.AddSeconds(responseRestSharp.Data.expires_in),
                                System.Web.Caching.Cache.NoSlidingExpiration
                            );
                            break;
                    }
                }
           
                if (string.IsNullOrEmpty((string)HttpContext.Current.Cache["CacheDB"]))
                {
                    new NhomHangBLL().DanhSachNhomHangCache();
                    new HangHoaBLL().DanhSachHangHoaCache();
                    new ChiNhanhBLL().DanhSachChiNhanh();
                    HttpContext.Current.Cache.Insert(
                         "CacheDB",
                         "Cache1hour",
                         null,
                         DateTime.UtcNow.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["ResetData"])),
                         System.Web.Caching.Cache.NoSlidingExpiration
                     );
                }
            }
        }
        catch (Exception ex) { }
    }
    private void Application_BeginRequest(object sender, EventArgs e)
    {
        
    }
    private void Application_EndRequest(object sender, EventArgs e)
    {
    }
    protected new void Application_Error(object sender, EventArgs e)
    {
    }
}
