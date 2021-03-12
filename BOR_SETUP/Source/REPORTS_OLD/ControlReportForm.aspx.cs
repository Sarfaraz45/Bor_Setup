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

    }
    protected void LoadReport(object sender, EventArgs e)
    {
        string starttime = " 00:00:00.000";
        string endtime = " 23:59:00.000";
              string rptName = "ControlReport.rpt";
              var BranchID = "";
              if (Request.Cookies["BranchID"] != null)
              {
                  BranchID = Request.Cookies["BranchID"].Value;
              }
             SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
             SqlParameter AccountsID = new SqlParameter("@ControlAccID", ddlCategory.SelectedValue);
             SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
             SqlParameter DateFrom = new SqlParameter("@StartDate", StartDate.Value + starttime);
             SqlParameter DateEnd = new SqlParameter("@EndDate", EndDate.Value + endtime);
             DataSet ds = AACommon.ReturnDatasetBySPForREPORT("CONTROL_ACC_BALANCE", "VW_CONTROL_ACC_BALANCE", Con, AccountsID, Branch, DateFrom, DateEnd);

             string query = "";
             if (ds.Tables[0].Rows.Count > 0)
             {
                 query = "select * from Accounts where ControlAccID='" + ddlCategory.SelectedValue + "'";
                 SqlDataAdapter da = new SqlDataAdapter(query, Con);
                 DataTable dt = new DataTable();
                 da.Fill(dt);
                 if (dt.Rows.Count > 0)
                 {
                     for (int i = 0; i < dt.Rows.Count; i++)
                     {
                         string quersum = "select cast(ISNULL(sum(DebitPKR) - Sum(CreditPKR),0) as NUMERIC(15,2))  from Transaction_Detail where AccountID = '" + dt.Rows[i]["AccountsID"].ToString() + "' and Date < '" + StartDate.Value + "' and IsDelete=0";
                         SqlDataAdapter dda = new SqlDataAdapter(quersum, Con);
                         DataTable dtt = new DataTable();
                         dda.Fill(dtt);
                         if (dtt.Rows.Count > 0)
                         {
                             foreach (DataRow row in ds.Tables[0].Select("AccountsID = '" + dt.Rows[i]["AccountsID"].ToString() + "'"))
                             {
                                 string d = Convert.ToString(Convert.ToDecimal(row[9]));
                                 string ee = Convert.ToString(dtt.Rows[0][0].ToString());
                                 row[9] = Convert.ToString(Convert.ToDecimal(row[9]) + Convert.ToDecimal(dtt.Rows[0][0].ToString()));
                                 //row[9] = Convert.ToString(Convert.ToDecimal(row[9]));
                                 row[10] = Convert.ToString(StartDate.Value);
                                 row[11] = Convert.ToString(EndDate.Value);
                             }

                             ds.Tables[0].AcceptChanges();
                         }

                     }
                 }
             }
    
             Session["RptDS"] = ds;
             Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_CONTROL_ACC_BALANCE");
        

    }

}