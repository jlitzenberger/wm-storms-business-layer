using System;

namespace WM.STORMS.BusinessLayer.Models {
    public class ApiException {
        public long Id { get; set; }
        public string ExceptionType { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string StackTrace { get; set; }
        public DateTime ExceptionDateTime { get; set; }
    }
}
