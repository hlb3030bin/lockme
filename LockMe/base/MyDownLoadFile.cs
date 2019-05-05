using System;
using System.Diagnostics;
using System.IO;
using CefSharp;
using NLog;

namespace LockMe
{
    public class MyDownLoadFile : IDownloadHandler
    {
        private ILogger logger = LogManager.GetCurrentClassLogger();

        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem,
            IBeforeDownloadCallback callback)
        {
            if (!callback.IsDisposed)
            {
                using (callback)
                {

                    string path = Directory.GetCurrentDirectory() +@"\"+
                                  $@"ica\" +
                                  downloadItem.SuggestedFileName;
                    //logger.Info(path);
                    callback.Continue(path,
                        showDialog: false);
                }
            }
        }

        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem,
            IDownloadItemCallback callback)
        {
            //throw new System.NotImplementedException();
            if (!callback.IsDisposed)
            {
                string path= downloadItem.FullPath;
                //logger.Info(path);
                if (path != ""&& downloadItem.IsComplete==true)
                {
                    Process pr = new Process();
                    pr.StartInfo.FileName = path;
                    pr.Start();
                }

                Console.WriteLine(path);
            }
        }
    }
}