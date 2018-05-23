using KiotVietBO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebVer1.BLL;

/// <summary>
/// Summary description for MemoryCache
/// </summary>
public static class MemoryCacheKiot
{
    public static RootNhomHangBO dsNhomHang;
    public static RootChiNhanhBO dsChiNhanh;
    public static RootChiTietHangHoaBO dsHangHoa;
}
public static class DbCacheMem
{
    public static DataContext db = new DataContext(new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase().Options);
}