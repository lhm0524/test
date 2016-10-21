using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZY.WEIKE.COMMONHELPER
{
    public class CommonHelper
    {

        public static string ComputeHash(string source)
        {
            MD5 md5 = MD5.Create();

            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(source));

            return Encoding.UTF8.GetString(hash);
        }

        /// <summary>
        ///截取第10s
        /// </summary>
        /// <param name="ffmpegpath">ffmpeg的路径</param>
        /// <param name="sourcevideo">视频源</param>
        /// <param name="targetpath">目标路径</param>
        /// <param name="targetname">图片名称</param>
        /// <returns>截取成功则返回路径，否则返回null</returns>
        public static string CaptureVideo(string ffmpegpath, string sourcevideo, string targetpath, string targetname)
        {
            Process p = new Process();
            p.StartInfo.FileName = ffmpegpath;
            p.StartInfo.Arguments = string.Format("-i \"{0}\" -y -f  image2  -ss 00:00:10 -vframes 1  {1}{2}.jpeg", sourcevideo, targetpath, targetname);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.ErrorDataReceived += p_ErrorDataReceived;
            p.OutputDataReceived += p_OutputDataReceived;

            p.Start();
            p.WaitForExit(1000);
            p.Close();
            p.Dispose();

            if (File.Exists(targetpath + targetname + ".jpeg"))
            {
                return targetname + ".jpeg";
            }
            return null;
        }

        public static double GetVideoDuration(string ffmpegpath, string sourcevideo)
        {
            Process p = new Process();
            ProcessStartInfo startinfo = new ProcessStartInfo();
            startinfo.FileName = ffmpegpath;
            startinfo.Arguments = string.Format(" -i {0}", sourcevideo);
            startinfo.UseShellExecute = false;
            startinfo.RedirectStandardError = true;
            startinfo.CreateNoWindow = true;
            startinfo.RedirectStandardOutput = false;
            p.StartInfo = startinfo;
            p.Start();
            string output = p.StandardError.ReadToEnd();
            p.WaitForExit(1000);
            p.Close();
            p.Dispose();
            string pattern = "Duration:\\s(\\d+:\\d+:.+),\\sstart:\\s(.+),\\sbitrate:\\s(\\d+)\\skb/s";
            Regex r = new Regex(pattern);
            Match m = r.Match(output);
            DateTime d = DateTime.Parse(m.Groups[1].ToString());
            double seconds = d.Hour * 60 + d.Minute * 60 + d.Second + d.Millisecond * 0.001;
            return seconds;
        }

        static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            //输出
            File.WriteAllText("e:\\e.txt", e.Data);
        }

        static void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            //e.Data;//错误日志

        }
    }
}
