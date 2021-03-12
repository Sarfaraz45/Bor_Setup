using System;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Serialization;

public partial class Login : System.Web.UI.Page
{


    public SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod]
    public static string loginUser(string LoginID, string password, string Branch)
    {

        password = Encryption.Encrypt(password);
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter UserID = new SqlParameter("@UserID", LoginID);
        SqlParameter Password = new SqlParameter("@Password", password);
        SqlParameter BranchID = new SqlParameter("@BranchID", Branch);
        DataSet ds = AACommon.ReturnDatasetBySP("USER_GET_BY_LOGINID", Conn, UserID, Password, BranchID);


        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClass dbdc = new GetRegionClass();

                dbdc.ID = ds.Tables[0].Rows[i]["UserID"].ToString();
                dbdc.Name = ds.Tables[0].Rows[i]["UserName"].ToString();
                dbdc.Login = ds.Tables[0].Rows[i]["loginID"].ToString();
                dbdc.Picture = ds.Tables[0].Rows[i]["Picture"].ToString();
                dbdc.Manager = ds.Tables[0].Rows[i]["ManagerID"].ToString();
                dbdc.UTID = ds.Tables[0].Rows[i]["UTID"].ToString();
                dbdc.Designation = ds.Tables[0].Rows[i]["UTID"].ToString();
                dbdc.BranchID = ds.Tables[0].Rows[i]["BranchID"].ToString();
                dbdc.UTDesc = ds.Tables[0].Rows[i]["UTDesc"].ToString();
                dbdc.BranchName = ds.Tables[0].Rows[i]["BranchName"].ToString();
                RegionList.Insert(i, dbdc);
            }

        }

        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);
    }

    public class GetRegionClass
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Picture { get; set; }
        public string Manager { get; set; }
        public string UTID { get; set; }
        public string Designation { get; set; }
        public string BranchID { get; set; }
        public string UTDesc { get; set; }
        public string BranchName { get; set; }       

    }







}