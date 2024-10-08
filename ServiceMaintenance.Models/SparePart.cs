using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceMaintenance.Models
{
    public class SparePart
    {
        [Key]
        public int SparePartID { get; set; }

        [MaxLength(255)]
        public string PartName { get; set; }
        public string Description { get; set; }

        [MaxLength(255)]
        public string PartNumber { get; set; }

        public int Qty { get; set; }  
  

        [MaxLength(255)]  
        public string Photo { get; set; }
        [MaxLength(255)] 
       
        public string Status { get; set; }

       
    }
}
