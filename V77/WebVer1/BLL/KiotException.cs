using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class ResponseStatus
{
    public string errorCode { get; set; }
    public string message { get; set; }
    public List<object> errors { get; set; }
}

public class RootKiotException
{
    public ResponseStatus responseStatus { get; set; }
}