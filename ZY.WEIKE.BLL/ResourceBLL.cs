using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.BLL
{
    public class ResourceBLL
    {
        private IDAL.IResourceDAL dal;

        public ResourceBLL()
        {
            dal = DALFACTORY.AbstractFactory.CreateResourceDALInstance();
        }

        public string GetImgPath(int weikeid)
        {
            return dal.GetImgPath(weikeid);
        }

        public MODAL.ResourceModel GetModelByPrimaryKey(int id)
        {
            return dal.GetModelByPrimaryKey(id);
        }
    }
}
