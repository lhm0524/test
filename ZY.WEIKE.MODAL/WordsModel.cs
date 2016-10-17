using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.MODEL
{
    public class WordsModel
    {
        public int Id { get; set; }
        public int WeiKeId { get; set; }
        public int UserId { get; set; }
        public string WordsTitle { get; set; }
        public string WordsBody { get; set; }
        public DateTime WordsTime { get; set; }
    }
}
