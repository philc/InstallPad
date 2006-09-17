using System;
using System.Collections.Generic;
using System.Text;
using CodeProject.Downloader;
namespace Downloader
{
    class TestDownload
    {
        static void Main(string[] args)
        {
            FileDownloader downloader = new FileDownloader();
            downloader.DownloadComplete += new EventHandler(downloader_DownloadedComplete);
            downloader.ProgressChanged += new DownloadProgressHandler(downloader_ProgressChanged);
            downloader.Download("http://download.mozilla.org/?product=firefox-1.5.0.4&os=win&lang=en-US");            
        }

        static void downloader_ProgressChanged(object sender, DownloadEventArgs e)
        {
            Console.WriteLine("Progress " + e.PercentDone);
        }

        static void downloader_DownloadedComplete(object sender, EventArgs e)
        {
            System.Console.WriteLine("Download complete.");
        }
    }
}
