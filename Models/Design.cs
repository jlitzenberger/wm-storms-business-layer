using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.STORMS.DataAccessLayer;

namespace WM.STORMS.BusinessLayer.Models
{
    public class Design
    {
        public string District { get; set; }          //not needed because design is part of WR??????
        public long WorkRequestID { get; set; }     //not needed because design is part of WR??????
        public string DesignNumber { get; set; }
        public string DesignDescription { get; set; }
        public bool FlagPreferred { get; set; }
        public bool FlagComplete { get; set; }
        public bool FlagExtDesign { get; set; }
        public string IDOper { get; set; }
        public List<Point> Points { get; set; }

        public Design()
        {
            //Points = new List<Point>();
        }
    }
}
