using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RdlcReports
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string FormatTimeSpan(long timeTicks)
        {
            return Math.Abs((TimeSpan.FromTicks(timeTicks).Days * 24 + TimeSpan.FromTicks(timeTicks).Hours)).ToString("d2") +
                ":" +
                Math.Abs(TimeSpan.FromTicks(timeTicks).Minutes).ToString("d2");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var dataset = new DataSet();
            var dataTable = new SharedDataSet.AttendanceDataTable();
            var row = dataTable.NewAttendanceRow();
            row.Date = DateTime.Today;
            row.CheckIn = new TimeSpan(10, 04, 00);
            row.CheckOut = new TimeSpan(18, 36, 00);
            row.IsHalfDay = true;
            row.HoursRequired = new TimeSpan(09, 00, 00);
            row.HoursWorked = (row.CheckOut - row.CheckIn);
            row.HoursDiff = row.HoursWorked - row.HoursRequired;

            var row1 = dataTable.NewAttendanceRow();
            row1.Date = DateTime.Today;
            row1.CheckIn = default(TimeSpan);
            row1.CheckOut = new TimeSpan(17, 13, 00);
            row1.HoursWorked = default(TimeSpan);
            row1.HoursDiff = default(TimeSpan);
            row1.HoursRequired = new TimeSpan(09, 00, 00);
            row1.IsHoliday = true;
            var row2 = dataTable.NewAttendanceRow();
            row2.Date = DateTime.Today;
            row2.CheckIn = new TimeSpan(09, 00, 00);
            row2.CheckOut = new TimeSpan(16, 00, 00);
            row2.IsLate = true;
            row2.HoursRequired = new TimeSpan(09, 00, 00);
            row2.HoursWorked = (row2.CheckOut - row2.CheckIn);
            row2.HoursDiff = row2.HoursWorked - row2.HoursRequired;

            var row3 = dataTable.NewAttendanceRow();
            row3.Date = DateTime.Today;
            row3.CheckIn = new TimeSpan(09, 00, 00);
            row3.CheckOut = new TimeSpan(16, 00, 00);
            //row3.IsLate = true;
            row3.HoursRequired = new TimeSpan(09, 00, 00);
            row3.HoursWorked = (row3.CheckOut - row3.CheckIn);
            row3.HoursDiff = row3.HoursWorked - row3.HoursRequired;


            dataTable.Rows.Add(row);
            dataTable.Rows.Add(row1);
            dataTable.Rows.Add(row2);
            dataTable.Rows.Add(row3);
            dataset.Tables.Add(dataTable);

            var summaryTable = new SharedDataSet.SummaryDataTable();
            var summaryRow = summaryTable.NewSummaryRow();
            summaryRow.FirstName = "Abdul Salam";
            summaryRow.PMName = "Nadir Shah";
            summaryRow.HrApproverName = "Bilal Sadiq";
            summaryTable.Rows.Add(summaryRow);
            dataset.Tables.Add(summaryTable);
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report.rdlc");

            ReportDataSource datasource = new ReportDataSource("Attendance", dataset.Tables[0]);
            ReportDataSource summarySource = new ReportDataSource("Summary", dataset.Tables[1]);
            ReportViewer1.LocalReport.ReportEmbeddedResource = "RdlcReports.AttendanceReport.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.DataSources.Add(summarySource);
            ReportViewer1.DataBind();
        }
    }
}