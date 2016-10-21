using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.MODEL
{
	public class MessagesModel
	{
		public System.Int32 Id { get; set; }
		public System.Int32 FromUserId { get; set; }
		public System.Int32 ToUserID { get; set; }
        public System.String Subject { get; set; }
		public System.Int32 MSGTypeId { get; set; }
		public System.String MSG { get; set; }
		public System.Boolean State { get; set; }
		public System.DateTime Time { get; set; }
	}
}

