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
    public static string LOAD_ACCOUNTS(string UserID, string BranchID)
    {

        string acc = ""; string htmUNT = "";
        string query = "SELECT AccountsID, AccountsTitle + ' ( ' +   convert(nvarchar(20),Round(Balance,2)) + ' ) ' + ' ^ ' + convert(Nvarchar(20), AccountsID) AS AccountsTitle FROM [VW_ACCOUNTS_NAME_WITH_BALANCE] where BranchID1='" + BranchID + "' and  BranchID='" + BranchID + "'";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(query, Con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            string itmID = dt.Rows[i - 1]["AccountsID"].ToString();
            string UntTyp = dt.Rows[i - 1]["AccountsID"].ToString();
            //'["Karachi","Hyderabad","USA","Arkansas","California","Colorado","Connecticut","Delaware","Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas","Kentucky"]'
            if (i == 1)
            { acc = acc + "['" + dt.Rows[i - 1]["AccountsTitle"].ToString() + "'"; }
            else if (i != 1 && i < dt.Rows.Count)
            { acc = acc + ",'" + dt.Rows[i - 1]["AccountsTitle"].ToString() + "'"; }
            else
            { acc = acc + ",'" + dt.Rows[i - 1]["AccountsTitle"].ToString() + "']"; }

            //htmUNT = htmUNT; +LoadUNITS(itmID, UntTyp);
        }
        //acc = acc + "`" + LoadNUMBER("SO", "SP_MASTER", "SPID", Con) + "`" + htmUNT;
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
    public static string LoadNUMBER(string frmt, string tbl, string col, string BranchID)
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
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
       // vNO = vNO + "`" + vDate;
        return vNO;
    }


  
    [WebMethod]
//    public static string SaveTransaction(string UserID, string txtVDate, string txtVoucherNo, string ddlType, string ddlAccountFrom, string ddlAccountTo, string txtAmount, string txtNarration, string RMBValue)
    public static string SaveTransaction(string UserID, string txtVDate, string ddlType, string RMBValue, string str, string ddlCurrency, string BranchID)
    {
        string msg = "";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
       
        if (ddlType== "PVD")
        {
            ////////////////////SAVE TRANSACTION IN ACCOUNTS ////////////////////


            string[] itmLST = str.Split('`');
            for (int i = 0; i < itmLST.Length; i++)
            {
                string[] itmROW = itmLST[i].Split('^');                
                string FrmAccID = itmROW[1].ToString();
                string ToAccID = itmROW[3].ToString();
                string Narration = itmROW[4].ToString();
                string Amount = itmROW[5].ToString();
                string SrNo = itmROW[6].ToString();
                double Rmb = 0;
                if (ddlCurrency == "PKR")
                {
                    Rmb = Convert.ToDouble(RMBValue);
                    Rmb = Convert.ToDouble(Amount) / Rmb;
                }
                else
                { 
                    Rmb = Convert.ToDouble(RMBValue);
                    double amntpkr = Convert.ToDouble(Amount) * Rmb;
                    Rmb = Convert.ToDouble(Amount);
                    Amount = Convert.ToString(amntpkr);
                }
                string txtVoucherNo = LoadNUMBER("PVD", "tbl_transaction", "TaskID", BranchID);
                //string Narration = "Purchase against PO Number " + rqID + "";
                SqlCommand cmd = new SqlCommand("insert into tbl_transaction (Date,Narration,AmountPKR,AmountRMB,RMBValue,TaskID,CreateBy,BranchID) values ('" + txtVDate + "','" + Narration + "','" + Amount + "','" + Rmb + "','" + RMBValue + "','" + txtVoucherNo + "','" + UserID + "','" + BranchID + "')", Con);
                Con.Open();
                cmd.ExecuteNonQuery();
                Con.Close();


                string NarrationFrom = "";
                string NarrationTo = "";
                SqlDataAdapter dafrom = new SqlDataAdapter("select AccountsTitle from Accounts where AccountsID='" + ToAccID + "' and BranchID='" + BranchID + "'", Con);
                DataTable dtfrom = new DataTable();
                dafrom.Fill(dtfrom);
                if (dtfrom.Rows.Count > 0)
                {
                    NarrationFrom = dtfrom.Rows[0][0].ToString() + "   , " + Narration;
                }

                SqlDataAdapter dato = new SqlDataAdapter("select AccountsTitle from Accounts where AccountsID='" + FrmAccID + "' and BranchID='" + BranchID + "'", Con);
                DataTable dtto = new DataTable();
                dato.Fill(dtto);
                if (dtto.Rows.Count > 0)
                {
                    NarrationTo = dtto.Rows[0][0].ToString() + "   , " + Narration;
                }

                SqlCommand cmdPurchase2 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + FrmAccID + "','PVD','0','0','" + Amount + "','" + Rmb + "','" + RMBValue + "','" + txtVoucherNo + "','" + NarrationFrom + "','" + UserID + "','" + BranchID + "')", Con);
                Con.Open();
                cmdPurchase2.ExecuteNonQuery();
                Con.Close();

                SqlCommand cmdPurchase1 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + ToAccID + "','PVD','" + Amount + "','" + Rmb + "','0','0','" + RMBValue + "','" + txtVoucherNo + "','" + NarrationTo + "','" + UserID + "','" + BranchID + "')", Con);
                Con.Open();
                cmdPurchase1.ExecuteNonQuery();
                Con.Close();

            }
            
            
        }
        else
        {
            string[] itmLST = str.Split('`');
            for (int i = 0; i < itmLST.Length; i++)
            {
                string[] itmROW = itmLST[i].Split('^');
                string FrmAccID = itmROW[1].ToString();
                string ToAccID = itmROW[3].ToString();
                string Narration = itmROW[4].ToString();
                string Amount = itmROW[5].ToString();
                string SrNo = itmROW[6].ToString();
                double Rmb = 0;
                if (ddlCurrency == "PKR")
                {
                    Rmb = Convert.ToDouble(RMBValue);
                    Rmb = Convert.ToDouble(Amount) / Rmb;
                }
                else
                {
                    Rmb = Convert.ToDouble(RMBValue);
                    double amntpkr = Convert.ToDouble(Amount) * Rmb;
                    Rmb = Convert.ToDouble(Amount);
                    Amount = Convert.ToString(amntpkr);
                }
                string txtVoucherNo = LoadNUMBER("PVC", "tbl_transaction", "TaskID", BranchID);

                SqlCommand cmd = new SqlCommand("insert into tbl_transaction (Date,Narration,AmountPKR,AmountRMB,RMBValue,TaskID,CreateBy,BranchID) values ('" + txtVDate + "','" + Narration + "','" + Amount + "','" + Rmb + "','" + RMBValue + "','" + txtVoucherNo + "','" + UserID + "','" + BranchID + "')", Con);
                Con.Open();
                cmd.ExecuteNonQuery();
                Con.Close();


                SqlCommand cmdPurchase2 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + ToAccID + "','PVC','" + Amount + "','" + Rmb + "','0','0','" + RMBValue + "','" + txtVoucherNo + "','" + Narration + "','" + UserID + "','" + BranchID + "')", Con);
                Con.Open();
                cmdPurchase2.ExecuteNonQuery();
                Con.Close();

                SqlCommand cmdPurchase1 = new SqlCommand("insert into Transaction_Detail (Date,AccountID,TransactionType,DebitPKR,DebitRMB,CreditPKR,CreditRMB,RMBValue,TaskID,Narration,CreateBy,BranchID) values ('" + txtVDate + "','" + FrmAccID + "','PVC','0','0','" + Amount + "','" + Rmb + "','" + RMBValue + "','" + txtVoucherNo + "','" + Narration + "','" + UserID + "','" + BranchID + "')", Con);
                Con.Open();
                cmdPurchase1.ExecuteNonQuery();
                Con.Close();
            }
            
        }
        


        return msg;
    }


}