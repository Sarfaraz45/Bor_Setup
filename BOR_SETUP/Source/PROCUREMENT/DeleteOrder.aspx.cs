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
        string query = "SELECT        TOP (100) PERCENT 'Order ID : ' + dbo.SP_MASTER.SPID + '       ----------^^^^^^^^^----------       Date : ' + CONVERT(nvarchar(20), dbo.SP_MASTER.SPDate, 106)                          + '       ----------^^^^^^^^^----------       Supplier : ' + dbo.Accounts.AccountsTitle AS Title, dbo.SP_MASTER.SPID FROM            dbo.SP_MASTER INNER JOIN                          dbo.Accounts ON dbo.SP_MASTER.AccountID = dbo.Accounts.AccountsID WHERE        (dbo.SP_MASTER.ISDELETE = 0) AND (dbo.SP_MASTER.SPID LIKE '%PR-%')  AND (dbo.SP_MASTER.BranchID = '" + BranchID + "')  AND (dbo.Accounts.BranchID = '" + BranchID + "') ORDER BY dbo.SP_MASTER.SPID DESC";
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
            htm = htm + "<td>" + dt.Rows[i]["QtyIn"].ToString() + "</td>";
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


    //public void LoadOrder()
    //{ 
    //    SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
    //    SqlDataAdapter da = new SqlDataAdapter("select 'Order ID : '+ SPID + '       ----------^^^^^^^^^----------       Date : '+convert(nvarchar(20),SPDate,106) AS Title,SPID  from SP_MASTER where IsDelete=0 and SPID like '%PR-%' order by SPID Desc", Con);
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
        if (dt.Rows.Count>0)
        {

            string PurchaseAccount = "";
            SqlDataAdapter daPurchase = new SqlDataAdapter("select AccountsID from Accounts where BranchID='" + BranchID + "' and AccountsTitle='Purchases'", Conn);
            DataTable dtPurchase = new DataTable();
            daPurchase.Fill(dtPurchase);
            if (dtPurchase.Rows.Count > 0)
            {
                PurchaseAccount = dtPurchase.Rows[0][0].ToString();
            }


            double Totalprice = Convert.ToDouble(dt.Rows[0]["TotalPrice"].ToString());
            double RMBTotalprice = Convert.ToDouble(dt.Rows[0]["RMBTotalprice"].ToString());

            SqlCommand cmddelete = new SqlCommand("delete from SP_DETAIL where ID = '" + ID + "' and BranchID='" + BranchID + "'", Conn);
            Conn.Open();
            a = cmddelete.ExecuteNonQuery();
            Conn.Close();

            SqlCommand cmdUpdateSP = new SqlCommand("update SP_MASTER set TotalAmount = TotalAmount - " + Totalprice + ",GrandTotal = GrandTotal - " + Totalprice + ",RMBAmount = RMBAmount - " + RMBTotalprice + " where SPID='" + dt.Rows[0]["SPID"].ToString() + "' and BranchID='"+BranchID+"'", Conn);
            Conn.Open();
            a = cmdUpdateSP.ExecuteNonQuery();
            Conn.Close();

            if (Convert.ToDouble(dt.Rows[0]["Carry"].ToString()) > 0)
            {
                double QtyIn = Convert.ToDouble(dt.Rows[0]["QtyIn"].ToString());
                double rmbValue = Convert.ToDouble(dt.Rows[0]["RMBValue"].ToString());
                double CarryPKR = Convert.ToDouble(dt.Rows[0]["Carry"].ToString());
                CarryPKR = CarryPKR * QtyIn;
                double CarryRMB = CarryPKR / rmbValue;

                SqlCommand cmdUpdateSPCarry = new SqlCommand("update SP_MASTER set CarryAmountPKR = CarryAmountPKR - " + CarryPKR + ",CarryAmountRMB = CarryAmountRMB - " + CarryRMB + " where SPID='" + dt.Rows[0]["SPID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
                Conn.Open();
                a = cmdUpdateSPCarry.ExecuteNonQuery();
                Conn.Close();

                SqlCommand cmdUpdateAccCarry = new SqlCommand("update tbl_transaction set AmountPKR = AmountPKR - " + CarryPKR + ",AmountRMB = AmountRMB - " + CarryRMB + " where TaskID='" + dt.Rows[0]["SPID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
                Conn.Open();
                a = cmdUpdateAccCarry.ExecuteNonQuery();
                Conn.Close();

                SqlCommand cmdUpdateAccCarryTDetail = new SqlCommand("update Transaction_Detail set CreditPKR = CreditPKR - " + CarryPKR + ",CreditRMB = CreditRMB - " + CarryRMB + " where TaskID='" + dt.Rows[0]["SPID"].ToString() + "' and CreditPKR > 0 and AccountID='" + dt.Rows[0]["CarryID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
                Conn.Open();
                a = cmdUpdateAccCarryTDetail.ExecuteNonQuery();
                Conn.Close();


                SqlCommand cmdUpdateAccCarryTDetailDebit = new SqlCommand("update Transaction_Detail set DebitPKR = DebitPKR - " + CarryPKR + ",DebitRMB = DebitRMB - " + CarryRMB + " where TaskID='" + dt.Rows[0]["SPID"].ToString() + "' and DebitPKR > 0 and AccountID='" + PurchaseAccount + "' and BranchID='" + BranchID + "'", Conn);
                Conn.Open();
                a = cmdUpdateAccCarryTDetailDebit.ExecuteNonQuery();
                Conn.Close();
            }


            if (Convert.ToDouble(dt.Rows[0]["ExtraCharges"].ToString()) > 0)
            {
                double QtyIn = Convert.ToDouble(dt.Rows[0]["QtyIn"].ToString());
                double rmbValue = Convert.ToDouble(dt.Rows[0]["RMBValue"].ToString());
                double CarryPKR = Convert.ToDouble(dt.Rows[0]["ExtraCharges"].ToString());
                CarryPKR = CarryPKR * QtyIn;
                double CarryRMB = CarryPKR / rmbValue;

                SqlCommand cmdUpdateSPCarry = new SqlCommand("update SP_MASTER set ExChargeAmountPKR = ExChargeAmountPKR - " + CarryPKR + ",ExChatgeAmountRMB = ExChatgeAmountRMB - " + CarryRMB + " where SPID='" + dt.Rows[0]["SPID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
                Conn.Open();
                a = cmdUpdateSPCarry.ExecuteNonQuery();
                Conn.Close();

                SqlCommand cmdUpdateAccCarry = new SqlCommand("update tbl_transaction set AmountPKR = AmountPKR - " + CarryPKR + ",AmountRMB = AmountRMB - " + CarryRMB + " where TaskID='" + dt.Rows[0]["SPID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
                Conn.Open();
                a = cmdUpdateAccCarry.ExecuteNonQuery();
                Conn.Close();


                SqlCommand cmdUpdateAccCarryTDetail = new SqlCommand("update Transaction_Detail set CreditPKR = CreditPKR - " + CarryPKR + ",CreditRMB = CreditRMB - " + CarryRMB + " where TaskID='" + dt.Rows[0]["SPID"].ToString() + "' and CreditPKR > 0 and AccountID='" + dt.Rows[0]["ExChargesID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
                Conn.Open();
                a = cmdUpdateAccCarryTDetail.ExecuteNonQuery();
                Conn.Close();


                SqlCommand cmdUpdateAccCarryTDetailDebit = new SqlCommand("update Transaction_Detail set DebitPKR = DebitPKR - " + CarryPKR + ",DebitRMB = DebitRMB - " + CarryRMB + " where TaskID='" + dt.Rows[0]["SPID"].ToString() + "' and DebitPKR > 0 and AccountID='" + PurchaseAccount + "' and BranchID='" + BranchID + "'", Conn);
                Conn.Open();
                a = cmdUpdateAccCarryTDetailDebit.ExecuteNonQuery();
                Conn.Close();
            }

            if (Convert.ToDouble(dt.Rows[0]["PKRPackingPrice"].ToString()) > 0)
            {
                double QtyIn = Convert.ToDouble(dt.Rows[0]["QtyIn"].ToString());
                double rmbValue = Convert.ToDouble(dt.Rows[0]["RMBPackingRate"].ToString());
                double CarryPKR = Convert.ToDouble(dt.Rows[0]["PKRPackingPrice"].ToString());
                CarryPKR = CarryPKR * QtyIn;
                double CarryRMB = CarryPKR / rmbValue;

                SqlCommand cmdUpdateSPCarry = new SqlCommand("update SP_MASTER set PackingAmountPKR = PackingAmountPKR - " + CarryPKR + ",PackingAmountRMB = PackingAmountRMB - " + CarryRMB + " where SPID='" + dt.Rows[0]["SPID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
                Conn.Open();
                a = cmdUpdateSPCarry.ExecuteNonQuery();
                Conn.Close();

                SqlCommand cmdUpdateAccCarry = new SqlCommand("update tbl_transaction set AmountPKR = AmountPKR - " + CarryPKR + ",AmountRMB = AmountRMB - " + CarryRMB + " where TaskID='" + dt.Rows[0]["SPID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
                Conn.Open();
                a = cmdUpdateAccCarry.ExecuteNonQuery();
                Conn.Close();


                SqlCommand cmdUpdateAccCarryTDetail = new SqlCommand("update Transaction_Detail set CreditPKR = CreditPKR - " + CarryPKR + ",CreditRMB = CreditRMB - " + CarryRMB + " where TaskID='" + dt.Rows[0]["SPID"].ToString() + "' and CreditPKR > 0 and AccountID='" + dt.Rows[0]["PackingID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
                Conn.Open();
                a = cmdUpdateAccCarryTDetail.ExecuteNonQuery();
                Conn.Close();


                SqlCommand cmdUpdateAccCarryTDetailDebit = new SqlCommand("update Transaction_Detail set DebitPKR = DebitPKR - " + CarryPKR + ",DebitRMB = DebitRMB - " + CarryRMB + " where TaskID='" + dt.Rows[0]["SPID"].ToString() + "' and DebitPKR > 0 and AccountID='" + PurchaseAccount + "' and BranchID='" + BranchID + "'", Conn);
                Conn.Open();
                a = cmdUpdateAccCarryTDetailDebit.ExecuteNonQuery();
                Conn.Close();
            }

            double Qty = Convert.ToDouble(dt.Rows[0]["QtyIn"].ToString());
            double Value = Convert.ToDouble(dt.Rows[0]["RMBValue"].ToString());
            double CarrPKR = Convert.ToDouble(dt.Rows[0]["Carry"].ToString());
            CarrPKR = CarrPKR * Qty;
            double CarrRMB =0;
            if (CarrPKR > 0 )
            {
                CarrRMB = CarrPKR / Value;
            }
            

            double ExPKR = Convert.ToDouble(dt.Rows[0]["ExtraCharges"].ToString());
            ExPKR = ExPKR * Qty;
            double ExRMB =0;
            if (ExPKR > 0 )
            {
                 ExRMB = ExPKR / Value;
            }
            double rmbpackingRate = Convert.ToDouble(dt.Rows[0]["RMBPackingRate"].ToString());
            double PackingPKR = Convert.ToDouble(dt.Rows[0]["PKRPackingPrice"].ToString());
            PackingPKR = PackingPKR * Qty;
            double PackingRMB =0;
            if (PackingPKR > 0 )
            {
                PackingRMB = PackingPKR / rmbpackingRate;

            }

            double SupplierPKR = Convert.ToDouble(dt.Rows[0]["TotalPrice"].ToString()) - CarrPKR - ExPKR - PackingPKR;
            double SupplierRMB = Convert.ToDouble(dt.Rows[0]["RMBTotalprice"].ToString()) - CarrRMB - ExRMB - PackingRMB;

            SqlCommand cmdUpdateSPSupplier = new SqlCommand("update SP_MASTER set SupplierAmountPKR = SupplierAmountPKR - " + SupplierPKR + ",SupplierAmountRMB = SupplierAmountRMB - " + SupplierRMB + " where SPID='" + dt.Rows[0]["SPID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
            Conn.Open();
            a = cmdUpdateSPSupplier.ExecuteNonQuery();
            Conn.Close();

            SqlCommand cmdUpdateAccSupplier = new SqlCommand("update tbl_transaction set AmountPKR = AmountPKR - " + SupplierPKR + ",AmountRMB = AmountRMB - " + SupplierRMB + " where TaskID='" + dt.Rows[0]["SPID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
            Conn.Open();
            a = cmdUpdateAccSupplier.ExecuteNonQuery();
            Conn.Close();



            SqlCommand cmdUpdateAccCarryTDetailSUpplier = new SqlCommand("update Transaction_Detail set CreditPKR = CreditPKR - " + SupplierPKR + ",CreditRMB = CreditRMB - " + SupplierRMB + " where TaskID='" + dt.Rows[0]["SPID"].ToString() + "' and CreditPKR > 0 and AccountID='" + dt.Rows[0]["AccountID"].ToString() + "' and BranchID='" + BranchID + "'", Conn);
            Conn.Open();
            a = cmdUpdateAccCarryTDetailSUpplier.ExecuteNonQuery();
            Conn.Close();


            SqlCommand cmdUpdateAccCarryTDetailDebitSUpplier = new SqlCommand("update Transaction_Detail set DebitPKR = DebitPKR - " + SupplierPKR + ",DebitRMB = DebitRMB - " + SupplierRMB + " where TaskID='" + dt.Rows[0]["SPID"].ToString() + "' and DebitPKR > 0 and AccountID='" + PurchaseAccount + "' and BranchID='" + BranchID + "'", Conn);
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
