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
    public static string InsertRegion(string UnitType, string UserID, string BranchID)
    {

        string retMessage = string.Empty;        
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlCommand cmdupdate = new SqlCommand("update CurrencyConversion set IsActive=0 where BranchID='" + BranchID + "'", Conn);
        Conn.Open();
        cmdupdate.ExecuteNonQuery();
        Conn.Close();

        SqlCommand cmd = new SqlCommand("insert into CurrencyConversion (RMBValue,BranchID) values ('" + UnitType + "','" + BranchID + "')", Conn);
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




        public class GetRegionClass
    {
        public string UnitTypeID { get; set; }
        public string UnitTypeDesc { get; set; }

    }





}