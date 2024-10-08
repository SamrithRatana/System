using System.ComponentModel.DataAnnotations;

namespace ServiceMaintenance.Models
{
    public class ServiceReportData
    {
        
        public int ID { get; set; }
       
        public string Code { get; set; }

      
        public DateTime ReportID { get; set; }

        
        public string CompanyName { get; set; }

       
        public string Attention { get; set; }

       
        public string Atten { get; set; }

       
        public string Address { get; set; }

       
        public string Mobile { get; set; }

       
        public string OfficeTel { get; set; }

       
        public string ProductName { get; set; }

      
        public string Instrument { get; set; }

       
        public string SerialNumber { get; set; }

       
        public string BranchSerial { get; set; }

       
        public string CustomerReq { get; set; }

     
        public string ActionTaken { get; set; }

       
        public string Solution { get; set; }

        public bool Onsite { get; set; }

        public bool CompanyService { get; set; }

        public bool ServiceContract { get; set; }

       
        public DateTime Datestart { get; set; }

       
        public DateTime DateFinish { get; set; }

      
        public string Engineer { get; set; }

       
        public string Verify { get; set; }

       
        public string Customer { get; set; }
    }
}

