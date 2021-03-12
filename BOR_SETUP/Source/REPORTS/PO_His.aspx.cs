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
        string rqID = Request.QueryString["ID"].ToString();
        string LType = Request.QueryString["Type"].ToString();
        string BID = Request.QueryString["BID"].ToString();
        if (LType == "Detail")
        {
            string rptName = "PO_His.rpt";
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
            SqlParameter ReqID = new SqlParameter("@SPID", rqID);
            SqlParameter Branch = new SqlParameter("@BranchID", BID);
            DataSet ds = AACommon.ReturnDatasetBySPForREPORT("PurchaseOrderPrint", "vwPurchseOrderPRINT", Con, ReqID, Branch);

            foreach (DataRow dr in ds.Tables["vwPurchseOrderPRINT"].Rows)
            {
                        SqlDataAdapter cd = new SqlDataAdapter("select AccountsTitle from Accounts where AccountsID='" + dr["CarryID"].ToString() + "'", Con);
                        DataTable dd = new DataTable();
                        cd.Fill(dd);
                        if (dd.Rows.Count > 0)
                        {
                            dr["CarryTitle"] = dd.Rows[0]["AccountsTitle"].ToString();
                        }
            }

            foreach (DataRow dr in ds.Tables["vwPurchseOrderPRINT"].Rows)
            {
                SqlDataAdapter cd = new SqlDataAdapter("select AccountsTitle from Accounts where AccountsID='" + dr["ExChargesID"].ToString() + "'", Con);
                DataTable dd = new DataTable();
                cd.Fill(dd);
                if (dd.Rows.Count > 0)
                {
                    dr["ExTitle"] = dd.Rows[0]["AccountsTitle"].ToString();
                }
            }

            foreach (DataRow dr in ds.Tables["vwPurchseOrderPRINT"].Rows)
            {
                SqlDataAdapter cd = new SqlDataAdapter("select AccountsTitle from Accounts where AccountsID='" + dr["PackingID"].ToString() + "'", Con);
                DataTable dd = new DataTable();
                cd.Fill(dd);
                if (dd.Rows.Count > 0)
                {
                    dr["PackingTitle"] = dd.Rows[0]["AccountsTitle"].ToString();
                }
            }

            ds.Tables["vwPurchseOrderPRINT"].AcceptChanges();
            Session["RptDS"] = ds;
            Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=vwPurchseOrderPRINT");
        }
        else if (LType == "Local")
        {
            string rptName = "PO_His_Local.rpt";
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
            SqlParameter ReqID = new SqlParameter("@SPID", rqID);
            SqlParameter Branch = new SqlParameter("@BranchID", BID);
            DataSet ds = AACommon.ReturnDatasetBySPForREPORT("PurchaseOrderPrint", "vwPurchseOrderPRINT", Con, ReqID, Branch);
            Session["RptDS"] = ds;
            Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=vwPurchseOrderPRINT");
        }
    }
}