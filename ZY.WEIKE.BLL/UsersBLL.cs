using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.BLL
{
    public class UsersBLL
    {
        private IDAL.IUserDAL dal { get; set; }
        public UsersBLL()
        {
            dal = DALFACTORY.AbstractFactory.CreateUserDALInstance();
        }

        public int Login(int loginMethod, string name, string pwd, out string role)
        {
            object out_role;
            int id = dal.Login((int)loginMethod, name, pwd, out out_role);
            if (out_role == DBNull.Value)
            {
                role = "null";
            }
            else
            {
                role = out_role.ToString();
            }
            return id;
        }

        public MODAL.UsersModel GetEntity(int primaryKey)
        {
            return dal.GetModelByPrimaryKey(primaryKey);
        }

        public MODAL.UsersModel LoadImageAndName(int primaryKey)
        {
            return dal.GetImageAndName(primaryKey);
        }
        public bool EditEntity(MODAL.UsersModel t)
        {
            return dal.EditEntity(t) > 0;
        }
        public bool IsExist(MODAL.UsersModel userModel)
        {
            return dal.IsExists(userModel) > 1;
        }

        public bool CreateEntity(MODAL.UsersModel user)
        {
            return dal.CreateEntity(user) > 0;
        }

        public bool EditUserImage(int primarykey, string imagename)
        {
            return dal.EditImage(primarykey, imagename) > 0;
        }
    }
}
