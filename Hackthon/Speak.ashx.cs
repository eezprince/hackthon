using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Hackthon.Helper;

namespace Hackthon
{
    /// <summary>
    /// Summary description for Speak
    /// </summary>
    public class Speak : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "audio/wav";

            string text = context.Request.Form["text"];
            if (string.IsNullOrEmpty(text))
            {
                text = context.Request.QueryString["text"];
            }
            if (!string.IsNullOrEmpty(text))
            {
                SpeakHelper.Speak(text, @"LinZhiling", context.Response.OutputStream);
            }
            
            context.Response.Flush();
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