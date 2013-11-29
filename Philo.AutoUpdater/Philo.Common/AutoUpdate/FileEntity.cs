using System;
using System.Collections.Generic;
using System.Text;

namespace Philo.Common.AutoUpdate
{
    [Serializable]
    public partial class FileEntity
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
        /// 版本号
        /// </summary>
        public string FileVersion { set; get; }     
        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { set; get; }
        /// <summary>
        /// 新建时间
        /// </summary>
        public DateTime CreateTime { set; get; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { set; get; }
        /// <summary>
        /// 哈希编码
        /// </summary>
        public int HashCode { set; get; }
        /// <summary>
        /// 是否存在
        /// </summary>
        public int IsExist { set; get; }
    }
}
