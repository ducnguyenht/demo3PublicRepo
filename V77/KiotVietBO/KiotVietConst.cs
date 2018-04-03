
using System;
using System.Linq.Expressions;
namespace KiotVietBO
{
    public static class KiotVietConst
    {
        public static string Retailer = "";
        public static string scopes = "";//PublicApi.Access
        public static string grant_type = "";//client_credentials
        public static string client_id = "";//0e48dca2-8530-498d-9ec1-80b47cb741a0
        public static string client_secret = "";//A15BA8A712ACF6B4B93EC51CA7884C786FD77256
        public static string kiot_token = "";
        public static string TokenEndPoint = "";//http://id.kiotviet.vn/connect/token
        public static string AuthorizationEndPoint = "";//http://id.kiotviet.vn/connect/authorize  

        public static string propNameRetailer
        {
            get { return GetPropertyName(() => KiotVietConst.Retailer); }
        }
        public static string propNameScopes
        {
            get { return GetPropertyName(() => KiotVietConst.scopes); }
        }
        public static string propNameGrantType
        {
            get { return GetPropertyName(() => KiotVietConst.grant_type); }
        }
        public static string propNameClientId
        {
            get { return GetPropertyName(() => KiotVietConst.client_id); }
        }
        public static string propNameClientSecret
        {
            get { return GetPropertyName(() => KiotVietConst.client_secret); }
        }
        public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            MemberExpression me = propertyLambda.Body as MemberExpression;
            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            string result = string.Empty;
            do
            {
                result = me.Member.Name + "." + result;
                me = me.Expression as MemberExpression;
            } while (me != null);

            result = result.Remove(result.Length - 1); // remove the trailing "."
            return result;
        }
    }
}
