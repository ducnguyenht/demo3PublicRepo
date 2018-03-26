using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CartBO
/// </summary>
public class CartBO
{
	public CartBO()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int id { get; set; }
    public string code { get; set; }
    public string name { get; set; }
    public int basePrice { get; set; }
    public int quantity { get; set; }
    public string description { get; set; }
    public string image { get; set; }

    public int total
    {
        get { return basePrice*quantity; }
    }
    
}