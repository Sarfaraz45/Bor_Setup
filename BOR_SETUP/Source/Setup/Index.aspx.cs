using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Serialization;


public partial class Setup_Index : System.Web.UI.Page
{
    public SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadData();
    }

    public void LoadData()
    {
        SqlDataAdapter da = new SqlDataAdapter("select count(ITEMID) from ITM_ITEM where IsDelete=0", Conn);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            TotalDocuments.InnerText = dt.Rows[0][0].ToString();
            TotalDocument2.InnerHtml = "<span class='append-icon'><i class='fa fa-map-marker'></i></span>" + dt.Rows[0][0].ToString();
        }

        SqlDataAdapter da1 = new SqlDataAdapter("select count(CatID) from Category where IsDelete=0", Conn);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            TotalCategories.InnerText = dt1.Rows[0][0].ToString();
        }

        SqlDataAdapter da2 = new SqlDataAdapter("select count(BrandID) from BRand where IsDelete=0", Conn);
        DataTable dt2 = new DataTable();
        da2.Fill(dt2);
        if (dt2.Rows.Count > 0)
        {
            TotalFolders.InnerText = dt2.Rows[0][0].ToString();
        }

        SqlDataAdapter da3 = new SqlDataAdapter("select count(UserID) from Users where IsDelete=0", Conn);
        DataTable dt3 = new DataTable();
        da3.Fill(dt3);
        if (dt3.Rows.Count > 0)
        {
            TotalUsers.InnerText = dt3.Rows[0][0].ToString();
        }

        //SqlDataAdapter da4 = new SqlDataAdapter("select count(ID) from DocumentDetail where IsDelete=0 and convert(nvarchar(20),CreateDate,106)=convert(nvarchar(20),GetDate(),106)", Conn);
        //DataTable dt4 = new DataTable();
        //da4.Fill(dt4);
        //if (dt4.Rows.Count > 0)
        //{
        //    TodaysDocument.InnerText = dt4.Rows[0][0].ToString();
        //}
    }

    public class GetDistrictClass
    {
        public string Checked { get; set; }
        public string User { get; set; }



    } 
    [WebMethod]
    public static string LoadDetailRegion()
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("SELECT        SUM(dbo.SP_DETAIL.QtyIn) - SUM(dbo.SP_DETAIL.QtyOut) AS CC, dbo.ITM_ITEM.ITEMName AS DesignationTitle FROM            dbo.ITM_ITEM INNER JOIN                          dbo.SP_DETAIL ON dbo.ITM_ITEM.ITEMID = dbo.SP_DETAIL.ITEMID WHERE        (dbo.ITM_ITEM.ISDELETE = 0) GROUP BY dbo.ITM_ITEM.ITEMName", Conn);
        DataSet ds = new DataSet();
        da.Fill(ds);
        List<GetDistrictClass> RegionList = new List<GetDistrictClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetDistrictClass dbdc = new GetDistrictClass();

                dbdc.Checked = ds.Tables[0].Rows[i]["CC"].ToString();
                dbdc.User = ds.Tables[0].Rows[i]["DesignationTitle"].ToString();
                RegionList.Insert(i, dbdc);
            }

        }


        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);


    }
}