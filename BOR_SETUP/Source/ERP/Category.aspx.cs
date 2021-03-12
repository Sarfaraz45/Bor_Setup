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

public partial class ERP_UnitType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod]
    public static string InsertRegion(string UnitType, string UserID,string BranchID)
    {

        string retMessage = string.Empty;        
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        string ID = AACommon.GetAlphaNumericIDSIX("Category", "CAT-", "CatID", Conn);
        SqlCommand cmd = new SqlCommand("insert into Category (CatID,CatTitle,CreateBy,BranchID) values ('" + ID + "','" + UnitType + "','" + UserID + "','" + BranchID + "')", Conn);
        Conn.Open();
        int a  = cmd.ExecuteNonQuery();
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
    public static string UpdateRegion(string UnitTypeID, string UnitTypeDesc, string BranchID)
    {
        string retMessage = string.Empty;        
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Update Category set CatTitle='" + UnitTypeDesc + "' where CatID='" + UnitTypeID + "' and BranchID='" + BranchID + "'", Conn);
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
    public static string DeleteRegion(string UnitTypeID, string UserID, string BranchID)
    {

        string retMessage = string.Empty;        
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Update Category set DeleteBy='" + UserID + "',DeleteDate='" + DateTime.Now + "',ISDELETE=1 where CatID='" + UnitTypeID + "' and BranchID='" + BranchID + "'", Conn);
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
        SqlDataAdapter da = new SqlDataAdapter("select * from Category Where IsDelete=0 and BranchID='" + BranchID + "'", Conn);
        DataSet ds = new DataSet();
        da.Fill(ds);
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





}