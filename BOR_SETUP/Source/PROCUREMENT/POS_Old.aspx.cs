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

public partial class PROCUREMENT_POS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod]
    public static string LoadSlip(string UserID, string BranchID)
    {


        string htm = "";
        string htmMaster = "";
        htmMaster = htmMaster + "<div style='text-align:center;border-bottom: 1px solid #d5d5d5;margin-bottom: 10px;'><img src='../AAUI/img/Logo.png' alt='POS'><p style='text-align:center;font-size: 16px;'><strong>AQM Solution</strong><br>B-707, Saima Trader Tower<br>I.I Chundrigar Road Karachi<br>012345678</p><p></p></div>";

        htm = htm + "<table id='data-table' class='table table-striped table-condensed' >";
        htm = htm + "<thead><tr>";
        htm = htm + "<th style='text-align:left; width: 50%; border-bottom: 2px solid #ddd;'>Description</th>";
        htm = htm + "<th style='text-align:left;width: 12%; border-bottom: 2px solid #ddd;'>Quantity</th>";
        htm = htm + "<th style='text-align:left;width: 24%; border-bottom: 2px solid #ddd;'>Price</th>";
        htm = htm + "<th style='text-align:left;width: 26%; border-bottom: 2px solid #ddd;'>Subtotal</th>";
        htm = htm + "</tr>";
        htm = htm + "</thead>";
        htm = htm + "<tbody>";
        string SPID = "";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da1 = new SqlDataAdapter("select top(1) SPID from SP_DETAIL where Description ='SALE BY POS' order by ID DESC", Con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            SPID = dt1.Rows[0][0].ToString();
        }
        SqlParameter SP = new SqlParameter("@SPID", SPID);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("SaleOrderPrint", Con, Branch, SP);
        DataTable dt = new DataTable();
        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            htm = htm + "<tr >";
            htm = htm + "<td style='text-align:left;'>" + dt.Rows[i]["ITEMName"].ToString() + "</td>";
            htm = htm + "<td style='text-align:left;'>" + dt.Rows[i]["Qty"].ToString() + "</td>";
            htm = htm + "<td style='text-align:left;'>" + dt.Rows[i]["UnitPrice"].ToString() + "</td>";
            htm = htm + "<td style='text-align:left;'>" + dt.Rows[i]["TotalPrice"].ToString() + "</td>";
            htm = htm + "</tr>";
        }

        htmMaster = htmMaster + "<p style='    font-weight: bold;'>Date: " + dt.Rows[0]["SPDate"].ToString() + " <br>Sale No/Ref: " + dt.Rows[0]["SPID"].ToString() + "<br>Customer: " + dt.Rows[0]["CustomerName"].ToString() + " <br>Sales Person: Admin Admin <br></p>";
        htm = htm + "</tbody>";
        htm = htm + "<tfoot><tr><th colspan='3' style='text-align:right;'>Total</th><th class='text-left'>" + dt.Rows[0]["TotalAmount"].ToString() + "</th></tr>";
        htm = htm + "</table>";
        htm = htm + "<table class='table table-striped table-condensed' style='margin-top:10px;'><tbody><tr><td class='text-right'>Paid by :</td><td>Cash</td><td class='text-right'>Amount :</td><td>" + dt.Rows[0]["TotalAmount"].ToString() + "</td><td class='text-right'>Change :</td><td>0</td></tr></tbody></table>";
        htm = htmMaster + htm;
        return htm;
    }


    [WebMethod]
    public static string SaveTransaction(string UserID, string str, string txtTotAmount, string txtDiscount, string txtTaxRate, string txtTotTax, string txtGrandTotal, string ddlSupplier, string txtDiscountPKR, string txtCusAmountPKR, string ddlShop, string txtCustomerName, string txtContactNo, string txtAddress, string txtBillNo, string BranchID)
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
        string aa = LoadNUMBER("CS", "SP_MASTER", "SPID", Con, BranchID);
        string[] idDT = aa.Split('`');
        rqID = idDT[0].ToString();
        rqDt = idDT[1].ToString();

        // rqHID = LoadHistoryID(rqID);

        string txtVDate = Convert.ToString(DateTime.Now.Date);


        /////////////////////////// INSERT in SP MASTER //////////////////
        SqlParameter SPID = new SqlParameter("@SPID", rqID);
        SqlParameter SPDate = new SqlParameter("@SPDate", DateTime.Now.Date);
        SqlParameter TotalAmount = new SqlParameter("@TotalAmount", txtTotAmount);
        SqlParameter DiscountAmount = new SqlParameter("@DiscountAmount", txtDiscount);
        SqlParameter TaxAmount = new SqlParameter("@TaxAmount", txtTotTax);
        SqlParameter TaxRate = new SqlParameter("@TaxRate", txtTaxRate);
        SqlParameter GrandTotal = new SqlParameter("@GrandTotal", txtGrandTotal);
        SqlParameter RMBAmount = new SqlParameter("@RMBAmount", txtGrandTotal);
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
        SqlParameter SalesDiscountRMB = new SqlParameter("@SalesDiscountRMB", txtDiscountPKR);
        SqlParameter CusAmountPKR = new SqlParameter("@CusAmountPKR", txtCusAmountPKR);
        SqlParameter CusAmountRMB = new SqlParameter("@CusAmountRMB", txtCusAmountPKR);

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
        SqlParameter CashRcvd = new SqlParameter("@CashRcvd", txtTotAmount);
        SqlParameter CashBalance = new SqlParameter("@CashBalance", "0");
        AACommon.Execute("SP_MASTER_INSERT", Con, SPID, SPDate, TotalAmount, DiscountAmount, TaxAmount, TaxRate, GrandTotal, RMBAmount, AccountID, SP, CarryAmountPKR, CarryAmountRMB, PackingAmountPKR, PackingAmountRMB, ExChargeAmountPKR, ExChatgeAmountRMB, SupplierAmountPKR, SupplierAmountRMB, SalesDiscountPKR, SalesDiscountRMB, CusAmountPKR, CusAmountRMB, CarryID, ExChargesID, PackingID, ShopName, CustomerName, CreateBy, CustomerContactNo, CustomerAddress, LocalBillNo, Branch, CashRcvd, CashBalance);


        /////////////////////// INSERT in SP DETAIL

        string[] itmLST = str.Split('`');
        int b = 0;
        for (int i = 0; i < itmLST.Length; i++)
        {
            b = b + 1;
            string[] itmROW = itmLST[i].Split('^');
            //txtAccID + "^" + txtAccTitle + "^" + ddlUnit + "^" + txtQty + "^" + txtPrice + "^" + txtTotalPrice
            string itmID = itmROW[0].ToString();
            string txtQty = itmROW[1].ToString();
            string txtPrice = itmROW[2].ToString();
            string txtTotalPrice = itmROW[3].ToString();
            string txtSrNo = Convert.ToString(b);

            SqlParameter SPID_1 = new SqlParameter("@SPID", rqID);
            SqlParameter ITEMID = new SqlParameter("@ITEMID", itmID);
            SqlParameter UnitID = new SqlParameter("@UnitID", "ITEM-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", txtPrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", txtTotalPrice);
            SqlParameter RMBValue = new SqlParameter("@RMBValue", txtTotalPrice);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", txtPrice);
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", txtTotalPrice);
            SqlParameter Type = new SqlParameter("@Type", "S");

            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", txtPrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", txtPrice);
            SqlParameter SRNO = new SqlParameter("@SrNo", txtSrNo);
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchID);
            SqlParameter DescriptionP = new SqlParameter("@Description", "SALE BY POS");

            AACommon.Execute("SP_DETAIL_INSERT_2", Con, SPID_1, ITEMID, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type,
                BasicUnitPricePKR, BasicUnitPriceRMB, SRNO, CreateByNew, BranchP, DescriptionP);

        }
        string RMBValueHDN = "0";
        //////////////    PRID = PRID.Replace(" Purchase # : ", "");
        //////////////    PRDID = PRDID.Replace(" ID : ", "");
        //////////////    SqlDataAdapter da = new SqlDataAdapter("select * from  SP_DETAIL where SPID='" + PRID + "' and ITEMID='" + itmID + "' and ID='" + PRDID + "'", Con);
        //////////////    DataTable dt = new DataTable();
        //////////////    da.Fill(dt);
        //////////////    if (dt.Rows.Count > 0)
        //////////////    {
        //////////////        double AvailedQty = Convert.ToDouble(dt.Rows[0]["QtyAvailed"].ToString());
        //////////////        double Qty = Convert.ToDouble(txtQty);
        //////////////        AvailedQty = AvailedQty + Qty;
        //////////////        SqlCommand cmd = new SqlCommand("update SP_DETAIL set QtyAvailed='" + AvailedQty + "' where SPID='" + PRID + "' and ITEMID='" + itmID + "' and ID='" + PRDID + "'", Con);
        //////////////        Con.Open();
        //////////////        cmd.ExecuteNonQuery();
        //////////////        Con.Close();

        //////////////        SqlDataAdapter da1 = new SqlDataAdapter("select MAX(ID) from  SP_DETAIL where SPID='" + rqID + "' and ITEMID='" + itmID + "'", Con);
        //////////////        DataTable dt1 = new DataTable();
        //////////////        da1.Fill(dt1);
        //////////////        if (dt1.Rows.Count > 0)
        //////////////        {
        //////////////            SqlCommand cmd1 = new SqlCommand("update SP_DETAIL set BatchID='" + PRDID + "' where ID='" + dt1.Rows[0][0].ToString() + "'", Con);
        //////////////            Con.Open();
        //////////////            cmd1.ExecuteNonQuery();
        //////////////            Con.Close();
        //////////////        }
        //////////////    }


        //////////////}

        ////////////////////SAVE TRANSACTION IN ACCOUNTS ////////////////////

        string FinalAmountPKR = "";
        string FinalAmountRMB = "";
        string txtCusAmountRMB = "";
        if (Convert.ToDouble(txtTotTax) > 0)
        {
            FinalAmountPKR = Convert.ToString(Convert.ToDouble(txtTotAmount) + Convert.ToDouble(txtTotTax));
            FinalAmountRMB = Convert.ToString(Convert.ToDouble(txtTotAmount) + Convert.ToDouble(txtTotTax));
        }
        string Narration = "Invoice By SO # " + rqID + "";
        SqlCommand cmd2 = new SqlCommand("insert into tbl_transaction (Date,Narration,AmountPKR,AmountRMB,RMBValue,TaskID,CreateBy,BranchID) values ('" + txtVDate + "','" + Narration + "','" + txtTotAmount + "','" + txtTotAmount + "','" + RMBValueHDN + "','" + rqID + "','" + UserID + "','" + BranchID + "')", Con);
        Con.Open();
        cmd2.ExecuteNonQuery();
        Con.Close();

        string SaleRevenuAccount = "";
        string SaleDiscountAccount = "";
        string SaleTaxAccount = "";
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

        SqlDataAdapter daTax = new SqlDataAdapter("select AccountsID from Accounts where BranchID='" + BranchID + "' and AccountsTitle='TAX Liability'", Con);
        DataTable dtTax = new DataTable();
        daTax.Fill(dtTax);
        if (dtTax.Rows.Count > 0)
        {
            SaleTaxAccount = dtTax.Rows[0][0].ToString();
        }


        if (Convert.ToDouble(txtTotTax) > 0)
        {
            txtCusAmountPKR = Convert.ToString(Convert.ToDouble(txtCusAmountPKR) + Convert.ToDouble(txtTotTax));
            txtCusAmountRMB = Convert.ToString(Convert.ToDouble(txtCusAmountRMB) + Convert.ToDouble(txtTotTax));
        }

        ///PURCHASE ENTRY
        SqlCommand cmdPurchase1 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + ddlSupplier + "','SALE','" + txtCusAmountPKR + "','" + txtCusAmountRMB + "','0','0','" + RMBValueHDN + "','" + rqID + "','" + Narration + "','" + UserID + "','" + BranchID + "')", Con);
        Con.Open();
        cmdPurchase1.ExecuteNonQuery();
        Con.Close();

        if (Convert.ToDouble(txtDiscountPKR) > 0)
        {
            Narration = "Sales Discount By SO # " + rqID + "";
            SqlCommand cmdPurchase3 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + SaleDiscountAccount + "','SALE','" + txtDiscountPKR + "','" + txtDiscountPKR + "','0','0','" + RMBValueHDN + "','" + rqID + "','" + Narration + "','" + UserID + "','" + BranchID + "')", Con);
            Con.Open();
            cmdPurchase3.ExecuteNonQuery();
            Con.Close();
        }

        if (Convert.ToDouble(txtTotTax) > 0)
        {
            Narration = "Sales Tax By SO # " + rqID + "";
            SqlCommand cmdPurchase3 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + SaleTaxAccount + "','SALE','0','0','" + txtTotTax + "','" + txtTotTax + "','" + RMBValueHDN + "','" + rqID + "','" + Narration + "','" + UserID + "','" + BranchID + "')", Con);
            Con.Open();
            cmdPurchase3.ExecuteNonQuery();
            Con.Close();
        }

        Narration = "Invoice By SO # " + rqID + "";
        SqlCommand cmdPurchase2 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + SaleRevenuAccount + "','SALE','0','0','" + txtTotAmount + "','" + txtDiscountPKR + "','" + RMBValueHDN + "','" + rqID + "','" + Narration + "','" + UserID + "','" + BranchID + "')", Con);
        Con.Open();
        cmdPurchase2.ExecuteNonQuery();
        Con.Close();
        ///PURCHASE ENTRY


        return msg;

    }
    //private static List<string> BreakingItemName(DataTable dt) { 
    //    List<string> Price = new List<string>();
    //    List<string> Qty  = new List<string>();
    //    List<string> itemName = new List<string>();
    //    string tempString;
    //    if(dt.Rows.Count > 0){
            
    //    }
    //    return (Price);
    //}



    [WebMethod]
    public static string LoadItem(string BranchID)
    {

        string acc = ""; string htmUNT = "";
        string query = "select * from VW_ITEM_FOR_SALE where BranchID='" + BranchID + "' and   BranchID2='" + BranchID + "' and BranchID3='" + BranchID + "' order by ITEMName";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(query, Con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        //BreakingItemName(dt);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            GetRegionClass dbdc = new GetRegionClass();

            dbdc.ITEMID = dt.Rows[i]["ITEMID"].ToString();
            dbdc.ITEMName = dt.Rows[i]["ITEMName"].ToString();
            dbdc.Price = dt.Rows[i]["SalePrice"].ToString();
            dbdc.Cost = dt.Rows[i]["PurchasePrice"].ToString();
            RegionList.Insert(i, dbdc);
        }
        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);
        //for (int i = 1; i <= dt.Rows.Count; i++)
        //{
        //    string itmID = "";
        //    string UntTyp = "";
        //    if (dt.Rows.Count == 1)
        //    {
        //        itmID = dt.Rows[i - 1]["ITEMID"].ToString();
        //        UntTyp = dt.Rows[i - 1]["UnitTypeID"].ToString();
        //        if (i == 1)
        //        { acc = acc + "['" + dt.Rows[i - 1]["ITEMName"].ToString() + "']"; }

        //    }
        //    else
        //    {

        //        itmID = dt.Rows[i - 1]["ITEMID"].ToString();
        //        UntTyp = dt.Rows[i - 1]["UnitTypeID"].ToString();
        //        if (i == 1)
        //        { acc = acc + "['" + dt.Rows[i - 1]["ITEMName"].ToString() + "'"; }
        //        else if (i != 1 && i < dt.Rows.Count)
        //        { acc = acc + ",'" + dt.Rows[i - 1]["ITEMName"].ToString() + "'"; }
        //        else


        //        { acc = acc + ",'" + dt.Rows[i - 1]["ITEMName"].ToString() + "']"; }

        //    }
        //    htmUNT = htmUNT + LoadUNITS(itmID, UntTyp, BranchID);
        //}
        //acc = acc + "`" + LoadNUMBER("CS", "SP_MASTER", "SPID", Con, BranchID) + "`" + htmUNT;
        //return acc;
    }



    [WebMethod]
    public static string LoadItemData(string UserID, string BranchID, string Code)
    {

        string acc = ""; string htmUNT = "";
        string query = "select * from VW_ITEM_FOR_SALE where BranchID='" + BranchID + "' and   BranchID2='" + BranchID + "' and BranchID3='" + BranchID + "' and itemcode='" + Code + "' order by ITEMName";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(query, Con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            GetRegionClass dbdc = new GetRegionClass();

            dbdc.ITEMID = dt.Rows[i]["ITEMID"].ToString();
            dbdc.ITEMName = dt.Rows[i]["ITEMName"].ToString();
            dbdc.Price = dt.Rows[i]["SalePrice"].ToString();
            dbdc.Cost = dt.Rows[i]["PurchasePrice"].ToString();
            RegionList.Insert(i, dbdc);
        }
        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);

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
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
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
        //string format1 = frmt + "-" + BranchID + "-" + mm1 + "-" + yy1 + "-";
        string format1 = frmt + "-" + BranchID + "-08-18-";

        vNO = AACommon.GetAlphaNumericIDSIX(tbl, format1, col, Con);
        vNO = vNO + "`" + vDate;
        return vNO;
    }

    [WebMethod]
    public static string InsertRegion(string ITEMName, string ItemCode, string BarCode, string Discription, string UserID, string UnitTypeID, string Category, string Brand, string BranchID, string PurchasePrice, string SalePrice)
    {

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        string ID = AACommon.GetAlphaNumericIDSIX("ITM_ITEM", "ITM-", "ITEMID", Conn);
        SqlParameter ITEMID_P = new SqlParameter("@ITEMID", ID);
        SqlParameter ITEMName_P = new SqlParameter("@ITEMName", ITEMName);
        SqlParameter ItemCode_P = new SqlParameter("@ItemCode", ItemCode);
        SqlParameter BarCode_P = new SqlParameter("@BarCode", BarCode);
        SqlParameter Discription_P = new SqlParameter("@Discription", Discription);
        SqlParameter CREATEBY = new SqlParameter("@CreateBy", UserID);
        SqlParameter IsSale_P = new SqlParameter("@IsSale", "1");
        SqlParameter IsPurchase_P = new SqlParameter("@IsPurchase", "0");
        SqlParameter UnitTypeID_P = new SqlParameter("@UnitTypeID", UnitTypeID);
        SqlParameter CatID = new SqlParameter("@CatID", Category);
        SqlParameter BrandID = new SqlParameter("@BrandID", Brand);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        SqlParameter PPrice = new SqlParameter("@PurchasePrice", PurchasePrice);
        SqlParameter SPrice = new SqlParameter("@SalePrice", SalePrice);


        //isale=1
        //ispurchase=0

        msg = AACommon.Execute("ITM_ITEM_INSERT", Conn, ITEMID_P, ITEMName_P, ItemCode_P, BarCode_P, Discription_P, CREATEBY, IsSale_P, IsPurchase_P, UnitTypeID_P, CatID, BrandID, Branch, PPrice, SPrice);


        if (msg == "Record Saved Successfully")
        {
            retMessage = "true";
        }
        else
        {
            retMessage = "false";
        }

        return retMessage;
    }


    [WebMethod]
    public static string UpdateRegion(string ITEMID, string ITEMName, string ItemCode, string BarCode, string Discription, string UnitTypeID, string Category, string Brand, string BranchID, string PurchasePrice, string SalePrice)
    {
        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter ITEMID_P = new SqlParameter("@ITEMID", ITEMID);
        SqlParameter ITEMName_P = new SqlParameter("@ITEMName", ITEMName);
        SqlParameter ItemCode_P = new SqlParameter("@ItemCode", ItemCode);
        SqlParameter BarCode_P = new SqlParameter("@BarCode", BarCode);
        SqlParameter Discription_P = new SqlParameter("@Discription", Discription);
        SqlParameter UnitTypeID_P = new SqlParameter("@UnitTypeID", UnitTypeID);
        SqlParameter CatID = new SqlParameter("@CatID", Category);
        SqlParameter BrandID = new SqlParameter("@BrandID", Brand);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        SqlParameter PPrice = new SqlParameter("@PurchasePrice", PurchasePrice);
        SqlParameter SPrice = new SqlParameter("@SalePrice", SalePrice);
        msg = AACommon.Execute("ITM_ITEM_UPDATE", Conn, ITEMID_P, ITEMName_P, ItemCode_P, BarCode_P, Discription_P, UnitTypeID_P, CatID, BrandID, Branch, PPrice, SPrice);


        if (msg == "Record Saved Successfully")
        {
            retMessage = "true";
        }
        else
        {
            retMessage = "false";
        }

        return retMessage;
    }


    [WebMethod]
    public static string DeleteRegion(string ITEMID, string UserID, string BranchID)
    {

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);


        SqlParameter ITEMID_P = new SqlParameter("@ITEMID", ITEMID);
        SqlParameter DeleteBy_P = new SqlParameter("@DeleteBy", UserID);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        msg = AACommon.Execute("ITM_ITEM_DELETE", Conn, ITEMID_P, DeleteBy_P, Branch);


        if (msg == "Record Saved Successfully")
        {
            retMessage = "true";
        }
        else
        {
            retMessage = "false";
        }

        return retMessage;

    }

    [WebMethod]
    public static string LoadRegion(string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("ITM_ITEM_GET", Conn, Branch);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClass dbdc = new GetRegionClass();

                dbdc.ITEMID = ds.Tables[0].Rows[i]["ITEMID"].ToString();
                dbdc.ITEMName = ds.Tables[0].Rows[i]["ITEMName"].ToString();
                dbdc.ItemCode = ds.Tables[0].Rows[i]["ItemCode"].ToString();
                dbdc.BarCode = ds.Tables[0].Rows[i]["BarCode"].ToString();
                dbdc.Discription = ds.Tables[0].Rows[i]["Discription"].ToString();
                dbdc.UnitTypeID = ds.Tables[0].Rows[i]["UnitTypeID"].ToString();
                dbdc.Category = ds.Tables[0].Rows[i]["CatID"].ToString();
                dbdc.Cost = ds.Tables[0].Rows[i]["BrandID"].ToString();
                dbdc.Price = ds.Tables[0].Rows[i]["CatTitle"].ToString();
                dbdc.BrandTitle = ds.Tables[0].Rows[i]["BrandTitle"].ToString();

                RegionList.Insert(i, dbdc);
            }

        }


        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);


    }

    public class GetRegionClass
    {
        public string BrandTitle;
        public string ITEMID { get; set; }
        public string ITEMName { get; set; }
        public string ItemCode { get; set; }
        public string BarCode { get; set; }
        public string Discription { get; set; }
        public string UnitTypeID { get; set; }
        public string Category { get; set; }
        public string Cost { get; set; }
        public string Price { get; set; }


    }




    //dropdown list

    [WebMethod]
    public static string LoadRegionCombo1(string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("ITM_UNIT_TYPE_Get", Conn, Branch);
        List<GetRegionClasss> RegionList = new List<GetRegionClasss>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClasss dbdc = new GetRegionClasss();

                dbdc.UnitTypeID = ds.Tables[0].Rows[i][0].ToString();
                dbdc.UnitTypeDesc = ds.Tables[0].Rows[i][1].ToString();
                RegionList.Insert(i, dbdc);
            }

        }

        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);
    }



    [WebMethod]
    public static string LoadBrand(string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("select * from Brand where IsDelete=0 and BranchID='" + BranchID + "'", Conn);
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
                dbdc.UnitTypeDesc = ds.Tables[0].Rows[i][1].ToString();
                RegionList.Insert(i, dbdc);
            }

        }

        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);
    }

    [WebMethod]
    public static string LoadCategory(string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("select * from category where IsDelete=0 and BranchID='" + BranchID + "'", Conn);
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
                dbdc.UnitTypeDesc = ds.Tables[0].Rows[i][1].ToString();
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










}