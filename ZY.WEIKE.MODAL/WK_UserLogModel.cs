using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.MODEL
{
    public class WK_UserLogModel
    {
        public int Z_ID { get; set; }
        public int Z_WeiKeId { get; set; }
        public int Z_UserId { get; set; }
        public int Z_PlayCount { get; set; }
        public int Z_SurfaceCount { get; set; }
        public float Z_Progress { get; set; }
    }
}
