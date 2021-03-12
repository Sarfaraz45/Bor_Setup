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

public partial class PROCUREMENT_PO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    public class GetRegionClasss
    {
        public string UnitTypeID { get; set; }
        public string UnitTypeDesc { get; set; }



    }


    [WebMethod]
    public static string LoadNUMBER(string frmt, string tbl, string col, SqlConnection Con)
    {
        string vNO = "";
        string vDate = "";
        string mm1 = System.DateTime.Now.Month.ToString();
        string yy1 = System.DateTime.Now.Year.ToString();
        //SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataSet dsDATE = AACommon.ReturnDatasetBySP("Get_CurrentDate", Con, null);
        if (dsDATE.Tables[0].Rows.Count > 0)
        {
            mm1 = dsDATE.Tables[0].Rows[0]["MM"].ToString();
            yy1 = dsDATE.Tables[0].Rows[0]["YY"].ToString();
            vDate = mm1 + "/" + dsDATE.Tables[0].Rows[0]["DD"].ToString() + "/" + yy1;
        }
        dsDATE.Dispose();
        yy1 = Convert.ToString(Convert.ToInt32(yy1) - 2000);

        if (mm1.Length == 1) { mm1 = "0" + mm1; }
        if (yy1.Length == 1) { yy1 = "0" + yy1; }
        string format1 = frmt + "-" + mm1 + "-" + yy1 + "-";

        vNO = AACommon.GetAlphaNumericIDSIX(tbl, format1, col, Con);
        vNO = vNO + "`" + vDate;
        return vNO;
    }


    
    [WebMethod]
    public static string LOAD_ITEMS(string UserID)
    {

        string acc = ""; string htmUNT = "";
        //string query = "SELECT  ITEMID, ITEMID+ ' ^ ' +ITEMName as ITEMName, ItemCode,UnitTypeID FROM ITM_ITEM WHERE ([IsDelete] = '0')";
        string query = "SELECT        dbo.ITM_ITEM.ITEMID, dbo.ITM_ITEM.ITEMID + ' ^ ' + dbo.ITM_ITEM.ITEMName + ' ^ ' + dbo.Category.CatTitle + ' ^ ' + dbo.Brand.BrandTitle AS ITEMName, dbo.ITM_ITEM.ItemCode,                           dbo.ITM_ITEM.UnitTypeID FROM            dbo.ITM_ITEM INNER JOIN                          dbo.Category ON dbo.ITM_ITEM.CatID = dbo.Category.CatID INNER JOIN                          dbo.Brand ON dbo.ITM_ITEM.BrandID = dbo.Brand.BrandID WHERE        (dbo.ITM_ITEM.ISDELETE = '0')";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(query, Con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            string itmID = dt.Rows[i - 1]["ITEMID"].ToString();
            string UntTyp = dt.Rows[i - 1]["UnitTypeID"].ToString();
            //'["Karachi","Hyderabad","USA","Arkansas","California","Colorado","Connecticut","Delaware","Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas","Kentucky"]'
            if (i == 1)
            { acc = acc + "['" + dt.Rows[i - 1]["ITEMName"].ToString() + "'"; }
            else if (i != 1 && i < dt.Rows.Count)
            { acc = acc + ",'" + dt.Rows[i - 1]["ITEMName"].ToString() + "'"; }
            else
            { acc = acc + ",'" + dt.Rows[i - 1]["ITEMName"].ToString() + "']"; }

            htmUNT = htmUNT+LoadUNITS(itmID, UntTyp);
        }
        acc = acc + "`" + LoadNUMBER("TR-U-IN", "WR_MASTER_MASTER_2", "WRIDMaster", Con) + "`" + htmUNT;
        return acc;
    }

    [WebMethod]
    public static string LoadUNITS(string itmID, string UntTyp)
    {
        string vUNT = "";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter UnitTypeID = new SqlParameter("@UnitTypeID", UntTyp);
        DataSet ds = AACommon.ReturnDatasetBySP("ITM_UNITS_GET", Con, UnitTypeID);
        vUNT = "<span id='spnUNT" + itmID + "' style='display:none;'>";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++ )
        {
            if (i == 0)
            {
                vUNT = vUNT + ds.Tables[0].Rows[i]["UnitID"].ToString() + "^" + ds.Tables[0].Rows[i]["DisplayName"].ToString();
            }
            else
            {
                vUNT = vUNT + "~" + ds.Tables[0].Rows[i]["UnitID"].ToString() + "^" + ds.Tables[0].Rows[i]["DisplayName"].ToString();
            }
            
        }
        ds.Dispose();
        vUNT = vUNT + "</span>";
        return vUNT;
    }

    [WebMethod]
    public static string SaveTransaction(string UserID, string str, string txtVDate, string txtVoucherNo, string txtDescription, string txtShopName)
    {
        string msg = "";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        string rqID = "";        
        string rqDt = "";
        string rqBy = UserID;        
        ///////////////////////// ID GNERATOR /////////////////////////////        
        string aa = LoadNUMBER("TR-U-IN", "WR_MASTER_MASTER_2", "WRIDMaster", Con);        
        string[] idDT = aa.Split('`');
        rqID = idDT[0].ToString();
        rqDt = idDT[1].ToString();

        /////////////////////////// INSERT in SP MASTER //////////////////
        SqlCommand cmdMasterInsert = new SqlCommand("insert into WR_MASTER_MASTER_2 (WRIDMaster,WRDateMaster,SP,CreateBy,ShopName,CustomerName) values ('" + txtVoucherNo + "','" + txtVDate + "','P','" + UserID + "','"+txtShopName+"','"+txtDescription+"')", Con);
        Con.Open();
        cmdMasterInsert.ExecuteNonQuery();
        Con.Close();
        
        /////////////////////// INSERT in SP DETAIL

        string[] itmLST = str.Split('`');
        for (int i = 0; i < itmLST.Length; i++)
        {
            string[] itmROW = itmLST[i].Split('^');        
            string itmID = itmROW[0].ToString();        
            string txtQty = itmROW[2].ToString();
            string txtSrNo = itmROW[3].ToString();

            SqlCommand cmdDetailInsert = new SqlCommand("insert into WR_DETAIL_MASTER_2 (WRIDMaster,ITEMID,UnitID,QtyIn,QtyOut,QtyAvailed,Type,CreateBy,SrNo) values ('" + txtVoucherNo + "','" + itmID + "','UN-000050','" + txtQty + "','0','0','P','" + UserID + "','" + txtSrNo + "')", Con);
            Con.Open();
            cmdDetailInsert.ExecuteNonQuery();
            Con.Close();

        }

        ////////////////////SAVE TRANSACTION IN ACCOUNTS ////////////////////

        return msg;
    }


}