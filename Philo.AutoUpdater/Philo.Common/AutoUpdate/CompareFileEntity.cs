using System;
using System.Collections.Generic;
using System.Text;

namespace Philo.Common.AutoUpdate
{
    public class CompareFileEntity
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { set; get; }
        /// <summary>
        /// 相对路径
        /// </summary>
        public string FilePath { set; get; }       
        /// <summary>
        /// 是否存在 -1 删除 0 更新 1 增加
        /// </summary>
        public int FileOpreation { set; get; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string ServerFileVersion { set; get; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long ServerFileSize { set; get; }
        /// <summary>
        /// 新建时间
        /// </summary>
        public DateTime ServerCreateTime { set; get; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ServerUpdateTime { set; get; }
        /// <summary>
        /// 哈希编码
        /// </summary>
        public int ServerHashCode { set; get; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string LocalFileVersion { set; get; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long LocalFileSize { set; get; }
        /// <summary>
        /// 新建时间
        /// </summary>
        public DateTime LocalCreateTime { set; get; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime LocalUpdateTime { set; get; }
        /// <summary>
        /// 哈希编码
        /// </summary>
        public int LocalHashCode { set; get; }
    }
}
