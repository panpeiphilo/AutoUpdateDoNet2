using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 自动更新相关
/// </summary>
namespace Philo.Common.AutoUpdate
{
    [Serializable]
    public partial class VersionEntity
    {
        /// <summary>
        /// 获取和设置主要版本号
        /// </summary>
        public int MajorPart { set; get; }
        /// <summary>
        /// 获取和设置次要版本号
        /// </summary>
        public int MinorPart { set; get; }
        /// <summary>
        /// 获取和设置私有版本号
        /// </summary>
        public int PrivatePart { set; get; }
        /// <summary>
        /// 获取和设置内部版本号
        /// </summary>
        public int BuildPart { set; get; }

        /// <summary>
        /// 获取版本号全称
        /// </summary>
        public string FullVersion
        {
            get { return MajorPart + "." + MinorPart + "." + PrivatePart + "." + BuildPart; }
        }

        /// <summary>
        /// 版本描述
        /// </summary>
        public string VersionDescription { set; get; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime BuildTime { set; get; }
    }
}
