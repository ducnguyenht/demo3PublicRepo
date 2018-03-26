using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RootCardBO
/// </summary>
public class RootCardBO
{
	public RootCardBO()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<CartBO> carts { get; set; }

    public int totalPrice
    {
        get {
            int tongTien = 0;
            if (carts.Count>0)
            {
                foreach (var item in carts)
                {
                    tongTien += item.total;
                }
            }
            return tongTien; 
        }
    }
    public int totalQuantity
    {
        get
        {
            int quantity = 0;
            if (carts.Count > 0)
            {
                foreach (var item in carts)
                {
                    quantity += item.quantity;
                }
            }
            return quantity;
        }
    }
    
}