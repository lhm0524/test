using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZY.WEIKE.UI.App_Start
{
    public abstract class Upload
    {
        private string _serverpath;
        public string ServerPath
        {
            get { return _serverpath; }
            set { _serverpath = value; }
        }

        private string _newname;
        public string NewName
        {
            get { return _newname; }
            set { _newname = value; }
        }

        private int _state;
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

        private int _maxsize;
        protected int MaxSize
        {
            get { return _maxsize; }
            set { _maxsize = value; }
        }

        private string _allowextension;
        protected string AllowExtension
        {
            get { return _allowextension; }
            set { _allowextension = value; }
        }

        private HttpPostedFileBase _file;
        public HttpPostedFileBase File
        {
            protected get
            {
                if (_file == null)
                {
                    throw new Exception("文件为空");
                }
                return _file;
            }
            set
            {
                _file = value;
            }
        }

        private string _extension;
        protected string Extension
        {
            get
            {
                return System.IO.Path.GetExtension(File.FileName.Trim());
            }
            private set
            {
                _extension = value;
            }
        }

        protected virtual bool CheckExtension()
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(Extension, AllowExtension, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                State = -1;
                return false;
            }
            return true;
        }
        protected virtual bool CheckSize()
        {
            if (File.ContentLength > MaxSize)
            {
                State = -2;
                return false;
            }
            return true;
        }
        public abstract void UploadAction();
    }

    public class ImageUploader : Upload
    {
        public ImageUploader()
        {
            AllowExtension = System.Configuration.ConfigurationManager.AppSettings["imageextension"];
            MaxSize = 2 * 1024 * 1024;
        }
        public ImageUploader(HttpPostedFileBase fi)
        {
            AllowExtension = System.Configuration.ConfigurationManager.AppSettings["imageextension"];
            MaxSize = 2 * 1024 * 1024;
            File = fi;
        }
        public override void UploadAction()
        {
            if (!CheckExtension())
            {
                return;
            }
            if (!CheckSize())
            {
                State = -2;
                return;
            }
            NewName = Guid.NewGuid() + Extension;
            File.SaveAs(ServerPath + NewName);
            State = 1;
        }
    }

    public class VideoUploader : Upload
    {
        public VideoUploader()
        {
            AllowExtension = System.Configuration.ConfigurationManager.AppSettings["vedioextension"];
            MaxSize = 200 * 1024 * 1024;
        }
        public override void UploadAction()
        {
            if (!CheckExtension())
            {
                return;
            }
            if (!CheckSize())
            {
                return;
            }
            NewName = Guid.NewGuid() + Extension;
            File.SaveAs(ServerPath + NewName);
            State = 1;
        }
    }

    public class AttachUploader : Upload
    {

        public AttachUploader ()
        {
            AllowExtension = System.Configuration.ConfigurationManager.AppSettings["attachextension"];
            MaxSize = 40 * 1024 * 1024;
        }

        public override void UploadAction()
        {
            if (!CheckExtension())
            {
                return;
            }
            if (!CheckSize())
            {
                return;
            }
            NewName = Guid.NewGuid() + Extension;
            File.SaveAs(ServerPath + NewName);
            State = 1;
        }
    }
}