using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.IDAL
{
    public interface IUserDAL : IBaseDAL<MODAL.UsersModel>
    {
        int Login(int logintype, string name, string pwd, out object role);
        MODAL.UsersModel GetImageAndName(int primaryKey);
        int IsExists(MODAL.UsersModel usermodel);
    }
}
