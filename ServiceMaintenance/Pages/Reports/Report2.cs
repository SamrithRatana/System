using DevExpress.XtraReports.UI;
using ServiceMaintenance.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ServiceMaintenance.Pages.Reports
{
    public partial class Report2 : DevExpress.XtraReports.UI.XtraReport
    {
        public Report2()
        {
            InitializeComponent();
        }
        public void SetParameters(RepairServices repairService, string serviceLocation)
        {
            this.Parameters["CompanyName"].Value = repairService.CompanyName;
            this.Parameters["ContactName"].Value = repairService.ContactName;
            this.Parameters["PhoneNumber"].Value = repairService.PhoneNumber;
            this.Parameters["Address"].Value = repairService.Address;
            this.Parameters["ItemName"].Value = repairService.itemName;
            this.Parameters["SerialNumber"].Value = repairService.serialNumber;
            this.Parameters["CustomerRequest"].Value = repairService.CustomerRequest;
            this.Parameters["inspection"].Value = repairService.Inspection;
            this.Parameters["Solution"].Value = repairService.Solution;

            // Check serviceLocation based on string values
            if (serviceLocation == "CompanyService") // CompanyService
            {
                this.Parameters["CompanyServiceChecked"].Value = true; // Check Company Service
                this.Parameters["OnSiteChecked"].Value = false;       // Uncheck OnSite
            }
            else if (serviceLocation == "OnSite") // OnSite
            {
                this.Parameters["CompanyServiceChecked"].Value = false; // Uncheck Company Service
                this.Parameters["OnSiteChecked"].Value = true;          // Check OnSite
            }
        }

    }
}
