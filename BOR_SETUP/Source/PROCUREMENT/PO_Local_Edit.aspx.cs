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
    public static string LoadType(string ddltype, string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        List<GetRegionClasss> RegionList = new List<GetRegionClasss>();
        if (ddltype == "PKR")
        {
            GetRegionClasss dbdc = new GetRegionClasss();
            dbdc.UnitTypeID = "1";
            RegionList.Insert(0, dbdc);
        }
        else
        {
            SqlDataAdapter da = new SqlDataAdapter("Select RMBValue  from CurrencyConversion where IsActive=1 and BranchID='" + BranchID + "'", Conn);
            DataSet ds = new DataSet();
            da.Fill(ds);         
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
        }

        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);
    }

    [WebMethod]
    public static string LoadRMbValue(string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        List<GetRegionClasss> RegionList = new List<GetRegionClasss>();

        SqlDataAdapter da = new SqlDataAdapter("Select RMBValue  from CurrencyConversion where IsActive=1 and BranchID='" + BranchID + "'", Conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
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

    public class GetDataClass
    {
        public string ID { get; set; }
        public string ITEMID { get; set; }
        public string ITEMName { get; set; }
        public string CatID { get; set; }
        public string CatTitle { get; set; }
        public string BrandID { get; set; }
        public string BrandTitle { get; set; }
        public string AccountsID { get; set; }
        public string AccountsTitle { get; set; }
        public string UnitID { get; set; }
        public string QtyIn { get; set; }
        public string QtyOut { get; set; }
        public string QtyAvailed { get; set; }
        public string UnitPrice { get; set; }
        public string RMBValue { get; set; }
        public string RMBUnitPrice { get; set; }
        public string RMBTotalPrice { get; set; }
        public string TotalPrice { get; set; }
        public string Carry { get; set; }
        public string ExtraCharges { get; set; }
        public string BatchID { get; set; }
        public string Type { get; set; }
        public string BasicUnitPricePKR { get; set; }
        public string BasicUnitPriceRMB { get; set; }
        public string RMBPackingRate { get; set; }
        public string RMBPackingPrice { get; set; }
        public string PKRPackingPrice { get; set; }
        public string SrNo { get; set; }
        public string SPID { get; set; }
        public string SPDate { get; set; }
        public string TotalAmount { get; set; }
        public string DiscountAmount { get; set; }
        public string TaxRate { get; set; }
        public string TaxAmount { get; set; }
        public string TaxAmountRMB { get; set; }
        public string GrandTotal { get; set; }
        public string RMBAmount { get; set; }
        public string CarryAmountPKR { get; set; }
        public string CarryAmountRMB { get; set; }
        public string PackingAmountPKR { get; set; }
        public string PackingAmountRMB { get; set; }
        public string ExChargeAmountPKR { get; set; }
        public string ExChatgeAmountRMB { get; set; }
        public string SupplierAmountPKR { get; set; }
        public string SupplierAmountRMB { get; set; }
        public string SalesDiscountPKR { get; set; }
        public string SalesDiscountRMB { get; set; }
        public string CusAmountPKR { get; set; }
        public string CusAmountRMB { get; set; }
        public string CarryID { get; set; }
        public string ExChargesID { get; set; }
        public string PackingID { get; set; }
        public string SP { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContactNo { get; set; }
        public string CustomerAddress { get; set; }
        public string LocalBillNo { get; set; }

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
        string query = "SELECT        dbo.ITM_ITEM.ITEMID, dbo.ITM_ITEM.ITEMID + ' ^ ' + dbo.ITM_ITEM.ITEMName + ' ^ ' + dbo.Category.CatTitle + ' ^ ' + dbo.Brand.BrandTitle AS ITEMName, dbo.ITM_ITEM.ItemCode,                           dbo.ITM_ITEM.UnitTypeID FROM            dbo.ITM_ITEM INNER JOIN                          dbo.Category ON dbo.ITM_ITEM.CatID = dbo.Category.CatID INNER JOIN                          dbo.Brand ON dbo.ITM_ITEM.BrandID = dbo.Brand.BrandID WHERE        (dbo.ITM_ITEM.ISDELETE = '0') and dbo.ITM_ITEM.BranchID='" + BranchID + "' and dbo.Brand.BranchID='" + BranchID + "' and dbo.Category.BranchID='" + BranchID + "'";
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
            htmUNT = htmUNT+LoadUNITS(itmID, UntTyp,BranchID);
        }
        acc = acc + "`" + LoadNUMBER("PR", "SP_MASTER", "SPID", Con, BranchID) + "`" + htmUNT;
        return acc;
    }



    [WebMethod]
    public static string LOAD_PURCHASE(string UserID, string BranchID, string VID)
    {


        List<GetDataClass> RegionList = new List<GetDataClass>();
        string acc = "";
        string query = "select * from VW_GET_PURCHASE_SALE_DATA where SPID='" + VID + "' and  BranchID='" + BranchID + "' and  BranchID1='" + BranchID + "' and  BranchID2='" + BranchID + "' and  BranchID3='" + BranchID + "' and  BranchID4='" + BranchID + "' and  BranchID5='" + BranchID + "' order by SrNo";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(query, Con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        RegionList.Clear();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GetDataClass dbdc = new GetDataClass();
                dbdc.SPDate = dt.Rows[i]["SPDate"].ToString();
                dbdc.SPID = dt.Rows[i]["SPID"].ToString();                
                dbdc.AccountsID = dt.Rows[i]["AccountsID"].ToString();
                dbdc.LocalBillNo = dt.Rows[i]["LocalBillNo"].ToString();
                dbdc.CustomerAddress = dt.Rows[i]["CustomerAddress"].ToString();
                dbdc.SrNo = dt.Rows[i]["SrNo"].ToString();
                dbdc.ITEMName = dt.Rows[i]["ITEMID"].ToString() + " ^ " + dt.Rows[i]["ITEMName"].ToString() + " ^ " + dt.Rows[i]["CatTitle"].ToString() + " ^ " + dt.Rows[i]["BrandTitle"].ToString();
                dbdc.QtyIn = dt.Rows[i]["QtyIn"].ToString();
                dbdc.QtyOut = dt.Rows[i]["QtyOut"].ToString();
                dbdc.QtyAvailed = dt.Rows[i]["QtyAvailed"].ToString();
                dbdc.UnitPrice = dt.Rows[i]["UnitPrice"].ToString();
                dbdc.RMBUnitPrice = dt.Rows[i]["RMBUnitPrice"].ToString();
                dbdc.TotalPrice = dt.Rows[i]["TotalPrice"].ToString();
                dbdc.RMBTotalPrice = dt.Rows[i]["RMBTotalPrice"].ToString();
                dbdc.SalesDiscountPKR = dt.Rows[i]["SalesDiscountPKR"].ToString();
                dbdc.SalesDiscountRMB = dt.Rows[i]["SalesDiscountRMB"].ToString();
                dbdc.SupplierAmountPKR = dt.Rows[i]["SupplierAmountPKR"].ToString();
                dbdc.SupplierAmountRMB = dt.Rows[i]["SupplierAmountRMB"].ToString();
                dbdc.CusAmountPKR = dt.Rows[i]["CusAmountPKR"].ToString();
                dbdc.CusAmountRMB = dt.Rows[i]["CusAmountRMB"].ToString();
                dbdc.TotalAmount = dt.Rows[i]["TotalAmount"].ToString();
                dbdc.RMBAmount = dt.Rows[i]["RMBAmount"].ToString();
                dbdc.RMBValue = dt.Rows[i]["RMBValue"].ToString();
                dbdc.BatchID = dt.Rows[i]["BatchID"].ToString();
                dbdc.ID = dt.Rows[i]["ID"].ToString();
                RegionList.Insert(i, dbdc);
            }
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
    public static string SaveTransaction(string UserID, string str, string txtVDate, string txtVoucherNo, string txtTotAmount, string txtDiscount, string txtTaxRate, string txtTotTax, string txtGrandTotal, string ddlSupplier, string txtTotAmountRMB, string RMBValueHDN, string txtSupplierPKR, string txtSupplierRMB, string txtDiscountPKR, string txtDiscountRMB, string BranchID, string txtAddress, string txtBillNo)
    {
        string msg = "";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        SqlCommand cmdMasterDelete = new SqlCommand("delete from SP_MASTER where SPID='" + txtVoucherNo + "'", Con);
        Con.Open();
        cmdMasterDelete.ExecuteNonQuery();
        Con.Close();


        SqlCommand cmdDetailDelete = new SqlCommand("delete from SP_DETAIL where SPID='" + txtVoucherNo + "'", Con);
        Con.Open();
        cmdDetailDelete.ExecuteNonQuery();
        Con.Close();


        SqlCommand cmdTransactionMasterDelete = new SqlCommand("delete from tbl_transaction where TaskID='" + txtVoucherNo + "'", Con);
        Con.Open();
        cmdTransactionMasterDelete.ExecuteNonQuery();
        Con.Close();


        SqlCommand cmdTransactionDetailDelete = new SqlCommand("delete from Transaction_Detail where TaskID='" + txtVoucherNo + "'", Con);
        Con.Open();
        cmdTransactionDetailDelete.ExecuteNonQuery();
        Con.Close();

        string rqID = "";
        string rqHID = "";
        string rqDt = "";
        string rqBy = UserID;
        string rqAt = "";
        string stsID = "";// 0:Pending, 1:Approved, 2:Rejected


        ///////////////////////// ID GNERATOR /////////////////////////////
        string aa = LoadNUMBER("PR", "SP_MASTER", "SPID",Con,BranchID);
        string[] idDT = aa.Split('`');
        rqID = idDT[0].ToString();
        rqDt = idDT[1].ToString();

       // rqHID = LoadHistoryID(rqID);


        rqID = txtVoucherNo;
        rqDt = txtVDate;

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
        SqlParameter SP = new SqlParameter("@SP", "P");

        SqlParameter CarryAmountPKR = new SqlParameter("@CarryAmountPKR", "0");
        SqlParameter CarryAmountRMB = new SqlParameter("@CarryAmountRMB", "0");
        SqlParameter PackingAmountPKR = new SqlParameter("@PackingAmountPKR", "0");
        SqlParameter PackingAmountRMB = new SqlParameter("@PackingAmountRMB", "0");
        SqlParameter ExChargeAmountPKR = new SqlParameter("@ExChargeAmountPKR", "0");
        SqlParameter ExChatgeAmountRMB = new SqlParameter("@ExChatgeAmountRMB", "0");
        SqlParameter SupplierAmountPKR = new SqlParameter("@SupplierAmountPKR", txtSupplierPKR);
        SqlParameter SupplierAmountRMB = new SqlParameter("@SupplierAmountRMB", txtSupplierRMB);
        SqlParameter SalesDiscountPKR = new SqlParameter("@SalesDiscountPKR", txtDiscountPKR);
        SqlParameter SalesDiscountRMB = new SqlParameter("@SalesDiscountRMB", txtDiscountRMB);
        SqlParameter CusAmountPKR = new SqlParameter("@CusAmountPKR", "0");
        SqlParameter CusAmountRMB = new SqlParameter("@CusAmountRMB", "0");

        SqlParameter CarryID = new SqlParameter("@CarryID", "0");
        SqlParameter ExChargesID = new SqlParameter("@ExChargesID", "0");
        SqlParameter PackingID = new SqlParameter("@PackingID", "0");
        SqlParameter ShopName = new SqlParameter("@ShopName", "");
        SqlParameter CustomerName = new SqlParameter("@CustomerName", "");
        SqlParameter CreateBy = new SqlParameter("@CreateBy", UserID);

        SqlParameter CustomerContactNo = new SqlParameter("@CustomerContactNo", "");        
        SqlParameter CustomerAddress = new SqlParameter("@CustomerAddress", txtAddress);
        SqlParameter LocalBillNo = new SqlParameter("@LocalBillNo", txtBillNo);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        AACommon.Execute("SP_MASTER_INSERT", Con, SPID, SPDate, TotalAmount, DiscountAmount, TaxAmount, TaxRate, GrandTotal, RMBAmount, AccountID, SP, CarryAmountPKR, CarryAmountRMB, PackingAmountPKR, PackingAmountRMB, ExChargeAmountPKR, ExChatgeAmountRMB, SupplierAmountPKR, SupplierAmountRMB, SalesDiscountPKR, SalesDiscountRMB, CusAmountPKR, CusAmountRMB, CarryID, ExChargesID, PackingID, ShopName, CustomerName, CreateBy, CustomerContactNo, CustomerAddress, LocalBillNo,Branch);


        /////////////////////// INSERT in SP DETAIL

        string[] itmLST = str.Split('`');
        for (int i = 0; i < itmLST.Length; i++)
        {
            string[] itmROW = itmLST[i].Split('^');
            //txtAccID + "^" + txtAccTitle + "^" + ddlUnit + "^" + txtQty + "^" + txtPrice + "^" + txtTotalPrice
            string itmID = itmROW[0].ToString();
            //string ddlUnit = itmROW[2].ToString();
            string txtQty = itmROW[2].ToString();
            string txtPrice = itmROW[3].ToString();
            string txtTotalPrice = itmROW[4].ToString();

            string txtUnitPriceRMB = itmROW[5].ToString();
            string txtTotalPriceRMB = itmROW[6].ToString();
            string txtRMBValue = itmROW[7].ToString();
            //string txtCarry = itmROW[8].ToString();
            //string txtExtraCharges = itmROW[9].ToString();
            //string txtPKRCost = itmROW[10].ToString();
            //string txtRMBCost = itmROW[11].ToString();
            //string txtPackingRate = itmROW[12].ToString();
            //string txtPackingPrice = itmROW[13].ToString();
            //string txtPackingPricePKR = itmROW[14].ToString();
            string txtSrNo = itmROW[8].ToString();
            string BatchID = itmROW[9].ToString();
            string Balance = itmROW[10].ToString();
            

            SqlParameter SPID_1 = new SqlParameter("@SPID", rqID);
            SqlParameter ITEMID = new SqlParameter("@ITEMID", itmID);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", txtPrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", txtTotalPrice);
            SqlParameter RMBValue = new SqlParameter("@RMBValue", txtRMBValue);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", txtUnitPriceRMB);
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", txtTotalPriceRMB);
            SqlParameter Type = new SqlParameter("@Type", "P");
            SqlParameter Carry = new SqlParameter("@Carry", "0");
            SqlParameter ExtraCharges = new SqlParameter("@ExtraCharges", "0");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", txtPrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", txtUnitPriceRMB);
            SqlParameter RMBPackingRate = new SqlParameter("@RMBPackingRate", "0");
            SqlParameter RMBPackingPrice = new SqlParameter("@RMBPackingPrice", "0");
            SqlParameter PKRPackingPrice = new SqlParameter("@PKRPackingPrice", "0");
            SqlParameter SRNO = new SqlParameter("@SrNo", txtSrNo);
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchID);
            AACommon.Execute("SP_DETAIL_INSERT", Con, SPID_1, ITEMID, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, Carry, ExtraCharges, BasicUnitPricePKR, BasicUnitPriceRMB, RMBPackingRate, RMBPackingPrice, PKRPackingPrice,SRNO,CreateByNew,BranchP);

            SqlDataAdapter daLast = new SqlDataAdapter("Select Top(1) ID from SP_Detail where SPID='" + txtVoucherNo + "' order by ID Desc", Con);
            DataTable dtLast = new DataTable();
            daLast.Fill(dtLast);
            if (dtLast.Rows.Count > 0)
            {
                SqlCommand cmdUpdateQtyOut = new SqlCommand("update SP_Detail set QtyAvailed='"+Balance+"' where ID='" + dtLast.Rows[0][0].ToString() + "' and Type='P'", Con);
                Con.Open();
                cmdUpdateQtyOut.ExecuteNonQuery();
                Con.Close();

                SqlCommand cmdupdate = new SqlCommand("update SP_Detail set BatchID='" + dtLast.Rows[0][0].ToString() + "' where BatchID='" + BatchID + "' and Type='S'", Con);
                Con.Open();
                cmdupdate.ExecuteNonQuery();
                Con.Close();

            }

        }

        ////////////////////SAVE TRANSACTION IN ACCOUNTS ////////////////////

        double totafterdiscountPKR =   Convert.ToDouble(txtSupplierPKR);
        double totafterdiscountRMB =   Convert.ToDouble(txtSupplierRMB);

        string Narration = "Purchase By PO # " + rqID + "";
        SqlCommand cmd = new SqlCommand("insert into tbl_transaction (Date,Narration,AmountPKR,AmountRMB,RMBValue,TaskID,CreateBy,BranchID) values ('" + txtVDate + "','" + Narration + "','" + totafterdiscountPKR + "','" + totafterdiscountRMB + "','" + RMBValueHDN + "','" + rqID + "','" + UserID + "','" + BranchID + "')", Con);
        Con.Open();
        cmd.ExecuteNonQuery();
        Con.Close();

        string PurchaseAccount = "";
        SqlDataAdapter daPurchase = new SqlDataAdapter("select AccountsID from Accounts where BranchID='" + BranchID + "' and AccountsTitle='Purchases'", Con);
        DataTable dtPurchase = new DataTable();
        daPurchase.Fill(dtPurchase);
        if (dtPurchase.Rows.Count > 0)
        {
            PurchaseAccount = dtPurchase.Rows[0][0].ToString();
        }



        ///PURCHASE ENTRY
        SqlCommand cmdPurchase1 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + PurchaseAccount + "','PURCHASE','" + totafterdiscountPKR + "','" + totafterdiscountRMB + "','0','0','" + RMBValueHDN + "','" + rqID + "','" + Narration + "','" + UserID + "','" + BranchID + "')", Con);
        Con.Open();
        cmdPurchase1.ExecuteNonQuery();
        Con.Close();

        SqlCommand cmdPurchase2 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + ddlSupplier + "','PURCHASE','0','0','" + txtSupplierPKR + "','" + txtSupplierRMB + "','" + RMBValueHDN + "','" + rqID + "','" + Narration + "','" + UserID + "','" + BranchID + "')", Con);
        Con.Open();
        cmdPurchase2.ExecuteNonQuery();
        Con.Close();

        return msg + rqID;
    }


}