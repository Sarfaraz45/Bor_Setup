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

public partial class ERP_ITM_Item : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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

    protected void Transfer(object sender, EventArgs e)
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        string rqID = "";
        string rqHID = "";
        string rqDt = "";
        //string rqBy = UserID;
        string rqAt = "";
        string stsID = "";// 0:Pending, 1:Approved, 2:Rejected
        string BranchID = "";
        string UserID = "";

        string rqIDPurchase = "";
        string rqHIDPurchase = "";
        string rqDtPurchase = "";
        string BranchIDPurchase = "";
        string UserIDPurchase = "";
        
        ///////////////////////// ID GNERATOR /////////////////////////////
        if (Request.Cookies["BranchID"] != null)
        {
            BranchID = Request.Cookies["BranchID"].Value;
        }

        if (Request.Cookies["UserID"] != null)
        {
            UserID = Request.Cookies["UserID"].Value;
        }


        string aa = LoadNUMBER("SO", "SP_MASTER", "SPID", Con, BranchID);
        string[] idDT = aa.Split('`');
        rqID = idDT[0].ToString();
        rqDt = idDT[1].ToString();
        double txtTotAmount = 0;
        double txtGrandTotal = 0;
        double txtTotAmountRMB = 0;
        double UnitPirce = 0;



        if (Convert.ToInt32(txtQty1.Value) > 0)
        {
            string itmvalue = ddlItemFrom1.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            txtTotAmount = txtTotAmount + (Convert.ToDouble(PurchasePrice) * Convert.ToDouble(txtQty1.Value));

        }


        if (Convert.ToInt32(txtQty2.Value) > 0)
        {
            string itmvalue = ddlItemFrom2.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            txtTotAmount = txtTotAmount + (Convert.ToDouble(PurchasePrice) * Convert.ToDouble(txtQty2.Value));
        }

        if (Convert.ToInt32(txtQty3.Value) > 0)
        {
            string itmvalue = ddlItemFrom3.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            txtTotAmount = txtTotAmount + (Convert.ToDouble(PurchasePrice) * Convert.ToDouble(txtQty3.Value));

        }


        if (Convert.ToInt32(txtQty4.Value) > 0)
        {
            string itmvalue = ddlItemFrom4.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            txtTotAmount = txtTotAmount + (Convert.ToDouble(PurchasePrice) * Convert.ToDouble(txtQty4.Value));
        }

        if (Convert.ToInt32(txtQty5.Value) > 0)
        {
            string itmvalue = ddlItemFrom5.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            txtTotAmount = txtTotAmount + (Convert.ToDouble(PurchasePrice) * Convert.ToDouble(txtQty5.Value));

        }


        if (Convert.ToInt32(txtQty6.Value) > 0)
        {
            string itmvalue = ddlItemFrom6.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            txtTotAmount = txtTotAmount + (Convert.ToDouble(PurchasePrice) * Convert.ToDouble(txtQty6.Value));
        }

        if (Convert.ToInt32(txtQty7.Value) > 0)
        {
            string itmvalue = ddlItemFrom7.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            txtTotAmount = txtTotAmount + (Convert.ToDouble(PurchasePrice) * Convert.ToDouble(txtQty7.Value));
        }

        if (Convert.ToInt32(txtQty8.Value) > 0)
        {
            string itmvalue = ddlItemFrom8.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            txtTotAmount = txtTotAmount + (Convert.ToDouble(PurchasePrice) * Convert.ToDouble(txtQty8.Value));
        }

        if (Convert.ToInt32(txtQty9.Value) > 0)
        {
            string itmvalue = ddlItemFrom9.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            txtTotAmount = txtTotAmount + (Convert.ToDouble(PurchasePrice) * Convert.ToDouble(txtQty9.Value));
        }

        if (Convert.ToInt32(txtQty10.Value) > 0)
        {
            string itmvalue = ddlItemFrom10.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            txtTotAmount = txtTotAmount + (Convert.ToDouble(PurchasePrice) * Convert.ToDouble(txtQty10.Value));
        }

        txtGrandTotal = txtTotAmount;
        txtTotAmountRMB = txtTotAmount / Convert.ToDouble(hdnRMBValue.Value);
        ///////////////////////// INSERT in SP MASTER //////////////////
        SqlParameter SPID = new SqlParameter("@SPID", rqID);
        SqlParameter SPDate = new SqlParameter("@SPDate", StartDate.Value);
        SqlParameter TotalAmount = new SqlParameter("@TotalAmount", txtTotAmount);
        SqlParameter DiscountAmount = new SqlParameter("@DiscountAmount", "0");
        SqlParameter TaxAmount = new SqlParameter("@TaxAmount", "0");
        SqlParameter TaxRate = new SqlParameter("@TaxRate", "0");
        SqlParameter GrandTotal = new SqlParameter("@GrandTotal", txtGrandTotal);
        SqlParameter RMBAmount = new SqlParameter("@RMBAmount", txtTotAmountRMB);
        SqlParameter AccountID = new SqlParameter("@AccountID", ddlTransferFromAccount.SelectedValue);
        SqlParameter SP = new SqlParameter("@SP", "S");
        SqlParameter CarryAmountPKR = new SqlParameter("@CarryAmountPKR", "0");
        SqlParameter CarryAmountRMB = new SqlParameter("@CarryAmountRMB", "0");
        SqlParameter PackingAmountPKR = new SqlParameter("@PackingAmountPKR", "0");
        SqlParameter PackingAmountRMB = new SqlParameter("@PackingAmountRMB", "0");
        SqlParameter ExChargeAmountPKR = new SqlParameter("@ExChargeAmountPKR", "0");
        SqlParameter ExChatgeAmountRMB = new SqlParameter("@ExChatgeAmountRMB", "0");
        SqlParameter SupplierAmountPKR = new SqlParameter("@SupplierAmountPKR", "0");
        SqlParameter SupplierAmountRMB = new SqlParameter("@SupplierAmountRMB", "0");

        SqlParameter SalesDiscountPKR = new SqlParameter("@SalesDiscountPKR", "0");
        SqlParameter SalesDiscountRMB = new SqlParameter("@SalesDiscountRMB", "0");
        SqlParameter CusAmountPKR = new SqlParameter("@CusAmountPKR", txtTotAmount);
        SqlParameter CusAmountRMB = new SqlParameter("@CusAmountRMB", txtTotAmountRMB);

        SqlParameter CarryID = new SqlParameter("@CarryID", "0");
        SqlParameter ExChargesID = new SqlParameter("@ExChargesID", "0");
        SqlParameter PackingID = new SqlParameter("@PackingID", "0");
        SqlParameter ShopName = new SqlParameter("@ShopName", "");
        SqlParameter CustomerName = new SqlParameter("@CustomerName", "");
        SqlParameter CreateBy = new SqlParameter("@CreateBy", UserID);

        SqlParameter CustomerContactNo = new SqlParameter("@CustomerContactNo", "");
        SqlParameter CustomerAddress = new SqlParameter("@CustomerAddress", txtRemarksFrom.Value);
        SqlParameter LocalBillNo = new SqlParameter("@LocalBillNo", txtBillFrom.Value);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        AACommon.Execute("SP_MASTER_INSERT", Con, SPID, SPDate, TotalAmount, DiscountAmount, TaxAmount, TaxRate, GrandTotal, RMBAmount, AccountID, SP, CarryAmountPKR, CarryAmountRMB, PackingAmountPKR, PackingAmountRMB, ExChargeAmountPKR, ExChatgeAmountRMB, SupplierAmountPKR, SupplierAmountRMB, SalesDiscountPKR, SalesDiscountRMB, CusAmountPKR, CusAmountRMB, CarryID, ExChargesID, PackingID, ShopName, CustomerName, CreateBy, CustomerContactNo, CustomerAddress, LocalBillNo, Branch);


        if (Convert.ToInt32(txtQty1.Value) > 0)
        {
            string itmvalue = ddlItemFrom1.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            SqlParameter SPID_1 = new SqlParameter("@SPID", rqID);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ITEMID);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty1.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty1.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty1.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "S");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter SRNO = new SqlParameter("@SrNo", "1");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchID);
            AACommon.Execute("SP_DETAIL_INSERT_2", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, BasicUnitPricePKR, BasicUnitPriceRMB, SRNO, CreateByNew, BranchP);


            string PRID = PurchaseSPID;
            string PRDID = PurchaseSPDetailID;
            SqlDataAdapter da = new SqlDataAdapter("select * from  SP_DETAIL where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                double AvailedQty = Convert.ToDouble(dt.Rows[0]["QtyAvailed"].ToString());
                double Qty = Convert.ToDouble(txtQty1.Value);
                AvailedQty = AvailedQty + Qty;
                SqlCommand cmd = new SqlCommand("update SP_DETAIL set QtyAvailed='" + AvailedQty + "' where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
                if (Con.State != ConnectionState.Open)
                {
                    Con.Open();
                }
                //Con.Open();
                cmd.ExecuteNonQuery();
                Con.Close();

                SqlDataAdapter da1 = new SqlDataAdapter("select MAX(ID) from  SP_DETAIL where SPID='" + rqID + "' and ITEMID='" + ITEMID + "'", Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("update SP_DETAIL set BatchID='" + PRDID + "' where ID='" + dt1.Rows[0][0].ToString() + "'", Con);
                    if (Con.State != ConnectionState.Open)
                    {
                        Con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    Con.Close();
                }
            }

        }


        if (Convert.ToInt32(txtQty2.Value) > 0)
        {
            string itmvalue = ddlItemFrom2.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");


            SqlParameter SPID_1 = new SqlParameter("@SPID", rqID);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ITEMID);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty2.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty2.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty2.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "S");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter SRNO = new SqlParameter("@SrNo", "2");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchID);
            AACommon.Execute("SP_DETAIL_INSERT_2", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, BasicUnitPricePKR, BasicUnitPriceRMB, SRNO, CreateByNew, BranchP);


            string PRID = PurchaseSPID;
            string PRDID = PurchaseSPDetailID;
            SqlDataAdapter da = new SqlDataAdapter("select * from  SP_DETAIL where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                double AvailedQty = Convert.ToDouble(dt.Rows[0]["QtyAvailed"].ToString());
                double Qty = Convert.ToDouble(txtQty2.Value);
                AvailedQty = AvailedQty + Qty;
                SqlCommand cmd = new SqlCommand("update SP_DETAIL set QtyAvailed='" + AvailedQty + "' where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
                if (Con.State != ConnectionState.Open)
                {
                    Con.Open();
                }
                cmd.ExecuteNonQuery();
                Con.Close();

                SqlDataAdapter da1 = new SqlDataAdapter("select MAX(ID) from  SP_DETAIL where SPID='" + rqID + "' and ITEMID='" + ITEMID + "'", Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("update SP_DETAIL set BatchID='" + PRDID + "' where ID='" + dt1.Rows[0][0].ToString() + "'", Con);
                    if (Con.State != ConnectionState.Open)
                    {
                        Con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    Con.Close();
                }
            }

        }

        if (Convert.ToInt32(txtQty3.Value) > 0)
        {
            string itmvalue = ddlItemFrom3.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");



            SqlParameter SPID_1 = new SqlParameter("@SPID", rqID);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ITEMID);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty3.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty3.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty3.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "S");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter SRNO = new SqlParameter("@SrNo", "3");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchID);
            AACommon.Execute("SP_DETAIL_INSERT_2", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, BasicUnitPricePKR, BasicUnitPriceRMB, SRNO, CreateByNew, BranchP);


            string PRID = PurchaseSPID;
            string PRDID = PurchaseSPDetailID;
            SqlDataAdapter da = new SqlDataAdapter("select * from  SP_DETAIL where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                double AvailedQty = Convert.ToDouble(dt.Rows[0]["QtyAvailed"].ToString());
                double Qty = Convert.ToDouble(txtQty2.Value);
                AvailedQty = AvailedQty + Qty;
                SqlCommand cmd = new SqlCommand("update SP_DETAIL set QtyAvailed='" + AvailedQty + "' where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
                if (Con.State != ConnectionState.Open)
                {
                    Con.Open();
                }
                cmd.ExecuteNonQuery();
                Con.Close();

                SqlDataAdapter da1 = new SqlDataAdapter("select MAX(ID) from  SP_DETAIL where SPID='" + rqID + "' and ITEMID='" + ITEMID + "'", Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("update SP_DETAIL set BatchID='" + PRDID + "' where ID='" + dt1.Rows[0][0].ToString() + "'", Con);
                    if (Con.State != ConnectionState.Open)
                    {
                        Con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    Con.Close();
                }
            }

        }


        if (Convert.ToInt32(txtQty4.Value) > 0)
        {
            string itmvalue = ddlItemFrom4.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            SqlParameter SPID_1 = new SqlParameter("@SPID", rqID);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ITEMID);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty4.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty4.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty4.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "S");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter SRNO = new SqlParameter("@SrNo", "4");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchID);
            AACommon.Execute("SP_DETAIL_INSERT_2", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, BasicUnitPricePKR, BasicUnitPriceRMB, SRNO, CreateByNew, BranchP);


            string PRID = PurchaseSPID;
            string PRDID = PurchaseSPDetailID;
            SqlDataAdapter da = new SqlDataAdapter("select * from  SP_DETAIL where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                double AvailedQty = Convert.ToDouble(dt.Rows[0]["QtyAvailed"].ToString());
                double Qty = Convert.ToDouble(txtQty4.Value);
                AvailedQty = AvailedQty + Qty;
                SqlCommand cmd = new SqlCommand("update SP_DETAIL set QtyAvailed='" + AvailedQty + "' where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
                if (Con.State != ConnectionState.Open)
                {
                    Con.Open();
                }
                cmd.ExecuteNonQuery();
                Con.Close();

                SqlDataAdapter da1 = new SqlDataAdapter("select MAX(ID) from  SP_DETAIL where SPID='" + rqID + "' and ITEMID='" + ITEMID + "'", Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("update SP_DETAIL set BatchID='" + PRDID + "' where ID='" + dt1.Rows[0][0].ToString() + "'", Con);
                    if (Con.State != ConnectionState.Open)
                    {
                        Con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    Con.Close();
                }
            }

          
        }

        if (Convert.ToInt32(txtQty5.Value) > 0)
        {
            string itmvalue = ddlItemFrom5.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");


            SqlParameter SPID_1 = new SqlParameter("@SPID", rqID);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ITEMID);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty5.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty5.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty5.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "S");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter SRNO = new SqlParameter("@SrNo", "5");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchID);
            AACommon.Execute("SP_DETAIL_INSERT_2", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, BasicUnitPricePKR, BasicUnitPriceRMB, SRNO, CreateByNew, BranchP);


            string PRID = PurchaseSPID;
            string PRDID = PurchaseSPDetailID;
            SqlDataAdapter da = new SqlDataAdapter("select * from  SP_DETAIL where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                double AvailedQty = Convert.ToDouble(dt.Rows[0]["QtyAvailed"].ToString());
                double Qty = Convert.ToDouble(txtQty5.Value);
                AvailedQty = AvailedQty + Qty;
                SqlCommand cmd = new SqlCommand("update SP_DETAIL set QtyAvailed='" + AvailedQty + "' where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
                if (Con.State != ConnectionState.Open)
                {
                    Con.Open();
                }
                cmd.ExecuteNonQuery();
                Con.Close();

                SqlDataAdapter da1 = new SqlDataAdapter("select MAX(ID) from  SP_DETAIL where SPID='" + rqID + "' and ITEMID='" + ITEMID + "'", Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("update SP_DETAIL set BatchID='" + PRDID + "' where ID='" + dt1.Rows[0][0].ToString() + "'", Con);
                    if (Con.State != ConnectionState.Open)
                    {
                        Con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    Con.Close();
                }
            }


        }


        if (Convert.ToInt32(txtQty6.Value) > 0)
        {
            string itmvalue = ddlItemFrom6.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");


            SqlParameter SPID_1 = new SqlParameter("@SPID", rqID);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ITEMID);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty6.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty6.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty6.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "S");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter SRNO = new SqlParameter("@SrNo", "6");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchID);
            AACommon.Execute("SP_DETAIL_INSERT_2", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, BasicUnitPricePKR, BasicUnitPriceRMB, SRNO, CreateByNew, BranchP);


            string PRID = PurchaseSPID;
            string PRDID = PurchaseSPDetailID;
            SqlDataAdapter da = new SqlDataAdapter("select * from  SP_DETAIL where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                double AvailedQty = Convert.ToDouble(dt.Rows[0]["QtyAvailed"].ToString());
                double Qty = Convert.ToDouble(txtQty6.Value);
                AvailedQty = AvailedQty + Qty;
                SqlCommand cmd = new SqlCommand("update SP_DETAIL set QtyAvailed='" + AvailedQty + "' where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
                if (Con.State != ConnectionState.Open)
                {
                    Con.Open();
                }
                cmd.ExecuteNonQuery();
                Con.Close();

                SqlDataAdapter da1 = new SqlDataAdapter("select MAX(ID) from  SP_DETAIL where SPID='" + rqID + "' and ITEMID='" + ITEMID + "'", Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("update SP_DETAIL set BatchID='" + PRDID + "' where ID='" + dt1.Rows[0][0].ToString() + "'", Con);
                    if (Con.State != ConnectionState.Open)
                    {
                        Con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    Con.Close();
                }
            }

        }

        if (Convert.ToInt32(txtQty7.Value) > 0)
        {
            string itmvalue = ddlItemFrom7.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");


            SqlParameter SPID_1 = new SqlParameter("@SPID", rqID);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ITEMID);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty7.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty7.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty7.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "S");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter SRNO = new SqlParameter("@SrNo", "7");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchID);
            AACommon.Execute("SP_DETAIL_INSERT_2", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, BasicUnitPricePKR, BasicUnitPriceRMB, SRNO, CreateByNew, BranchP);


            string PRID = PurchaseSPID;
            string PRDID = PurchaseSPDetailID;
            SqlDataAdapter da = new SqlDataAdapter("select * from  SP_DETAIL where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                double AvailedQty = Convert.ToDouble(dt.Rows[0]["QtyAvailed"].ToString());
                double Qty = Convert.ToDouble(txtQty7.Value);
                AvailedQty = AvailedQty + Qty;
                SqlCommand cmd = new SqlCommand("update SP_DETAIL set QtyAvailed='" + AvailedQty + "' where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
                if (Con.State != ConnectionState.Open)
                {
                    Con.Open();
                }
                cmd.ExecuteNonQuery();
                Con.Close();

                SqlDataAdapter da1 = new SqlDataAdapter("select MAX(ID) from  SP_DETAIL where SPID='" + rqID + "' and ITEMID='" + ITEMID + "'", Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("update SP_DETAIL set BatchID='" + PRDID + "' where ID='" + dt1.Rows[0][0].ToString() + "'", Con);
                    if (Con.State != ConnectionState.Open)
                    {
                        Con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    Con.Close();
                }
            }

        }

        if (Convert.ToInt32(txtQty8.Value) > 0)
        {
            string itmvalue = ddlItemFrom8.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");


            SqlParameter SPID_1 = new SqlParameter("@SPID", rqID);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ITEMID);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty8.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty8.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty8.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "S");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter SRNO = new SqlParameter("@SrNo", "8");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchID);
            AACommon.Execute("SP_DETAIL_INSERT_2", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, BasicUnitPricePKR, BasicUnitPriceRMB, SRNO, CreateByNew, BranchP);


            string PRID = PurchaseSPID;
            string PRDID = PurchaseSPDetailID;
            SqlDataAdapter da = new SqlDataAdapter("select * from  SP_DETAIL where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                double AvailedQty = Convert.ToDouble(dt.Rows[0]["QtyAvailed"].ToString());
                double Qty = Convert.ToDouble(txtQty8.Value);
                AvailedQty = AvailedQty + Qty;
                SqlCommand cmd = new SqlCommand("update SP_DETAIL set QtyAvailed='" + AvailedQty + "' where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
                if (Con.State != ConnectionState.Open)
                {
                    Con.Open();
                }
                cmd.ExecuteNonQuery();
                Con.Close();

                SqlDataAdapter da1 = new SqlDataAdapter("select MAX(ID) from  SP_DETAIL where SPID='" + rqID + "' and ITEMID='" + ITEMID + "'", Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("update SP_DETAIL set BatchID='" + PRDID + "' where ID='" + dt1.Rows[0][0].ToString() + "'", Con);
                    if (Con.State != ConnectionState.Open)
                    {
                        Con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    Con.Close();
                }
            }

        }

        if (Convert.ToInt32(txtQty9.Value) > 0)
        {
            string itmvalue = ddlItemFrom9.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");


            SqlParameter SPID_1 = new SqlParameter("@SPID", rqID);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ITEMID);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty9.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty9.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty9.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "S");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter SRNO = new SqlParameter("@SrNo", "9");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchID);
            AACommon.Execute("SP_DETAIL_INSERT_2", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, BasicUnitPricePKR, BasicUnitPriceRMB, SRNO, CreateByNew, BranchP);


            string PRID = PurchaseSPID;
            string PRDID = PurchaseSPDetailID;
            SqlDataAdapter da = new SqlDataAdapter("select * from  SP_DETAIL where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                double AvailedQty = Convert.ToDouble(dt.Rows[0]["QtyAvailed"].ToString());
                double Qty = Convert.ToDouble(txtQty9.Value);
                AvailedQty = AvailedQty + Qty;
                SqlCommand cmd = new SqlCommand("update SP_DETAIL set QtyAvailed='" + AvailedQty + "' where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
                if (Con.State != ConnectionState.Open)
                {
                    Con.Open();
                }
                cmd.ExecuteNonQuery();
                Con.Close();

                SqlDataAdapter da1 = new SqlDataAdapter("select MAX(ID) from  SP_DETAIL where SPID='" + rqID + "' and ITEMID='" + ITEMID + "'", Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("update SP_DETAIL set BatchID='" + PRDID + "' where ID='" + dt1.Rows[0][0].ToString() + "'", Con);
                    if (Con.State != ConnectionState.Open)
                    {
                        Con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    Con.Close();
                }
            }

        }

        if (Convert.ToInt32(txtQty10.Value) > 0)
        {
            string itmvalue = ddlItemFrom10.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");


            SqlParameter SPID_1 = new SqlParameter("@SPID", rqID);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ITEMID);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty10.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty10.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty10.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "S");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter SRNO = new SqlParameter("@SrNo", "10");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchID);
            AACommon.Execute("SP_DETAIL_INSERT_2", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, BasicUnitPricePKR, BasicUnitPriceRMB, SRNO, CreateByNew, BranchP);


            string PRID = PurchaseSPID;
            string PRDID = PurchaseSPDetailID;
            SqlDataAdapter da = new SqlDataAdapter("select * from  SP_DETAIL where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                double AvailedQty = Convert.ToDouble(dt.Rows[0]["QtyAvailed"].ToString());
                double Qty = Convert.ToDouble(txtQty10.Value);
                AvailedQty = AvailedQty + Qty;
                SqlCommand cmd = new SqlCommand("update SP_DETAIL set QtyAvailed='" + AvailedQty + "' where SPID='" + PRID + "' and ITEMID='" + ITEMID + "' and ID='" + PRDID + "'", Con);
                if (Con.State != ConnectionState.Open)
                {
                    Con.Open();
                }
                cmd.ExecuteNonQuery();
                Con.Close();

                SqlDataAdapter da1 = new SqlDataAdapter("select MAX(ID) from  SP_DETAIL where SPID='" + rqID + "' and ITEMID='" + ITEMID + "'", Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("update SP_DETAIL set BatchID='" + PRDID + "' where ID='" + dt1.Rows[0][0].ToString() + "'", Con);
                    if (Con.State != ConnectionState.Open)
                    {
                        Con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    Con.Close();
                }
            }

        }



        ////////////////////SAVE TRANSACTION IN ACCOUNTS ////////////////////

        string Narration = "Invoice By SO # " + rqID + "";
        string NarrationTransfer = "Invoice (Transfer) By SO # " + rqID + "";
        SqlCommand cmd2 = new SqlCommand("insert into tbl_transaction (Date,Narration,AmountPKR,AmountRMB,RMBValue,TaskID,CreateBy,BranchID) values ('" + StartDate.Value + "','" + NarrationTransfer + "','" + txtTotAmount + "','" + txtTotAmountRMB + "','" + hdnRMBValue.Value + "','" + rqID + "','" + UserID + "','" + BranchID + "')", Con);
        if (Con.State != ConnectionState.Open)
        {
            Con.Open();
        }
        cmd2.ExecuteNonQuery();
        Con.Close();

        string SaleRevenuAccount = "";        
        SqlDataAdapter daPurchase = new SqlDataAdapter("select AccountsID from Accounts where BranchID='" + BranchID + "' and AccountsTitle='Sales Revenue'", Con);
        DataTable dtPurchase = new DataTable();
        daPurchase.Fill(dtPurchase);
        if (dtPurchase.Rows.Count > 0)
        {
            SaleRevenuAccount = dtPurchase.Rows[0][0].ToString();
        }

        ///PURCHASE ENTRY
        SqlCommand cmdPurchase1 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + StartDate.Value + "','" + ddlTransferFromAccount.SelectedValue + "','SALE','" + txtTotAmount + "','" + txtTotAmountRMB + "','0','0','" + hdnRMBValue.Value + "','" + rqID + "','" + NarrationTransfer + "','" + UserID + "','" + BranchID + "')", Con);
        if (Con.State != ConnectionState.Open)
        {
            Con.Open();
        }
        cmdPurchase1.ExecuteNonQuery();
        Con.Close();
       

        Narration = "Invoice By SO # " + rqID + "";
        NarrationTransfer = "Invoice (Transfer) By SO # " + rqID + "";
        SqlCommand cmdPurchase2 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + StartDate.Value + "','" + SaleRevenuAccount + "','SALE','0','0','" + txtTotAmount + "','" + txtTotAmountRMB + "','" + hdnRMBValue.Value + "','" + rqID + "','" + NarrationTransfer + "','" + UserID + "','" + BranchID + "')", Con);
        if (Con.State != ConnectionState.Open)
        {
            Con.Open();
        }
        cmdPurchase2.ExecuteNonQuery();
        Con.Close();
        ///PURCHASE ENTRY





























        BranchIDPurchase = ddlTransferToBranch.SelectedValue;
        string aaPurchase = LoadNUMBER("PR", "SP_MASTER", "SPID", Con, BranchIDPurchase);
        string[] idDTPurchase = aaPurchase.Split('`');
        rqIDPurchase = idDTPurchase[0].ToString();
        rqDtPurchase = idDTPurchase[1].ToString();


        /////////////////////////// INSERT in SP MASTER //////////////////
        SqlParameter SPIDPurchase = new SqlParameter("@SPID", rqIDPurchase);
        SqlParameter SPDatePurchase = new SqlParameter("@SPDate", StartDate.Value);
        SqlParameter TotalAmountPurchase = new SqlParameter("@TotalAmount", txtTotAmount);
        SqlParameter DiscountAmountPurchase = new SqlParameter("@DiscountAmount", "0");
        SqlParameter TaxAmountPurchase = new SqlParameter("@TaxAmount", "0");
        SqlParameter TaxRatePurchase = new SqlParameter("@TaxRate", "0");
        SqlParameter GrandTotalPurchase = new SqlParameter("@GrandTotal", txtGrandTotal);
        SqlParameter RMBAmountPurchase = new SqlParameter("@RMBAmount", txtTotAmountRMB);
        SqlParameter AccountIDPurchase = new SqlParameter("@AccountID", ddlTransferToAccount.SelectedValue);
        SqlParameter SPPurchase = new SqlParameter("@SP", "P");

        SqlParameter CarryAmountPKRPurchase = new SqlParameter("@CarryAmountPKR", "0");
        SqlParameter CarryAmountRMBPurchase = new SqlParameter("@CarryAmountRMB", "0");
        SqlParameter PackingAmountPKRPurchase = new SqlParameter("@PackingAmountPKR", "0");
        SqlParameter PackingAmountRMBPurchase = new SqlParameter("@PackingAmountRMB", "0");
        SqlParameter ExChargeAmountPKRPurchase = new SqlParameter("@ExChargeAmountPKR", "0");
        SqlParameter ExChatgeAmountRMBPurchase = new SqlParameter("@ExChatgeAmountRMB", "0");
        SqlParameter SupplierAmountPKRPurchase = new SqlParameter("@SupplierAmountPKR", txtTotAmount);
        SqlParameter SupplierAmountRMBPurchase = new SqlParameter("@SupplierAmountRMB", txtTotAmountRMB);
        SqlParameter SalesDiscountPKRPurchase = new SqlParameter("@SalesDiscountPKR", "0");
        SqlParameter SalesDiscountRMBPurchase = new SqlParameter("@SalesDiscountRMB", "0");
        SqlParameter CusAmountPKRPurchase = new SqlParameter("@CusAmountPKR", "0");
        SqlParameter CusAmountRMBPurchase = new SqlParameter("@CusAmountRMB", "0");
        SqlParameter CarryIDPurchase = new SqlParameter("@CarryID", "0");
        SqlParameter ExChargesIDPurchase = new SqlParameter("@ExChargesID", "0");
        SqlParameter PackingIDPurchase = new SqlParameter("@PackingID", "0");
        SqlParameter ShopNamePurchase = new SqlParameter("@ShopName", "");
        SqlParameter CustomerNamePurchase = new SqlParameter("@CustomerName", "");
        SqlParameter CreateByPurchase = new SqlParameter("@CreateBy", UserID);

        SqlParameter CustomerContactNoPurchase = new SqlParameter("@CustomerContactNo", "");
        SqlParameter CustomerAddressPurchase = new SqlParameter("@CustomerAddress", txtRemarksTo.Value);
        SqlParameter LocalBillNoPurchase = new SqlParameter("@LocalBillNo", txtBillTo.Value);
        SqlParameter BranchPurchase = new SqlParameter("@BranchID", BranchIDPurchase);
        AACommon.Execute("SP_MASTER_INSERT", Con, SPIDPurchase, SPDatePurchase, TotalAmountPurchase, DiscountAmountPurchase, TaxAmountPurchase, TaxRatePurchase, GrandTotalPurchase, RMBAmountPurchase, AccountIDPurchase, SPPurchase, CarryAmountPKRPurchase, CarryAmountRMBPurchase, PackingAmountPKRPurchase, PackingAmountRMBPurchase, ExChargeAmountPKRPurchase, ExChatgeAmountRMBPurchase, SupplierAmountPKRPurchase, SupplierAmountRMBPurchase, SalesDiscountPKRPurchase, SalesDiscountRMBPurchase, CusAmountPKRPurchase, CusAmountRMBPurchase, CarryIDPurchase, ExChargesIDPurchase, PackingIDPurchase, ShopNamePurchase, CustomerNamePurchase, CreateByPurchase, CustomerContactNoPurchase, CustomerAddressPurchase, LocalBillNoPurchase, BranchPurchase);



        if (Convert.ToInt32(txtQty1.Value) > 0)
        {
            string itmvalue = ddlItemFrom1.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            SqlParameter SPID_1 = new SqlParameter("@SPID", rqIDPurchase);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ddlItemTo1.SelectedValue);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty1.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty1.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty1.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "P");
            SqlParameter Carry = new SqlParameter("@Carry", "0");
            SqlParameter ExtraCharges = new SqlParameter("@ExtraCharges", "0");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBPackingRate = new SqlParameter("@RMBPackingRate", "0");
            SqlParameter RMBPackingPrice = new SqlParameter("@RMBPackingPrice", "0");
            SqlParameter PKRPackingPrice = new SqlParameter("@PKRPackingPrice", "0");
            SqlParameter SRNO = new SqlParameter("@SrNo", "1");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchIDPurchase);
            AACommon.Execute("SP_DETAIL_INSERT", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, Carry, ExtraCharges, BasicUnitPricePKR, BasicUnitPriceRMB, RMBPackingRate, RMBPackingPrice, PKRPackingPrice, SRNO, CreateByNew, BranchP);

        }

        if (Convert.ToInt32(txtQty2.Value) > 0)
        {
            string itmvalue = ddlItemFrom2.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            SqlParameter SPID_1 = new SqlParameter("@SPID", rqIDPurchase);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ddlItemTo2.SelectedValue);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty2.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty2.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty2.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "P");
            SqlParameter Carry = new SqlParameter("@Carry", "0");
            SqlParameter ExtraCharges = new SqlParameter("@ExtraCharges", "0");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBPackingRate = new SqlParameter("@RMBPackingRate", "0");
            SqlParameter RMBPackingPrice = new SqlParameter("@RMBPackingPrice", "0");
            SqlParameter PKRPackingPrice = new SqlParameter("@PKRPackingPrice", "0");
            SqlParameter SRNO = new SqlParameter("@SrNo", "2");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchIDPurchase);
            AACommon.Execute("SP_DETAIL_INSERT", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, Carry, ExtraCharges, BasicUnitPricePKR, BasicUnitPriceRMB, RMBPackingRate, RMBPackingPrice, PKRPackingPrice, SRNO, CreateByNew, BranchP);

        }
        if (Convert.ToInt32(txtQty3.Value) > 0)
        {
            string itmvalue = ddlItemFrom3.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            SqlParameter SPID_1 = new SqlParameter("@SPID", rqIDPurchase);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ddlItemTo3.SelectedValue);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty3.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty3.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty3.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "P");
            SqlParameter Carry = new SqlParameter("@Carry", "0");
            SqlParameter ExtraCharges = new SqlParameter("@ExtraCharges", "0");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBPackingRate = new SqlParameter("@RMBPackingRate", "0");
            SqlParameter RMBPackingPrice = new SqlParameter("@RMBPackingPrice", "0");
            SqlParameter PKRPackingPrice = new SqlParameter("@PKRPackingPrice", "0");
            SqlParameter SRNO = new SqlParameter("@SrNo", "3");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchIDPurchase);
            AACommon.Execute("SP_DETAIL_INSERT", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, Carry, ExtraCharges, BasicUnitPricePKR, BasicUnitPriceRMB, RMBPackingRate, RMBPackingPrice, PKRPackingPrice, SRNO, CreateByNew, BranchP);

        }

        if (Convert.ToInt32(txtQty4.Value) > 0)
        {
            string itmvalue = ddlItemFrom4.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            SqlParameter SPID_1 = new SqlParameter("@SPID", rqIDPurchase);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ddlItemTo4.SelectedValue);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty4.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty4.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty4.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "P");
            SqlParameter Carry = new SqlParameter("@Carry", "0");
            SqlParameter ExtraCharges = new SqlParameter("@ExtraCharges", "0");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBPackingRate = new SqlParameter("@RMBPackingRate", "0");
            SqlParameter RMBPackingPrice = new SqlParameter("@RMBPackingPrice", "0");
            SqlParameter PKRPackingPrice = new SqlParameter("@PKRPackingPrice", "0");
            SqlParameter SRNO = new SqlParameter("@SrNo", "4");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchIDPurchase);
            AACommon.Execute("SP_DETAIL_INSERT", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, Carry, ExtraCharges, BasicUnitPricePKR, BasicUnitPriceRMB, RMBPackingRate, RMBPackingPrice, PKRPackingPrice, SRNO, CreateByNew, BranchP);

        }

        if (Convert.ToInt32(txtQty5.Value) > 0)
        {
            string itmvalue = ddlItemFrom5.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            SqlParameter SPID_1 = new SqlParameter("@SPID", rqIDPurchase);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ddlItemTo5.SelectedValue);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty5.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty5.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty5.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "P");
            SqlParameter Carry = new SqlParameter("@Carry", "0");
            SqlParameter ExtraCharges = new SqlParameter("@ExtraCharges", "0");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBPackingRate = new SqlParameter("@RMBPackingRate", "0");
            SqlParameter RMBPackingPrice = new SqlParameter("@RMBPackingPrice", "0");
            SqlParameter PKRPackingPrice = new SqlParameter("@PKRPackingPrice", "0");
            SqlParameter SRNO = new SqlParameter("@SrNo", "5");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchIDPurchase);
            AACommon.Execute("SP_DETAIL_INSERT", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, Carry, ExtraCharges, BasicUnitPricePKR, BasicUnitPriceRMB, RMBPackingRate, RMBPackingPrice, PKRPackingPrice, SRNO, CreateByNew, BranchP);

        }

        if (Convert.ToInt32(txtQty6.Value) > 0)
        {
            string itmvalue = ddlItemFrom6.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            SqlParameter SPID_1 = new SqlParameter("@SPID", rqIDPurchase);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ddlItemTo6.SelectedValue);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty6.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty6.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty6.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "P");
            SqlParameter Carry = new SqlParameter("@Carry", "0");
            SqlParameter ExtraCharges = new SqlParameter("@ExtraCharges", "0");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBPackingRate = new SqlParameter("@RMBPackingRate", "0");
            SqlParameter RMBPackingPrice = new SqlParameter("@RMBPackingPrice", "0");
            SqlParameter PKRPackingPrice = new SqlParameter("@PKRPackingPrice", "0");
            SqlParameter SRNO = new SqlParameter("@SrNo", "6");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchIDPurchase);
            AACommon.Execute("SP_DETAIL_INSERT", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, Carry, ExtraCharges, BasicUnitPricePKR, BasicUnitPriceRMB, RMBPackingRate, RMBPackingPrice, PKRPackingPrice, SRNO, CreateByNew, BranchP);

        }

        if (Convert.ToInt32(txtQty7.Value) > 0)
        {
            string itmvalue = ddlItemFrom7.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            SqlParameter SPID_1 = new SqlParameter("@SPID", rqIDPurchase);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ddlItemTo7.SelectedValue);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty7.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty7.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty7.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "P");
            SqlParameter Carry = new SqlParameter("@Carry", "0");
            SqlParameter ExtraCharges = new SqlParameter("@ExtraCharges", "0");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBPackingRate = new SqlParameter("@RMBPackingRate", "0");
            SqlParameter RMBPackingPrice = new SqlParameter("@RMBPackingPrice", "0");
            SqlParameter PKRPackingPrice = new SqlParameter("@PKRPackingPrice", "0");
            SqlParameter SRNO = new SqlParameter("@SrNo", "7");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchIDPurchase);
            AACommon.Execute("SP_DETAIL_INSERT", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, Carry, ExtraCharges, BasicUnitPricePKR, BasicUnitPriceRMB, RMBPackingRate, RMBPackingPrice, PKRPackingPrice, SRNO, CreateByNew, BranchP);

        }

        if (Convert.ToInt32(txtQty8.Value) > 0)
        {
            string itmvalue = ddlItemFrom8.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            SqlParameter SPID_1 = new SqlParameter("@SPID", rqIDPurchase);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ddlItemTo8.SelectedValue);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty8.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty8.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty8.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "P");
            SqlParameter Carry = new SqlParameter("@Carry", "0");
            SqlParameter ExtraCharges = new SqlParameter("@ExtraCharges", "0");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBPackingRate = new SqlParameter("@RMBPackingRate", "0");
            SqlParameter RMBPackingPrice = new SqlParameter("@RMBPackingPrice", "0");
            SqlParameter PKRPackingPrice = new SqlParameter("@PKRPackingPrice", "0");
            SqlParameter SRNO = new SqlParameter("@SrNo", "8");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchIDPurchase);
            AACommon.Execute("SP_DETAIL_INSERT", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, Carry, ExtraCharges, BasicUnitPricePKR, BasicUnitPriceRMB, RMBPackingRate, RMBPackingPrice, PKRPackingPrice, SRNO, CreateByNew, BranchP);

        }
        if (Convert.ToInt32(txtQty9.Value) > 0)
        {
            string itmvalue = ddlItemFrom9.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            SqlParameter SPID_1 = new SqlParameter("@SPID", rqIDPurchase);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ddlItemTo9.SelectedValue);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty9.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty9.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty9.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "P");
            SqlParameter Carry = new SqlParameter("@Carry", "0");
            SqlParameter ExtraCharges = new SqlParameter("@ExtraCharges", "0");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBPackingRate = new SqlParameter("@RMBPackingRate", "0");
            SqlParameter RMBPackingPrice = new SqlParameter("@RMBPackingPrice", "0");
            SqlParameter PKRPackingPrice = new SqlParameter("@PKRPackingPrice", "0");
            SqlParameter SRNO = new SqlParameter("@SrNo", "9");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchIDPurchase);
            AACommon.Execute("SP_DETAIL_INSERT", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, Carry, ExtraCharges, BasicUnitPricePKR, BasicUnitPriceRMB, RMBPackingRate, RMBPackingPrice, PKRPackingPrice, SRNO, CreateByNew, BranchP);

        }
        if (Convert.ToInt32(txtQty10.Value) > 0)
        {
            string itmvalue = ddlItemFrom10.SelectedItem.Text;
            string[] arr = itmvalue.Split('^');
            string PurchasePrice = arr[1];
            string ITEMID = arr[3];
            string PurchaseSPID = arr[4];
            string PurchaseSPDetailID = arr[5];

            PurchasePrice = PurchasePrice.Replace("Price : ", "");
            PurchasePrice = PurchasePrice.Replace(" ", "");

            ITEMID = ITEMID.Replace(" ", "");

            PurchaseSPID = PurchaseSPID.Replace("Purchase # : ", "");
            PurchaseSPID = PurchaseSPID.Replace(" ", "");

            PurchaseSPDetailID = PurchaseSPDetailID.Replace("ID : ", "");
            PurchaseSPDetailID = PurchaseSPDetailID.Replace(" ", "");

            SqlParameter SPID_1 = new SqlParameter("@SPID", rqIDPurchase);
            SqlParameter ITEMID_1 = new SqlParameter("@ITEMID", ddlItemTo10.SelectedValue);
            SqlParameter UnitID = new SqlParameter("@UnitID", "UN-000050");
            SqlParameter QtyIn = new SqlParameter("@QtyIn", txtQty10.Value);
            SqlParameter UnitPrice = new SqlParameter("@UnitPrice", PurchasePrice);
            SqlParameter TotalPrice = new SqlParameter("@TotalPrice", Convert.ToDouble(txtQty10.Value) * Convert.ToDouble(PurchasePrice));
            SqlParameter RMBValue = new SqlParameter("@RMBValue", hdnRMBValue.Value);
            SqlParameter RMBUnitPrice = new SqlParameter("@RMBUnitPrice", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBTotalPrice = new SqlParameter("@RMBTotalPrice", (Convert.ToDouble(txtQty10.Value) * Convert.ToDouble(PurchasePrice)) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter Type = new SqlParameter("@Type", "P");
            SqlParameter Carry = new SqlParameter("@Carry", "0");
            SqlParameter ExtraCharges = new SqlParameter("@ExtraCharges", "0");
            SqlParameter BasicUnitPricePKR = new SqlParameter("@BasicUnitPricePKR", PurchasePrice);
            SqlParameter BasicUnitPriceRMB = new SqlParameter("@BasicUnitPriceRMB", Convert.ToDouble(PurchasePrice) / Convert.ToDouble(hdnRMBValue.Value));
            SqlParameter RMBPackingRate = new SqlParameter("@RMBPackingRate", "0");
            SqlParameter RMBPackingPrice = new SqlParameter("@RMBPackingPrice", "0");
            SqlParameter PKRPackingPrice = new SqlParameter("@PKRPackingPrice", "0");
            SqlParameter SRNO = new SqlParameter("@SrNo", "10");
            SqlParameter CreateByNew = new SqlParameter("@CreateBy", UserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchIDPurchase);
            AACommon.Execute("SP_DETAIL_INSERT", Con, SPID_1, ITEMID_1, UnitID, QtyIn, UnitPrice, TotalPrice, RMBValue, RMBUnitPrice, RMBTotalPrice, Type, Carry, ExtraCharges, BasicUnitPricePKR, BasicUnitPriceRMB, RMBPackingRate, RMBPackingPrice, PKRPackingPrice, SRNO, CreateByNew, BranchP);

        }




        string NarrationPurchaseTransfer = "Purchase (Transfer) By PO # " + rqIDPurchase + "";
        SqlCommand cmdPurchase = new SqlCommand("insert into tbl_transaction (Date,Narration,AmountPKR,AmountRMB,RMBValue,TaskID,CreateBy,BranchID) values ('" + StartDate.Value + "','" + NarrationPurchaseTransfer + "','" + txtTotAmount + "','" + txtTotAmountRMB + "','" + hdnRMBValue.Value + "','" + rqIDPurchase + "','" + UserID + "','" + BranchIDPurchase + "')", Con);
        if (Con.State != ConnectionState.Open)
        {
            Con.Open();
        }
        cmdPurchase.ExecuteNonQuery();
        Con.Close();

        ///PURCHASE ENTRY

        string PurchaseAccount = "";
        SqlDataAdapter daPurchasePurchase = new SqlDataAdapter("select AccountsID from Accounts where BranchID='" + BranchIDPurchase + "' and AccountsTitle='Purchases'", Con);
        DataTable dtPurchasePurchase = new DataTable();
        daPurchasePurchase.Fill(dtPurchasePurchase);
        if (dtPurchasePurchase.Rows.Count > 0)
        {
            PurchaseAccount = dtPurchasePurchase.Rows[0][0].ToString();
        }

        SqlCommand cmdPurchase1Purchase = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + StartDate.Value + "','" + PurchaseAccount + "','PURCHASE','" + txtTotAmount + "','" + txtTotAmountRMB + "','0','0','" + hdnRMBValue.Value + "','" + rqIDPurchase + "','" + NarrationPurchaseTransfer + "','" + UserID + "','" + BranchIDPurchase + "')", Con);
        if (Con.State != ConnectionState.Open)
        {
            Con.Open();
        }
        cmdPurchase1Purchase.ExecuteNonQuery();
        Con.Close();

        SqlCommand cmdPurchase2Purchase = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + StartDate.Value + "','" + ddlTransferToAccount.SelectedValue + "','PURCHASE','0','0','" + txtTotAmount + "','" + txtTotAmountRMB + "','" + hdnRMBValue.Value + "','" + rqIDPurchase + "','" + NarrationPurchaseTransfer + "','" + UserID + "','" + BranchIDPurchase + "')", Con);
        if (Con.State != ConnectionState.Open)
        {
            Con.Open();
        }
        cmdPurchase2Purchase.ExecuteNonQuery();
        Con.Close();


        Response.Redirect("~/PROCUREMENT/Transfer.aspx");

    }


    [WebMethod]
    public static string InsertRegion(string ITEMName, string ItemCode, string BarCode, string Discription, string UserID, string UnitTypeID, string Category, string Brand,string BranchID)
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


        //isale=1
        //ispurchase=0

        msg = AACommon.Execute("ITM_ITEM_INSERT", Conn, ITEMID_P, ITEMName_P, ItemCode_P, BarCode_P, Discription_P, CREATEBY, IsSale_P, IsPurchase_P, UnitTypeID_P, CatID, BrandID, Branch);


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
    public static string UpdateRegion(string ITEMID, string ITEMName, string ItemCode, string BarCode, string Discription, string UnitTypeID, string Category, string Brand, string BranchID)
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
        msg = AACommon.Execute("ITM_ITEM_UPDATE", Conn, ITEMID_P, ITEMName_P, ItemCode_P, BarCode_P, Discription_P, UnitTypeID_P, CatID, BrandID, Branch);


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
        msg = AACommon.Execute("ITM_ITEM_DELETE", Conn, ITEMID_P, DeleteBy_P,Branch);


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
        DataSet ds = AACommon.ReturnDatasetBySP("ITM_ITEM_GET", Conn,Branch);
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
                dbdc.Brand = ds.Tables[0].Rows[i]["BrandID"].ToString();
                dbdc.CatTitle = ds.Tables[0].Rows[i]["CatTitle"].ToString();
                dbdc.BrandTitle = ds.Tables[0].Rows[i]["BrandTitle"].ToString();

                RegionList.Insert(i, dbdc);
            }

        }


        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);


    }

    public class GetRegionClass
    {
        public string ITEMID { get; set; }
        public string ITEMName { get; set; }
        public string ItemCode { get; set; }
        public string BarCode { get; set; }
        public string Discription { get; set; }
        public string UnitTypeID { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string CatTitle { get; set; }
        public string BrandTitle { get; set; }


    }




    //dropdown list

    [WebMethod]
    public static string LoadRegionCombo1(string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("ITM_UNIT_TYPE_Get", Conn,Branch);
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
        SqlDataAdapter da = new SqlDataAdapter("select * from Brand where IsDelete=0 and BranchID='"+BranchID+"'", Conn);
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