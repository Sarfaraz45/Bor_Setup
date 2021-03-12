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

public partial class Setup_Forms : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["PageTitle"] = "Form";

    }


    [WebMethod]
    public static string InsertRegion(string DistrictName, string pid, string path, string UserID)
    {

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter FormTitle = new SqlParameter("@FormTitle", DistrictName);
        SqlParameter PID = new SqlParameter("@PID", pid);
        SqlParameter PATH = new SqlParameter("@Path", path);
        SqlParameter CREATEBY = new SqlParameter("@CREATEBY", UserID);
        msg = AACommon.Execute("FORM_INSERT", Conn, FormTitle, PID, PATH, CREATEBY);


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
    public static string UpdateRegion(string DistrictID, string DistrictName, string pid, string path, string UserID)
    {
        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter FormID = new SqlParameter("@FormID", DistrictID);
        SqlParameter FormTitle = new SqlParameter("@FormTitle", DistrictName);
        SqlParameter PID = new SqlParameter("@PID", pid);
        SqlParameter PATH = new SqlParameter("@Path", path);
        SqlParameter MODIFYBY = new SqlParameter("@MODIFYBY", UserID);
        msg = AACommon.Execute("FORM_UPDATE", Conn, FormID, FormTitle, PID, PATH, MODIFYBY);


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
        //SqlParameter DISTRICT_ID_1 = new SqlParameter("@DISTRICT_ID", DistrictID);
        //DataSet ds = AACommon.ReturnDatasetBySP("TALUKA_CHECK_FOR_DELETE_DISTRICT", Conn, DISTRICT_ID_1);
        //if (ds.Tables[0].Rows.Count > 0)
        //{ retMessage = "false"; return retMessage; }
        //else
        //{
        SqlParameter DISTRICT_ID = new SqlParameter("@FormID", DistrictID);
        SqlParameter MODIFYBY = new SqlParameter("@MODIFYBY", UserID);
        msg = AACommon.Execute("FORM_DELETE", Conn, DISTRICT_ID, MODIFYBY);


        if (msg == "Record Saved Successfully")
        {
            retMessage = "true";
        }
        else
        {
            retMessage = "false";
        }

        return retMessage;
        //}
    }

    [WebMethod]
    public static string LoadRegion()
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataSet ds = AACommon.ReturnDatasetBySPWithoutParameter("FORM_GET_ALL", Conn);
        List<GetDistrictClass> RegionList = new List<GetDistrictClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetDistrictClass dbdc = new GetDistrictClass();

                dbdc.ID = ds.Tables[0].Rows[i][0].ToString();
                dbdc.Name = ds.Tables[0].Rows[i][1].ToString();
                dbdc.Region = ds.Tables[0].Rows[i][2].ToString();
                dbdc.Path = ds.Tables[0].Rows[i][3].ToString();
                RegionList.Insert(i, dbdc);
            }

        }


        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);


    }
    [WebMethod]
    public static string LoadRegionName(string RegionName)
    {
        string retMessage = string.Empty;
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter FormTitle = new SqlParameter("@FormTitle", RegionName);
        DataSet ds = AACommon.ReturnDatasetBySP("FORM_GET_BY_NAME", Conn, FormTitle);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            retMessage = "false";

        }
        else
        {
            retMessage = "true";
        }



        return retMessage;
    }
    public class GetRegionClass
    {
        public string ID { get; set; }
        public string Name { get; set; }

    }
    public class GetDistrictClass
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Path { get; set; }

    }

    [WebMethod]
    public static string LoadRegionCombo()
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataSet ds = AACommon.ReturnDatasetBySPWithoutParameter("FORM_PARENT_GET_ALL_COMBO", Conn);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClass dbdc = new GetRegionClass();

                dbdc.ID = ds.Tables[0].Rows[i][0].ToString();
                dbdc.Name = ds.Tables[0].Rows[i][1].ToString();
                RegionList.Insert(i, dbdc);
            }

        }

        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);
    }





}