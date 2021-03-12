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

public partial class ERP_POS : System.Web.UI.Page
{
    private static DataColumn UnitTypeDesc;
    private static DataColumn UnitTypeID;
    protected void Page_Load(object sender, EventArgs e)
    {

    }








    [WebMethod]
    public static string LoadItem(string BranchID)
    {

        string acc = ""; string htmUNT = "";
        string query = "select * from VW_ITEM_FOR_SALE where BranchID='" + BranchID + "' and   BranchID2='" + BranchID + "' and BranchID3='" + BranchID + "' order by ITEMName";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(query, Con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            GetRegionClass dbdc = new GetRegionClass();

            dbdc.ITEMID = dt.Rows[i]["ITEMID"].ToString();
            dbdc.ITEMName = dt.Rows[i]["ITEMName"].ToString();
            dbdc.Price = dt.Rows[i]["SalePrice"].ToString();
            dbdc.Cost = dt.Rows[i]["PurchasePrice"].ToString();
            RegionList.Insert(i, dbdc);
        }
        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);
        //for (int i = 1; i <= dt.Rows.Count; i++)
        //{
        //    string itmID = "";
        //    string UntTyp = "";
        //    if (dt.Rows.Count == 1)
        //    {
        //        itmID = dt.Rows[i - 1]["ITEMID"].ToString();
        //        UntTyp = dt.Rows[i - 1]["UnitTypeID"].ToString();
        //        if (i == 1)
        //        { acc = acc + "['" + dt.Rows[i - 1]["ITEMName"].ToString() + "']"; }

        //    }
        //    else
        //    {

        //        itmID = dt.Rows[i - 1]["ITEMID"].ToString();
        //        UntTyp = dt.Rows[i - 1]["UnitTypeID"].ToString();
        //        if (i == 1)
        //        { acc = acc + "['" + dt.Rows[i - 1]["ITEMName"].ToString() + "'"; }
        //        else if (i != 1 && i < dt.Rows.Count)
        //        { acc = acc + ",'" + dt.Rows[i - 1]["ITEMName"].ToString() + "'"; }
        //        else
        //        { acc = acc + ",'" + dt.Rows[i - 1]["ITEMName"].ToString() + "']"; }

        //    }
        //    htmUNT = htmUNT + LoadUNITS(itmID, UntTyp, BranchID);
        //}
        //acc = acc + "`" + LoadNUMBER("CS", "SP_MASTER", "SPID", Con, BranchID) + "`" + htmUNT;
        //return acc;
    }


    [WebMethod]
    public static string LoadRegion(string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("ITM_ITEM_GET", Conn, Branch);
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

    [WebMethod]
    public static string UpdateRegion(string ITEMID, string ITEMName, string ItemCode, string BarCode, string Discription, string UnitTypeID, string Category, string Brand, string BranchID, string PurchasePrice, string SalePrice)
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
        SqlParameter PPrice = new SqlParameter("@PurchasePrice", PurchasePrice);
        SqlParameter SPrice = new SqlParameter("@SalePrice", SalePrice);
        msg = AACommon.Execute("ITM_ITEM_UPDATE", Conn, ITEMID_P, ITEMName_P, ItemCode_P, BarCode_P, Discription_P, UnitTypeID_P, CatID, BrandID, Branch, PPrice, SPrice);


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
    public static string LoadBrand(string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("select * from Brand where IsDelete=0 and BranchID='" + BranchID + "'", Conn);
        DataSet ds = new DataSet();
        da.Fill(ds);
        List<GetRegionClasss> RegionList = new List<GetRegionClasss>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClasss dbdc = new GetRegionClasss();

                dbdc.UnitTypeID = ds.Tables[0].Rows[i]["UnitTypeID"].ToString();
                dbdc.UnitTypeDesc = ds.Tables[0].Rows[i]["UnitTypeDesc"].ToString();
                RegionList.Insert(i, dbdc);
            }

        }

        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);
    }
    public class GetRegionClass
    {
        public string Price;
        public string Cost;
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









    public class GetRegionClasss
    {
        public string UnitTypeID { get; set; }
        public string UnitTypeDesc { get; set; }



    }










}