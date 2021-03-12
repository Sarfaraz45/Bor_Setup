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
using System.IO;


public partial class PROCUREMENT_PO_LIST : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string LoadLIST(string UserID)
    {
        string htm = "";
        htm = htm + "<table id='data-table' class='table table-striped' >";
        htm = htm + "<thead><tr>";
        htm = htm + "<th>TR ID</th>";
        htm = htm + "<th>TR Date</th>";
        htm = htm + "<th>Party</th>";
        htm = htm + "<th>Description / Narration</th>";        
        htm = htm + "<th></th>";
        htm = htm + "</tr>";
        htm = htm + "</thead>";
        htm = htm + "<tbody>";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataSet ds = AACommon.ReturnDatasetBySP("TR_LIST_Master_2", Con, null);
        DataTable dt = new DataTable();
        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];        
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            htm = htm + "<tr >";
            htm = htm + "<td>" + dt.Rows[i]["WRIDMaster"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["WRDateMaster"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["ShopName"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["CustomerName"].ToString() + "</td>";
            htm = htm + "<td><a href='../REPORTS/TR_IN_HIS_2.aspx?ID=" + dt.Rows[i]["WRIDMaster"].ToString() + "&Type=Detail' target='_blank'>View Report</a></td>";            
            htm = htm + "</tr>";
        }
        htm = htm + "</tbody>";
        htm = htm + "</table>";
        return htm;
    }

    [WebMethod]
    public static string LoadLISTSearch(string UserID, string PaymentType)
    {
        string htm = "";
        htm = htm + "<table id='data-table' class='table table-striped' >";
        htm = htm + "<thead><tr>";
        htm = htm + "<th>TR ID</th>";
        htm = htm + "<th>TR Date</th>";
        htm = htm + "<th>Party</th>";
        htm = htm + "<th>Description / Narration</th>";        
        htm = htm + "<th></th>";
        htm = htm + "</tr>";
        htm = htm + "</thead>";
        htm = htm + "<tbody>";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter SPID = new SqlParameter("@WRIDMaster", PaymentType);
        DataSet ds = AACommon.ReturnDatasetBySP("TR_LIST_Search_Master_2", Con, SPID);
        DataTable dt = new DataTable();
        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            htm = htm + "<tr >";
            htm = htm + "<td>" + dt.Rows[i]["WRIDMaster"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["WRDateMaster"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["ShopName"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["CustomerName"].ToString() + "</td>";
            htm = htm + "<td><a href='../REPORTS/TR_IN_HIS_2.aspx?ID=" + dt.Rows[i]["WRIDMaster"].ToString() + "&Type=Detail' target='_blank'>View Report</a></td>";
            htm = htm + "</tr>";
        }
        htm = htm + "</tbody>";
        htm = htm + "</table>";
        return htm;
    }

    [WebMethod]
    public static string LOAD_ITEMS(string UserID)
    {

        string acc = "";
        //string htmUNT = "";
        string query = "SELECT        TOP (100) PERCENT 'Voucher ID : ' + dbo.WR_MASTER_MASTER_2.WRIDMaster + '       ----------^^^^^^^^^----------       Date : ' + CONVERT(nvarchar(20), dbo.WR_MASTER_MASTER_2.WRDateMaster, 106) AS Title, dbo.WR_MASTER_MASTER_2.WRIDMaster AS TaskID FROM            dbo.WR_MASTER_MASTER_2 WHERE        (dbo.WR_MASTER_MASTER_2.ISDELETE = 0) AND (dbo.WR_MASTER_MASTER_2.SP = 'P') ORDER BY TaskID DESC";
        //string query = "SELECT  ITEMID, ITEMID+ ' ^ ' +ITEMName as ITEMName, ItemCode,UnitTypeID FROM ITM_ITEM WHERE ([IsDelete] = '0')";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(query, Con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            string itmID = dt.Rows[i - 1]["TaskID"].ToString();
            string UntTyp = dt.Rows[i - 1]["TaskID"].ToString();
            //'["Karachi","Hyderabad","USA","Arkansas","California","Colorado","Connecticut","Delaware","Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas","Kentucky"]'
            if (dt.Rows.Count == 1)
            { acc = acc + "['" + dt.Rows[i - 1]["Title"].ToString() + "']"; }
            else
            {
                if (i == 1)
                { acc = acc + "['" + dt.Rows[i - 1]["Title"].ToString() + "'"; }
                else if (i != 1 && i < dt.Rows.Count)
                { acc = acc + ",'" + dt.Rows[i - 1]["Title"].ToString() + "'"; }
                else
                { acc = acc + ",'" + dt.Rows[i - 1]["Title"].ToString() + "']"; }
            }
            //htmUNT = htmUNT + LoadUNITS(itmID, UntTyp);
        }
        //acc = acc + "`" + LoadNUMBER("PR", "SP_MASTER", "SPID", Con) + "`" + htmUNT;

        return acc;
    }
}