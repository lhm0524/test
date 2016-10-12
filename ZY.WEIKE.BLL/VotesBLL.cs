using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZY.WEIKE.BLL
{
    public class VotesBLL
    {
        private IDAL.IVotesDAL dal { get; set; }

        public VotesBLL()
        {
            dal = DALFACTORY.AbstractFactory.CreateVotesDALInstance();
        }

        public MODAL.VotesModel GetEntity(string where, Dictionary<string, object> dic)
        {
            return dal.GetEntity(where, dic);
        }
        public int Vote(int userid, MODAL.VotesModel m)
        {
            return dal.Vote(userid, m);
        }
    }
}
