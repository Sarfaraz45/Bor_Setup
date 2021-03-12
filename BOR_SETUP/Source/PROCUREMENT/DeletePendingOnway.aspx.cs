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
     //   LoadOrder();
    }

    [WebMethod]
    public static string LOAD_ITEMS(string UserID, string BranchID)
    {

        string acc = "";
        //string htmUNT = "";
        string query = "SELECT        TOP (100) PERCENT 'Order ID : ' + dbo.SP_MASTER_ON_WAY.SPID + '       ----------^^^^^^^^^----------       Date : ' + CONVERT(nvarchar(20), dbo.SP_MASTER_ON_WAY.SPDate, 106)                          + '       ----------^^^^^^^^^----------       Supplier : ' + dbo.Accounts.AccountsTitle AS Title, dbo.SP_MASTER_ON_WAY.SPID FROM            dbo.SP_MASTER_ON_WAY INNER JOIN                          dbo.Accounts ON dbo.SP_MASTER_ON_WAY.AccountID = dbo.Accounts.AccountsID WHERE        (dbo.SP_MASTER_ON_WAY.ISDELETE = 0) AND (dbo.SP_MASTER_ON_WAY.SPID LIKE '%PR-O-%') AND (dbo.SP_MASTER_ON_WAY.IsReceived = 0)  AND (dbo.SP_MASTER_ON_WAY.BranchID = '" + BranchID + "')  AND (dbo.Accounts.BranchID = '" + BranchID + "') ORDER BY dbo.SP_MASTER_ON_WAY.SPID DESC";
        //string query = "SELECT  ITEMID, ITEMID+ ' ^ ' +ITEMName as ITEMName, ItemCode,UnitTypeID FROM ITM_ITEM WHERE ([IsDelete] = '0')";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(query, Con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            string itmID = dt.Rows[i - 1]["SPID"].ToString();
            string UntTyp = dt.Rows[i - 1]["SPID"].ToString();
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
        //acc = acc + "`" + LoadNUMBER("PR", "SP_MASTER", "SPID", Con) + "`" + htmUNT;
        return acc;
    }

    [WebMethod]
    public static string LoadLIST(string UserID, string PaymentType, string BranchID)
    {
        string htm = "";
        htm = htm + "<table id='data-table' class='table table-striped table-bordered' >";
        htm = htm + "<thead><tr>";
        htm = htm + "<th>Order ID</th>";
        htm = htm + "<th>Date</th>";
        htm = htm + "<th>Amount PKR</th>";
        htm = htm + "<th>Amount RMB</th>";
        htm = htm + "<th>ITEM</th>";
        htm = htm + "<th>Quantity</th>";
        htm = htm + "<th>Unit Price</th>";
        htm = htm + "<th>RMB Unit Price</th>";
        htm = htm + "<th>Supplier</th>";        
        htm = htm + "</tr>";
        htm = htm + "</thead>";
        htm = htm + "<tbody>";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter Type = new SqlParameter("@SPID", PaymentType);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("Order_Detail_BY_SPID_ONWAY", Con, Type, Branch);
        DataTable dt = new DataTable();
        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];        
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            htm = htm + "<tr >";
            htm = htm + "<td>" + dt.Rows[i]["SPID"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["Date"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["TotalPrice"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["RMBTotalPrice"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["ITEMName"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["QtyIn"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["UnitPrice"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["RMBUnitPrice"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["AccountsTitle"].ToString() + "</td>";            
            htm = htm + "</tr>";
        }
        htm = htm + "</tbody>";
        htm = htm + "</table>";
        htm = htm + "<div class='col-lg-12'><input type='button' id='btnSave' style='font-weight:bold; font-size:20px;' class='btn btn-danger btn-block' value='D E L E T E &nbsp;&nbsp;&nbsp; O N  &nbsp;&nbsp;&nbsp; W A Y &nbsp;&nbsp;&nbsp; O R D E R' onclick='DeleteTransaction(\"" + PaymentType + "\");'></div>";
        return htm;
    }



 

    [WebMethod]
    public static string DeleteMaster(string ID, string BranchID)
    {

        string retMessage = string.Empty;
        int a = 0;
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        SqlCommand cmddelete = new SqlCommand("delete from SP_DETAIL_ON_WAY where SPID = '" + ID + "' and BranchID='"+BranchID+"'", Conn);
        Conn.Open();
        a = cmddelete.ExecuteNonQuery();
        Conn.Close();

        SqlCommand cmddeleteMaster = new SqlCommand("delete from SP_MASTER_ON_WAY where SPID = '" + ID + "' and BranchID='" + BranchID + "'", Conn);
        Conn.Open();
        a = cmddeleteMaster.ExecuteNonQuery();
        Conn.Close();

        SqlCommand cmddeleteTransactionMaster = new SqlCommand("delete from tbl_transaction where TaskID = '" + ID + "' and BranchID='" + BranchID + "'", Conn);
        Conn.Open();
        a = cmddeleteTransactionMaster.ExecuteNonQuery();
        Conn.Close();

        SqlCommand cmddeleteTransactionDetail = new SqlCommand("delete from Transaction_Detail where TaskID = '" + ID + "' and BranchID='" + BranchID + "'", Conn);
        Conn.Open();
        a = cmddeleteTransactionDetail.ExecuteNonQuery();
        Conn.Close();
        retMessage = "true";


        return retMessage;

    }
}
