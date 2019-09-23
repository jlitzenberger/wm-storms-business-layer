using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using WM.Common;

namespace WM.STORMS.BusinessLayer.BusinessLogic {
    public class WorkRequestKeyGenerator : BaseBl {
        public int NextValSeq { get; set; }

        public WorkRequestKeyGenerator()
        {
            NextValSeq = GetNextSeq();
        }

        private int GetNextSeq() {
            using (OleDbConnection conn = new OleDbConnection(ConfigurationManager.AppSettings["StormsOleDb"])) { //TODO: Change This
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = " SELECT we_s_so_extdsg.nextval FROM dual ";

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}