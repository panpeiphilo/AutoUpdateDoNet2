using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text;

namespace UpdateServer
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FileDownLoad : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            StringBuilder str = new StringBuilder();
            try
            {
                string action = context.Request.QueryString["action"];
                if (!string.IsNullOrEmpty(action))
                {
                    switch (action)
                    {
                        case "GetSingleFile":
                            string path = context.Request.QueryString["path"];
                            string filename = context.Request.QueryString["filename"];

                            if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(filename))
                            {
                                string fileurl = "~/ServerFiles" + path + filename;
                                context.Response.WriteFile(fileurl);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                str.Append("参数错误！" + ex.Message);
            }
            if (str.Length > 0)
            {
                context.Response.Write(str.ToString());
            }
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
