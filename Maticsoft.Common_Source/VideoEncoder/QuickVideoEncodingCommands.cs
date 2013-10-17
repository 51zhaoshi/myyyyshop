namespace VideoEncoder
{
    using System;

    public class QuickVideoEncodingCommands
    {
        private static string CIF = "cif";
        public static string FLVHighQualityKeepOriginalSize = string.Format("-y -b {0} -ab {1} -ar {2} -f flv", new object[] { HQVideoBitrate, HQAudioBitrate, HQAudioSamplingFrequency, VGA });
        public static string FLVHighQualityVGA = string.Format("-y -b {0} -ab {1} -ar {2} -s {3} -f flv", new object[] { HQVideoBitrate, HQAudioBitrate, HQAudioSamplingFrequency, VGA });
        public static string FLVLowQualityKeepOriginalSize = string.Format("-y -b {0} -ab {1} -ar {2} -f flv", new object[] { LQVideoBitrate, LQAudioBitrate, LQAudioSamplingFrequency, QVGA });
        public static string FLVLowQualityQCIF = string.Format("-y -b {0} -ab {1} -ar {2} -s {3} -f flv", new object[] { LQVideoBitrate, LQAudioBitrate, LQAudioSamplingFrequency, QVGA });
        public static string FLVMediumQualityCIF = string.Format("-y -b {0} -ab {1} -ar {2} -s {3} -f flv", new object[] { MQVideoBitrate, MQAudioBitrate, MQAudioSamplingFrequency, CIF });
        public static string FLVMediumQualityKeepOriginalSize = string.Format("-y -b {0} -ab {1} -ar {2} -f flv", new object[] { MQVideoBitrate, MQAudioBitrate, MQAudioSamplingFrequency, CIF });
        public static string FLVVeryHighQualityKeepOriginalSize = string.Format("-y -b {0} -ab {1} -ar {2} -f flv", new object[] { VHQVideoBitrate, VHQAudioBitrate, HQAudioSamplingFrequency, SVGA });
        public static string FLVVeryHighQualitySVGA = string.Format("-y -b {0} -ab {1} -ar {2} -s {3} -f flv", new object[] { VHQVideoBitrate, VHQAudioBitrate, HQAudioSamplingFrequency, SVGA });
        private static string HQAudioBitrate = "96k";
        private static string HQAudioSamplingFrequency = "44100";
        private static string HQVideoBitrate = "756k";
        private static string LQAudioBitrate = "32k";
        private static string LQAudioSamplingFrequency = "22050";
        private static string LQVideoBitrate = "256k";
        public static string MP4HighQualityKeepOriginalSize = string.Format("-y -b {0} -ab {1} -ar {2} -f mp4", new object[] { HQVideoBitrate, HQAudioBitrate, HQAudioSamplingFrequency, VGA });
        public static string MP4HighQualityVGA = string.Format("-y -b {0} -ab {1} -ar {2} -s {3} -f mp4", new object[] { HQVideoBitrate, HQAudioBitrate, HQAudioSamplingFrequency, VGA });
        public static string MP4LowQualityKeepOriginalSize = string.Format("-y -b {0} -ab {1} -ar {2} -f mp4", new object[] { LQVideoBitrate, LQAudioBitrate, LQAudioSamplingFrequency, QVGA });
        public static string MP4LowQualityQVGA = string.Format("-y -b {0} -ab {1} -ar {2} -s {3} -f mp4", new object[] { LQVideoBitrate, LQAudioBitrate, LQAudioSamplingFrequency, QVGA });
        public static string MP4MediumQualityCIF = string.Format("-y -b {0} -ab {1} -ar {2} -s {3} -f mp4", new object[] { MQVideoBitrate, MQAudioBitrate, MQAudioSamplingFrequency, CIF });
        public static string MP4MediumQualityKeepOriginalSize = string.Format("-y -b {0} -ab {1} -ar {2} -f mp4", new object[] { MQVideoBitrate, MQAudioBitrate, MQAudioSamplingFrequency, CIF });
        private static string MQAudioBitrate = "64k";
        private static string MQAudioSamplingFrequency = "44100";
        private static string MQVideoBitrate = "512k";
        private static string QCIF = "qcif";
        private static string QVGA = "qvga";
        private static string SQCIF = "sqcif";
        private static string SVGA = "svga";
        public static string THREEGPHighQualityCIF = string.Format("-y -acodec aac -b {0} -ab {1} -ar {2} -s {3} -f 3gp", new object[] { VHQVideoBitrate, VHQAudioBitrate, HQAudioSamplingFrequency, CIF });
        public static string THREEGPLowQualitySQCIF = string.Format("-y -acodec aac -ac 1 -b {0} -ab {1} -ar {2} -s {3} -f 3gp", new object[] { LQVideoBitrate, LQAudioBitrate, LQAudioSamplingFrequency, SQCIF });
        public static string THREEGPMediumQualityQCIF = string.Format("-y -acodec aac -b {0} -ab {1} -ar {2} -s {3} -f 3gp", new object[] { MQVideoBitrate, MQAudioBitrate, MQAudioSamplingFrequency, QCIF });
        private static string VGA = "vga";
        private static string VHQAudioBitrate = "128k";
        private static string VHQVideoBitrate = "1024k";
        public static string WMVHighQualityKeepOriginalSize = string.Format("-y -vcodec wmv2  -acodec wmav2 -b {0} -ab {1} -ar {2}", new object[] { HQVideoBitrate, HQAudioBitrate, HQAudioSamplingFrequency, VGA });
        public static string WMVHighQualityVGA = string.Format("-y -vcodec wmv2  -acodec wmav2 -b {0} -ab {1} -ar {2} -s {3}", new object[] { HQVideoBitrate, HQAudioBitrate, HQAudioSamplingFrequency, VGA });
        public static string WMVLowQualityKeepOriginalSize = string.Format("-y -vcodec wmv2  -acodec wmav2 -b {0} -ab {1} -ar {2}", new object[] { LQVideoBitrate, LQAudioBitrate, LQAudioSamplingFrequency, QVGA });
        public static string WMVLowQualityQVGA = string.Format("-y -vcodec wmv2  -acodec wmav2 -b {0} -ab {1} -ar {2} -s {3}", new object[] { LQVideoBitrate, LQAudioBitrate, LQAudioSamplingFrequency, QVGA });
        public static string WMVMediumQualityCIF = string.Format("-y -vcodec wmv2  -acodec wmav2 -b {0} -ab {1} -ar {2} -s {3}", new object[] { MQVideoBitrate, MQAudioBitrate, MQAudioSamplingFrequency, CIF });
        public static string WMVMediumQualityKeepOriginalSize = string.Format("-y -vcodec wmv2  -acodec wmav2 -b {0} -ab {1} -ar {2}", new object[] { MQVideoBitrate, MQAudioBitrate, MQAudioSamplingFrequency, CIF });
        public static string WMVVeryHighQualityKeepOriginalSize = string.Format("-y -vcodec wmv2  -acodec wmav2 -b {0} -ab {1} -ar {2}", new object[] { VHQVideoBitrate, VHQAudioBitrate, HQAudioSamplingFrequency, SVGA });
        public static string WMVVeryHighQualitySVGA = string.Format("-y -vcodec wmv2  -acodec wmav2 -b {0} -ab {1} -ar {2} -s {3}", new object[] { VHQVideoBitrate, VHQAudioBitrate, HQAudioSamplingFrequency, SVGA });

        public static string AutoDetect(long videoBitrate)
        {
            return string.Format("-y -b {0}", videoBitrate.ToString());
        }
    }
}

