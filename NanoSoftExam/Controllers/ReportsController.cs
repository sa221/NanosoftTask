using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections.Generic;
using NanoSoftExam.Data;
using System.IO;
using System.Text;

namespace NanoSoftExam.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        private StudentContext db=new StudentContext();
        public ActionResult StudentList()
        {
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "StudentList", fileName = "Student List Report" });
        }
        public ActionResult StudentAttendenceList()
        {
            //DateTime Date = Convert.ToDateTime(Request["Date"]);
            DateTime Date;
            if (Request["AttendanceDate"] != null)
            {
                Date = DateTime.ParseExact(Request["AttendanceDate"], "dd-MM-yyyy", null);
            }
            else
            {
                Date = DateTime.Now;
            }
            string selectedFormula;
           
                selectedFormula = "{Attendences.Date}=Date (" + Date.ToString("yyyy,MM,dd") +")";
           
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "AttendenceReport", fileName = "Student Attendence List Report" ,selectedFormula});
        }
        #region Common
        public ActionResult PrintReport(string sourceName, string fileName, string selectedFormula)
        {
            if (String.IsNullOrEmpty(sourceName) && String.IsNullOrEmpty(fileName))
            {
                sourceName = "test";
                fileName = "test";
            }
            else if (String.IsNullOrEmpty(fileName))
            {
                fileName = sourceName;
            }
            ReportDocument rd = GetReport(sourceName);
            SetResponceProperty();
            if (!String.IsNullOrEmpty(selectedFormula))
            {
                rd = SetFolmula(rd, selectedFormula);
            }
            return ExportReport(rd, fileName);
        }

        private ReportDocument SetFolmula(ReportDocument rd, string selectedFormula)
        {
            rd.RecordSelectionFormula = selectedFormula;
            return rd;
        }
        private ReportDocument GetReport(string reportName)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), reportName + ".rpt"));
            //rd.SetDatabaseLogon(user, password, dataSource, databaseName);
            return rd;
        }

        private void SetResponceProperty()
        {
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
        }


        private ActionResult ExportReport(ReportDocument rd, string reportName)
        {
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", reportName + ".pdf");
            }
            catch (Exception exception)
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(exception.Message);
                //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
                MemoryStream stream = new MemoryStream(byteArray);
                return File(stream, "application/pdf", "Error.pdf");
            }
        }

        #endregion
    }
}