using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMaintenance.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string ListId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifyBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CustomerTypeListId { get; set; }
        public string VATTIN { get; set; }
        public string BankAccountListId { get; set; }
        public byte InvoiceType { get; set; }
        public string SalesRepListId { get; set; }
        public string TermListId { get; set; }
        public bool IsActive { get; set; }

    }
}
