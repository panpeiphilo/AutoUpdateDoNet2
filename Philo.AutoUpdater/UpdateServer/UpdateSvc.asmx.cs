using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using Philo.Common.AutoUpdate;
using Philo.Common.XML;
using System.Xml;
using System.Text;
using System.IO;

namespace UpdateServer
{
    /// <summary>
    /// UpdateSvc 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class UpdateSvc : System.Web.Services.WebService
    {
        
        [WebMethod(Description = "测试服务器连接")]
        public bool TestConnection()
        {
            return true;
        }

        [WebMethod(Description = "获取服务器版本号")]
        public string GetServerVersion()
        {
            //获取服务器版本号
            string configpath = Server.MapPath("~/") + "AutoUpdateConfig.xml";
            string version = XMLHelper.GetXmlNodeByXpath(configpath, "//Root//CurrentVersion").InnerText;
            return version;
        }


        [WebMethod(Description="获取更新日志")]
        public string GetUpdateLog(string clientVersion)
        {
            StringBuilder returnstr = new StringBuilder();
            string configpath = Server.MapPath("~/") + "AutoUpdateConfig.xml";
            XmlNodeList loglist = XMLHelper.GetXmlNodeListByXpath(configpath, "//Root//VersionList//VersionInfo");
            List<VersionEntity> velist = new List<VersionEntity>();
            foreach (XmlNode item in loglist)
            {
                VersionEntity veEntity = new VersionEntity();
                XmlNodeList childlist = item.ChildNodes;
                foreach (XmlNode child in childlist)
                {
                    switch (child.Name)
                    {
                        case "Version":
                            veEntity.MajorPart = Convert.ToInt32(child.Attributes["MajorPart"].Value);
                            veEntity.MinorPart = Convert.ToInt32(child.Attributes["MinorPart"].Value);
                            veEntity.PrivatePart = Convert.ToInt32(child.Attributes["PrivatePart"].Value);
                            veEntity.BuildPart = Convert.ToInt32(child.Attributes["BuildPart"].Value);
                            break;
                        case "BuildDate":
                            veEntity.BuildTime = Convert.ToDateTime(child.InnerText);
                            break;
                        case "UpdateLog":
                            veEntity.VersionDescription = child.InnerXml;
                            break;
                    }
                }
                velist.Add(veEntity);
            }
            //对版本信息进行排序按日期
            Comparison<VersionEntity> com = new Comparison<VersionEntity>(Compare);
            velist.Sort(com);
            bool exist = false;
            for (int i = velist.Count - 1; i >= 0; i--)
            {
                if (clientVersion.Trim().Equals(velist[i].FullVersion))
                {
                    exist = true;
                    if (i == velist.Count - 1)
                    {
                        return "当前版本为最新版本！";
                    }
                    break;
                }
                else
                {
                    returnstr.Append("版本号：" + velist[i].FullVersion + "<br />更新时间：" + velist[i].BuildTime.ToString() + "<br />");
                    returnstr.Append("描述：<br />" + velist[i].VersionDescription + "<br />");
                    returnstr.Append("<br />");
                }
            }
            if (!exist)
            {
                return "无法找到客户端版本对应的服务器版本！";
            }
            else
            {
                return returnstr.ToString();
            }
        }

        private int Compare(VersionEntity info1, VersionEntity info2)
        {
            int result;
            CaseInsensitiveComparer ObjectCompare = new CaseInsensitiveComparer();
            result = ObjectCompare.Compare(info1.BuildTime, info2.BuildTime);
            return result;
        }

        [WebMethod(Description="获取不需更新的文件")]
        public List<FileEntity> GetIgnoreFileList()
        {
            List<FileEntity> felist = new List<FileEntity>();
            StringBuilder returnstr = new StringBuilder();
            string configpath = Server.MapPath("~/") + "AutoUpdateConfig.xml";
            XmlNodeList ifilelist = XMLHelper.GetXmlNodeListByXpath(configpath, "//Root//IgnoreFileList//IgnoreFile");
            foreach (XmlNode item in ifilelist)
            {
                FileEntity feEntity = new FileEntity();
                feEntity.FilePath = item.Attributes["Path"].Value;
                feEntity.FileName = item.Attributes["FileName"].Value;
                felist.Add(feEntity);
            }
            return felist;
        }

        [WebMethod(Description = "获取服务器文件列表")]
        public List<FileEntity> GetServerFileList()
        {
            List<FileEntity> felist = new List<FileEntity>();
            DirectoryInfo dic = new DirectoryInfo(Server.MapPath("~/") + "ServerFiles/");
            AutoUpdateHelper.GetChildFile(dic, felist, "/");
            return felist;
        }

        [WebMethod(Description = "获取下载地址链接")]
        public string GetDownLoadLink()
        {
            string configpath = Server.MapPath("~/") + "AutoUpdateConfig.xml";
            XmlNode downlink = XMLHelper.GetXmlNodeByXpath(configpath, "//Root//FileUrl");
            return downlink.InnerText;
        }

        
    }
}
