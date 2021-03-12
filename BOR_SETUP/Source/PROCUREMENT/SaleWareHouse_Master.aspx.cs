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
        string query = "select * from VW_ITEM_FOR_OUT_MASTER_WAREHOUSE";
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
        
        acc = acc + "`" + LoadNUMBER("TR-M-OUT", "WR_MASTER_MASTER", "WRIDMaster", Con) + "`" + htmUNT;
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
        string rqHID = "";
        string rqDt = "";
        string rqBy = UserID;
        string rqAt = "";
        string stsID = "";// 0:Pending, 1:Approved, 2:Rejected


        ///////////////////////// ID GNERATOR /////////////////////////////
        string aa = LoadNUMBER("TR-M-OUT", "WR_MASTER_MASTER", "WRIDMaster", Con);        
        string[] idDT = aa.Split('`');
        rqID = idDT[0].ToString();
        rqDt = idDT[1].ToString();

       // rqHID = LoadHistoryID(rqID);

        
       

        /////////////////////////// INSERT in SP MASTER //////////////////
        SqlCommand cmdMasterInsert = new SqlCommand("insert into WR_MASTER_MASTER (WRIDMaster,WRDateMaster,SP,CreateBy,ShopName,CustomerName) values ('" + txtVoucherNo + "','" + txtVDate + "','S','" + UserID + "','" + txtShopName + "','" + txtDescription + "')", Con);
        Con.Open();
        cmdMasterInsert.ExecuteNonQuery();
        Con.Close();
        

        /////////////////////// INSERT in SP DETAIL

        string[] itmLST = str.Split('`');
        for (int i = 0; i < itmLST.Length; i++)
        {
            string[] itmROW = itmLST[i].Split('^');
            //txtAccID + "^" + txtAccTitle + "^" + ddlUnit + "^" + txtQty + "^" + txtPrice + "^" + txtTotalPrice
            string itmID = itmROW[0].ToString();
            string ddlUnit = itmROW[2].ToString();
            string txtQty = itmROW[3].ToString();
            string PRID = itmROW[4].ToString();
            string PRDID = itmROW[5].ToString();
            string txtSrNo = itmROW[6].ToString();

            SqlCommand cmdDetailInsert = new SqlCommand("insert into WR_DETAIL_MASTER (WRIDMaster,ITEMID,UnitID,QtyIn,QtyOut,QtyAvailed,Type,CreateBy,SrNo) values ('" + txtVoucherNo + "','" + itmID + "','UN-000050','0','" + txtQty + "','0','S','" + UserID + "','" + txtSrNo + "')", Con);
            Con.Open();
            cmdDetailInsert.ExecuteNonQuery();
            Con.Close();

            PRID = PRID.Replace(" Purchase # : ", "");
            PRDID = PRDID.Replace(" ID : ", "");
            SqlDataAdapter da = new SqlDataAdapter("select * from  WR_DETAIL_MASTER where WRIDMaster='" + PRID + "' and ITEMID='" + itmID + "' and ID='" + PRDID + "'", Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                double AvailedQty = Convert.ToDouble(dt.Rows[0]["QtyAvailed"].ToString());
                double Qty = Convert.ToDouble(txtQty);
                AvailedQty = AvailedQty + Qty;
                SqlCommand cmd = new SqlCommand("update WR_DETAIL_MASTER set QtyAvailed='" + AvailedQty + "' where WRIDMaster='" + PRID + "' and ITEMID='" + itmID + "' and ID='" + PRDID + "'", Con);
                Con.Open();
                cmd.ExecuteNonQuery();
                Con.Close();

                SqlDataAdapter da1 = new SqlDataAdapter("select MAX(ID) from  WR_DETAIL_MASTER where WRIDMaster='" + rqID + "' and ITEMID='" + itmID + "'", Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("update WR_DETAIL_MASTER set BatchID='" + PRDID + "' where ID='" + dt1.Rows[0][0].ToString() + "'", Con);
                    Con.Open();
                    cmd1.ExecuteNonQuery();
                    Con.Close();
                }
            }

           
        }


            return msg;
    }


}