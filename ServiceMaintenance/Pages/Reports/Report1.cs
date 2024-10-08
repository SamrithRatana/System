using DevExpress.XtraReports.UI;
using ServiceMaintenance.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ServiceMaintenance.Pages
{
    public partial class Report1 : DevExpress.XtraReports.UI.XtraReport
    {
        public Report1()
        {
            InitializeComponent();
        }
        public void SetData(IEnumerable<ServiceReportData> data)
        {
            this.DataSource = data;
        }
    }
}
