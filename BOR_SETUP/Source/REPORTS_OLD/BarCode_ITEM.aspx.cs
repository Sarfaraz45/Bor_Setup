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
        var BranchID = "";
        BranchID = Request.Cookies["BranchID"].Value;
        string rptName = "BarCode_ITEM.rpt";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter BranchID_P = new SqlParameter("@BranchID", BranchID);
        SqlParameter ITEMID = new SqlParameter("@ITEMID", ddlITEM.SelectedValue);
        DataSet ds = AACommon.ReturnDatasetBySPForREPORT("BARCODE_ITEM_BRANCH_WISE_SINGLE", "VW_BARCODE_ITEM_BRANCH_WISE", Con, BranchID_P, ITEMID);
        Session["RptDS"] = ds;
        Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_BARCODE_ITEM_BRANCH_WISE");
    }

  
}