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
        htm = htm + "<th></th>";
        htm = htm + "</tr>";
        htm = htm + "</thead>";
        htm = htm + "<tbody>";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter Type = new SqlParameter("@SPID", PaymentType);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("Order_Detail_BY_SPID", Con, Type, Branch);
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
            htm = htm + "<td>" + dt.Rows[i]["QtyOut"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["UnitPrice"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["RMBUnitPrice"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["AccountsTitle"].ToString() + "</td>";
            htm = htm + "<td><a href='#' onclick='Delete(" + dt.Rows[i]["ID"].ToString() + ");'>Delete</a></td>";
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
        string query = "SELECT        TOP (100) PERCENT 'Order ID : ' + dbo.SP_MASTER.SPID + '       ----------^^^^^^^^^----------       Date : ' + CONVERT(nvarchar(20), dbo.SP_MASTER.SPDate, 106)                          + '       ----------^^^^^^^^^----------       Customer : ' + dbo.Accounts.AccountsTitle AS Title, dbo.SP_MASTER.SPID FROM            dbo.SP_MASTER INNER JOIN                          dbo.Accounts ON dbo.SP_MASTER.AccountID = dbo.Accounts.AccountsID WHERE        (dbo.SP_MASTER.ISDELETE = 0) AND (dbo.SP_MASTER.SPID LIKE '%SO-%' OR dbo.SP_MASTER.SPID LIKE '%CS-%') AND (dbo.SP_MASTER.BranchID = '" + BranchID + "')  AND (dbo.Accounts.BranchID = '" + BranchID + "') ORDER BY dbo.SP_MASTER.SPID DESC";        
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

    //public void LoadOrder()
    //{ 
    //    SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
    //    SqlDataAdapter da = new SqlDataAdapter("select 'Order ID : '+ SPID + '       ----------^^^^^^^^^----------       Date : '+convert(nvarchar(20),SPDate,106) AS Title,SPID  from SP_MASTER where IsDelete=0 and SPID like '%SO-%' order by SPID Desc", Con);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlType.DataSource = dt;
    //        ddlType.DataTextField = "Title";
    //        ddlType.DataValueField = "SPID";
    //        ddlType.DataBind();
            
            

    //    }
    //}

    [WebMethod]
    public static string DeleteRegion(string ID, string BranchID)
    {

        string retMessage = string.Empty;
        int a = 0;
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("SELECT        dbo.SP_DETAIL.*, dbo.SP_MASTER.AccountID, dbo.SP_MASTER.CarryID, dbo.SP_MASTER.ExChargesID, dbo.SP_MASTER.PackingID  FROM            dbo.SP_DETAIL INNER JOIN                          dbo.SP_MASTER ON dbo.SP_DETAIL.SPID = dbo.SP_MASTER.SPID   where dbo.SP_DETAIL.ID = '" + ID + "' and  dbo.SP_DETAIL.BranchID='" + BranchID + "' and  dbo.SP_MASTER.BranchID='" + BranchID + "'", Conn);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            string SaleRevenuAccount = "";
            string SaleDiscountAccount = "";
            SqlDataAdapter daPurchase = new SqlDataAdapter("select AccountsID from Accounts where BranchID='" + BranchID + "' and AccountsTitle='Sales Revenue'", Conn);
            DataTable dtPurchase = new DataTable();
            daPurchase.Fill(dtPurchase);
            if (dtPurchase.Rows.Count > 0)
            {
                SaleRevenuAccount = dtPurchase.Rows[0][0].ToString();
            }

            SqlDataAdapter daDiscount = new SqlDataAdapter("select AccountsID from Accounts where BranchID='" + BranchID + "' and AccountsTitle='Sales Discount'", Conn);
            DataTable dtDiscount = new DataTable();
            daDiscount.Fill(dtDiscount);
            if (dtDiscount.Rows.Count > 0)
            {
                SaleDiscountAccount = dtDiscount.Rows[0][0].ToString();
            }
            double Totalprice = Convert.ToDouble(dt.Rows[0]["TotalPrice"].ToString());
            double RMBTotalprice = Convert.ToDouble(dt.Rows[0]["RMBTotalprice"].ToString());

            SqlCommand cmddelete = new SqlCommand("delete from SP_DETAIL where ID = '" + ID + "' and BranchID='" + BranchID + "'", Conn);
            Conn.Open();
            a = cmddelete.ExecuteNonQuery();
            Conn.Close();

            SqlCommand updspdetail = new SqlCommand("update  SP_DETAIL set QtyAvailed=QtyAvailed- '" + dt.Rows[0]["QtyOut"].ToString() + "' where ID = '" + dt.Rows[0]["BatchID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
            Conn.Open();
            a = updspdetail.ExecuteNonQuery();
            Conn.Close();

            SqlCommand cmdUpdateSP = new SqlCommand("update SP_MASTER set TotalAmount = TotalAmount - " + Totalprice + ",GrandTotal = GrandTotal - " + Totalprice + ",RMBAmount = RMBAmount - " + RMBTotalprice + " where SPID='" + dt.Rows[0]["SPID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
            Conn.Open();
            a = cmdUpdateSP.ExecuteNonQuery();
            Conn.Close();

            double Qty = Convert.ToDouble(dt.Rows[0]["QtyOut"].ToString());
            double Value = Convert.ToDouble(dt.Rows[0]["RMBValue"].ToString());

            SqlCommand cmdUpdateSPSupplier = new SqlCommand("update SP_MASTER set CusAmountPKR = CusAmountPKR - " + Totalprice + ",CusAmountRMB = CusAmountRMB - " + RMBTotalprice + " where SPID='" + dt.Rows[0]["SPID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
            Conn.Open();
            a = cmdUpdateSPSupplier.ExecuteNonQuery();
            Conn.Close();

            SqlCommand cmdUpdateAccSupplier = new SqlCommand("update tbl_transaction set AmountPKR = AmountPKR - " + Totalprice + ",AmountRMB = AmountRMB - " + RMBTotalprice + " where TaskID='" + dt.Rows[0]["SPID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
            Conn.Open();
            a = cmdUpdateAccSupplier.ExecuteNonQuery();
            Conn.Close();



            SqlCommand cmdUpdateAccCarryTDetailSUpplier = new SqlCommand("update Transaction_Detail set CreditPKR = CreditPKR - " + Totalprice + ",CreditRMB = CreditRMB - " + RMBTotalprice + " where TaskID='" + dt.Rows[0]["SPID"].ToString() + "' and CreditPKR > 0 and AccountID='"+SaleRevenuAccount+"' and BranchID='" + BranchID + "'", Conn);
            Conn.Open();
            a = cmdUpdateAccCarryTDetailSUpplier.ExecuteNonQuery();
            Conn.Close();


            SqlCommand cmdUpdateAccCarryTDetailDebitSUpplier = new SqlCommand("update Transaction_Detail set DebitPKR = DebitPKR - " + Totalprice + ",DebitRMB = DebitRMB - " + RMBTotalprice + " where TaskID='" + dt.Rows[0]["SPID"].ToString() + "' and DebitPKR > 0 and AccountID='" + dt.Rows[0]["AccountID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
            Conn.Open();
            a = cmdUpdateAccCarryTDetailDebitSUpplier.ExecuteNonQuery();
            Conn.Close();

        }

        if (a == 1)
        {
            retMessage = "true";
        }
        else
        {
            retMessage = "false";
        }

        return retMessage;

    }



}
