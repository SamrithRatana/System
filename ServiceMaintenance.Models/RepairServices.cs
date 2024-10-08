using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceMaintenance.Models
{
    public class RepairServices
    {
        public Guid Id { get; set; } = Guid.Empty;

        [Required(ErrorMessage = "Service Date is required.")]
        public DateTime ServiceDate { get; set; } = DateTime.Now;

        public Guid CustomerId { get; set; } = Guid.Empty;

        [Required(ErrorMessage = "Company Name is required.")]
        [StringLength(100, ErrorMessage = "Company Name cannot exceed 100 characters.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Contact Name is required.")]
        [StringLength(100, ErrorMessage = "Contact Name cannot exceed 100 characters.")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Item is required.")]
        public Guid itemId { get; set; }

      
        public string itemName { get; set; }
       
        public string serialNumber { get; set; }
        [StringLength(500, ErrorMessage = "Customer Request cannot exceed 500 characters.")]
        public string CustomerRequest { get; set; }

        [StringLength(500, ErrorMessage = "Inspection cannot exceed 500 characters.")]
        public string Inspection { get; set; }

        [StringLength(500, ErrorMessage = "Solution cannot exceed 500 characters.")]
        public string Solution { get; set; }

        [Required(ErrorMessage = "Service Location is required.")]
        [StringLength(250, ErrorMessage = "Service Location cannot exceed 250 characters.")]
        public string ServiceLocation { get; set; }


        
        public string ServiceType { get; set; }

       
        public string ServicePriority { get; set; }

        
        public string Status { get; set; }

        public int ServiceTypeId { get; set; }
        public int ServicePriorityId { get; set; }
        public int StatusId { get; set; }
    }

    public class ServiceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ServicePriority
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ServiceStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
