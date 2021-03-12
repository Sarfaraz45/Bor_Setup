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
        SqlDataAdapter da = new SqlDataAdapter("select * from SP_MASTER_ON_WAY_HISTORY where SPID='" + Product + "' and BranchID='"+ BranchID +"' Order by ID Desc", Conn);
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
            SqlDataAdapter da = new SqlDataAdapter("Select RMBValue  from CurrencyConversion where IsActive=1 and BranchID='"+ BranchID +"'", Conn);
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
      
            SqlDataAdapter da = new SqlDataAdapter("Select RMBValue  from CurrencyConversion where IsActive=1 and BranchID='"+ BranchID +"'", Conn);
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

            htmUNT = htmUNT+LoadUNITS(itmID, UntTyp,BranchID);
        }
        acc = acc + "`" + LoadNUMBER("PR", "SP_MASTER", "SPID",Con,BranchID) + "`" + htmUNT;
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
    public static string SaveTransaction(string UserID, string str, string txtVDate, string txtVoucherNo, string txtTotAmount, string txtDiscount, string txtTaxRate, string txtTotTax, string txtGrandTotal, string ddlSupplier, string txtTotAmountRMB, string RMBValueHDN, string txtCarryPKR, string txtCarryRMB, string txtPackingPKR, string txtPackingRMB, string txtExChargePKR, string txtExChargeRMB, string txtSupplierPKR, string txtSupplierRMB, string DDLPacking, string DDLCarry, string DDLExCharge, string txtDiscountPKR, string txtDiscountRMB, string txtONWAYID, string txtRemarks, string txtContainerNo, string BranchID)
    {
        string msg = "";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        SqlCommand cmdUpdate = new SqlCommand("update SP_MASTER_ON_WAY set SPDate='" + txtVDate + "', Remarks='" + txtRemarks + "',ContainerNo='" + txtContainerNo + "' where SPID='" + txtONWAYID + "' and BranchID='"+BranchID+"'", Con);
        Con.Open();
        cmdUpdate.ExecuteNonQuery();
        Con.Close();

        SqlCommand cmdUpdateHistory = new SqlCommand("update SP_MASTER_ON_WAY_HISTORY set IsActive=0  where SPID='" + txtONWAYID + "' and BranchID='" + BranchID + "'", Con);
        Con.Open();
        cmdUpdateHistory.ExecuteNonQuery();
        Con.Close();

        SqlCommand cmdUpdateHistoryNo = new SqlCommand("insert into SP_MASTER_ON_WAY_HISTORY (Remarks,SPID,BranchID,CreateBy) values ('" + txtRemarks + "','" + txtONWAYID + "','"+BranchID+"','"+UserID+"')", Con);
        Con.Open();
        cmdUpdateHistoryNo.ExecuteNonQuery();
        Con.Close();

        SqlCommand cmdUpdatetblMaster = new SqlCommand("update tbl_transaction set Date='" + txtVDate + "' where TaskID='" + txtONWAYID + "' and BranchID='" + BranchID + "'", Con);
        Con.Open();
        cmdUpdatetblMaster.ExecuteNonQuery();
        Con.Close();

        SqlCommand cmdUpdatetblDetail = new SqlCommand("update Transaction_Detail set Date='" + txtVDate + "' where TaskID='" + txtONWAYID + "' and BranchID='" + BranchID + "'", Con);
        Con.Open();
        cmdUpdatetblDetail.ExecuteNonQuery();
        Con.Close();
        
        return msg;
    }


}