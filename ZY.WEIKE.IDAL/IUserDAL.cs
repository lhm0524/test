using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.IDAL
{
    public interface IUserDAL : IBaseDAL<MODEL.UsersModel>
    {
        int Login(int logintype, string name, string pwd, out object role);
        MODEL.UsersModel GetImageAndName(int primaryKey);
        int IsExists(MODEL.UsersModel usermodel);
        int EditImage(int primarykey, string imagepath);
    }
}
