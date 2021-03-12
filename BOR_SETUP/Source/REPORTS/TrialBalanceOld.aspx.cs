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
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void LoadReport(object sender, EventArgs e)
    {

        
        //string LType = Request.QueryString["LType"].ToString();
      
            string rptName = "TrialBalance.rpt";
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select SUM(DebitPKR) AS DebitPKR,SUM(CreditPKR) AS CreditPKR,AccountsID,AccountsTitle,ControlAccID,ControlAccTitle,AccountTypeID,AccountTypeTitle from VW_TRIAL_BALANCE where Date between '" + StartDate.Value + "' and '" + EndDate.Value + "' group by AccountsID,AccountsTitle,ControlAccID,ControlAccTitle,AccountTypeID,AccountTypeTitle", Con);
            DataSet ds = new DataSet();
            da.Fill(ds, "VW_TRIAL_BALANCE");

            Session["RptDS"] = ds;
            Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_TRIAL_BALANCE");
        
    }
}