using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

public partial class REPORTS_Ledger_Updated : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // string rqID = Request.QueryString["ID"].ToString();
        string rptName = "InComeStatement.rpt";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        //SqlParameter AccountsID = new SqlParameter("@AccountsID", ddlITEM.SelectedValue);
        DataSet ds = AACommon.ReturnDatasetBySPForREPORT("INCOME_STATEMENT", "VW_INCOME_STATEMENT_DETAIL", Con, null);
        Session["RptDS"] = ds;
        Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_INCOME_STATEMENT_DETAIL");
    }
    protected void LoadReport(object sender, EventArgs e)
    {

       
    }
}