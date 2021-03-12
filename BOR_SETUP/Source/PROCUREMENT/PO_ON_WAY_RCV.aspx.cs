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

    public class GetRegionClass
    {
        public string SPID { get; set; }
        public string Supplier { get; set; }
        public string CurrenctyType { get; set; }
        public string Rate { get; set; }
        public string PackingRate { get; set; }
        public string PackingAccount { get; set; }
        public string SupplierAmountPKR { get; set; }
        public string SupplierAmountRMB { get; set; }
        public string PackingAmountPKR { get; set; }
        public string PackingAmountRMB { get; set; }
        public string DiscountAmountPKR { get; set; }
        public string DiscountAmountRMB { get; set; }
        public string TotalAmountPKR { get; set; }
        public string TotalAmountRMB { get; set; }
        public string ItemID { get; set; }
        public string Qty { get; set; }


        public string UnitPricePKR { get; set; }
        public string UnitPriceRMB { get; set; }
        public string PackingRMB { get; set; }
        public string CostPKR { get; set; }
        public string CostRMB { get; set; }
        public string TotalPricePKR { get; set; }
        public string TOtalPriceRMB { get; set; }
        public string Remarks { get; set; }
        public string SPDate { get; set; }
        public string Container { get; set; }
        

    }

    [WebMethod]
    public static string LoadOnwayHistory(string Product, string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("select * from SP_MASTER_ON_WAY_HISTORY where SPID='" + Product + "'  and BranchID='" + BranchID + "' Order by ID Desc", Conn);
        DataSet ds = new DataSet();
        da.Fill(ds);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClass dbdc = new GetRegionClass();
                dbdc.Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString();
                RegionList.Insert(i, dbdc);

            }

        }


        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);


    }

    [WebMethod]
    public static string LoadOnway(string Product, string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("select * from VW_ON_WAY_PENDING_DETAIL where SPID='" + Product + "' and BranchID='" + BranchID + "' and BranchID1='" + BranchID + "' and BranchID2='" + BranchID + "' and BranchID3='" + BranchID + "' and BranchID4='" + BranchID + "'", Conn);
        DataSet ds = new DataSet();
        da.Fill(ds);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClass dbdc = new GetRegionClass();
                dbdc.SPID = ds.Tables[0].Rows[i]["SPID"].ToString();
                dbdc.Supplier = ds.Tables[0].Rows[i]["AccountID"].ToString();
                dbdc.CurrenctyType = "PKR";
                dbdc.Rate = ds.Tables[0].Rows[i]["RMBValue"].ToString();
                dbdc.PackingRate = ds.Tables[0].Rows[i]["RMBPackingRate"].ToString();
                dbdc.PackingAccount = ds.Tables[0].Rows[i]["PackingID"].ToString();
                dbdc.SupplierAmountPKR = ds.Tables[0].Rows[i]["SupplierAmountPKR"].ToString();
                dbdc.SupplierAmountRMB = ds.Tables[0].Rows[i]["SupplierAmountRMB"].ToString();
                dbdc.PackingAmountPKR = ds.Tables[0].Rows[i]["PackingAmountPKR"].ToString();
                dbdc.PackingAmountRMB = ds.Tables[0].Rows[i]["PackingAmountRMB"].ToString();
                dbdc.DiscountAmountPKR = ds.Tables[0].Rows[i]["SalesDiscountPKR"].ToString();
                dbdc.DiscountAmountRMB = ds.Tables[0].Rows[i]["SalesDiscountRMB"].ToString();
                dbdc.TotalAmountPKR = ds.Tables[0].Rows[i]["GrandTotal"].ToString();
                dbdc.TotalAmountRMB = ds.Tables[0].Rows[i]["RMBAmount"].ToString();
                dbdc.ItemID = ds.Tables[0].Rows[i]["ITEMID"].ToString();
                dbdc.Qty = ds.Tables[0].Rows[i]["QtyIn"].ToString();

                dbdc.UnitPricePKR = ds.Tables[0].Rows[i]["BasicUnitPricePKR"].ToString();
                dbdc.UnitPriceRMB = ds.Tables[0].Rows[i]["BasicUnitPriceRMB"].ToString();
                dbdc.PackingRMB = ds.Tables[0].Rows[i]["RMBPackingPrice"].ToString();
                dbdc.CostPKR = ds.Tables[0].Rows[i]["UnitPrice"].ToString();
                dbdc.CostRMB = ds.Tables[0].Rows[i]["RMBUnitPrice"].ToString();
                dbdc.TotalPricePKR = ds.Tables[0].Rows[i]["TotalPrice"].ToString();
                dbdc.TOtalPriceRMB = ds.Tables[0].Rows[i]["RMBTotalPrice"].ToString();
                dbdc.Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString();
                dbdc.SPDate = ds.Tables[0].Rows[i]["SPDate"].ToString();
                dbdc.Container = ds.Tables[0].Rows[i]["ContainerNo"].ToString();
                RegionList.Insert(i, dbdc);

            }

        }


        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);


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
//        string query = "SELECT  ITEMID, ITEMID+ ' ^ ' +ITEMName as ITEMName, ItemCode,UnitTypeID FROM ITM_ITEM WHERE ([IsDelete] = '0')";
        string query = "SELECT        dbo.ITM_ITEM.ITEMID, dbo.ITM_ITEM.ITEMID + ' ^ ' + dbo.ITM_ITEM.ITEMName + ' ^ ' + dbo.Category.CatTitle + ' ^ ' + dbo.Brand.BrandTitle AS ITEMName, dbo.ITM_ITEM.ItemCode,                           dbo.ITM_ITEM.UnitTypeID FROM            dbo.ITM_ITEM INNER JOIN                          dbo.Category ON dbo.ITM_ITEM.CatID = dbo.Category.CatID INNER JOIN                          dbo.Brand ON dbo.ITM_ITEM.BrandID = dbo.Brand.BrandID WHERE        (dbo.ITM_ITEM.ISDELETE = '0') and dbo.ITM_ITEM.BranchID='" + BranchID + "' and dbo.Brand.BranchID='" + BranchID + "' and dbo.Category.BranchID='" + BranchID + "'";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(query, Con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            string itmID = dt.Rows[i - 1]["ITEMID"].ToString();
            string UntTyp = dt.Rows[i - 1]["UnitTypeID"].ToString();
            //'["Karachi","Hyderabad","USA","Arkansas","California","Colorado","Connecticut","Delaware","Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas","Kentucky"]'
            if (i == 1)
            { acc = acc + "['" + dt.Rows[i - 1]["ITEMName"].ToString() + "'"; }
            else if (i != 1 && i < dt.Rows.Count)
            { acc = acc + ",'" + dt.Rows[i - 1]["ITEMName"].ToString() + "'"; }
            else
            { acc = acc + ",'" + dt.Rows[i - 1]["ITEMName"].ToString() + "']"; }

            htmUNT = htmUNT + LoadUNITS(itmID, UntTyp, BranchID);
        }
        acc = acc + "`" + LoadNUMBER("PR", "SP_MASTER", "SPID", Con, BranchID) + "`" + htmUNT;
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
    public static string SaveTransaction(string UserID, string str, string txtVDate, string txtVoucherNo, string txtTotAmount, string txtDiscount, string txtTaxRate, string txtTotTax, string txtGrandTotal, string ddlSupplier, string txtTotAmountRMB, string RMBValueHDN, string txtCarryPKR, string txtCarryRMB, string txtPackingPKR, string txtPackingRMB, string txtExChargePKR, string txtExChargeRMB, string txtSupplierPKR, string txtSupplierRMB, string DDLPacking, string DDLCarry, string DDLExCharge, string txtDiscountPKR, string txtDiscountRMB, string txtONWAYID, string BranchID)
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
        string aa = LoadNUMBER("PR", "SP_MASTER", "SPID",Con,BranchID);
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
        SqlParameter SP = new SqlParameter("@SP", "P");

        SqlParameter CarryAmountPKR = new SqlParameter("@CarryAmountPKR", txtCarryPKR);
        SqlParameter CarryAmountRMB = new SqlParameter("@CarryAmountRMB", txtCarryRMB);
        SqlParameter PackingAmountPKR = new SqlParameter("@PackingAmountPKR", txtPackingPKR);
        SqlParameter PackingAmountRMB = new SqlParameter("@PackingAmountRMB", txtPackingRMB);
        SqlParameter ExChargeAmountPKR = new SqlParameter("@ExChargeAmountPKR", txtExChargePKR);
        SqlParameter ExChatgeAmountRMB = new SqlParameter("@ExChatgeAmountRMB", txtExChargeRMB);
        SqlParameter SupplierAmountPKR = new SqlParameter("@SupplierAmountPKR", txtSupplierPKR);
        SqlParameter SupplierAmountRMB = new SqlParameter("@SupplierAmountRMB", txtSupplierRMB);
        SqlParameter SalesDiscountPKR = new SqlParameter("@SalesDiscountPKR", txtDiscountPKR);
        SqlParameter SalesDiscountRMB = new SqlParameter("@SalesDiscountRMB", txtDiscountRMB);
        SqlParameter CusAmountPKR = new SqlParameter("@CusAmountPKR", "0");
        SqlParameter CusAmountRMB = new SqlParameter("@CusAmountRMB", "0");

        SqlParameter CarryID = new SqlParameter("@CarryID", DDLCarry);
        SqlParameter ExChargesID = new SqlParameter("@ExChargesID", DDLExCharge);
        SqlParameter PackingID = new SqlParameter("@PackingID", DDLPacking);
        SqlParameter ShopName = new SqlParameter("@ShopName", "");
        SqlParameter CustomerName = new SqlParameter("@CustomerName", "");
        SqlParameter CreateBy = new SqlParameter("@CreateBy", UserID);

        SqlParameter CustomerContactNo = new SqlParameter("@CustomerContactNo", "");
        SqlParameter CustomerAddress = new SqlParameter("@CustomerAddress", "");
        SqlParameter LocalBillNo = new SqlParameter("@LocalBillNo", "");
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
            string txtCarry = itmROW[8].ToString();
            string txtExtraCharges = itmROW[9].ToString();
            string txtPKRCost = itmROW[10].ToString();
            string txtRMBCost = itmROW[11].ToString();
            string txtPackingRate = itmROW[12].ToString();
            string txtPackingPrice = itmROW[13].ToString();
            string txtPackingPricePKR = itmROW[14].ToString();
            string txtSrNo = itmROW[15].ToString();


            SqlParameter SPID_1 = new SqlParameter("@SPID", rqID);
            SqlParameter ITEMID = new SqlParameter("@ITEMID", itmID);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", txtPKRCost);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", txtTotalPrice);
            SqlParameter RMBValue = new SqlParameter("@RMBValue", txtRMBValue);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", txtRMBCost);
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", txtTotalPriceRMB);
            SqlParameter Type = new SqlParameter("@Type", "P");
            SqlParameter Carry = new SqlParameter("@Carry", txtCarry);
            SqlParameter ExtraCharges = new SqlParameter("@ExtraCharges", txtExtraCharges);
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", txtPrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", txtUnitPriceRMB);
            SqlParameter RMBPackingRate = new SqlParameter("@RMBPackingRate", txtPackingRate);
            SqlParameter RMBPackingPrice = new SqlParameter("@RMBPackingPrice", txtPackingPrice);
            SqlParameter PKRPackingPrice = new SqlParameter("@PKRPackingPrice", txtPackingPricePKR);
            SqlParameter SRNO = new SqlParameter("@SrNo", txtSrNo);
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchID);
            AACommon.Execute("SP_DETAIL_INSERT", Con, SPID_1, ITEMID, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, Carry, ExtraCharges, BasicUnitPricePKR, BasicUnitPriceRMB, RMBPackingRate, RMBPackingPrice, PKRPackingPrice, SRNO, CreateByNew, BranchP);

        }

        ////////////////////SAVE TRANSACTION IN ACCOUNTS ////////////////////

        double totafterdiscountPKR = Convert.ToDouble(txtCarryPKR) + Convert.ToDouble(txtPackingPKR) + Convert.ToDouble(txtExChargePKR) + Convert.ToDouble(txtSupplierPKR);
        double totafterdiscountRMB = Convert.ToDouble(txtCarryRMB) + Convert.ToDouble(txtPackingRMB) + Convert.ToDouble(txtExChargeRMB) + Convert.ToDouble(txtSupplierRMB);

        double finalnewamountPKR = Convert.ToDouble(txtCarryPKR) + Convert.ToDouble(txtExChargePKR);
        double finalnewamountRMB = Convert.ToDouble(txtCarryRMB) +  Convert.ToDouble(txtExChargeRMB);

        string Narration = "Purchase By PO # " + rqID + "";
        SqlCommand cmd = new SqlCommand("insert into tbl_transaction (Date,Narration,AmountPKR,AmountRMB,RMBValue,TaskID,CreateBy,BranchID) values ('" + txtVDate + "','" + Narration + "','" + finalnewamountPKR + "','" + finalnewamountRMB + "','" + RMBValueHDN + "','" + rqID + "','" + UserID + "','"+BranchID+"')", Con);
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

        /////PURCHASE ENTRY
        SqlCommand cmdPurchase1 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + PurchaseAccount + "','PURCHASE','" + finalnewamountPKR + "','" + finalnewamountRMB + "','0','0','" + RMBValueHDN + "','" + rqID + "','" + Narration + "','" + UserID + "','" + BranchID + "')", Con);
        Con.Open();
        cmdPurchase1.ExecuteNonQuery();
        Con.Close();

        
        if (Convert.ToDouble(txtCarryPKR) > 0)
        {
            Narration = "Carry Charges By PO # " + rqID + "";
            SqlCommand cmdPurchase3 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + DDLCarry + "','PURCHASE','0','0','" + txtCarryPKR + "','" + txtCarryRMB + "','" + RMBValueHDN + "','" + rqID + "','" + Narration + "','" + UserID + "','" + BranchID + "')", Con);
            Con.Open();
            cmdPurchase3.ExecuteNonQuery();
            Con.Close();
        }

        
        if (Convert.ToDouble(txtExChargePKR) > 0)
        {
            Narration = "Extra Charges By PO # " + rqID + "";
            SqlCommand cmdPurchase5 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + DDLExCharge + "','PURCHASE','0','0','" + txtExChargePKR + "','" + txtExChargeRMB + "','" + RMBValueHDN + "','" + rqID + "','" + Narration + "','" + UserID + "','" + BranchID + "')", Con);
            Con.Open();
            cmdPurchase5.ExecuteNonQuery();
            Con.Close();
        }
        ///PURCHASE ENTRY
        



        SqlCommand cmdONWAYRCVD = new SqlCommand("update SP_MASTER_ON_WAY set IsReceived=1 where SPID='"+txtONWAYID+"' and BranchID='"+BranchID+"'", Con);
        Con.Open();
        cmdONWAYRCVD.ExecuteNonQuery();
        Con.Close();
        return msg;
    }


}