using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.Models
{
    public class CostAdjustment
    {
        public long WorkRequestId { get; set; }
        public string District { get; set; }
        public string Entity { get; set; }

        public decimal? AmountAnnualRevEst { get; set; }
        public decimal? AmountFixedBid { get; set; }

        public decimal? MultiplierAmountLaborCompany { get; set; }
        public decimal? MultiplierAmountLaborContractor { get; set; }
        public decimal? MultiplierAmountMaterial { get; set; }
        public decimal? MultiplierHoursLaborCompany { get; set; }
        
        public int QuantityAfudcMonths { get; set; }
        public short? NoAsbDesign { get; set; }
        public string BidItem { get; set; }
        public decimal? QuantityBidItem { get; set; }

    }
}
