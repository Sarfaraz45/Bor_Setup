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
public partial class PROCUREMENT_AATemp_Opening : System.Web.UI.Page
{
    public SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        GetLedgerBalance();
        GetStock();

        //////////////////////UPDATE Accounts SET OpCredit = ABS(OpCredit)

        //////////////////////update Accounts set RMBValue=17

        //////////////////////update Accounts set OpDebitRMB = OpDebit / RMBValue where OpDebit > 0

        //////////////////////update Accounts set OpDebitRMB=cast(OpDebitRMB as decimal(10,2))


        //////////////////////update Accounts set OpCreditRMB = OpCredit / RMBValue where OpCredit > 0

        //////////////////////update Accounts set OpCreditRMB=cast(OpCreditRMB as decimal(10,2))

        ///ASK FOR BMA OPENING 2018 ID
        ///WAREHOUSE PENDING
        
    }

    [WebMethod]
    public static string LoadNUMBER(string frmt, string tbl, string col, SqlConnection Con)
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
        string format1 = frmt + "-" + mm1 + "-" + yy1 + "-";

        vNO = AACommon.GetAlphaNumericIDSIX(tbl, format1, col, Con);
        vNO = vNO + "`" + vDate;
        return vNO;
    }

    public void GetStock()
    {
        SqlCommand cmdDelete = new SqlCommand("delete from AA_STOCK_OPENING_TEMP", Conn);
        Conn.Open();
        cmdDelete.ExecuteNonQuery();
        Conn.Close();

        SqlDataAdapter da = new SqlDataAdapter("select * from VW_TEMP_AVAILABLE_STOCK_FOR_OPENING", Conn);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SqlCommand cmd = new SqlCommand("insert into AA_STOCK_OPENING_TEMP (ITEMID,UnitID,Qty,UnitPrice,RMBValue,RMBUnitPrice) values ('" + dt.Rows[i]["ITEMID"].ToString() + "','UN-000050','" + dt.Rows[i]["Qty"].ToString() + "','" + dt.Rows[i]["UnitPrice"].ToString() + "','17','" + Convert.ToDouble(dt.Rows[i]["UnitPrice"].ToString()) / 17 + "')", Conn);
                Conn.Open();
                cmd.ExecuteNonQuery();
                Conn.Close();

            }

            SqlCommand cmdupdate = new SqlCommand("update AA_STOCK_OPENING_TEMP set RMBTotalPrice=Qty*RMBUnitPrice,TotalPrice=Qty*UnitPrice", Conn);
            Conn.Open();
            cmdupdate.ExecuteNonQuery();
            Conn.Close();

            SqlCommand cmdupdateDecimal = new SqlCommand("update AA_STOCK_OPENING_TEMP set TotalPrice=cast(TotalPrice as decimal(10,2)), RMBUnitPrice=cast(RMBUnitPrice as decimal(10,2)),RMBTotalPrice=cast(RMBTotalPrice as decimal(10,2))", Conn);
            Conn.Open();
            cmdupdateDecimal.ExecuteNonQuery();
            Conn.Close();
            //
            string rqID = "";
            string rqDt = "";

            ///////////////////////// ID GNERATOR /////////////////////////////
            string aa = LoadNUMBER("PR", "SP_MASTER", "SPID", Con);
            string[] idDT = aa.Split('`');
            rqID = idDT[0].ToString();
            rqDt = idDT[1].ToString();
            // rqHID = LoadHistoryID(rqID);

            SqlDataAdapter da2 = new SqlDataAdapter("select sum(RMBTotalPrice) AS RMBTotalPrice,sum(TotalPrice) AS TotalPrice from AA_STOCK_OPENING_TEMP", Conn);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {


                /////////////////////////// INSERT in SP MASTER //////////////////
                SqlParameter SPID = new SqlParameter("@SPID", rqID);
                SqlParameter SPDate = new SqlParameter("@SPDate", DateTime.Now);
                SqlParameter TotalAmount = new SqlParameter("@TotalAmount", dt2.Rows[0]["TotalPrice"].ToString());
                SqlParameter DiscountAmount = new SqlParameter("@DiscountAmount", '0');
                SqlParameter TaxAmount = new SqlParameter("@TaxAmount", '0');
                SqlParameter TaxRate = new SqlParameter("@TaxRate", '0');
                SqlParameter GrandTotal = new SqlParameter("@GrandTotal", dt2.Rows[0]["TotalPrice"].ToString());
                SqlParameter RMBAmount = new SqlParameter("@RMBAmount", dt2.Rows[0]["RMBTotalPrice"].ToString());
                SqlParameter AccountID = new SqlParameter("@AccountID", "89");
                SqlParameter SP = new SqlParameter("@SP", "P");
                SqlParameter CarryAmountPKR = new SqlParameter("@CarryAmountPKR", "0");
                SqlParameter CarryAmountRMB = new SqlParameter("@CarryAmountRMB", "0");
                SqlParameter PackingAmountPKR = new SqlParameter("@PackingAmountPKR", "0");
                SqlParameter PackingAmountRMB = new SqlParameter("@PackingAmountRMB", "0");
                SqlParameter ExChargeAmountPKR = new SqlParameter("@ExChargeAmountPKR", "0");
                SqlParameter ExChatgeAmountRMB = new SqlParameter("@ExChatgeAmountRMB", "0");
                SqlParameter SupplierAmountPKR = new SqlParameter("@SupplierAmountPKR", dt2.Rows[0]["TotalPrice"].ToString());
                SqlParameter SupplierAmountRMB = new SqlParameter("@SupplierAmountRMB", dt2.Rows[0]["RMBTotalPrice"].ToString());
                SqlParameter SalesDiscountPKR = new SqlParameter("@SalesDiscountPKR", '0');
                SqlParameter SalesDiscountRMB = new SqlParameter("@SalesDiscountRMB", '0');
                SqlParameter CusAmountPKR = new SqlParameter("@CusAmountPKR", "0");
                SqlParameter CusAmountRMB = new SqlParameter("@CusAmountRMB", "0");
                SqlParameter CarryID = new SqlParameter("@CarryID", "0");
                SqlParameter ExChargesID = new SqlParameter("@ExChargesID", "0");
                SqlParameter PackingID = new SqlParameter("@PackingID", "0");
                SqlParameter ShopName = new SqlParameter("@ShopName", "");
                SqlParameter CustomerName = new SqlParameter("@CustomerName", "");
                SqlParameter CreateBy = new SqlParameter("@CreateBy", "USR-000001");
                SqlParameter CustomerContactNo = new SqlParameter("@CustomerContactNo", "");
                SqlParameter CustomerAddress = new SqlParameter("@CustomerAddress", "");
                SqlParameter LocalBillNo = new SqlParameter("@LocalBillNo", "");
                AACommon.Execute("SP_MASTER_INSERT", Con, SPID, SPDate, TotalAmount, DiscountAmount, TaxAmount, TaxRate, GrandTotal, RMBAmount, AccountID, SP, CarryAmountPKR, CarryAmountRMB, PackingAmountPKR, PackingAmountRMB, ExChargeAmountPKR, ExChatgeAmountRMB, SupplierAmountPKR, SupplierAmountRMB, SalesDiscountPKR, SalesDiscountRMB, CusAmountPKR, CusAmountRMB, CarryID, ExChargesID, PackingID, ShopName, CustomerName, CreateBy, CustomerContactNo, CustomerAddress, LocalBillNo);


                /////////////////////// INSERT in SP DETAIL
                SqlDataAdapter da3 = new SqlDataAdapter("select * from AA_STOCK_OPENING_TEMP", Conn);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);
                if (dt3.Rows.Count > 0)
                {


                    for (int j = 0; j < dt3.Rows.Count; j++)
                    {
                        SqlParameter SPID_1 = new SqlParameter("@SPID", rqID);
                        SqlParameter ITEMID = new SqlParameter("@ITEMID", dt3.Rows[j]["ITEMID"].ToString());
                        SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
                        SqlParameter QtyIn = new SqlParameter("@QtyIn", dt3.Rows[j]["Qty"].ToString());
                        SqlParameter UnitPrice = new SqlParameter("@UnitPrice", dt3.Rows[j]["UnitPrice"].ToString());
                        SqlParameter TotalPrice = new SqlParameter("@TotalPrice", dt3.Rows[j]["TotalPrice"].ToString());
                        SqlParameter RMBValue = new SqlParameter("@RMBValue", dt3.Rows[j]["RMBValue"].ToString());
                        SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", dt3.Rows[j]["RMBUnitPrice"].ToString());
                        SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", dt3.Rows[j]["RMBTotalPrice"].ToString());
                        SqlParameter Type = new SqlParameter("@Type", "P");
                        SqlParameter Carry = new SqlParameter("@Carry", "0");
                        SqlParameter ExtraCharges = new SqlParameter("@ExtraCharges", "0");
                        SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", dt3.Rows[j]["UnitPrice"].ToString());
                        SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", dt3.Rows[j]["RMBUnitPrice"].ToString());
                        SqlParameter RMBPackingRate = new SqlParameter("@RMBPackingRate", "0");
                        SqlParameter RMBPackingPrice = new SqlParameter("@RMBPackingPrice", "0");
                        SqlParameter PKRPackingPrice = new SqlParameter("@PKRPackingPrice", "0");
                        SqlParameter SRNO = new SqlParameter("@SrNo", j);
                        SqlParameter CreateByNew = new SqlParameter("@CreateBy", "USR-000001");
                        AACommon.Execute("SP_DETAIL_INSERT", Con, SPID_1, ITEMID, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, Carry, ExtraCharges, BasicUnitPricePKR, BasicUnitPriceRMB, RMBPackingRate, RMBPackingPrice, PKRPackingPrice, SRNO, CreateByNew);

                    }
                }

                ////////////////////SAVE TRANSACTION IN ACCOUNTS ////////////////////

                double totafterdiscountPKR = Convert.ToDouble(dt2.Rows[0]["TotalPrice"].ToString());
                double totafterdiscountRMB = Convert.ToDouble(dt2.Rows[0]["RMBTotalPrice"].ToString());

                string Narration = "Opening Purchase against PO Number " + rqID + "";
                SqlCommand cmd = new SqlCommand("insert into tbl_transaction (Date,Narration,AmountPKR,AmountRMB,RMBValue,TaskID,CreateBy) values ('" + DateTime.Now + "','" + Narration + "','" + totafterdiscountPKR + "','" + totafterdiscountRMB + "','17','" + rqID + "','USR-000001')", Con);
                Con.Open();
                cmd.ExecuteNonQuery();
                Con.Close();

                ///PURCHASE ENTRY
                SqlCommand cmdPurchase1 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy) values ('" + DateTime.Now + "','49','PURCHASE','" + totafterdiscountPKR + "','" + totafterdiscountRMB + "','0','0','17','" + rqID + "','" + Narration + "','USR-000001')", Con);
                Con.Open();
                cmdPurchase1.ExecuteNonQuery();
                Con.Close();

                SqlCommand cmdPurchase2 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy) values ('" + DateTime.Now + "','89','PURCHASE','0','0','" + totafterdiscountPKR + "','" + totafterdiscountRMB + "','17','" + rqID + "','" + Narration + "','USR-000001')", Con);
                Con.Open();
                cmdPurchase2.ExecuteNonQuery();
                Con.Close();
            }
        }
    }


    public void GetLedgerBalance()
    {
        DataSet ds = AACommon.ReturnDatasetBySP("AA_Get_Ledger_Current_Balance", Con, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string query = "";
                if (Convert.ToDouble(ds.Tables[0].Rows[i]["Balance"].ToString()) > 0)
                {
                    query = "update Accounts set OpDebit='" + ds.Tables[0].Rows[i]["Balance"].ToString() + "', OpCredit='0' where AccountsID='" + ds.Tables[0].Rows[i]["AccountsID"].ToString() + "'";
                }
                else
                {
                    query = "update Accounts set OpCredit='" + ds.Tables[0].Rows[i]["Balance"].ToString() + "', OpDebit='0' where AccountsID='" + ds.Tables[0].Rows[i]["AccountsID"].ToString() + "'";
                }
                SqlCommand cmd = new SqlCommand(query, Con);
                Con.Open();
                cmd.ExecuteNonQuery();
                Con.Close();
                
            }
        }
    }



}
