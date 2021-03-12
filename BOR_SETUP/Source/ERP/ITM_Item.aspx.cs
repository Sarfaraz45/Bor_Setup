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
    public static string CheckDuplication(string ITEMName , string BranchID)
    {

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        SqlDataAdapter da = new SqlDataAdapter("select * from ITM_ITEM where ITEMName='" + ITEMName + "' and BranchID='" + BranchID + "' and IsDelete=0", Conn);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        { retMessage = "true"; }
        else
        { retMessage = "false"; }

       
        return retMessage;
    }



    [WebMethod]
    public static string InsertRegion(string ITEMName, string ItemCode, string BarCode, string Discription, string Loose, string LoosePrice, string Strip, string StripPrice, string Box, string BoxPrice,  string Carton, string CartonPrice, string UserID, string UnitTypeID, string Category, string Brand,string BranchID)
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
        SqlParameter Loose_P = new SqlParameter("@Loose", Loose);
        SqlParameter LoosePrice_P = new SqlParameter("@LoosePrice", LoosePrice);
       SqlParameter Strip_P = new SqlParameter("@Strip", Strip);
        SqlParameter StripPrice_P = new SqlParameter("@StripPrice", StripPrice);
         SqlParameter Box_P = new SqlParameter("@Box", Box);
        SqlParameter BoxPrice_P = new SqlParameter("@BoxPrice", BoxPrice);
       SqlParameter Carton_P = new SqlParameter("@Carton", Carton);
        SqlParameter CartonPrice_P = new SqlParameter("@CartonPrice", CartonPrice);
       SqlParameter CREATEBY = new SqlParameter("@CreateBy", UserID);
        SqlParameter IsSale_P = new SqlParameter("@IsSale", "1");
        SqlParameter IsPurchase_P = new SqlParameter("@IsPurchase", "0");
        SqlParameter UnitTypeID_P = new SqlParameter("@UnitTypeID", UnitTypeID);
        SqlParameter CatID = new SqlParameter("@CatID", Category);
        SqlParameter BrandID = new SqlParameter("@BrandID", Brand);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);


        //isale=1
        //ispurchase=0

        msg = AACommon.Execute("ITM_ITEM_INSERT", Conn, ITEMID_P, ITEMName_P, ItemCode_P, BarCode_P, Discription_P, Loose_P, LoosePrice_P, Strip_P, StripPrice_P, Box_P, BoxPrice_P, Carton_P, CartonPrice_P,  CREATEBY, IsSale_P, IsPurchase_P, UnitTypeID_P, CatID, BrandID, Branch);


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
    public static string UpdateRegion(string ITEMID, string ITEMName, string ItemCode, string BarCode, string Discription, string Loose, string LoosePrice, string Strip, string StripPrice, string Box, string BoxPrice,  string Carton, string CartonPrice, string UnitTypeID, string Category, string Brand, string BranchID)
    {
        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter ITEMID_P = new SqlParameter("@ITEMID", ITEMID);
        SqlParameter ITEMName_P = new SqlParameter("@ITEMName", ITEMName);
        SqlParameter ItemCode_P = new SqlParameter("@ItemCode", ItemCode);
        SqlParameter BarCode_P = new SqlParameter("@BarCode", BarCode);
        SqlParameter Discription_P = new SqlParameter("@Discription", Discription);
        SqlParameter Loose_P = new SqlParameter("@Loose", Loose);
        SqlParameter LoosePrice_P = new SqlParameter("@LoosePrice", LoosePrice);
       
        SqlParameter Strip_P = new SqlParameter("@Strip", Strip);
        SqlParameter StripPrice_P = new SqlParameter("@StripPrice", StripPrice);
        
        SqlParameter Box_P = new SqlParameter("@Box", Box);
        SqlParameter BoxPrice_P = new SqlParameter("@BoxPrice", BoxPrice);
       
        SqlParameter Carton_P = new SqlParameter("@Carton", Carton);
        SqlParameter CartonPrice_P = new SqlParameter("@CartonPrice", CartonPrice);
    
        SqlParameter UnitTypeID_P = new SqlParameter("@UnitTypeID", UnitTypeID);
        SqlParameter CatID = new SqlParameter("@CatID", Category);
        SqlParameter BrandID = new SqlParameter("@BrandID", Brand);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        msg = AACommon.Execute("ITM_ITEM_UPDATE", Conn, ITEMID_P, ITEMName_P, ItemCode_P, BarCode_P, Discription_P, Loose_P, LoosePrice_P, Strip_P, StripPrice_P,  Box_P, BoxPrice_P,  Carton_P, CartonPrice_P,UnitTypeID_P, CatID, BrandID, Branch);


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
                dbdc.Loose = ds.Tables[0].Rows[i]["Loose"].ToString();
                dbdc.LoosePrice = ds.Tables[0].Rows[i]["LoosePrice"].ToString();
                
                dbdc.Strip = ds.Tables[0].Rows[i]["Strip"].ToString();
                dbdc.StripPrice = ds.Tables[0].Rows[i]["StripPrice"].ToString();
                
                dbdc.Box = ds.Tables[0].Rows[i]["Box"].ToString();
                dbdc.BoxPrice = ds.Tables[0].Rows[i]["BoxPrice"].ToString();
                
                dbdc.Carton = ds.Tables[0].Rows[i]["Carton"].ToString();
                dbdc.CartonPrice = ds.Tables[0].Rows[i]["CartonPrice"].ToString();
               
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
        public string Loose { get; set; }
        public string LoosePrice { get; set; }
        public string Strip { get; set; }
        public string StripPrice { get; set; }
        public string Box { get; set; }
        public string BoxPrice { get; set; }
        public string Carton { get; set; }
        public string CartonPrice {get; set;}
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
       // SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("ITM_UNIT_TYPE_Get", Conn);
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