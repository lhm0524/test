using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.MODAL
{
    public class UsersModel
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public DateTime LastLoginTime { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime RegeistTime { get; set; }
        public string Sex { get; set; }
        public string UserPwd { get; set; }
        public string UserName { get; set; }
        public string Answer { get; set; }
        public string RequireInfomation { get; set; }
        public object UserImagePath { get; set; }
        public string Email { get; set; }
    }
}
