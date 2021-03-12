using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class REPORTS_Viewer : System.Web.UI.Page
{
    
    int count;
    string par;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    void Page_Init(object sender, EventArgs e)
    {
        count = Convert.ToInt32(Request.QueryString["no"]);


        if (Request.QueryString["no"] == "0")
        {
            CrystalReportSource1.Report.FileName = Server.MapPath("~") + "\\Reports\\" + Request.QueryString["name"];
            CrystalReportSource1.ReportDocument.SetDataSource((DataSet)Session["RptDS"]);
            CrystalReportViewer1.ReportSource = CrystalReportSource1;            
           // CrystalReportViewer1.ParameterFieldInfo=Session["dd"];
            //////CrystalDecisions.CrystalReports.Engine.ReportDocument rptDoc =
            //////           new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            //////rptDoc.Load(Server.MapPath("~") + "\\Reports\\" + Request.QueryString["name"]);

            //////rptDoc.PrintToPrinter(1, false, 0, 0);
        }
        else
        {
            CrystalReportSource1.Report.FileName = Server.MapPath("~") + "\\Reports\\" + Request.QueryString["name"];
            CrystalReportSource1.ReportDocument.SetDataSource((DataSet)Session["RptDS"]);
            CrystalReportViewer1.ReportSource = CrystalReportSource1;

            for (int i = 0; i < count; i++)
            {
                int p = i + 1;
                par = "p" + p;
                CrystalReportSource1.ReportDocument.SetParameterValue(i, Request.QueryString[par]);

            }
            CrystalReportViewer1.ReportSource = CrystalReportSource1;
        }

    }
    void Page_LoadComplete(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}