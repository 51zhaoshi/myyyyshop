namespace Maticsoft.Common.Video
{
    using Maticsoft.Common;
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Web;
    using VideoEncoder;

    public class ConvertVideo
    {
        private string _errorMessage;
        private string ffmpegTools = ConfigHelper.GetConfigString("ffmpeg");

        private void ConvertFlv(string srcFileName, string destFileName)
        {
            Process process = new Process {
                StartInfo = { FileName = AppDomain.CurrentDomain.BaseDirectory + this.ffmpegTools, UseShellExecute = false, WindowStyle = ProcessWindowStyle.Normal, Arguments = "-i " + srcFileName + " -ab 128 -acodec libmp3lame -ac 1 -ar 22050 -r 29.97 -qscale 6 -y " + destFileName, RedirectStandardInput = true, RedirectStandardOutput = true, RedirectStandardError = true }
            };
            process.ErrorDataReceived += new DataReceivedEventHandler(this.p_ErrorDataReceived);
            process.OutputDataReceived += new DataReceivedEventHandler(this.p_OutputDataReceived);
            process.Start();
            process.BeginErrorReadLine();
            process.WaitForExit();
            process.Close();
            process.Dispose();
        }

        private void CreateThumb(string srcFileName, string jpgFileName)
        {
            try
            {
                Process process = new Process {
                    StartInfo = { FileName = AppDomain.CurrentDomain.BaseDirectory + this.ffmpegTools, UseShellExecute = false, Arguments = "-i " + srcFileName + " -y -f mjpeg  -ss 2 -t 3 -s 598x520 " + jpgFileName, RedirectStandardInput = true, RedirectStandardOutput = true, RedirectStandardError = true }
                };
                process.ErrorDataReceived += new DataReceivedEventHandler(this.p_ErrorDataReceived);
                process.OutputDataReceived += new DataReceivedEventHandler(this.p_OutputDataReceived);
                process.Start();
                process.BeginErrorReadLine();
                process.WaitForExit();
                process.Close();
                process.Dispose();
            }
            catch (Exception)
            {
                this._errorMessage = "图片截取失败！";
            }
        }

        public static string GetExtension(string fileName)
        {
            int startIndex = fileName.LastIndexOf(".") + 1;
            return fileName.Substring(startIndex);
        }

        public static string GetFileName(string fileName)
        {
            int startIndex = fileName.LastIndexOf(@"\") + 1;
            return fileName.Substring(startIndex);
        }

        public TimeSpan GetVideoTotalTime(string videoPath)
        {
            Encoder encoder = new Encoder {
                FFmpegPath = AppDomain.CurrentDomain.BaseDirectory + this.ffmpegTools
            };
            string path = videoPath;
            VideoFile input = new VideoFile(path);
            encoder.GetVideoInfo(input);
            return input.Duration;
        }

        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
        }

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
        }

        public bool UploadVideo(HttpPostedFile postFile, bool isConvert, string savePath, int? configSize, bool isGetImg, bool isGetSpan, out VideoModel model, string extend)
        {
            model = new VideoModel();
            if (postFile == null)
            {
                return false;
            }
            if (configSize.HasValue && (postFile.ContentLength > configSize.Value))
            {
                this._errorMessage = "上传文件过大";
                return false;
            }
            ConfigHelper.GetConfigString("UploadFolder");
            string str = Path.GetExtension(postFile.FileName).ToLower();
            string str2 = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture) + str;
            string path = HttpContext.Current.Server.MapPath("/" + savePath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string str4 = savePath + str2;
            postFile.SaveAs(HttpContext.Current.Server.MapPath(str4));
            model.SavePath = str4;
            if (isConvert)
            {
                string str5 = Path.ChangeExtension(str4, extend);
                string destFileName = Path.Combine(path, Path.ChangeExtension(postFile.FileName, extend));
                this.ConvertFlv(HttpContext.Current.Server.MapPath(str4), destFileName);
                model.SavePath = str5;
            }
            if (isGetImg)
            {
                string str7 = Path.ChangeExtension(str4, ".jpg");
                string str8 = Path.Combine(HttpContext.Current.Server.MapPath(str4), Path.ChangeExtension(str4, ".jpg"));
                this.CreateThumb(HttpContext.Current.Server.MapPath(str4), HttpContext.Current.Server.MapPath(str8));
                model.ImgPath = str7;
            }
            if (isGetSpan)
            {
                TimeSpan videoTotalTime = this.GetVideoTotalTime(HttpContext.Current.Server.MapPath(str4));
                int num = TimeParser.TimeToSecond(videoTotalTime.Hours, videoTotalTime.Minutes, videoTotalTime.Seconds);
                model.VideoSpan = num;
            }
            return true;
        }

        public string errorMessage
        {
            get
            {
                return this._errorMessage;
            }
        }
    }
}

