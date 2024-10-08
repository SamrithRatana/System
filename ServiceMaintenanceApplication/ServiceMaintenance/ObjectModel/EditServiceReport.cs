using System.ComponentModel.DataAnnotations;

namespace ServiceMaintenance.ObjectModel
{
    public class EditServiceReport
    {
        [Key]
        public int ID { get; set; }

        public string Code { get; set; }


        public DateTime ReportID { get; set; }

        [Required(ErrorMessage = "CompanyName is required")]
        [StringLength(255, ErrorMessage = "CompanyName cannot exceed 255 characters")]
        public string CompanyName { get; set; }

        [StringLength(255, ErrorMessage = "Attention cannot exceed 255 characters")]
        public string Attention { get; set; }

        [StringLength(255, ErrorMessage = "Atten cannot exceed 255 characters")]
        public string Atten { get; set; }

        [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters")]
        public string Address { get; set; }

        [StringLength(50, ErrorMessage = "Mobile cannot exceed 50 characters")]
        public string Mobile { get; set; }

        [StringLength(50, ErrorMessage = "OfficeTel cannot exceed 50 characters")]
        public string OfficeTel { get; set; }

        [StringLength(255, ErrorMessage = "ProductName cannot exceed 255 characters")]
        public string ProductName { get; set; }

        [StringLength(255, ErrorMessage = "Instrument cannot exceed 255 characters")]
        public string Instrument { get; set; }

        [StringLength(255, ErrorMessage = "SerialNumber cannot exceed 255 characters")]
        public string SerialNumber { get; set; }

        [StringLength(255, ErrorMessage = "BranchSerial cannot exceed 255 characters")]
        public string BranchSerial { get; set; }

        [StringLength(255, ErrorMessage = "CustomerReq cannot exceed 255 characters")]
        public string CustomerReq { get; set; }

        [StringLength(255, ErrorMessage = "ActionTaken cannot exceed 255 characters")]
        public string ActionTaken { get; set; }

        [StringLength(255, ErrorMessage = "Solution cannot exceed 255 characters")]
        public string Solution { get; set; }

        public bool Onsite { get; set; }

        public bool CompanyService { get; set; }

        public bool ServiceContract { get; set; }

        [Required(ErrorMessage = "Datestart is required")]
        public DateTime Datestart { get; set; }

        [Required(ErrorMessage = "DateFinish is required")]
        public DateTime DateFinish { get; set; }

        [StringLength(50, ErrorMessage = "Engineer cannot exceed 50 characters")]
        public string Engineer { get; set; }

        [StringLength(50, ErrorMessage = "Verify cannot exceed 50 characters")]
        public string Verify { get; set; }

        [StringLength(50, ErrorMessage = "Customer cannot exceed 50 characters")]
        public string Customer { get; set; }
    }
}
