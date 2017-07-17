using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackthon
{
    /// <summary>
    /// Summary description for Speak
    /// </summary>
    public class Speak : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}