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

public partial class ERP_Customer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }




    [WebMethod]
    public static string InsertRegion(string CustomerTitle, string CustomerCode, string Phone, string Fax, string Email, string AddressLine1, string AddressLine2, string NTN, string GST, string SRB, string UserID, string InvDays, string AgDays, string opBal)
    {

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlConnection ConACC = new SqlConnection(ConfigurationManager.ConnectionStrings["ConACC"].ConnectionString);

        string ID = AACommon.GetAlphaNumericIDSIX("Customer", "CUS-", "CustomerID", Conn);
        SqlParameter CustomerID_P = new SqlParameter("@CustomerID", ID);
        SqlParameter CustomerTitle_P = new SqlParameter("@CustomerTitle", CustomerTitle);
        SqlParameter CustomerCode_P = new SqlParameter("@CustomerCode", CustomerCode);
        SqlParameter Phone_P = new SqlParameter("@Phone", Phone);
        SqlParameter Fax_P = new SqlParameter("@Fax", Fax);
        SqlParameter Email_P = new SqlParameter("@Email", Email);
        SqlParameter AddressLine1_P = new SqlParameter("@AddressLine1", AddressLine1);
        SqlParameter AddressLine2_P = new SqlParameter("@AddressLine2", AddressLine2);
        SqlParameter NTN_P = new SqlParameter("@NTN", NTN);
        SqlParameter GST_P = new SqlParameter("@GST", GST);
        SqlParameter SRB_P = new SqlParameter("@SRB", SRB);

        SqlParameter InvoiceDays = new SqlParameter("@InvoiceDays", InvDays);
        SqlParameter AgingDays = new SqlParameter("@AgingDays", AgDays);
        SqlParameter OpBal = new SqlParameter("@OpBal", opBal);

        SqlParameter CREATEBY = new SqlParameter("@CreateBy", UserID);
        msg = AACommon.Execute("Customer_Insert", Conn, CustomerID_P, CustomerTitle_P, CustomerCode_P, Phone_P, Fax_P, Email_P, AddressLine1_P, AddressLine2_P, NTN_P, GST_P, SRB_P, InvoiceDays, AgingDays, OpBal, CREATEBY);


       

        /////////////////////ACCOUNTS /////////////////////////
        string opD = "0"; string opC = "0";
        if (Convert.ToDecimal(opBal) > 0) { opD = opBal; opC = "0"; }
        else if (Convert.ToDecimal(opBal) < 0) { opD = "0"; opC = opBal; }

        string L4ID = AACommon.GetData("SELECT AccountID FROM AACCOUNTS_Integration WHERE (Description = 'Accounts Receivable')", ConACC, "AccountID");
        string ledgerID = AACommon.GetAlphaNumericIDTHREE("Accounts", L4ID, "L4_ID", ConACC);
        SqlParameter L4_ID = new SqlParameter("@L4_ID", ledgerID);
        SqlParameter Desc_Level4 = new SqlParameter("@Desc_Level4", CustomerTitle);
        SqlParameter L4_IDNew = new SqlParameter("@L4_IDNew", L4ID);
        SqlParameter Opdr = new SqlParameter("@Opdr", opD);
        SqlParameter Opcr = new SqlParameter("@Opcr", opC);
        SqlParameter CompanyID = new SqlParameter("@CompanyID", "12");
        SqlParameter FiscalID = new SqlParameter("@FiscalID", "22");
        SqlParameter BalanceValidation = new SqlParameter("@BalanceValidation", "0");
        AACommon.Execute("Insert_Accounts", ConACC, L4_ID, Desc_Level4, L4_IDNew, Opdr, Opcr, CompanyID, FiscalID, BalanceValidation);
        /////////////////////////////////////

        string str = "update Customer set LedgerID='" + ledgerID + "' where CustomerID='" + ID + "'";
        SqlCommand cmd = new SqlCommand(str, Conn);
        if (Conn.State == ConnectionState.Closed) { Conn.Open(); }
        cmd.ExecuteNonQuery();
        if (Conn.State == ConnectionState.Open) { Conn.Close(); }



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
    public static string UpdateRegion(string CustomerID, string CustomerTitle, string CustomerCode, string Phone, string Fax, string Email, string AddressLine1, string AddressLine2, string NTN, string GST, string SRB, string InvDays, string AgDays, string opBal)
    {
        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter CustomerID_P = new SqlParameter("@CustomerID", CustomerID);
        SqlParameter CustomerTitle_P = new SqlParameter("@CustomerTitle", CustomerTitle);
        SqlParameter CustomerCode_P = new SqlParameter("@CustomerCode", CustomerCode);
        SqlParameter Phone_P = new SqlParameter("@Phone", Phone);
        SqlParameter Fax_P = new SqlParameter("@Fax", Fax);
        SqlParameter Email_P = new SqlParameter("@Email", Email);
        SqlParameter AddressLine1_P = new SqlParameter("@AddressLine1", AddressLine1);
        SqlParameter AddressLine2_P = new SqlParameter("@AddressLine2", AddressLine2);
        SqlParameter NTN_P = new SqlParameter("@NTN", NTN);
        SqlParameter GST_P = new SqlParameter("@GST", GST);
        SqlParameter SRB_P = new SqlParameter("@SRB", SRB);
        SqlParameter InvoiceDays = new SqlParameter("@InvoiceDays", InvDays);
        SqlParameter AgingDays = new SqlParameter("@AgingDays", AgDays);
        SqlParameter OpBal = new SqlParameter("@OpBal", opBal);

        msg = AACommon.Execute("Customer_Update", Conn, CustomerID_P, CustomerTitle_P, CustomerCode_P, Phone_P, Fax_P, Email_P, AddressLine1_P, AddressLine2_P, NTN_P, GST_P, SRB_P, InvoiceDays, AgingDays, OpBal);


        //Update_Account_OpBal
        SqlConnection ConACC = new SqlConnection(ConfigurationManager.ConnectionStrings["ConACC"].ConnectionString);
        string lgrID = AACommon.GetData("select LedgerID from Customer where CustomerID='" + CustomerID + "'", Conn, "LedgerID");
        string opD = "0"; string opC = "0";
        if (Convert.ToDecimal(opBal) > 0) { opD = opBal; opC = "0"; }
        else if (Convert.ToDecimal(opBal) < 0) { opD = "0"; opC = opBal; }
        SqlParameter L4_ID = new SqlParameter("@L4_ID", lgrID);
        SqlParameter Opcr = new SqlParameter("@Opcr", opC);
        SqlParameter Opdr = new SqlParameter("@Opdr", opD);
        AACommon.Execute("Update_Account_OpBal", ConACC, L4_ID, Opcr, Opdr);




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
    public static string DeleteRegion(string CustomerID, string UserID)
    {

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);


        SqlParameter CustomerID_P = new SqlParameter("@CustomerID", CustomerID);
        SqlParameter DeleteBy_P = new SqlParameter("@DeleteBy", UserID);
        msg = AACommon.Execute("Customer_Delete", Conn, CustomerID_P, DeleteBy_P);


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
    public static string LoadRegion()
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataSet ds = AACommon.ReturnDatasetBySPWithoutParameter("Customer_Get", Conn);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClass dbdc = new GetRegionClass();
                
                dbdc.CustomerID = ds.Tables[0].Rows[i]["CustomerID"].ToString();
                dbdc.CustomerTitle = ds.Tables[0].Rows[i]["CustomerTitle"].ToString();
                dbdc.CustomerCode = ds.Tables[0].Rows[i]["CustomerCode"].ToString();
                dbdc.Phone = ds.Tables[0].Rows[i]["Phone"].ToString();
                dbdc.Fax = ds.Tables[0].Rows[i]["Fax"].ToString();
                dbdc.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                dbdc.AddressLine1 = ds.Tables[0].Rows[i]["AddressLine1"].ToString();
                dbdc.AddressLine2 = ds.Tables[0].Rows[i]["AddressLine2"].ToString();
                dbdc.NTN = ds.Tables[0].Rows[i]["NTN"].ToString();
                dbdc.GST = ds.Tables[0].Rows[i]["GST"].ToString();
                dbdc.SRB = ds.Tables[0].Rows[i]["SRB"].ToString();
                dbdc.InvoiceDays = ds.Tables[0].Rows[i]["InvoiceDays"].ToString();
                dbdc.AgingDays = ds.Tables[0].Rows[i]["AgingDays"].ToString();
                dbdc.OpBal = ds.Tables[0].Rows[i]["OpBal"].ToString();
                RegionList.Insert(i, dbdc);
            }

        }

        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);
    }

    public class GetRegionClass
    {
        public string CustomerID { get; set; }
        public string CustomerTitle { get; set; }
        public string CustomerCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string NTN { get; set; }
        public string GST { get; set; }
        public string SRB { get; set; }
        public string InvoiceDays { get; set; }
        public string AgingDays { get; set; }
        public string OpBal { get; set; }



    }









}