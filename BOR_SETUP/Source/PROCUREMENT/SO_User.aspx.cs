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

public partial class PROCUREMENT_PO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod]
    public static string LoadRMbValue(string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("Select RMBValue  from CurrencyConversion where IsActive=1 and BranchID='" + BranchID + "'", Conn);
        DataSet ds = new DataSet();
        da.Fill(ds);
        List<GetRegionClasss> RegionList = new List<GetRegionClasss>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClasss dbdc = new GetRegionClasss();

                dbdc.UnitTypeID = ds.Tables[0].Rows[i][0].ToString();             
                RegionList.Insert(i, dbdc);
            }

        }

        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);
    }


    public class GetRegionClasss
    {
        public string UnitTypeID { get; set; }
        public string UnitTypeDesc { get; set; }



    }


    [WebMethod]
    public static string LoadNUMBER(string frmt, string tbl, string col, SqlConnection Con, string BranchID)
    {
        string vNO = "";
        string vDate = "";
        string mm1 = System.DateTime.Now.Month.ToString();
        string yy1 = System.DateTime.Now.Year.ToString();
        //SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataSet dsDATE = AACommon.ReturnDatasetBySP("Get_CurrentDate", Con, null);
        if (dsDATE.Tables[0].Rows.Count > 0)
        {
            mm1 = dsDATE.Tables[0].Rows[0]["MM"].ToString();
            yy1 = dsDATE.Tables[0].Rows[0]["YY"].ToString();
            vDate = mm1 + "/" + dsDATE.Tables[0].Rows[0]["DD"].ToString() + "/" + yy1;
        }
        dsDATE.Dispose();
        yy1 = Convert.ToString(Convert.ToInt32(yy1) - 2000);

        if (mm1.Length == 1) { mm1 = "0" + mm1; }
        if (yy1.Length == 1) { yy1 = "0" + yy1; }
        string format1 = frmt + "-" + BranchID + "-" + mm1 + "-" + yy1 + "-";

        vNO = AACommon.GetAlphaNumericIDSIX(tbl, format1, col, Con);
        vNO = vNO + "`" + vDate;
        return vNO;
    }


    
    [WebMethod]
    public static string LOAD_ITEMS(string UserID, string BranchID)
    {

        string acc = ""; string htmUNT = "";
        string query = "select * from VW_ITEM_FOR_SALE_USER where BranchID='" + BranchID + "' and  BranchID1='" + BranchID + "' and  BranchID2='" + BranchID + "' and BranchID3='" + BranchID + "'";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(query, Con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            string itmID = "";
            string UntTyp = "";
            if (dt.Rows.Count == 1)
            {
                itmID = dt.Rows[i - 1]["ITEMID"].ToString();
                UntTyp = dt.Rows[i - 1]["UnitTypeID"].ToString();
                if (i == 1)
                { acc = acc + "['" + dt.Rows[i - 1]["ITEMName"].ToString() + "']"; }

            }
            else
            {

                itmID = dt.Rows[i - 1]["ITEMID"].ToString();
                UntTyp = dt.Rows[i - 1]["UnitTypeID"].ToString();
                if (i == 1)
                { acc = acc + "['" + dt.Rows[i - 1]["ITEMName"].ToString() + "'"; }
                else if (i != 1 && i < dt.Rows.Count)
                { acc = acc + ",'" + dt.Rows[i - 1]["ITEMName"].ToString() + "'"; }
                else
                { acc = acc + ",'" + dt.Rows[i - 1]["ITEMName"].ToString() + "']"; }

            }

            htmUNT = htmUNT + LoadUNITS(itmID, UntTyp, BranchID);
        }
        acc = acc + "`" + LoadNUMBER("SO", "SP_MASTER", "SPID", Con, BranchID) + "`" + htmUNT;
        return acc;
    }

    [WebMethod]
    public static string LoadUNITS(string itmID, string UntTyp, string BranchID)
    {
        string vUNT = "";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter UnitTypeID = new SqlParameter("@UnitTypeID", UntTyp);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("ITM_UNITS_GET", Con, UnitTypeID, Branch);
        vUNT = "<span id='spnUNT" + itmID + "' style='display:none;'>";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++ )
        {
            if (i == 0)
            {
                vUNT = vUNT + ds.Tables[0].Rows[i]["UnitID"].ToString() + "^" + ds.Tables[0].Rows[i]["DisplayName"].ToString();
            }
            else
            {
                vUNT = vUNT + "~" + ds.Tables[0].Rows[i]["UnitID"].ToString() + "^" + ds.Tables[0].Rows[i]["DisplayName"].ToString();
            }
            
        }
        ds.Dispose();
        vUNT = vUNT + "</span>";
        return vUNT;
    }

    [WebMethod]
    public static string SaveTransaction(string UserID, string str, string txtVDate, string txtVoucherNo, string txtTotAmount, string txtDiscount, string txtTaxRate, string txtTotTax, string txtGrandTotal, string ddlSupplier, string txtTotAmountRMB, string RMBValueHDN, string txtDiscountPKR, string txtDiscountRMB, string txtCusAmountPKR, string txtCusAmountRMB, string ddlShop, string txtCustomerName, string txtContactNo, string txtAddress, string txtBillNo, string BranchID)
    {
        string msg = "";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        string rqID = "";
        string rqHID = "";
        string rqDt = "";
        string rqBy = UserID;
        string rqAt = "";
        string stsID = "";// 0:Pending, 1:Approved, 2:Rejected


        ///////////////////////// ID GNERATOR /////////////////////////////
        string aa = LoadNUMBER("SO", "SP_MASTER", "SPID", Con, BranchID);
        string[] idDT = aa.Split('`');
        rqID = idDT[0].ToString();
        rqDt = idDT[1].ToString();

       // rqHID = LoadHistoryID(rqID);

        
       

        /////////////////////////// INSERT in SP MASTER //////////////////
        SqlParameter SPID = new SqlParameter("@SPID", rqID);
        SqlParameter SPDate = new SqlParameter("@SPDate", txtVDate);
        SqlParameter TotalAmount = new SqlParameter("@TotalAmount",txtTotAmount);
        SqlParameter DiscountAmount = new SqlParameter("@DiscountAmount", txtDiscount);
        SqlParameter TaxAmount = new SqlParameter("@TaxAmount", txtTotTax);
        SqlParameter TaxRate = new SqlParameter("@TaxRate", txtTaxRate);
        SqlParameter GrandTotal = new SqlParameter("@GrandTotal", txtGrandTotal);
        SqlParameter RMBAmount = new SqlParameter("@RMBAmount", txtTotAmountRMB);
        SqlParameter AccountID = new SqlParameter("@AccountID", ddlSupplier);
        SqlParameter SP = new SqlParameter("@SP", "S");
        SqlParameter CarryAmountPKR = new SqlParameter("@CarryAmountPKR", "0");
        SqlParameter CarryAmountRMB = new SqlParameter("@CarryAmountRMB", "0");
        SqlParameter PackingAmountPKR = new SqlParameter("@PackingAmountPKR", "0");
        SqlParameter PackingAmountRMB = new SqlParameter("@PackingAmountRMB", "0");
        SqlParameter ExChargeAmountPKR = new SqlParameter("@ExChargeAmountPKR", "0");
        SqlParameter ExChatgeAmountRMB = new SqlParameter("@ExChatgeAmountRMB", "0");
        SqlParameter SupplierAmountPKR = new SqlParameter("@SupplierAmountPKR", "0");
        SqlParameter SupplierAmountRMB = new SqlParameter("@SupplierAmountRMB", "0");

        SqlParameter SalesDiscountPKR = new SqlParameter("@SalesDiscountPKR", txtDiscountPKR);
        SqlParameter SalesDiscountRMB = new SqlParameter("@SalesDiscountRMB", txtDiscountRMB);
        SqlParameter CusAmountPKR = new SqlParameter("@CusAmountPKR", txtCusAmountPKR);
        SqlParameter CusAmountRMB = new SqlParameter("@CusAmountRMB", txtCusAmountRMB);

        SqlParameter CarryID = new SqlParameter("@CarryID", "0");
        SqlParameter ExChargesID = new SqlParameter("@ExChargesID", "0");
        SqlParameter PackingID = new SqlParameter("@PackingID", "0");
        SqlParameter ShopName = new SqlParameter("@ShopName", ddlShop);
        SqlParameter CustomerName = new SqlParameter("@CustomerName", txtCustomerName);
        SqlParameter CreateBy = new SqlParameter("@CreateBy", UserID);

        SqlParameter CustomerContactNo = new SqlParameter("@CustomerContactNo", txtContactNo);
        SqlParameter CustomerAddress = new SqlParameter("@CustomerAddress", txtAddress);
        SqlParameter LocalBillNo = new SqlParameter("@LocalBillNo", txtBillNo);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        AACommon.Execute("SP_MASTER_INSERT", Con, SPID, SPDate, TotalAmount, DiscountAmount, TaxAmount, TaxRate, GrandTotal, RMBAmount, AccountID, SP, CarryAmountPKR, CarryAmountRMB, PackingAmountPKR, PackingAmountRMB, ExChargeAmountPKR, ExChatgeAmountRMB, SupplierAmountPKR, SupplierAmountRMB, SalesDiscountPKR, SalesDiscountRMB, CusAmountPKR, CusAmountRMB, CarryID, ExChargesID, PackingID, ShopName, CustomerName, CreateBy, CustomerContactNo, CustomerAddress, LocalBillNo, Branch);


        /////////////////////// INSERT in SP DETAIL

        string[] itmLST = str.Split('`');
        for (int i = 0; i < itmLST.Length; i++)
        {
            string[] itmROW = itmLST[i].Split('^');
            //txtAccID + "^" + txtAccTitle + "^" + ddlUnit + "^" + txtQty + "^" + txtPrice + "^" + txtTotalPrice
            string itmID = itmROW[0].ToString();
            string ddlUnit = itmROW[2].ToString();
            string txtQty = itmROW[3].ToString();
            string txtPrice = itmROW[4].ToString();
            string txtTotalPrice = itmROW[5].ToString();

            string txtUnitPriceRMB = itmROW[6].ToString();
            string txtTotalPriceRMB = itmROW[7].ToString();
            string txtRMBValue = itmROW[8].ToString();
            string PRID = itmROW[9].ToString();
            string PRDID = itmROW[10].ToString();
            string txtSrNo = itmROW[11].ToString();
            SqlParameter SPID_1 = new SqlParameter("@SPID", rqID);
            SqlParameter ITEMID = new SqlParameter("@ITEMID", itmID);
            SqlParameter UnitID = new SqlParameter("@UnitID", ddlUnit);
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", txtPrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", txtTotalPrice);
            SqlParameter RMBValue = new SqlParameter("@RMBValue", txtRMBValue);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", txtUnitPriceRMB);
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", txtTotalPriceRMB);
            SqlParameter Type = new SqlParameter("@Type", "S");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", txtPrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", txtUnitPriceRMB);
            SqlParameter SRNO = new SqlParameter("@SrNo", txtSrNo);
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchID);
            AACommon.Execute("SP_DETAIL_INSERT_2", Con, SPID_1, ITEMID, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, BasicUnitPricePKR, BasicUnitPriceRMB, SRNO, CreateByNew, BranchP);

            PRID = PRID.Replace(" Purchase # : ", "");
            PRDID = PRDID.Replace(" ID : ", "");
            SqlDataAdapter da = new SqlDataAdapter("select * from  SP_DETAIL where SPID='" + PRID + "' and ITEMID='" + itmID + "' and ID='"+PRDID+"'", Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                double AvailedQty = Convert.ToDouble(dt.Rows[0]["QtyAvailed"].ToString());
                double Qty = Convert.ToDouble(txtQty);
                AvailedQty = AvailedQty + Qty;
                SqlCommand cmd = new SqlCommand("update SP_DETAIL set QtyAvailed='" + AvailedQty + "' where SPID='" + PRID + "' and ITEMID='" + itmID + "' and ID='" + PRDID + "'", Con);
                Con.Open();
                cmd.ExecuteNonQuery();
                Con.Close();

                SqlDataAdapter da1 = new SqlDataAdapter("select MAX(ID) from  SP_DETAIL where SPID='" + rqID + "' and ITEMID='" + itmID + "'", Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("update SP_DETAIL set BatchID='" + PRDID + "' where ID='" + dt1.Rows[0][0].ToString() + "'", Con);
                    Con.Open();
                    cmd1.ExecuteNonQuery();
                    Con.Close();
                }
            }

           
        }

        ////////////////////SAVE TRANSACTION IN ACCOUNTS ////////////////////

        string Narration = "Invoice By SO # " + rqID + "";
        SqlCommand cmd2 = new SqlCommand("insert into tbl_transaction (Date,Narration,AmountPKR,AmountRMB,RMBValue,TaskID,CreateBy,BranchID) values ('" + txtVDate + "','" + Narration + "','" + txtTotAmount + "','" + txtTotAmountRMB + "','" + RMBValueHDN + "','" + rqID + "','" + UserID + "','" + BranchID + "')", Con);
        Con.Open();
        cmd2.ExecuteNonQuery();
        Con.Close();

        string SaleRevenuAccount = "";
        string SaleDiscountAccount = "";
        SqlDataAdapter daPurchase = new SqlDataAdapter("select AccountsID from Accounts where BranchID='" + BranchID + "' and AccountsTitle='Sales Revenue'", Con);
        DataTable dtPurchase = new DataTable();
        daPurchase.Fill(dtPurchase);
        if (dtPurchase.Rows.Count > 0)
        {
            SaleRevenuAccount = dtPurchase.Rows[0][0].ToString();
        }


        SqlDataAdapter daDiscount = new SqlDataAdapter("select AccountsID from Accounts where BranchID='" + BranchID + "' and AccountsTitle='Sales Discount'", Con);
        DataTable dtDiscount = new DataTable();
        daDiscount.Fill(dtDiscount);
        if (dtDiscount.Rows.Count > 0)
        {
            SaleDiscountAccount = dtDiscount.Rows[0][0].ToString();
        }

        SqlCommand cmdPurchase1 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + ddlSupplier + "','SALE','" + txtCusAmountPKR + "','" + txtCusAmountRMB + "','0','0','" + RMBValueHDN + "','" + rqID + "','" + Narration + "','" + UserID + "','" + BranchID + "')", Con);
        Con.Open();
        cmdPurchase1.ExecuteNonQuery();
        Con.Close();

        if (Convert.ToDouble(txtDiscountPKR) > 0)
        {
            Narration = "Sales Discount By SO # " + rqID + "";
            SqlCommand cmdPurchase3 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + SaleDiscountAccount + "','SALE','" + txtDiscountPKR + "','" + txtDiscountRMB + "','0','0','" + RMBValueHDN + "','" + rqID + "','" + Narration + "','" + UserID + "','" + BranchID + "')", Con);
            Con.Open();
            cmdPurchase3.ExecuteNonQuery();
            Con.Close();
        }

        Narration = "Invoice By SO # " + rqID + "";
        SqlCommand cmdPurchase2 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + SaleRevenuAccount + "','SALE','0','0','" + txtTotAmount + "','" + txtTotAmountRMB + "','" + RMBValueHDN + "','" + rqID + "','" + Narration + "','" + UserID + "','" + BranchID + "')", Con);
        Con.Open();
        cmdPurchase2.ExecuteNonQuery();
        Con.Close();
        ///PURCHASE ENTRY


        return msg + rqID;
    }


}