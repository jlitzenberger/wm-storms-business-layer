using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WM.STORMS.BusinessLayer.Models {
   public class WMException : Exception {

        public string nm_interface { get; set; }
        public DateTime? ts_error { get; set; }
        public int cd_seq_error { get; set; }
        public int cd_seq_error_run { get; set; }
        public int cd_sqlcode { get; set; }
        public string txt_sqlerrtext { get; set; }
        public string txt_iferror { get; set; }
        public string cd_isr_no { get; set; }
        public Int64 cd_wr { get; set; }
        public Int64 cd_workpacket { get; set; }
        public string cd_project { get; set; }
        public string cd_crew { get; set; }
        public string id_empl { get; set; }
        public string cd_fleet { get; set; }
        public string nm_table { get; set; }
        public string nm_column { get; set; }
        public string fg_data_error { get; set; }
        public DateTime? ts_error_logged { get; set; }

    }
}

