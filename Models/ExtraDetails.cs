using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class ExtraDetails
    {
        public string workRequestId { get; set; }
        public string district { get; set; }
        public string fg_complex { get; set; }
        public string dog_code { get; set; }
        public string nm_subdivision { get; set; }
        public string no_lot { get; set; }        
        
        /// <summary>
        /// These values are for ELECTRIC only...might need to inherit a base class for both GAS/ELECTRIC.
        /// </summary>    
        public string id_map_no { get; set; }
        public string id_feeder { get; set; }
        public string cd_phase { get; set; }
        public string id_feeder2 { get; set; }
        public string cd_phase2 { get; set; }
        public string cd_service_size { get; set; }
        public string css_cd_mtr_phs { get; set; }
        public string cd_service_volt { get; set; }
        public string cd_customer_type { get; set; }
        public string dt_design_reqd { get; set; }
        public string field_alert { get; set; }
        public string cd_equip_id { get; set; }
                
        /// <summary>
        /// These Values are GAS
        /// </summary>
        public string utility_type { get; set; }
        public string ds_designer_job_gas { get; set; }
        public string dt_cust_want { get; set; }
        public string trs_quarter { get; set; }
        public string trs_section { get; set; }
        public string trs_town { get; set; }
        public string trs_direction { get; set; }
        public string trs_range { get; set; }
        public string construction_type { get; set; }
        public string on_off_main { get; set; }
        public string building_type { get; set; }
        public string pre_1978_construction { get; set; }
        public string main_to_lot_line_footage { get; set; }
        public string pbi_area_no { get; set; }
        public string public_building { get; set; }
        public string branch_service { get; set; }
        public string existing_service_fln { get; set; }
        public string trailer_park_name { get; set; }
        public string main_order_no { get; set; }
        public string ds_designer_job_el { get; set; }      
        public string system_pressure { get; set; }
        //public string dt_design_reqd { get; set; }

        public ExtraDetails()
        {
        }
    }
}
