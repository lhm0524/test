using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.BLL
{
    public class DepartmentBLL
    {
        private IDAL.IDepartmentDAL dal;
        public DepartmentBLL()
        {
            dal = DALFACTORY.AbstractFactory.CreateDepartmentDALInstance();
        }
        public IEnumerable<MODEL.DepartmentModel> LoadByParentID(int parentId)
        {
            return dal.LoadByParentID(parentId);
        }

        public MODEL.DepartmentModel GetEntityByPrimarykey(int primaryKey)
        {
            return dal.GetModelByPrimaryKey(primaryKey);
        }
    }
}
