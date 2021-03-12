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

public partial class REPORTS_Req_His : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // string rqID = Request.QueryString["ID"].ToString();
        string rptName = "Ledger.rpt";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        DataSet ds = AACommon.ReturnDatasetBySPForREPORT("LedgerReport", "VW_LEDGER", Con, null);
        Session["RptDS"] = ds;
        Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_LEDGER");
    }
}