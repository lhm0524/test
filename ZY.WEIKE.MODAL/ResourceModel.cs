using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.MODEL
{
    public class ResourceModel
    {
        public int Id { get; set; }
        public int WeiKeId { get; set; }
        public String AttachmentPath { get; set; }
        public string VideoPath { get; set; }
        public string VideoImgPath { get; set; }
        public double TotalProgress { get; set; }
    }
}
