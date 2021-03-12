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
            string ITEMDetail = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GetDataClass dbdc = new GetDataClass();
                dbdc.SPDate = dt.Rows[i]["SPDate"].ToString();
                dbdc.SPID = dt.Rows[i]["SPID"].ToString();
                dbdc.AccountsID = dt.Rows[i]["AccountsID"].ToString();
                dbdc.LocalBillNo = dt.Rows[i]["LocalBillNo"].ToString();
                dbdc.CustomerAddress = dt.Rows[i]["CustomerAddress"].ToString();
                dbdc.SrNo = dt.Rows[i]["SrNo"].ToString();
                ITEMDetail = dt.Rows[i]["ITEMName"].ToString() + " " + dt.Rows[i]["CatTitle"].ToString() + " " + dt.Rows[i]["BrandTitle"].ToString();
                SqlDataAdapter dda = new SqlDataAdapter("select * from SP_DETAIL where ID='" + dt.Rows[i]["BatchID"].ToString() + "'", Con);
                DataTable dtt = new DataTable();
                dda.Fill(dtt);
                if (dtt.Rows.Count > 0)
                {
                    decimal bl = Convert.ToDecimal(dtt.Rows[0]["QtyIn"].ToString()) - Convert.ToDecimal(dtt.Rows[0]["QtyAvailed"].ToString());
                    ITEMDetail = ITEMDetail + " ^ Price : " + dtt.Rows[0]["UnitPrice"].ToString() + " ^ QTY : " + bl + " ^ " + dtt.Rows[0]["ITEMID"].ToString() + " ^ Purchase # : " + dtt.Rows[0]["SPID"].ToString() + " ^ ID : " + dtt.Rows[0]["ID"].ToString();
                    dbdc.QtyAvailed = dtt.Rows[0]["QtyAvailed"].ToString();
                    dbdc.QtyIn = dtt.Rows[0]["QtyIn"].ToString();
                }
                dbdc.ITEMName = ITEMDetail;
                //dbdc.QtyIn = dt.Rows[i]["QtyIn"].ToString();
                dbdc.QtyOut = dt.Rows[i]["QtyOut"].ToString();
               // dbdc.QtyAvailed = dt.Rows[i]["QtyAvailed"].ToString();
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
                dbdc.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                dbdc.CustomerContactNo = dt.Rows[i]["CustomerContactNo"].ToString();

                dbdc.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
                dbdc.TaxRate = dt.Rows[i]["TaxRate"].ToString();
                dbdc.GrandTotal = dt.Rows[i]["GrandTotal"].ToString();
                dbdc.CashBack = dt.Rows[i]["CashBack"].ToString();
                dbdc.CashRcvd = dt.Rows[i]["CashRcvd"].ToString();
                dbdc.Description = dt.Rows[i]["Description"].ToString();
                SqlDataAdapter daPayment = new SqlDataAdapter("select Top(1) * from Transaction_Detail where TaskID='C-" + VID + "'", Con);
                DataTable dtPayment = new DataTable();
                daPayment.Fill(dtPayment);
                if (dtPayment.Rows.Count > 0)
                {
                    dbdc.DebitPKR = dtPayment.Rows[0]["DebitPKR"].ToString();
                    dbdc.AccountID = dtPayment.Rows[0]["AccountID"].ToString();
                }

                RegionList.Insert(i, dbdc);
            }
        }

        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);

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
        public string Description { get; set; }
        public string CashBack { get; set; }
        public string CashRcvd { get; set; }

        public string DebitPKR { get; set; }
        public string AccountID { get; set; }
          
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
        string query = "select * from VW_ITEM_FOR_SALE where BranchID='" + BranchID + "' and  BranchID1='" + BranchID + "' and  BranchID2='" + BranchID + "' and BranchID3='" + BranchID + "'";
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
        acc = acc + "`" + LoadNUMBER("SO", "SP_MASTER", "SPID",Con,BranchID) + "`" + htmUNT;
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
    public static string SaveTransaction(string UserID, string str, string txtVDate, string txtVoucherNo, string txtTotAmount, string txtDiscount, string txtTaxRate, string txtTotTax, string txtGrandTotal, string ddlSupplier, string txtTotAmountRMB, string RMBValueHDN, string txtDiscountPKR, string txtDiscountRMB, string txtCusAmountPKR, string txtCusAmountRMB, string ddlShop, string txtCustomerName, string txtContactNo, string txtAddress, string txtBillNo, string BranchID, string RcvdAccount, string RcvdPKR, string RcvdRMB, string txtCashBack, string txtCashRcvd, string X5000, string X1000, string X500, string X100, string X50, string X20, string X10, string X5, string X2, string X1 )
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


        SqlCommand cmdTransactionMasterDeletePayment = new SqlCommand("delete from tbl_transaction where TaskID='C-" + txtVoucherNo + "'", Con);
        Con.Open();
        cmdTransactionMasterDeletePayment.ExecuteNonQuery();
        Con.Close();


        SqlCommand cmdTransactionDetailDeletePayment = new SqlCommand("delete from Transaction_Detail where TaskID='C-" + txtVoucherNo + "'", Con);
        Con.Open();
        cmdTransactionDetailDeletePayment.ExecuteNonQuery();
        Con.Close();
    

        string rqID = "";
        string rqHID = "";
        string rqDt = "";
        string rqBy = UserID;
        string rqAt = "";
        string stsID = "";// 0:Pending, 1:Approved, 2:Rejected


        ///////////////////////// ID GNERATOR /////////////////////////////
        string aa = LoadNUMBER("SO", "SP_MASTER", "SPID",Con,BranchID);
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

        SqlParameter CashBack = new SqlParameter("@CashBack", txtCashBack);
        SqlParameter CashRcvd = new SqlParameter("@CashRcvd", txtCashRcvd);

        SqlParameter P5000 = new SqlParameter("@X5000", X5000);
        SqlParameter P1000 = new SqlParameter("@X1000", X1000);
        SqlParameter P500 = new SqlParameter("@X500", X500);
        SqlParameter P100 = new SqlParameter("@X100", X100);
        SqlParameter P50 = new SqlParameter("@X50", X50);
        SqlParameter P20 = new SqlParameter("@X20", X20);
        SqlParameter P10 = new SqlParameter("@X10", X10);
        SqlParameter P5 = new SqlParameter("@X5", X5);
        SqlParameter P2 = new SqlParameter("@X2", X2);
        SqlParameter P1 = new SqlParameter("@X1", X1);

        AACommon.Execute("SP_MASTER_INSERT_CASH_SALE", Con, SPID, SPDate, TotalAmount, DiscountAmount, TaxAmount, TaxRate, GrandTotal, RMBAmount, AccountID, SP, CarryAmountPKR, CarryAmountRMB, PackingAmountPKR, PackingAmountRMB, ExChargeAmountPKR, ExChatgeAmountRMB, SupplierAmountPKR, SupplierAmountRMB, SalesDiscountPKR, SalesDiscountRMB, CusAmountPKR, CusAmountRMB, CarryID, ExChargesID, PackingID, ShopName, CustomerName, CreateBy, CustomerContactNo, CustomerAddress, LocalBillNo, Branch, CashBack, CashRcvd, P5000, P1000, P500, P100, P50, P20, P10, P5, P2, P1);


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
            string BatchID = itmROW[12].ToString();
            string OrgQty = itmROW[13].ToString();
            string txtDescription = itmROW[14].ToString();
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
            SqlParameter DescriptionP = new SqlParameter("@Description", txtDescription);
            AACommon.Execute("SP_DETAIL_INSERT_2", Con, SPID_1, ITEMID, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, BasicUnitPricePKR, BasicUnitPriceRMB, SRNO, CreateByNew, BranchP, DescriptionP);

            decimal qt = Convert.ToDecimal(OrgQty);
            //SqlDataAdapter daAv = new SqlDataAdapter("select BatchID from SP_DETAIL where ID='"+BatchID+"'", Con);
            //DataTable dtAv = new DataTable();
            //daAv.Fill(dtAv);
            //if (dtAv.Rows.Count > 0)
            //{

                SqlCommand cmdUPdateBatch = new SqlCommand("update SP_DETAIL Set QtyAvailed = QtyAvailed-" + qt + " where ID='" + BatchID + "' ", Con);
                Con.Open();
                cmdUPdateBatch.ExecuteNonQuery();
                Con.Close();
            //}

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

        string FinalAmountPKR = "";
        string FinalAmountRMB = "";
        if (Convert.ToDouble(txtTotTax) > 0)
        {
            FinalAmountPKR = Convert.ToString(Convert.ToDouble(txtTotAmount) + Convert.ToDouble(txtTotTax));
            FinalAmountRMB = Convert.ToString(Convert.ToDouble(txtTotAmountRMB) + Convert.ToDouble(txtTotTax));
        }

        string Narration = "Invoice By SO # " + rqID + "";
        SqlCommand cmd2 = new SqlCommand("insert into tbl_transaction (Date,Narration,AmountPKR,AmountRMB,RMBValue,TaskID,CreateBy,BranchID) values ('" + txtVDate + "','" + Narration + "','" + FinalAmountPKR + "','" + FinalAmountRMB + "','" + RMBValueHDN + "','" + rqID + "','" + UserID + "','" + BranchID + "')", Con);
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
            SqlCommand cmdPurchase3 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + SaleDiscountAccount + "','SALE','" + txtDiscountPKR + "','" + txtDiscountRMB + "','0','0','" + RMBValueHDN + "','" + rqID + "','" + Narration + "','" + UserID + "','" + BranchID + "')", Con);
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
        SqlCommand cmdPurchase2 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + SaleRevenuAccount + "','SALE','0','0','" + txtTotAmount + "','" + txtTotAmountRMB + "','" + RMBValueHDN + "','" + rqID + "','" + Narration + "','" + UserID + "','" + BranchID + "')", Con);
        Con.Open();
        cmdPurchase2.ExecuteNonQuery();
        Con.Close();
        ///PURCHASE ENTRY

        ////////////////////SAVE TRANSACTION IN ACCOUNTS FOR CASH////////////////////
        if (Convert.ToDouble(RcvdPKR) > 0)
        {
            string NarrationCash = "Payment Rcvd By CS # " + rqID + "";
            SqlCommand cmd2Cash = new SqlCommand("insert into tbl_transaction (Date,Narration,AmountPKR,AmountRMB,RMBValue,TaskID,CreateBy,BranchID) values ('" + txtVDate + "','" + NarrationCash + "','" + RcvdPKR + "','" + RcvdRMB + "','" + RMBValueHDN + "','C-" + rqID + "','" + UserID + "','" + BranchID + "')", Con);
            Con.Open();
            cmd2Cash.ExecuteNonQuery();
            Con.Close();

            ///PURCHASE ENTRY
            SqlCommand cmdPurchase1Cash = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + RcvdAccount + "','CASH-PVC','" + RcvdPKR + "','" + RcvdRMB + "','0','0','" + RMBValueHDN + "','C-" + rqID + "','" + NarrationCash + "','" + UserID + "','" + BranchID + "')", Con);
            Con.Open();
            cmdPurchase1Cash.ExecuteNonQuery();
            Con.Close();


            NarrationCash = "Payment Rcvd By CS # " + rqID + "";
            SqlCommand cmdPurchase2Cash = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + ddlSupplier + "','CASH-PVC','0','0','" + RcvdPKR + "','" + RcvdRMB + "','" + RMBValueHDN + "','C-" + rqID + "','" + NarrationCash + "','" + UserID + "','" + BranchID + "')", Con);
            Con.Open();
            cmdPurchase2Cash.ExecuteNonQuery();
            Con.Close();
        }

        return msg + rqID;
    }


}