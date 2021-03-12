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
    public static string InsertRegion(string UnitTitle, string DisplayName, string UnitTypeID, string IsBase, string BaseMultiplier, string UserID)
    {

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        string ID = AACommon.GetAlphaNumericIDSIX("ITM_UNIT", "UN-", "UnitID", Conn);
        SqlParameter UnitID_P = new SqlParameter("@UnitID", ID);
        SqlParameter UnitTypeDesc_P = new SqlParameter("@UnitTitle", UnitTitle);
        SqlParameter DisplayName_P = new SqlParameter("@DisplayName", DisplayName);
        SqlParameter UnitTypeID_P = new SqlParameter("@UnitTypeID", UnitTypeID);
        SqlParameter IsBase_P = new SqlParameter("@IsBase", IsBase);
        SqlParameter BaseMultiplier_P = new SqlParameter("@BaseMultiplier", BaseMultiplier);
        SqlParameter CREATEBY = new SqlParameter("@CreateBy", UserID);

        msg = AACommon.Execute("ITM_UNIT_Insert", Conn, UnitID_P, UnitTypeDesc_P, DisplayName_P, UnitTypeID_P, IsBase_P, BaseMultiplier_P, CREATEBY);


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
    public static string UpdateRegion(string UnitID, string UnitTitle, string DisplayName,  string UnitTypeID,  string IsBase, string BaseMultiplier)
    {
        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter UnitID_P = new SqlParameter("@UnitID", UnitID);
        SqlParameter UnitTitle_P = new SqlParameter("@UnitTitle", UnitTitle);
        SqlParameter DisplayName_P = new SqlParameter("@DisplayName", DisplayName);
        SqlParameter UnitTypeID_P = new SqlParameter("@UnitTypeID", UnitTypeID);
        SqlParameter IsBase_P = new SqlParameter("@IsBase", IsBase);
        SqlParameter BaseMultiplier_P = new SqlParameter("@BaseMultiplier", BaseMultiplier);

        msg = AACommon.Execute("ITM_UNIT_Update", Conn, UnitID_P,  UnitTitle_P, DisplayName_P,UnitTypeID_P, IsBase_P,BaseMultiplier_P );


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


        SqlParameter UnitID_P = new SqlParameter("@UnitID", UnitID);
        SqlParameter DeleteBy_P = new SqlParameter("@DeleteBy", UserID);
        msg = AACommon.Execute("ITM_UNIT_Delete", Conn, UnitID_P, DeleteBy_P);


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
        DataSet ds = AACommon.ReturnDatasetBySPWithoutParameter("ITM_UNIT_TYPE_Get", Conn);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClass dbdc = new GetRegionClass();

                dbdc.UnitTypeID = ds.Tables[0].Rows[i][0].ToString();
                dbdc.UnitTypeDesc = ds.Tables[0].Rows[i][1].ToString();
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



    }






    [WebMethod]
    public static string LoadRegion()
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataSet ds = AACommon.ReturnDatasetBySPWithoutParameter("ITM_UNIT_Get", Conn);
        List<GetRegionClasss> RegionList = new List<GetRegionClasss>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClasss dbdc = new GetRegionClasss();

                dbdc.UnitID = ds.Tables[0].Rows[i][0].ToString();
                dbdc.UnitTitle = ds.Tables[0].Rows[i][1].ToString();
                dbdc.DisplayName = ds.Tables[0].Rows[i][2].ToString();
                dbdc.UnitTypeID = ds.Tables[0].Rows[i][3].ToString();
                dbdc.IsBase = ds.Tables[0].Rows[i][4].ToString();
                dbdc.BaseMultiplier = ds.Tables[0].Rows[i][5].ToString();


                RegionList.Insert(i, dbdc);
            }

        }


        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);


    }

    public class GetRegionClasss
    {
        public string UnitID { get; set; }
        public string UnitTitle { get; set; }
        public string DisplayName { get; set; }
        public string UnitTypeID { get; set; }
        public string IsBase { get; set; }
        public string BaseMultiplier { get; set; }



    }











}