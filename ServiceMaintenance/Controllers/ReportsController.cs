using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ServiceMaintenance
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly string _connectionString;

        public ReportsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBConnection");
        }

        [HttpGet("generate-report")]
        public IActionResult GenerateReport(int id)
        {
            try
            {
                DataTable dt = GetData(id);
                byte[] reportBytes = RenderReport(dt);
                return File(reportBytes, "application/pdf", "Report.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private DataTable GetData(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM PersonalInfoTable WHERE ID = @ID", con);
            da.SelectCommand.Parameters.AddWithValue("@ID", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        private byte[] RenderReport(DataTable data)
        {
            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "ReportTutorialMyInfo.rdlc");

            if (!System.IO.File.Exists(reportPath))
            {
                throw new FileNotFoundException("The report file was not found.", reportPath);
            }

            LocalReport report = new LocalReport(reportPath);
            report.AddDataSource("DataSet1", data);

            var result = report.Execute(RenderType.Pdf);
            return result.MainStream;
        }
    }
}
