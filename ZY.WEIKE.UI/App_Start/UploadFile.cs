using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZY.WEIKE.UI.App_Start
{
    public static class UploadFile
    {
        /// <summary>
        /// 处理上传图片
        /// 0:表示为空, 1:表示非法拓展名, -2:文件过大
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="serverpath">服务器的临时文件夹绝对路径</param>
        /// <param name="newname">新的文件名称</param>
        /// <returns></returns>
        public static int UploadImage(HttpPostedFileBase file, string serverpath , ref string newname)
        {
            if (file == null)
            {
                return 0;
            }
            string extension = System.IO.Path.GetExtension(file.FileName);
            if (!System.Text.RegularExpressions.Regex.IsMatch(extension, System.Configuration.ConfigurationManager.AppSettings["imageextension"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                return -1;
            }
            if (file.ContentLength > 2 * 1024 * 1024)
            {
                return -2;
            }
            newname = Guid.NewGuid() + extension;
            file.SaveAs(serverpath + newname);
            return 1;
        }
    }
    
}