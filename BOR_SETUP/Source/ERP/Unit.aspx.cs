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

public partial class ERP_Unit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }




    [WebMethod]
        public static string InsertRegion(string UnitTitle, string UserID)
{

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        string ID = AACommon.GetAlphaNumericIDSIX("Unit", "UNIT-", "UnitID", Conn);
        SqlParameter DistrictID_P = new SqlParameter("@UnitID", ID);
        SqlParameter DistrictTitle_P = new SqlParameter("@UnitTitle", UnitTitle);
        SqlParameter CREATEBY = new SqlParameter("@CreateBy", UserID);
        msg = AACommon.Execute("SP_Unit_Insert", Conn, DistrictID_P, DistrictTitle_P, CREATEBY);


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
    public static string UpdateRegion(string UnitID, string UnitTitle)
    {
        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter DistrictID_P = new SqlParameter("@UnitID", UnitID);
        SqlParameter DistrictTitle_P = new SqlParameter("@UnitTitle", UnitTitle);
        msg = AACommon.Execute("SP_Unit_Update", Conn, DistrictID_P, DistrictTitle_P);


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
    public static string DeleteRegion(string UnitID, string UserID)
    {

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);


        SqlParameter DistrictID_P = new SqlParameter("@UnitID", UnitID);
        SqlParameter DeleteBy_P = new SqlParameter("@DeleteBy", UserID);
        msg = AACommon.Execute("SP_Unit_Delete", Conn, DistrictID_P, DeleteBy_P);


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
        DataSet ds = AACommon.ReturnDatasetBySPWithoutParameter("SP_Unit_Get", Conn);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClass dbdc = new GetRegionClass();

                dbdc.UnitID = ds.Tables[0].Rows[i][0].ToString();
                dbdc.UnitTitle = ds.Tables[0].Rows[i][1].ToString();
                RegionList.Insert(i, dbdc);
            }

        }


        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);


    }

    public class GetRegionClass
    {
        public string UnitID { get; set; }
        public string UnitTitle { get; set; }

        public string DistrictID { get; set; }
        public string DistrictTitle { get; set; }
    }








}