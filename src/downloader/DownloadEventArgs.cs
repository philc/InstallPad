// Author: Phil Crosby
using System;

namespace CodeProject.Downloader
{
    /// <summary>
    /// Progress of a downloading file.
    /// </summary>
	public class DownloadEventArgs : EventArgs
	{
		private int percentDone;
		private string downloadState;
        private long totalFileSize;

        public long TotalFileSize
        {
            get { return totalFileSize; }
            set { totalFileSize = value; }
        }
        private long currentFileSize;

        public long CurrentFileSize
        {
            get { return currentFileSize; }
            set { currentFileSize = value; }
        }

        public DownloadEventArgs(long totalFileSize, long currentFileSize)
        {
            this.totalFileSize = totalFileSize;
            this.currentFileSize = currentFileSize;

            this.percentDone = (int)((((double)currentFileSize) / totalFileSize) * 100);
        }

		public DownloadEventArgs(string state)
		{
			this.downloadState = state;
		}

		public DownloadEventArgs(int percentDone, string state)
		{
			this.percentDone = percentDone;
			this.downloadState = state;
		}

		public int PercentDone
		{
			get
			{
				return this.percentDone;
			}
		}

		public string DownloadState
		{
			get
			{
				return this.downloadState;
			}
		}
	}

	public delegate void DownloadProgressHandler(object sender, DownloadEventArgs e);
}