using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Philo.Common.AutoUpdate
{
    public class AutoUpdateHelper
    {
        /// <summary>
        /// 获取目录下的所有文件
        /// </summary>
        /// <param name="dic">DirectoryInfo目录信息</param>
        /// <param name="felist">文件列表</param>
        /// <param name="path">路径初始为"/"</param>
        public static void GetChildFile(DirectoryInfo dic, List<FileEntity> felist, string path)
        {
            //遍历所有文件
            FileInfo[] filist = dic.GetFiles();
            foreach (var item in filist)
            {
                FileEntity newEntity = new FileEntity();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(item.FullName);
                newEntity.FileVersion = fvi.FileVersion == null ? "0.0.0.0" : fvi.FileVersion;
                newEntity.FilePath = path;
                newEntity.FileName = item.Name;
                newEntity.FileSize = item.Length;
                newEntity.CreateTime = item.CreationTime;
                newEntity.HashCode = item.GetHashCode();
                newEntity.UpdateTime = item.LastWriteTime;
                felist.Add(newEntity);
            }

            //遍历所有目录并递归查询
            DirectoryInfo[] dilist = dic.GetDirectories();
            foreach (var item in dilist)
            {
                GetChildFile(item, felist, path + item.Name + "/");
            }

        }

        /// <summary>
        /// 获取需要更新的文件列表
        /// </summary>
        /// <param name="serverFileList">服务器文件列表</param>
        /// <param name="clientFileList">客户端文件列表</param>
        /// <param name="ignoreFileList">不检测的文件列表</param>
        /// <returns>需要更新的文件列表及比对信息</returns>
        public static List<CompareFileEntity> GetUpdateFileList(List<FileEntity> serverFileList, List<FileEntity> clientFileList, List<FileEntity> ignoreFileList)
        {
            List<CompareFileEntity> cfelist = new List<CompareFileEntity>();
            //判断更新和添加
            for (int sflIndex = 0; sflIndex < serverFileList.Count; sflIndex++)
            {
                bool clientExist = false;//判断客户端是否存在
                for (int cflIndex = 0; cflIndex < clientFileList.Count; cflIndex++)
                {
                    if (serverFileList[sflIndex].FileName.Equals(clientFileList[cflIndex].FileName) && serverFileList[sflIndex].FilePath.Equals(clientFileList[cflIndex].FilePath))//路径及名称相同
                    {
                        //设置该文件存在 
                        clientFileList[cflIndex].IsExist = 1;
                        clientExist = true;
                                                
                        //添加许更新文件至列表
                        if (!IsIgnoreFile(serverFileList[sflIndex].FilePath, serverFileList[sflIndex].FileName, ignoreFileList))
                        {
                            if (!serverFileList[sflIndex].FileVersion.Equals(clientFileList[cflIndex].FileVersion) || !serverFileList[sflIndex].FileSize.Equals(clientFileList[cflIndex].FileSize))//这里进行比较版本和文件大小
                            {
                                cfelist.Add(FillCompareFileEntity(0, serverFileList[sflIndex], clientFileList[cflIndex]));
                            }
                        }
                        break;
                    }
                }

                if (!clientExist) //如果不存在就添加文件 代号1
                {
                    //添加许更新文件至列表
                    if (!IsIgnoreFile(serverFileList[sflIndex].FilePath, serverFileList[sflIndex].FileName, ignoreFileList))
                    {
                        cfelist.Add(FillCompareFileEntity(1, serverFileList[sflIndex], null));
                    }
                }
            }
            //判断删除
            for (int cflIndex = 0; cflIndex < clientFileList.Count; cflIndex++)
            {
                if (clientFileList[cflIndex].IsExist != 1)
                {
                    if (!IsIgnoreFile(clientFileList[cflIndex].FilePath, clientFileList[cflIndex].FileName, ignoreFileList))
                    {
                        cfelist.Add(FillCompareFileEntity(-1, null, clientFileList[cflIndex]));
                    }
                }
            }
            return cfelist;
        }

        /// <summary>
        /// 判断文件是否需要忽略
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="filename">文件名</param>
        /// <param name="ignoreFileList">忽略文件列表</param>
        /// <returns></returns>
        private static bool IsIgnoreFile(string path, string filename, List<FileEntity> ignoreFileList)
        {
            bool isIgnore = false;
            foreach (var item in ignoreFileList)
            {
                if (item.FilePath.Equals("*"))
                {
                    if (item.FileName.Equals(filename))
                    {
                        isIgnore = true;
                        break;
                    }
                }
                else
                {
                    if (item.FileName.Equals(filename) && item.FilePath.Equals(path))
                    {
                        isIgnore = true;
                        break;
                    }

                }
            }
            return isIgnore;
        }

        /// <summary>
        /// 填充文件比较信息
        /// </summary>
        /// <param name="operation">操作类型</param>
        /// <param name="serverfile">服务器文件</param>
        /// <param name="localfile">客户端文件</param>
        /// <returns>文件比较信息</returns>
        private static CompareFileEntity FillCompareFileEntity(int operation, FileEntity serverfile, FileEntity localfile)
        {
            CompareFileEntity cfEntity = new CompareFileEntity();
            cfEntity.FileOpreation = operation;//标示修改

            if (serverfile != null)
            {
                cfEntity.FileName = serverfile.FileName;
                cfEntity.FilePath = serverfile.FilePath;
                cfEntity.ServerCreateTime = serverfile.CreateTime;
                cfEntity.ServerUpdateTime = serverfile.UpdateTime;
                cfEntity.ServerHashCode = serverfile.HashCode;
                cfEntity.ServerFileVersion = serverfile.FileVersion;
                cfEntity.ServerFileSize = serverfile.FileSize;
            }
            if (localfile != null)
            {
                cfEntity.FileName = localfile.FileName;
                cfEntity.FilePath = localfile.FilePath;
                cfEntity.LocalCreateTime = localfile.CreateTime;
                cfEntity.LocalUpdateTime = localfile.UpdateTime;
                cfEntity.LocalHashCode = localfile.HashCode;
                cfEntity.LocalFileVersion = localfile.FileVersion;
                cfEntity.LocalFileSize = localfile.FileSize;
            }
            return cfEntity;
        }

        /// <summary>
        /// 下载单个文件
        /// </summary>
        /// <param name="url">下载链接地址</param>
        /// <param name="path">相对路径</param>
        /// <param name="filename">文件名</param>
        /// <param name="temppath">本地临时文件夹</param>
        public static void DownLoadSingleFile(string url, string path, string filename, string temppath)
        {
            string downloadurl = url + "?action=GetSingleFile&path=" + path + "&filename=" + filename;

            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(downloadurl);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;

                System.IO.Stream st = myrp.GetResponseStream();
                string localname = temppath + path + filename;
                System.IO.Stream so = new System.IO.FileStream(localname, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, (int)by.Length);
                }
                so.Close();
                st.Close();
                myrp.Close();
                Myrq.Abort();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 更新单个文件
        /// </summary>
        /// <param name="cfeEntity">文件比较信息</param>
        /// <param name="temppath">本地临时文件夹</param>
        /// <param name="path">本地应用文件夹</param>
        public static void UpdateTempSingleFile(CompareFileEntity cfeEntity, string temppath, string path)
        {
            switch (cfeEntity.FileOpreation)
            {
                case 1://添加                    
                    CreateDirectory(path + cfeEntity.FilePath);
                    File.Copy(temppath + cfeEntity.FilePath + cfeEntity.FileName, path + cfeEntity.FilePath + cfeEntity.FileName);
                    break;
                case 0://更新
                    DeleteFile(path + cfeEntity.FilePath + cfeEntity.FileName);
                    File.Copy(temppath + cfeEntity.FilePath + cfeEntity.FileName, path + cfeEntity.FilePath + cfeEntity.FileName);
                    break;
                case -1://删除                    
                    DeleteFile(path + cfeEntity.FilePath + cfeEntity.FileName);
                    break;
            }
        }

        /// <summary>
        /// 添加目录
        /// </summary>
        /// <param name="dicpath">目录地址</param>
        public static void CreateDirectory(string dicpath)
        {
            if (!Directory.Exists(dicpath))
            {
                Directory.CreateDirectory(dicpath);
            }
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dicpath">目录地址</param>
        public static void DeleteDirectory(string dicpath)
        {
            if (Directory.Exists(dicpath))
            {
                DirectoryInfo di = new DirectoryInfo(dicpath);
                FileInfo[] filist = di.GetFiles();
                foreach (var item in filist)
                {
                    DeleteFile(item.FullName);
                }
                DirectoryInfo[] dilist = di.GetDirectories();
                foreach (var item in dilist)
                {
                    DeleteDirectory(item.FullName);
                }
                Directory.Delete(dicpath);
            }
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="filename">文件全名</param>
        public static void CreateFile(string filename)
        {
            if (!File.Exists(filename))
            {
                File.Create(filename);
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filename">文件全名</param>
        public static void DeleteFile(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }
    }
}
