using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.MODEL
{
    public class WeiKeModel
    {
        public System.Int32 Id { get; set; }
        public System.String Name { get; set; }
        public System.String Detail { get; set; }
        public System.Int32 TeacherId { get; set; }
        public System.Int32 TypeId { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.String Description { get; set; }
        public System.Int32 CollectSum { get; set; }
        public System.Int32 PlaySum { get; set; }
        public System.Int32 SupprtSum { get; set; }
        public System.Int32 Rating { get; set; }
    }
}
