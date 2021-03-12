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

public partial class ERP_Area : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }







    [WebMethod]
    public static string InsertRegion(string AreaTitle, string TalukaID, string UserID)
    {

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        string ID = AACommon.GetAlphaNumericIDSIX("Area", "AR-", "AreaID", Conn);
        SqlParameter UnitID_P = new SqlParameter("@AreaID", ID);
        SqlParameter UnitTypeDesc_P = new SqlParameter("@AreaTitle", AreaTitle);
        SqlParameter UnitTypeID_P = new SqlParameter("@TalukaID", TalukaID);
        SqlParameter CREATEBY = new SqlParameter("@CreateBy", UserID);

        msg = AACommon.Execute("SP_Area_Insert", Conn, UnitID_P, UnitTypeDesc_P, UnitTypeID_P, CREATEBY);


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
    public static string UpdateRegion(string AreaID, string AreaTitle, string TalukaID)
    {
        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter UnitID_P = new SqlParameter("@AreaID", AreaID);
        SqlParameter UnitTitle_P = new SqlParameter("@AreaTitle", AreaTitle);
        SqlParameter UnitTypeID_P = new SqlParameter("@TalukaID", TalukaID);

        msg = AACommon.Execute("SP_Area_Update", Conn, UnitID_P, UnitTitle_P, UnitTypeID_P);


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
    public static string DeleteRegion(string AreaID, string UserID)
    {

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);


        SqlParameter UnitID_P = new SqlParameter("@AreaID", AreaID);
        SqlParameter DeleteBy_P = new SqlParameter("@DeleteBy", UserID);
        msg = AACommon.Execute("SP_Area_Delete", Conn, UnitID_P, DeleteBy_P);


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















    //dropdown list

    [WebMethod]
    public static string LoadRegionCombo1()
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataSet ds = AACommon.ReturnDatasetBySPWithoutParameter("SP_Taluka_Get", Conn);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClass dbdc = new GetRegionClass();

                dbdc.TalukaID = ds.Tables[0].Rows[i][0].ToString();
                dbdc.TalukaTitle = ds.Tables[0].Rows[i][1].ToString();
                RegionList.Insert(i, dbdc);
            }

        }

        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);
    }




    public class GetRegionClass
    {
        public string UnitTypeID { get; set; }
        public string UnitTypeDesc { get; set; }

        public string DistrictID { get; set; }
        public string DistrictTitle { get; set; }

        public string TalukaID { get; set; }
        public string TalukaTitle { get; set; }



    }






    [WebMethod]
    public static string LoadRegion()
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataSet ds = AACommon.ReturnDatasetBySPWithoutParameter("SP_Area_Get", Conn);
        List<GetRegionClasss> RegionList = new List<GetRegionClasss>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClasss dbdc = new GetRegionClasss();

                dbdc.AreaID = ds.Tables[0].Rows[i][0].ToString();
                dbdc.AreaTitle = ds.Tables[0].Rows[i][1].ToString();
                dbdc.TalukaID = ds.Tables[0].Rows[i][2].ToString();
           


                RegionList.Insert(i, dbdc);
            }

        }


        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);


    }

    public class GetRegionClasss
    {
        public string AreaID { get; set; }
        public string AreaTitle { get; set; }
        public string DisplayName { get; set; }
        public string UnitTypeID { get; set; }
        public string IsBase { get; set; }
        public string BaseMultiplier { get; set; }
        public string TalukaID { get; set; }

        public string TalukaTitle { get; set; }
        public string DistrictID { get; set; }
        public string DistricitTitle { get; set; }

    }









}