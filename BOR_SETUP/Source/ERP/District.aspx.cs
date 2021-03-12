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

public partial class ERP_District : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }




    [WebMethod]
        public static string InsertRegion(string DistrictTitle, string UserID)
{

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        string ID = AACommon.GetAlphaNumericIDSIX("District", "DISTRICT-", "DistrictID", Conn);
        SqlParameter DistrictID_P = new SqlParameter("@DistrictID", ID);
        SqlParameter DistrictTitle_P = new SqlParameter("@DistrictTitle", DistrictTitle);
        SqlParameter CREATEBY = new SqlParameter("@CreateBy", UserID);
        msg = AACommon.Execute("SP_District_Insert", Conn, DistrictID_P, DistrictTitle_P, CREATEBY);


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
    public static string UpdateRegion(string DistrictID, string DistrictTitle)
    {
        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter DistrictID_P = new SqlParameter("@DistrictID", DistrictID);
        SqlParameter DistrictTitle_P = new SqlParameter("@DistrictTitle", DistrictTitle);
        msg = AACommon.Execute("SP_District_Update", Conn, DistrictID_P, DistrictTitle_P);


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
    public static string DeleteRegion(string DistrictID, string UserID)
    {

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);


        SqlParameter DistrictID_P = new SqlParameter("@DistrictID", DistrictID);
        SqlParameter DeleteBy_P = new SqlParameter("@DeleteBy", UserID);
        msg = AACommon.Execute("SP_District_Delete", Conn, DistrictID_P, DeleteBy_P);


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
        DataSet ds = AACommon.ReturnDatasetBySPWithoutParameter("SP_Distict_Title_Get", Conn);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClass dbdc = new GetRegionClass();

                dbdc.DistrictID = ds.Tables[0].Rows[i][0].ToString();
                dbdc.DistrictTitle = ds.Tables[0].Rows[i][1].ToString();
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
    }








}