﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliRanking.Core.Download
{
    public class MultiThreadedWebDownloaderEx : MultiThreadedWebDownloader
    {

        public float Percent
        {
            get
            {
                return (float)DownloadedSize / TotalSize;
            }
        }

        public MultiThreadedWebDownloaderEx(string url) : base(url, 1024, 1048576, 64, Environment.ProcessorCount)
        {
        }

        public new void BeginDownload()
        {
            base.DownloadPath = CheckDuplicateName(base.DownloadPath);
            base.BeginDownload();
        }
        /// <summary>
        /// 检测是否有重名，有则加入(1)，如仍重复加(2)，以此类推
        /// </summary>
        /// <param name="fileName"></param>
        public virtual void CheckDuplicateName(ref string fileName)
        {
            if (File.Exists(fileName))
            {
                string fn1 = fileName.Substring(0, fileName.IndexOf('.')); //TODO: 换成File类自带的方法
                string fn2 = fileName.Substring(fileName.IndexOf('.'));
                int i = 0;
                string nFileName;
                do
                {
                    i++;
                    nFileName = string.Format("{0}({1}){2}", fn1, i, fn2);
                } while (File.Exists(nFileName));
                fileName = nFileName;
            }
        }

        public virtual string CheckDuplicateName(string fileName)
        {
            CheckDuplicateName(ref fileName);
            return fileName;
        }
    }
}
