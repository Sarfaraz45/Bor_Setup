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
    public static string LoadLISTSearchDate(string UserID, string StartDate, string EndDate,string BranchID)
    {
        string htm = "";
        htm = htm + "<table id='data-table' class='table table-striped' >";
        htm = htm + "<thead><tr>";
        htm = htm + "<th>SO ID</th>";
        htm = htm + "<th>SO Date</th>";
        htm = htm + "<th>Customer</th>";
        htm = htm + "<th>Local Bill #</th>";
        htm = htm + "<th>Contact</th>";
        htm = htm + "<th></th>";
        htm = htm + "<th></th>";
        htm = htm + "</tr>";
        htm = htm + "</thead>";
        htm = htm + "<tbody>";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter StartDate_P = new SqlParameter("@StartDate", StartDate);
        SqlParameter EndDate_P = new SqlParameter("@EndDate", EndDate);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("SO_LIST_SEARCH_BY_DATE", Con, StartDate_P, EndDate_P, Branch);
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
            htm = htm + "<td>" + dt.Rows[i]["LocalBillNo"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["CustomerContactNo"].ToString() + "</td>";
            htm = htm + "<td><a href='../REPORTS/SO_His.aspx?ID=" + dt.Rows[i]["SPID"].ToString() + "&BID=" + BranchID + "' target='_blank'>Print</a></td>";
            string abc = dt.Rows[i]["SPID"].ToString();
            abc = abc.Substring(0, 2);
            if (abc == "SO")
            {
                htm = htm + "<td><a href='../PROCUREMENT/SO_Edit.aspx?ID=" + dt.Rows[i]["SPID"].ToString() + "' target='_blank'>Edit</a></td>";
            }
            else if (abc == "CS")
            {
                htm = htm + "<td><a href='../PROCUREMENT/SO_CASH_Edit.aspx?ID=" + dt.Rows[i]["SPID"].ToString() + "' target='_blank'>Edit</a></td>";
            } 
            htm = htm + "</tr>";
        }
        htm = htm + "</tbody>";
        htm = htm + "</table>";
        return htm;
    }

    [WebMethod]
    public static string LoadLIST(string UserID, string BranchID)
    {
        string htm = "";
        htm = htm + "<table id='data-table' class='table table-striped' >";
        htm = htm + "<thead><tr>";
        htm = htm + "<th>SO ID</th>";
        htm = htm + "<th>SO Date</th>";
        htm = htm + "<th>Customer</th>";
        htm = htm + "<th>Local Bill #</th>";
        htm = htm + "<th>Contact</th>";
        htm = htm + "<th></th>";
        htm = htm + "<th></th>";
        htm = htm + "</tr>";
        htm = htm + "</thead>";
        htm = htm + "<tbody>";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("SO_LIST", Con, Branch);
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
            htm = htm + "<td>" + dt.Rows[i]["LocalBillNo"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["CustomerContactNo"].ToString() + "</td>";
            htm = htm + "<td><a href='../REPORTS/SO_His.aspx?ID=" + dt.Rows[i]["SPID"].ToString() + "&BID=" + BranchID + "' target='_blank'>Print</a></td>";
            string abc = dt.Rows[i]["SPID"].ToString();
            abc = abc.Substring(0, 2);
            if (abc == "SO")
            {
                htm = htm + "<td><a href='../PROCUREMENT/SO_Edit.aspx?ID=" + dt.Rows[i]["SPID"].ToString() + "' target='_blank'>Edit</a></td>";
            }
            else if (abc == "CS")
            {
                htm = htm + "<td><a href='../PROCUREMENT/SO_CASH_Edit.aspx?ID=" + dt.Rows[i]["SPID"].ToString() + "' target='_blank'>Edit</a></td>";
            } 
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
        htm = htm + "<th>SO ID</th>";
        htm = htm + "<th>SO Date</th>";
        htm = htm + "<th>Customer</th>";
        htm = htm + "<th>Local Bill #</th>";
        htm = htm + "<th>Contact</th>";
        htm = htm + "<th></th>";
        htm = htm + "<th></th>";
        htm = htm + "</tr>";
        htm = htm + "</thead>";
        htm = htm + "<tbody>";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter SPID = new SqlParameter("@SPID", PaymentType);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("SO_LIST_Search", Con, SPID,Branch);
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
            htm = htm + "<td>" + dt.Rows[i]["LocalBillNo"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["CustomerContactNo"].ToString() + "</td>";
            htm = htm + "<td><a href='../REPORTS/SO_His.aspx?ID=" + dt.Rows[i]["SPID"].ToString() + "&BID=" + BranchID + "' target='_blank'>Print</a></td>";
            string abc = dt.Rows[i]["SPID"].ToString();
            abc = abc.Substring(0,2);
            if (abc == "SO")
            {
                htm = htm + "<td><a href='../PROCUREMENT/SO_Edit.aspx?ID=" + dt.Rows[i]["SPID"].ToString() + "' target='_blank'>Edit</a></td>";
            }
            else if (abc == "CS")
            {
                htm = htm + "<td><a href='../PROCUREMENT/SO_CASH_Edit.aspx?ID=" + dt.Rows[i]["SPID"].ToString() + "' target='_blank'>Edit</a></td>";
            } 
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
        //string htmUNT = "";
        //string query = "SELECT        TOP (100) PERCENT 'Voucher ID : ' + dbo.SP_MASTER.SPID + '       ----------^^^^^^^^^----------       Date : ' + CONVERT(nvarchar(20), dbo.SP_MASTER.SPDate, 106) + '       ----------^^^^^^^^^----------       Amount : ' + CONVERT(Nvarchar(20), dbo.SP_MASTER.TotalAmount)  + '       ----------^^^^^^^^^----------       Customer : ' + dbo.Accounts.AccountsTitle AS Title, dbo.SP_MASTER.SPID AS TaskID FROM            dbo.SP_MASTER INNER JOIN dbo.Accounts ON dbo.SP_MASTER.AccountID = dbo.Accounts.AccountsID WHERE        (dbo.SP_MASTER.ISDELETE = 0) AND (dbo.SP_MASTER.SP = 'S') ORDER BY TaskID DESC";
        string query = "SELECT TOP(100)    'Voucher ID : ' + dbo.SP_MASTER.SPID + ' ^^^ ' + CONVERT(nvarchar(20), dbo.SP_MASTER.SPDate, 106) + ' ^^^ Amount : ' + CONVERT(Nvarchar(20), dbo.SP_MASTER.TotalAmount)  + ' ^^^ Customer : ' + dbo.Accounts.AccountsTitle  + ' ^^^ Contact # : ' +  ISNULL(dbo.SP_MASTER.CustomerContactNo,0) + ' ^^^ Local Bill # : ' +  dbo.SP_MASTER.LocalBillNo + ' ^^^ Customer Name : ' +  dbo.SP_MASTER.CustomerName AS Title, dbo.SP_MASTER.SPID AS TaskID FROM            dbo.SP_MASTER INNER JOIN dbo.Accounts ON dbo.SP_MASTER.AccountID = dbo.Accounts.AccountsID WHERE        (dbo.SP_MASTER.ISDELETE = 0) AND (dbo.SP_MASTER.SP = 'S')  and dbo.Accounts.BranchID='" + BranchID + "' and dbo.SP_MASTER.BranchID='" + BranchID + "' ORDER BY dbo.SP_MASTER.SPID DESC";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(query, Con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            string itmID = dt.Rows[i - 1]["TaskID"].ToString();
            string UntTyp = dt.Rows[i - 1]["TaskID"].ToString();
            //'["Karachi","Hyderabad","USA","Arkansas","California","Colorado","Connecticut","Delaware","Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas","Kentucky"]'
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
            //htmUNT = htmUNT + LoadUNITS(itmID, UntTyp);
        }
        //acc = acc + "`" + LoadNUMBER("PR", "SP_MASTER", "SPID", Con) + "`" + htmUNT;

        return acc;
    }
}