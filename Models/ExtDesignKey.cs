using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Runtime.Serialization;
using WM.STORMS.DataAccessLayer;
using WM.Common;

namespace WM.STORMS.BusinessLayer.Models 
{
    public class ExtDesignKey 
    {
        private string _IdOper;
        public DateTime TsExtDsgn { get; set; }
        public string IdOper {
            get {
                return _IdOper;
            }
            set {
                if (string.IsNullOrEmpty(value)) {
                    _IdOper = "WMExtDesig";
                } else {
                    _IdOper = value;
                }
            }
        }
        public int? ExtDsgnFacAttSeq { get; set; }
       
        public ExtDesignKey()
        { 
            
        
        }

        //public int? GetNextExtDsgnFacAttSeq()
        //{
        //    using (OleDbConnection conn = new OleDbConnection(OLEDB_ConnectStr))
        //    {
        //        conn.Open();
        //        OleDbCommand cmd = conn.CreateCommand();
        //        cmd.CommandType = CommandType.Text;

        //        cmd.CommandText = " select WE_S_SO_EXTDSG_FACATT.nextval from dual ";

        //        this.ExtDsgnFacAttSeq = Convert.ToInt32(cmd.ExecuteScalar());

        //        return this.ExtDsgnFacAttSeq;
        //    }
        //}
    }
}