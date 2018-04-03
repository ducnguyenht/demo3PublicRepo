using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using umbraco.cms.businesslogic.web;
using umbraco.NodeFactory;
using System.Reflection;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections.Specialized;
using System.ComponentModel;
/// <summary>
/// Money-umbraco my extension
/// </summary>
public static class Extension
{
 


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
    public static IEnumerable<T> DucOrderBy<T>(this IEnumerable<T> entities, string sort)
    {
        string propertyName = sort.Split(' ')[0];
        string descending = sort.Split(' ')[1].ToLower();
        if (!entities.Any() || string.IsNullOrEmpty(propertyName))
            return entities;
       
        var propertyInfo = entities.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        //var CreateDate = entities.First().GetType().GetProperty("CreateDate", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (descending=="desc")
        {
          return entities.OrderByDescending(e => propertyInfo.GetValue(e, null));//.ThenByDescending(e => CreateDate.GetValue(e, null))
        }
        return entities.OrderBy(e => propertyInfo.GetValue(e, null));//.ThenByDescending(e => CreateDate.GetValue(e, null)
    }


    /// <summary>
    /// use int id = request.QueryString.GetValue<int>("id");
    /// DateTime date = request.QueryString.GetValue<DateTime>("date");
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T GetValue<T>(this NameValueCollection collection, string key)
    {
        if(collection == null)
        {
            throw new ArgumentNullException("collection");
        }

        var value = collection[key];

        if(value == null)
        {
            throw new ArgumentOutOfRangeException("key");
        }

        var converter = TypeDescriptor.GetConverter(typeof(T));

        if(!converter.CanConvertFrom(typeof(string)))
        {
            throw new ArgumentException(String.Format("Cannot convert '{0}' to {1}", value, typeof(T)));
        }

        return (T) converter.ConvertFrom(value);
    }

    private static Regex oClearHtmlScript = new Regex(@"<(.|\n)*?>", RegexOptions.Compiled);

    public static string RemoveAllHTMLTags(string sHtml)
    {
        if (sHtml!=null  )
        {
            sHtml = Regex.Replace(sHtml, "<.*?>", "");
            if (string.IsNullOrEmpty(sHtml))
                return string.Empty;
            sHtml = oClearHtmlScript.Replace(sHtml, string.Empty);
        }

        return sHtml;
    }
    public static bool CheckAlias(string alias)
        {
            if (!alias.Contains(" "))
                if (!alias.Contains("-"))
                    return true;
            if (string.IsNullOrEmpty(alias))
                return false;
            if (alias.Contains(" "))
                return false;
            if (!alias.Contains("-"))
                return false;
            return true;
        }
        public static bool isNumber(string check)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            if (regex.IsMatch(check) == true)
                return true;
            return false;
        }

        public static bool isEmail(string check)
        {
            Regex regex = new Regex(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$");
            if (regex.IsMatch(check) == true)
                return true;
            return false;
        }

        public static bool isImage(HtmlInputFile fileUpload)
        {
            Regex imageFilenameRegex = new Regex(@"(.*?)\.(jpg|jpeg|png|gif)$");
            if (imageFilenameRegex.IsMatch(Path.GetFileName(fileUpload.PostedFile.FileName)))
            {
                if (fileUpload.PostedFile.ContentLength > 10048576)
                    return false;
                return true;
            }
            return false;
        }

        public static string GetRandomNumbers(int numChars)
        {
            string[] chars = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S",
                        "T", "U", "V", "W", "X", "Y", "Z","0","1", "2", "3", "4", "5", "6", "7", "8", "9" };

            Random rnd = new Random();
            string random = string.Empty;
            for (int i = 0; i < numChars; i++)
            {
                random += chars[rnd.Next(0, 33)];
            }
            return random;
        }

        public static string RejectMarksStr(this string text)
        {
            string[] pattern = new string[7];
            pattern[0] = "a|(À|Á|Ả|Ã|Ạ|Â|Ấ|Ậ|Ẩ|Ầ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ|à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ)";
            pattern[1] = "o|(Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ|ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ)";
            pattern[2] = "e|(É|È|Ẻ|Ẽ|Ẹ|Ê|Ế|Ề|Ể|Ệ|Ễ|é|è|ẻ|ẹ|ẽ|ê|ế|ề|ể|ệ|ễ)";
            pattern[3] = "u|(Ú|Ù|Ủ|Ũ|Ư|Ứ|Ừ|Ử|Ự|Ữ|ú|ù|ủ|ụ|ũ|ư|ứ|ừ|ử|ự|ữ)";
            pattern[4] = "i|(Í|Ì|Ỉ|Ị|Ĩ|í|ì|ỉ|ị|ĩ)";
            pattern[5] = "y|(Ý|Ỳ|Ỷ|Ỵ|Ỹ|ý|ỳ|ỷ|ỵ|ỹ)";
            pattern[6] = "d|(D|Đ|đ)";
            for (int i = 0; i < pattern.Length; i++)
            {// kí tự sẽ thay thế   
                char replaceChar = pattern[i][0];
                MatchCollection matchs = Regex.Matches(text, pattern[i]);
                foreach (Match m in matchs)
                {
                    text = text.Replace(m.Value[0], replaceChar);
                }
            }
            return text;
        }

        public static string RejectMarks(string text)
        {
            string[] pattern= new string[7];
            pattern[0] = "a|(À|Á|Ả|Ã|Ạ|Â|Ấ|Ậ|Ẩ|Ầ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ|à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ)";
            pattern[1]=  "o|(Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ|ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ)";
            pattern[2] = "e|(É|È|Ẻ|Ẽ|Ẹ|Ê|Ế|Ề|Ể|Ệ|Ễ|é|è|ẻ|ẹ|ẽ|ê|ế|ề|ể|ệ|ễ)";
            pattern[3] = "u|(Ú|Ù|Ủ|Ũ|Ư|Ứ|Ừ|Ử|Ự|Ữ|ú|ù|ủ|ụ|ũ|ư|ứ|ừ|ử|ự|ữ)";
            pattern[4] = "i|(Í|Ì|Ỉ|Ị|Ĩ|í|ì|ỉ|ị|ĩ)";
            pattern[5] = "y|(Ý|Ỳ|Ỷ|Ỵ|Ỹ|ý|ỳ|ỷ|ỵ|ỹ)";
            pattern[6] = "d|(D|Đ|đ)";
            for (int i = 0; i < pattern.Length; i++)
            {// kí tự sẽ thay thế   
                char replaceChar = pattern[i][0];
                MatchCollection matchs = Regex.Matches(text,pattern[i]);
                foreach (Match m in matchs)
                {
                 text = text.Replace(m.Value[0],replaceChar);
                }
            }
            return text;
        }



    public static System.Collections.Generic.IEnumerable<Document> GetDescendants(this Document document)
    {
        try
        {
            Document[] children = document.Children;
            for (int i = 0; i < children.Length; i++)
            {
                Document document2 = children[i];
                yield return document2;
                foreach (Document document3 in document2.GetDescendants())
                {
                    yield return document3;
                }
            }
        }
        finally
        {
        }
        yield break;
    }
    public static System.Collections.Generic.IEnumerable<Node> GetDescendants(this Node node)
    {
        foreach (Node node2 in node.Children)
        {
            yield return node2;
            foreach (Node current in node2.GetDescendants())
            {
                yield return current;
            }
        }
        yield break;
    }
    public static bool ContainsAny(this string haystack, System.Collections.Generic.List<string> needles)
    {
        bool result;
        if (!string.IsNullOrEmpty(haystack) && needles.Count > 0)
        {
            foreach (string current in needles)
            {
                if (haystack.Contains(current))
                {
                    result = true;
                    return result;
                }
            }
        }
        result = false;
        return result;
    }

    public static int timkiemtrendiendan(string haystack,string needles)
    {
        int result=0;
        if (!string.IsNullOrEmpty(haystack) && !string.IsNullOrEmpty(needles))
        {
            var list2 = RejectMarks(haystack).ToLower().Split(' ');
            var list1 = RejectMarks(needles).ToLower().Split(' ');

            foreach (string current in list1)
            {
                foreach (string current1 in list2)
                {
                    if (current1.Contains(current))
                    {
                        result ++;
                    }
                }
            }
            var dem = list1.Count();
            if (result >= dem)
            {
                result = 1;
                return result;
            }
        }
        result = 0;
        return result;
    }
  
   
}