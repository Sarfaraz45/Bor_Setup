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
     //   LoadOrder();
    }
    [WebMethod]
    public static string LOAD_ITEMS(string UserID, string BranchID)
    {

        string acc = "";
        //string htmUNT = "";
        string query = "SELECT      'Voucher ID : ' + TaskID + '       ----------^^^^^^^^^----------       Date : ' + CONVERT(nvarchar(20), Date, 106) + '       ----------^^^^^^^^^----------       Amount : ' + CONVERT(Nvarchar(20), AmountPKR)  AS Title, TaskID  FROM            dbo.tbl_transaction WHERE        (IsDelete = 0) AND (TaskID LIKE '%PV%' OR TaskID LIKE '%C-CS%') and BranchID='"+ BranchID +"' ORDER BY TaskID DESC";
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

    [WebMethod]
    public static string LoadLIST(string UserID, string PaymentType, string BranchID)
    {
        string htm = "";
        htm = htm + "<table id='data-table' class='table table-striped table-bordered' >";
        htm = htm + "<thead><tr>";
        htm = htm + "<th>Voucher ID</th>";
        htm = htm + "<th>Date</th>";
        htm = htm + "<th>Amount PKR</th>";
        htm = htm + "<th>Amount RMB</th>";
        htm = htm + "<th>Account</th>";
        htm = htm + "<th>Debit PKR</th>";
        htm = htm + "<th>Credit PKR</th>";
        htm = htm + "<th>Narration</th>";
        //htm = htm + "<th>Supplier</th>";
        //htm = htm + "<th></th>";
        htm = htm + "</tr>";
        htm = htm + "</thead>";
        htm = htm + "<tbody>";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter Type = new SqlParameter("@TaskID", PaymentType);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("VoucherDetail_PVD_PVC", Con, Type, Branch);
        DataTable dt = new DataTable();
        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];        
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            htm = htm + "<tr >";
            htm = htm + "<td>" + dt.Rows[i]["TaskID"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["Date"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["AmountPKR"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["AmountRMB"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["AccountsTitle"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["DebitPKR"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["CreditPKR"].ToString() + "</td>";
            htm = htm + "<td>" + dt.Rows[i]["Narration"].ToString() + "</td>";
            //htm = htm + "<td>" + dt.Rows[i]["AccountsTitle"].ToString() + "</td>";
            //htm = htm + "<td><a href='#' onclick='Delete(" + dt.Rows[i]["ID"].ToString() + ");'>Delete</a></td>";
            htm = htm + "</tr>";
        }
        htm = htm + "</tbody>";
        htm = htm + "</table>";
        return htm;
    }


    //public void LoadOrder()
    //{ 
    //    SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
    //    SqlDataAdapter da = new SqlDataAdapter("select 'Voucher ID : '+ TaskID + '       ----------^^^^^^^^^----------       Date : '+convert(nvarchar(20),Date,106) AS Title,TaskID  from tbl_transaction where IsDelete=0 and TaskID like '%PV%' order by TaskID Desc", Con);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlType.DataSource = dt;
    //        ddlType.DataTextField = "Title";
    //        ddlType.DataValueField = "TaskID";
    //        ddlType.DataBind();
            
            

    //    }
    //}

    [WebMethod]
    public static string DeleteRegion(string ID, string BranchID)
    {

        string retMessage = string.Empty;
        int a = 0;
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        SqlCommand cmddeletedetail = new SqlCommand("delete from Transaction_Detail where TaskID='" + ID + "' and BranchID='"+BranchID+"'", Conn);
        Conn.Open();
        a = cmddeletedetail.ExecuteNonQuery();
        Conn.Close();

        SqlCommand cmddeleteMaster = new SqlCommand("delete from tbl_transaction where TaskID='" + ID + "' and BranchID='" + BranchID + "'", Conn);
        Conn.Open();
        a = cmddeleteMaster.ExecuteNonQuery();
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



}
