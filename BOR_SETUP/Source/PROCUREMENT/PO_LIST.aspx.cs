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
    public static string LoadLIST(string UserID,string BranchID)
    {
        string htm = "";
        htm = htm + "<table id='data-table' class='table table-striped' >";
        htm = htm + "<thead><tr>";
        htm = htm + "<th>PO ID</th>";
        htm = htm + "<th>PO Date</th>";
        htm = htm + "<th>Supplier</th>";
        htm = htm + "<th>Amount</th>";
        htm = htm + "<th>Local Bill #</th>";
        htm = htm + "<th></th>";
        htm = htm + "<th></th>";
        htm = htm + "<th></th>";
        htm = htm + "</tr>";
        htm = htm + "</thead>";
        htm = htm + "<tbody>";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("PO_LIST", Con, Branch);
        DataTable dt = new DataTable();
        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];        
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            htm = htm + "<tr >";
            htm = htm + "<td>" + dt.Rows[i]["SPID"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["PODate"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["AccountsTitle"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["TotalAmount"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["LocalBillNo"].ToString() + "</td>";
            htm = htm + "<td><a href='../REPORTS/PO_His.aspx?ID=" + dt.Rows[i]["SPID"].ToString() + "&Type=Detail&BID=" + BranchID + "' target='_blank'>Print Detail</a></td>";
            htm = htm + "<td><a href='../REPORTS/PO_His.aspx?ID=" + dt.Rows[i]["SPID"].ToString() + "&Type=Local&BID=" + BranchID + "' target='_blank'>Print Local</a></td>";
            htm = htm + "<td><a href='../PROCUREMENT/PO_Local_Edit.aspx?ID=" + dt.Rows[i]["SPID"].ToString() + "' target='_blank'>Edit</a></td>";
            htm = htm + "</tr>";
            
        }
        htm = htm + "</tbody>";
        htm = htm + "</table>";
        return htm;
    }

    [WebMethod]
    public static string LoadLISTSearch(string UserID, string PaymentType, string BranchID)
    {
        string htm = "";
        htm = htm + "<table id='data-table' class='table table-striped' >";
        htm = htm + "<thead><tr>";
        htm = htm + "<th>PO ID</th>";
        htm = htm + "<th>PO Date</th>";
        htm = htm + "<th>Supplier</th>";
        htm = htm + "<th>Amount</th>";
        htm = htm + "<th>Local Bill #</th>";
        htm = htm + "<th></th>";
        htm = htm + "<th></th>";
        htm = htm + "<th></th>";
        htm = htm + "</tr>";
        htm = htm + "</thead>";
        htm = htm + "<tbody>";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter SPID = new SqlParameter("@SPID", PaymentType);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("PO_LIST_Search", Con, SPID,Branch);
        DataTable dt = new DataTable();
        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            htm = htm + "<tr >";
            htm = htm + "<td>" + dt.Rows[i]["SPID"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["PODate"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["AccountsTitle"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["TotalAmount"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["LocalBillNo"].ToString() + "</td>";
            htm = htm + "<td><a href='../REPORTS/PO_His.aspx?ID=" + dt.Rows[i]["SPID"].ToString() + "&Type=Detail&BID=" + BranchID + "' target='_blank'>Print Detail</a></td>";
            htm = htm + "<td><a href='../REPORTS/PO_His.aspx?ID=" + dt.Rows[i]["SPID"].ToString() + "&Type=Local&BID=" + BranchID + "' target='_blank'>Print Local</a></td>";
            htm = htm + "<td><a href='../PROCUREMENT/PO_Local_Edit.aspx?ID=" + dt.Rows[i]["SPID"].ToString() + "' target='_blank'>Edit</a></td>";
            htm = htm + "</tr>";
        }
        htm = htm + "</tbody>";
        htm = htm + "</table>";
        return htm;
    }

    [WebMethod]
    public static string LOAD_ITEMS(string UserID, string BranchID)
    {

        string acc = "";
        string query = "SELECT        TOP (100) PERCENT 'Voucher ID : ' + dbo.SP_MASTER.SPID + '       ----------^^^^^^^^^----------       Date : ' + CONVERT(nvarchar(20), dbo.SP_MASTER.SPDate, 106) + '       ----------^^^^^^^^^----------       Amount : ' + CONVERT(Nvarchar(20), dbo.SP_MASTER.TotalAmount)  + '       ----------^^^^^^^^^----------       Supplier : ' + dbo.Accounts.AccountsTitle AS Title, dbo.SP_MASTER.SPID AS TaskID FROM            dbo.SP_MASTER INNER JOIN dbo.Accounts ON dbo.SP_MASTER.AccountID = dbo.Accounts.AccountsID WHERE        (dbo.SP_MASTER.ISDELETE = 0) AND (dbo.SP_MASTER.SP = 'P')  and dbo.Accounts.BranchID='" + BranchID + "' and dbo.SP_MASTER.BranchID='" + BranchID + "' ORDER BY TaskID DESC";        
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(query, Con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            string itmID = dt.Rows[i - 1]["TaskID"].ToString();
            string UntTyp = dt.Rows[i - 1]["TaskID"].ToString();            
            if (dt.Rows.Count == 1)
            { acc = acc + "['" + dt.Rows[i - 1]["Title"].ToString() + "']"; }
            else
            {
                if (i == 1)
                { acc = acc + "['" + dt.Rows[i - 1]["Title"].ToString() + "'"; }
                else if (i != 1 && i < dt.Rows.Count)
                { acc = acc + ",'" + dt.Rows[i - 1]["Title"].ToString() + "'"; }
                else
                { acc = acc + ",'" + dt.Rows[i - 1]["Title"].ToString() + "']"; }
            }
            
        }
        return acc;
    }
}