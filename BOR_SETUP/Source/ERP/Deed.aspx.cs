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

public partial class ERP_Deed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }




    [WebMethod]
        public static string InsertRegion(string DeedTitle, string UserID)
{

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        string ID = AACommon.GetAlphaNumericIDSIX("Deed", "DEED-", "DeedID", Conn);
        SqlParameter DistrictID_P = new SqlParameter("@DeedID", ID);
        SqlParameter DistrictTitle_P = new SqlParameter("@DeedTitle", DeedTitle);
        SqlParameter CREATEBY = new SqlParameter("@CreateBy", UserID);
        msg = AACommon.Execute("SP_Deed_Insert", Conn, DistrictID_P, DistrictTitle_P, CREATEBY);


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
    public static string UpdateRegion(string DeedID, string DeedTitle)
    {
        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter DistrictID_P = new SqlParameter("@DeedID", DeedID);
        SqlParameter DistrictTitle_P = new SqlParameter("@DeedTitle", DeedTitle);
        msg = AACommon.Execute("SP_Deed_Update", Conn, DistrictID_P, DistrictTitle_P);


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
    public static string DeleteRegion(string DeedID, string UserID)
    {

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);


        SqlParameter DistrictID_P = new SqlParameter("@DeedID", DeedID);
        SqlParameter DeleteBy_P = new SqlParameter("@DeleteBy", UserID);
        msg = AACommon.Execute("SP_Deed_Delete", Conn, DistrictID_P, DeleteBy_P);


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
        DataSet ds = AACommon.ReturnDatasetBySPWithoutParameter("SP_Deed_Get", Conn);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClass dbdc = new GetRegionClass();

                dbdc.DeedID = ds.Tables[0].Rows[i][0].ToString();
                dbdc.DeedTitle = ds.Tables[0].Rows[i][1].ToString();
                RegionList.Insert(i, dbdc);
            }

        }


        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);


    }

    public class GetRegionClass
    {
        public string DeedID { get; set; }
        public string DeedTitle { get; set; }

        public string DistrictID { get; set; }
        public string DistrictTitle { get; set; }
    }








}