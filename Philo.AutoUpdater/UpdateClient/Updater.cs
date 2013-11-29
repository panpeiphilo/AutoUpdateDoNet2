using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Philo.Common.AutoUpdate;
using System.IO;

namespace UpdateClient
{
    public partial class Updater : Form
    {
        AutoUpdateSvc.UpdateSvc updatesvc = new AutoUpdateSvc.UpdateSvc();//更新服务引用
        public Updater()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            
            string path = Application.StartupPath;
            string temppath = path + "/Temp";
            string downloadurl = updatesvc.GetDownLoadLink();

            //新建Temp文件夹
            if (!Directory.Exists(temppath))
            {
                Directory.CreateDirectory(temppath);
            }

            List<CompareFileEntity> comlist = GetNeedUpdateFileList();
            pgbCurrentFile.Maximum = comlist.Count * 2;
            int n = 1;
            //下载所有文件
            lblCurrentFile.Text = "开始下载所有文件！";
            foreach (var item in comlist)
            {
                pgbCurrentFile.Value = n++;
                lblCurrentFile.Text = "正在下载:" + item.FilePath + item.FileName;
                AutoUpdateHelper.DownLoadSingleFile(downloadurl, item.FilePath, item.FileName, temppath);
                lblCurrentFile.Text = "完成:" + item.FilePath + item.FileName;
            }

            lblCurrentFile.Text = "开始更新所有文件！";
            //更新所有文件
            foreach (var item in comlist)
            {
                pgbCurrentFile.Value = n++;
                lblCurrentFile.Text = "正在更新:" + item.FilePath + item.FileName;
                try
                {
                    AutoUpdateHelper.UpdateTempSingleFile(item, temppath, path);
                }
                catch (Exception ex)
                {
                    lblCurrentFile.Text = ex.Message;
                }
                lblCurrentFile.Text = "完成:" + item.FilePath + item.FileName;
            }

            //删除Temp文件夹
            lblCurrentFile.Text = "开始清除缓存！";
            if (Directory.Exists(temppath))
            {
                lblCurrentFile.Text = "正在删除临时文件";
                try
                {
                    AutoUpdateHelper.DeleteDirectory(temppath);
                }
                catch (Exception ex)
                {
                    lblCurrentFile.Text = ex.Message;
                }

            }

            dgvUpdateFileList.DataSource = ConvertUpdateListToDataTable(GetNeedUpdateFileList());
            lblCurrentFile.Text = "修改本地版本号！";
            SetClientVersion(updatesvc.GetServerVersion());
            lblCurrentFile.Text = "更新完成！";
            
            MessageBox.Show("更新完成！");
            //重新加载
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Updater_Load(object sender, EventArgs e)
        {
            string localversion = GetClientVersion();
            string serverversion = updatesvc.GetServerVersion();

            lblCurrentVersion.Text = "本地版本：" + localversion;
            lblNewVersion.Text = "服务器版本：" + serverversion;
            txtUpdateDetail.Text = updatesvc.GetUpdateLog(GetClientVersion()).Replace("<br />", "\r\n");
            if (!localversion.Equals(serverversion))
            {
                //获取本地版本号
                DataTable comlist = ConvertUpdateListToDataTable(GetNeedUpdateFileList());

                dgvUpdateFileList.DataSource = comlist;
            }
            else
            {
                btnUpdate.Enabled = false;
            }
        }

        private DataTable ConvertUpdateListToDataTable(List<CompareFileEntity> comlist)
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("操作");
            DataColumn dc2 = new DataColumn("路径");
            DataColumn dc3 = new DataColumn("文件名");
            DataColumn dc4 = new DataColumn("服务器版本号");
            DataColumn dc5 = new DataColumn("本地版本号");
            DataColumn dc6 = new DataColumn("服务器大小");
            DataColumn dc7 = new DataColumn("本地大小");
            //DataColumn dc8 = new DataColumn("服务器哈希码");
            //DataColumn dc9 = new DataColumn("本地哈希码");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);
            dt.Columns.Add(dc7);
            //dt.Columns.Add(dc8);
            //dt.Columns.Add(dc9);
            for (int i = 0; i < comlist.Count; i++)
            {
                DataRow newRow = dt.NewRow();
                switch (comlist[i].FileOpreation)
                {
                    case 1:
                        newRow[dc1] = "添加";
                        break;
                    case 0:
                        newRow[dc1] = "更新";
                        break;
                    case -1:
                        newRow[dc1] = "删除";
                        break;
                }
                newRow[dc2] = comlist[i].FilePath;
                newRow[dc3] = comlist[i].FileName;
                newRow[dc4] = comlist[i].ServerFileVersion;
                newRow[dc5] = comlist[i].LocalFileVersion;
                newRow[dc6] = comlist[i].ServerFileSize;
                newRow[dc7] = comlist[i].LocalFileSize;
                //newRow[dc8] = comlist[i].ServerHashCode;
                //newRow[dc9] = comlist[i].LocalHashCode;
                dt.Rows.Add(newRow);
            }
            return dt;
        }

        private List<CompareFileEntity> GetNeedUpdateFileList()
        {
            //获取本地版本与服务器差异
            List<FileEntity> localfelist = new List<FileEntity>();
            DirectoryInfo dic = new DirectoryInfo(Application.StartupPath);
            AutoUpdateHelper.GetChildFile(dic, localfelist, "/");

            List<FileEntity> serverfelist = new List<FileEntity>();
            AutoUpdateSvc.FileEntity[] sflist = updatesvc.GetServerFileList();
            foreach (var item in sflist)
            {
                serverfelist.Add(ConvertToFileEntity(item));
            }


            List<FileEntity> ignorefelist = new List<FileEntity>();
            AutoUpdateSvc.FileEntity[] iglist = updatesvc.GetIgnoreFileList();
            foreach (var item in iglist)
            {
                ignorefelist.Add(ConvertToFileEntity(item));
            }

            List<CompareFileEntity> comlist = new List<CompareFileEntity>();

            comlist = AutoUpdateHelper.GetUpdateFileList(serverfelist, localfelist, ignorefelist);


            return comlist;
        }

        private FileEntity ConvertToFileEntity(UpdateClient.AutoUpdateSvc.FileEntity item)
        {
            FileEntity feEntity = new FileEntity();
            feEntity.FileName = item.FileName;
            feEntity.CreateTime = item.CreateTime;
            feEntity.FilePath = item.FilePath;
            feEntity.FileSize = item.FileSize;
            feEntity.FileVersion = item.FileVersion;
            feEntity.HashCode = item.HashCode;
            feEntity.UpdateTime = item.UpdateTime;
            feEntity.IsExist = item.IsExist;
            return feEntity;
        }

        /// <summary>
        /// 获取客户端版本
        /// </summary>
        /// <returns></returns>
        private string GetClientVersion()
        {
            return ConfigurationManager.AppSettings.Get("ClientVersion").Trim();
        }

        /// <summary>
        /// 更新客户端版本号
        /// </summary>
        /// <param name="serverVersion"></param>
        private void SetClientVersion(string serverVersion)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("ClientVersion");
            config.AppSettings.Settings.Add("ClientVersion", serverVersion);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
        }
    }
}
