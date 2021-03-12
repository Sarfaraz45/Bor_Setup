using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Sql;
using System.Web.Script.Serialization;
using System.IO;


public partial class PROCUREMENT_PO_LIST : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string LoadLISTSearch(string UserID, string PaymentType, string StartDate, string EndDate, string BranchID)
    {
        string htm = "";
        htm = htm + "<table id='data-table' class='table table-striped' >";
        htm = htm + "<thead><tr>";
        htm = htm + "<th>Voucher ID</th>";
        htm = htm + "<th>Voucher Date</th>";
        htm = htm + "<th>Amount</th>";
        htm = htm + "<th></th>";
        htm = htm + "</tr>";
        htm = htm + "</thead>";
        htm = htm + "<tbody>";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter Type = new SqlParameter("@Type", PaymentType);
        SqlParameter DateStart = new SqlParameter("@StartDate", StartDate);
        SqlParameter DateEnd = new SqlParameter("@EndDate", EndDate);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("Voucher_LIST_Search_By_Date", Con, Type, DateStart, DateEnd,Branch);
        DataTable dt = new DataTable();
        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            htm = htm + "<tr >";
            htm = htm + "<td>" + dt.Rows[i]["TaskID"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["Date"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["AmountPKR"].ToString() + "</td>";
            htm = htm + "<td><a href='../REPORTS/Voucher_His.aspx?ID=" + dt.Rows[i]["TaskID"].ToString() + "&BID="+BranchID+"' target='_blank'>Print</a></td>";
            htm = htm + "</tr>";
        }
        htm = htm + "</tbody>";
        htm = htm + "</table>";
        return htm;
    }

    [WebMethod]
    public static string LoadLIST(string UserID, string PaymentType, string BranchID)
    {
        string htm = "";
        htm = htm + "<table id='data-table' class='table table-striped' >";
        htm = htm + "<thead><tr>";
        htm = htm + "<th>Voucher ID</th>";
        htm = htm + "<th>Voucher Date</th>";
        htm = htm + "<th>Amount</th>";
        htm = htm + "<th></th>";
        htm = htm + "</tr>";
        htm = htm + "</thead>";
        htm = htm + "<tbody>";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter Type = new SqlParameter("@Type", PaymentType);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("Voucher_LIST", Con, Type, Branch);
        DataTable dt = new DataTable();
        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];        
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            htm = htm + "<tr >";
            htm = htm + "<td>" + dt.Rows[i]["TaskID"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["Date"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["AmountPKR"].ToString() + "</td>";
            htm = htm + "<td><a href='../REPORTS/Voucher_His.aspx?ID=" + dt.Rows[i]["TaskID"].ToString() + "&BID=" + BranchID + "' target='_blank'>Print</a></td>";
            htm = htm + "</tr>";
        }
        htm = htm + "</tbody>";
        htm = htm + "</table>";
        return htm;
    }
}