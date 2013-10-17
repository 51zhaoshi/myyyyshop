namespace VideoEncoder
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;

    public class Encoder
    {
        private int iProgressErrorCount;
        private EncodeFinishedEventHandler OnEncodeFinished;
        private EncodeProgressEventHandler OnEncodeProgress;
        private const int PROGRESS_ERROR_LIMIT = 100;
        private Control tempCaller;
        private EncodedVideo tempEncodedVideo;
        private VideoFile tempVideoFile;

        public event EncodeFinishedEventHandler OnEncodeFinished
        {
            add
            {
                EncodeFinishedEventHandler handler2;
                EncodeFinishedEventHandler onEncodeFinished = this.OnEncodeFinished;
                do
                {
                    handler2 = onEncodeFinished;
                    EncodeFinishedEventHandler handler3 = (EncodeFinishedEventHandler) Delegate.Combine(handler2, value);
                    onEncodeFinished = Interlocked.CompareExchange<EncodeFinishedEventHandler>(ref this.OnEncodeFinished, handler3, handler2);
                }
                while (onEncodeFinished != handler2);
            }
            remove
            {
                EncodeFinishedEventHandler handler2;
                EncodeFinishedEventHandler onEncodeFinished = this.OnEncodeFinished;
                do
                {
                    handler2 = onEncodeFinished;
                    EncodeFinishedEventHandler handler3 = (EncodeFinishedEventHandler) Delegate.Remove(handler2, value);
                    onEncodeFinished = Interlocked.CompareExchange<EncodeFinishedEventHandler>(ref this.OnEncodeFinished, handler3, handler2);
                }
                while (onEncodeFinished != handler2);
            }
        }

        public event EncodeProgressEventHandler OnEncodeProgress
        {
            add
            {
                EncodeProgressEventHandler handler2;
                EncodeProgressEventHandler onEncodeProgress = this.OnEncodeProgress;
                do
                {
                    handler2 = onEncodeProgress;
                    EncodeProgressEventHandler handler3 = (EncodeProgressEventHandler) Delegate.Combine(handler2, value);
                    onEncodeProgress = Interlocked.CompareExchange<EncodeProgressEventHandler>(ref this.OnEncodeProgress, handler3, handler2);
                }
                while (onEncodeProgress != handler2);
            }
            remove
            {
                EncodeProgressEventHandler handler2;
                EncodeProgressEventHandler onEncodeProgress = this.OnEncodeProgress;
                do
                {
                    handler2 = onEncodeProgress;
                    EncodeProgressEventHandler handler3 = (EncodeProgressEventHandler) Delegate.Remove(handler2, value);
                    onEncodeProgress = Interlocked.CompareExchange<EncodeProgressEventHandler>(ref this.OnEncodeProgress, handler3, handler2);
                }
                while (onEncodeProgress != handler2);
            }
        }

        protected virtual void DoEncodeFinished(EncodeFinishedEventArgs e)
        {
            if (this.OnEncodeFinished != null)
            {
                this.OnEncodeFinished(this, e);
            }
        }

        protected virtual void DoEncodeProgress(EncodeProgressEventArgs e)
        {
            if (this.OnEncodeProgress != null)
            {
                this.OnEncodeProgress(this, e);
            }
        }

        public EncodedVideo EncodeVideo(VideoFile input, string encodingCommand, string outputFile, bool getVideoThumbnail)
        {
            EncodedVideo video = new EncodedVideo();
            this.Params = string.Format("-i \"{0}\" {1} \"{2}\"", input.Path, encodingCommand, outputFile);
            string str = this.RunProcess(this.Params);
            video.EncodingLog = str;
            video.EncodedVideoPath = outputFile;
            if (File.Exists(outputFile))
            {
                video.Success = true;
                if (getVideoThumbnail)
                {
                    string saveThumbnailTo = outputFile + "_thumb.jpg";
                    if (this.GetVideoThumbnail(input, saveThumbnailTo))
                    {
                        video.ThumbnailPath = saveThumbnailTo;
                    }
                }
                return video;
            }
            video.Success = false;
            return video;
        }

        public void EncodeVideoAsync(VideoFile input, string encodingCommand, string outputFile, int threadCount)
        {
            this.EncodeVideoAsync(input, encodingCommand, outputFile, null, threadCount);
        }

        public void EncodeVideoAsync(VideoFile input, string encodingCommand, string outputFile, Control caller, int threadCount)
        {
            if (!input.infoGathered)
            {
                this.GetVideoInfo(input);
            }
            this.tempEncodedVideo = new EncodedVideo();
            this.tempEncodedVideo.EncodedVideoPath = outputFile;
            this.tempCaller = caller;
            this.tempVideoFile = input;
            if (threadCount.Equals(1))
            {
                this.Params = string.Format("-i \"{0}\" {1} \"{2}\"", input.Path, encodingCommand, outputFile);
            }
            else
            {
                this.Params = string.Format("-i \"{0}\" -threads {1} {2} \"{3}\"", new object[] { input.Path, threadCount.ToString(), encodingCommand, outputFile });
            }
            this.RunProcessAsync(this.Params);
        }

        public void EncodeVideoAsyncAutoCommand(VideoFile input, string outputFile, int threadCount)
        {
            this.EncodeVideoAsyncAutoCommand(input, outputFile, null, threadCount);
        }

        public void EncodeVideoAsyncAutoCommand(VideoFile input, string outputFile, Control caller, int treadCount)
        {
            if (!input.infoGathered)
            {
                this.GetVideoInfo(input);
            }
            if (input.VideoBitRate == 0.0)
            {
                int height = input.Height;
                if (height < 180)
                {
                    input.VideoBitRate = 400.0;
                }
                else if (height < 260)
                {
                    input.VideoBitRate = 1000.0;
                }
                else if (height < 400)
                {
                    input.VideoBitRate = 2000.0;
                }
                else if (height < 800)
                {
                    input.VideoBitRate = 5000.0;
                }
                else
                {
                    input.VideoBitRate = 8000.0;
                }
            }
            if (input.AudioBitRate == 0.0)
            {
                input.AudioBitRate = 128.0;
            }
            string str = string.Format("-threads {0} -y -b {1} -ab {2}", treadCount.ToString(), input.VideoBitRate.ToString() + "k", input.AudioBitRate.ToString() + "k");
            this.tempEncodedVideo = new EncodedVideo();
            this.tempEncodedVideo.EncodedVideoPath = outputFile;
            this.tempCaller = caller;
            this.tempVideoFile = input;
            this.Params = string.Format("-i \"{0}\" {1} \"{2}\"", input.Path, str, outputFile);
            this.RunProcessAsync(this.Params);
        }

        private double ExtractAudioBitRate(string rawAudioFormat)
        {
            string[] strArray = rawAudioFormat.Split(new string[] { ", " }, StringSplitOptions.None);
            double result = 0.0;
            foreach (string str in strArray)
            {
                if (str.ToLower().Contains("kb/s"))
                {
                    double.TryParse(str.ToLower().Replace("kb/s", "").Replace(".", ",").Trim(), out result);
                    return result;
                }
            }
            return result;
        }

        private string ExtractAudioFormat(string rawAudioFormat)
        {
            return rawAudioFormat.Split(new string[] { ", " }, StringSplitOptions.None)[0].Replace("Audio: ", "");
        }

        private double ExtractBitrate(string rawInfo)
        {
            Match match = new Regex(@"[B|b]itrate:.((\d|:)*)", RegexOptions.Compiled).Match(rawInfo);
            double result = 0.0;
            if (match.Success)
            {
                double.TryParse(match.Groups[1].Value, out result);
            }
            return result;
        }

        private TimeSpan ExtractDuration(string rawInfo)
        {
            TimeSpan span = new TimeSpan(0L);
            Match match = new Regex(@"[D|d]uration:.((\d|:|\.)*)", RegexOptions.Compiled).Match(rawInfo);
            if (match.Success)
            {
                string[] strArray = match.Groups[1].Value.Split(new char[] { ':', '.' });
                if (strArray.Length == 4)
                {
                    span = new TimeSpan(0, Convert.ToInt16(strArray[0]), Convert.ToInt16(strArray[1]), Convert.ToInt16(strArray[2]), Convert.ToInt16(strArray[3]));
                }
            }
            return span;
        }

        private double ExtractFrameRate(string rawVideoFormat)
        {
            string[] strArray = rawVideoFormat.Split(new string[] { ", " }, StringSplitOptions.None);
            double result = 0.0;
            foreach (string str in strArray)
            {
                if (str.ToLower().Contains("fps"))
                {
                    double.TryParse(str.ToLower().Replace("fps", "").Replace(".", ",").Trim(), out result);
                    return result;
                }
                if (str.ToLower().Contains("tbr"))
                {
                    double.TryParse(str.ToLower().Replace("tbr", "").Replace(".", ",").Trim(), out result);
                    return result;
                }
            }
            return result;
        }

        private string ExtractRawAudioFormat(string rawInfo)
        {
            string str = string.Empty;
            Match match = new Regex("[A|a]udio:.*", RegexOptions.Compiled).Match(rawInfo);
            if (match.Success)
            {
                str = match.Value;
            }
            return str.Replace("Audio: ", "");
        }

        private string ExtractRawVideoFormat(string rawInfo)
        {
            string str = string.Empty;
            Match match = new Regex("[V|v]ideo:.*", RegexOptions.Compiled).Match(rawInfo);
            if (match.Success)
            {
                str = match.Value;
            }
            return str.Replace("Video: ", "");
        }

        private long ExtractTotalFrames(TimeSpan duration, double frameRate)
        {
            return (long) Math.Round((double) (duration.TotalSeconds * frameRate), 0);
        }

        private double ExtractVideoBitRate(string rawVideoFormat)
        {
            string[] strArray = rawVideoFormat.Split(new string[] { ", " }, StringSplitOptions.None);
            double result = 0.0;
            foreach (string str in strArray)
            {
                if (str.ToLower().Contains("kb/s"))
                {
                    double.TryParse(str.ToLower().Replace("kb/s", "").Replace(".", ",").Trim(), out result);
                    return result;
                }
            }
            return result;
        }

        private string ExtractVideoFormat(string rawVideoFormat)
        {
            return rawVideoFormat.Split(new string[] { ", " }, StringSplitOptions.None)[0].Replace("Video: ", "");
        }

        private int ExtractVideoHeight(string rawInfo)
        {
            int result = 0;
            Match match = new Regex(@"(\d{2,4})x(\d{2,4})", RegexOptions.Compiled).Match(rawInfo);
            if (match.Success)
            {
                int.TryParse(match.Groups[2].Value, out result);
            }
            return result;
        }

        private int ExtractVideoWidth(string rawInfo)
        {
            int result = 0;
            Match match = new Regex(@"(\d{2,4})x(\d{2,4})", RegexOptions.Compiled).Match(rawInfo);
            if (match.Success)
            {
                int.TryParse(match.Groups[1].Value, out result);
            }
            return result;
        }

        public void GetVideoInfo(VideoFile input)
        {
            string parameters = string.Format("-i \"{0}\"", input.Path);
            string str2 = this.RunProcess(parameters);
            input.RawInfo = str2;
            input.Duration = this.ExtractDuration(input.RawInfo);
            input.BitRate = this.ExtractBitrate(input.RawInfo);
            input.RawAudioFormat = this.ExtractRawAudioFormat(input.RawInfo);
            input.AudioFormat = this.ExtractAudioFormat(input.RawAudioFormat);
            input.RawVideoFormat = this.ExtractRawVideoFormat(input.RawInfo);
            input.VideoFormat = this.ExtractVideoFormat(input.RawVideoFormat);
            input.Width = this.ExtractVideoWidth(input.RawInfo);
            input.Height = this.ExtractVideoHeight(input.RawInfo);
            input.FrameRate = this.ExtractFrameRate(input.RawVideoFormat);
            input.TotalFrames = this.ExtractTotalFrames(input.Duration, input.FrameRate);
            input.AudioBitRate = this.ExtractAudioBitRate(input.RawAudioFormat);
            input.VideoBitRate = this.ExtractVideoBitRate(input.RawVideoFormat);
            input.infoGathered = true;
        }

        public bool GetVideoThumbnail(VideoFile input, string saveThumbnailTo)
        {
            if (!input.infoGathered)
            {
                this.GetVideoInfo(input);
            }
            int num = (int) Math.Round(TimeSpan.FromTicks(input.Duration.Ticks / 3L).TotalSeconds, 0);
            if (num.Equals(0))
            {
                num = 1;
            }
            string parameters = string.Format("-i \"{0}\" \"{1}\" -vcodec mjpeg -ss {2} -vframes 1 -an -f rawvideo", input.Path, saveThumbnailTo, num);
            this.RunProcess(parameters);
            if (File.Exists(saveThumbnailTo))
            {
                return true;
            }
            parameters = string.Format("-i \"{0}\" \"{1}\" -vcodec mjpeg -ss {2} -vframes 1 -an -f rawvideo", input.Path, saveThumbnailTo, 1);
            this.RunProcess(parameters);
            return File.Exists(saveThumbnailTo);
        }

        private void proc_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                this.tempEncodedVideo.EncodingLog = this.tempEncodedVideo.EncodingLog + e.Data + Environment.NewLine;
                if (e.Data.StartsWith("frame"))
                {
                    this.iProgressErrorCount = 0;
                    EncodeProgressEventArgs args = new EncodeProgressEventArgs {
                        RawOutputLine = e.Data,
                        TotalFrames = this.tempVideoFile.TotalFrames
                    };
                    string[] strArray = e.Data.Split(new string[] { " ", "=" }, StringSplitOptions.RemoveEmptyEntries);
                    long result = 0L;
                    long.TryParse(strArray[1], out result);
                    args.CurrentFrame = result;
                    short num2 = 0;
                    short.TryParse(strArray[3], out num2);
                    args.FPS = num2;
                    double currentFrame = args.CurrentFrame;
                    double totalFrames = args.TotalFrames;
                    short num5 = (short) Math.Round((double) ((currentFrame * 100.0) / totalFrames), 0);
                    args.Percentage = num5;
                    if (this.tempCaller != null)
                    {
                        this.tempCaller.BeginInvoke(new EncodeProgressEventHandler(this.OnEncodeProgress.Invoke), new object[] { this.tempCaller, args });
                    }
                    else
                    {
                        this.DoEncodeProgress(args);
                    }
                }
                else
                {
                    this.iProgressErrorCount++;
                }
            }
            else
            {
                this.iProgressErrorCount++;
            }
            if (this.iProgressErrorCount > 100)
            {
                Process process = (Process) sender;
                try
                {
                    process.Kill();
                }
                catch
                {
                }
            }
        }

        private void proc_Exited(object sender, EventArgs e)
        {
            Process process = (Process) sender;
            int exitCode = process.ExitCode;
            bool flag = File.Exists(this.tempEncodedVideo.EncodedVideoPath);
            this.tempEncodedVideo.Success = exitCode.Equals(0) && flag;
            EncodeFinishedEventArgs args = new EncodeFinishedEventArgs {
                EncodedVideoInfo = this.tempEncodedVideo
            };
            if (this.tempCaller != null)
            {
                this.tempCaller.BeginInvoke(new EncodeFinishedEventHandler(this.OnEncodeFinished.Invoke), new object[] { this.tempCaller, args });
            }
            else
            {
                this.DoEncodeFinished(args);
            }
            this.iProgressErrorCount = 0;
            process.Close();
        }

        private string RunProcess(string Parameters)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(this.FFmpegPath, Parameters) {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            string str = null;
            try
            {
                Process process = Process.Start(startInfo);
                str = process.StandardError.ReadToEnd();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception)
            {
                str = string.Empty;
            }
            return str;
        }

        private void RunProcessAsync(string Parameters)
        {
            ProcessStartInfo info = new ProcessStartInfo(this.FFmpegPath, Parameters) {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = false,
                RedirectStandardError = true
            };
            Process process = new Process {
                StartInfo = info,
                EnableRaisingEvents = true
            };
            process.ErrorDataReceived += new DataReceivedEventHandler(this.proc_ErrorDataReceived);
            process.Exited += new EventHandler(this.proc_Exited);
            process.Start();
            process.BeginErrorReadLine();
        }

        public string FFmpegPath { get; set; }

        private string Params { get; set; }
    }
}

