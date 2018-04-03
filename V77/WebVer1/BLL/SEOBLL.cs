using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SEOBLL
/// </summary>
public class SEOBLL
{
	public SEOBLL()
	{
        //SEO seo = new SEOBLL().SEO(0);

	}
    public SEO SEO(int pk)
    {
        SEO seo= new SEO();
        if (pk!=0)
        {
            var idInt = pk;
            var category = new NhomHangBLL().DanhNhomHangPlane().Where(o => o.categoryId == idInt).FirstOrDefault();

            if (category != null)
            {
                seo.Name = category.categoryName;
                seo.Url = @"/danh-muc?pt=" + pk;
            }
            else
            {
                var hanghoa = MemoryCacheKiot.dsHangHoa.data.Where(o => o.id == idInt).FirstOrDefault();
                seo.Name = hanghoa.name;
                seo.Description = hanghoa.description;
                seo.Url = @"/danh-muc/san-pham?pt=" + pk;
            }
        }
        
        return seo;
    }
}
public class SEO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Keyword { get; set; }
    public string Url { get; set; }
}