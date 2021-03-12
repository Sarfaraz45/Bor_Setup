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
    public static string CheckDuplication(string ITEMName, string BranchID)
    {

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        SqlDataAdapter da = new SqlDataAdapter("select * from Accounts where AccountsTitle='" + ITEMName + "' and BranchID='" + BranchID + "'", Conn);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        { retMessage = "true"; }
        else
        { retMessage = "false"; }


        return retMessage;
    }



    [WebMethod]
    public static string InsertRegion(string ITEMName, string Category, string UserID, string Contact, string Email, string Address, string City, string Country, string Reference, string OpDebit, string OpCredit,string OpDebitRMB,string OpCreditRMB,string RMBValue,string BranchID)
    {
        string retMessage = string.Empty;        
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        //string ID = AACommon.GetAlphaNumericIDSIX("ITM_ITEM", "ITM-", "ITEMID", Conn);
        SqlCommand cmd = new SqlCommand("insert into Accounts (AccountsTitle,ControlAccID,Contact,Email,Address,City,Country,Reference,OpDebit,OpCredit,OpDebitRMB,OpCreditRMB,RMBValue,BranchID) values ('" + ITEMName + "','" + Category + "','" + Contact + "','" + Email + "','" + Address + "','" + City + "','" + Country + "','" + Reference + "','" + OpDebit + "','" + OpCredit + "','" + OpDebitRMB + "','" + OpCreditRMB + "','" + RMBValue + "','"+BranchID+"')", Conn);
        Conn.Open();
        int a = cmd.ExecuteNonQuery();
        Conn.Close();
        
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


    [WebMethod]
    public static string UpdateRegion(string ITEMID, string ITEMName, string Category, string Contact, string Email, string Address, string City, string Country, string Reference, string OpDebit, string OpCredit, string OpDebitRMB, string OpCreditRMB, string RMBValue, string BranchID)
    {
        string retMessage = string.Empty;
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlCommand cmd = new SqlCommand("update  Accounts set AccountsTitle='" + ITEMName + "',ControlAccID='" + Category + "',Contact='" + Contact + "',Email='" + Email + "',Address='" + Address + "',City='" + City + "',Country='" + Country + "',Reference='" + Reference + "',OpDebit='" + OpDebit + "',OpCredit='" + OpCredit + "',OpDebitRMB='" + OpDebitRMB + "',OpCreditRMB='" + OpCreditRMB + "',RMBValue='" + RMBValue + "' where AccountsID='" + ITEMID + "' and BranchID='" + BranchID + "' and IsRestrict=0", Conn);
        Conn.Open();
        int a = cmd.ExecuteNonQuery();
        Conn.Close();
     
        if (a==1)
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

        SqlCommand cmd = new SqlCommand("delete  Accounts  where AccountsID='" + ITEMID + "' and BranchID='" + BranchID + "' and IsRestrict=0", Conn);
        Conn.Open();
        int a = cmd.ExecuteNonQuery();
        Conn.Close();

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


    [WebMethod]
    public static string LoadRegion(string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("SELECT        dbo.Accounts.AccountsID, dbo.Accounts.AccountsTitle, dbo.AccountTypeControl.ControlAccID AS AccountTypeID, dbo.AccountTypeControl.ControlAccTitle AS AccountTypeTitle, dbo.Accounts.Contact, dbo.Accounts.Email, dbo.Accounts.Address, dbo.Accounts.City, dbo.Accounts.Country, dbo.Accounts.Reference, dbo.Accounts.OpDebit,                          dbo.Accounts.OpCredit,dbo.Accounts.OpDebitRMB,dbo.Accounts.OpCreditRMB,dbo.Accounts.RMBValue FROM            dbo.Accounts INNER JOIN                          dbo.AccountTypeControl ON dbo.Accounts.ControlAccID = dbo.AccountTypeControl.ControlAccID where  dbo.AccountTypeControl.BranchID='" + BranchID + "' and dbo.Accounts.BranchID='" + BranchID + "' ", Conn);
        DataSet ds = new DataSet();
        da.Fill(ds);        
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClass dbdc = new GetRegionClass();

                dbdc.ITEMID = ds.Tables[0].Rows[i][0].ToString();
                dbdc.ITEMName = ds.Tables[0].Rows[i][1].ToString();
                dbdc.ItemCode = ds.Tables[0].Rows[i][2].ToString();
                dbdc.BarCode = ds.Tables[0].Rows[i][3].ToString();
                dbdc.Contact = ds.Tables[0].Rows[i][4].ToString();
                dbdc.Email = ds.Tables[0].Rows[i][5].ToString();
                dbdc.Address = ds.Tables[0].Rows[i][6].ToString();
                dbdc.City = ds.Tables[0].Rows[i][7].ToString();
                dbdc.Country = ds.Tables[0].Rows[i][8].ToString();
                dbdc.Reference = ds.Tables[0].Rows[i][9].ToString();
                dbdc.OpDebit = ds.Tables[0].Rows[i][10].ToString();
                dbdc.OpCredit = ds.Tables[0].Rows[i][11].ToString();
                dbdc.OpDebitRMB = ds.Tables[0].Rows[i][12].ToString();
                dbdc.OpCreditRMB = ds.Tables[0].Rows[i][13].ToString();
                dbdc.RMBValue = ds.Tables[0].Rows[i][14].ToString();
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
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Reference { get; set; }
        public string OpDebit { get; set; }
        public string OpCredit { get; set; }
        public string OpDebitRMB { get; set; }
        public string OpCreditRMB { get; set; }
        public string RMBValue { get; set; }        
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
    public static string LoadCategory(string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("select * from AccountTypeControl where BranchID='"+BranchID+"'", Conn);
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