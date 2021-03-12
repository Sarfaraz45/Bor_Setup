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

public partial class ERP_ITM_Item : System.Web.UI.Page
{
    public static List<string> frequentItems = new List<string>();    
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string LoadRegion(string Category)
    {
     
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        if (Category == "Brand")
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT        dbo.SP_MASTER.SPID, dbo.SP_MASTER.SPDate, dbo.Accounts.AccountsID, dbo.Accounts.AccountsTitle FROM            dbo.SP_MASTER INNER JOIN                          dbo.Accounts ON dbo.SP_MASTER.AccountID = dbo.Accounts.AccountsID", Conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string[][] arr = new string[ds.Tables[0].Rows.Count][];
            string[][] arrdetail = new string[3][];
            int c = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("SELECT        dbo.Accounts.AccountsID, dbo.Accounts.AccountsTitle, dbo.SP_DETAIL.ITEMID, dbo.SP_DETAIL.QtyIn, dbo.SP_DETAIL.UnitPrice, dbo.SP_MASTER.SPDate FROM            dbo.SP_MASTER INNER JOIN                          dbo.SP_DETAIL ON dbo.SP_MASTER.SPID = dbo.SP_DETAIL.SPID INNER JOIN                          dbo.Accounts ON dbo.SP_MASTER.AccountID = dbo.Accounts.AccountsID where SP_MASTER.SPID='" + ds.Tables[0].Rows[i]["SPID"].ToString() + "'", Conn);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        arrdetail[c] = new string[dt1.Rows.Count];
                        for (int k = 0; k < dt1.Rows.Count; k++)
                        {
                            arrdetail[c][k] = dt1.Rows[k]["ITEMID"].ToString();
                        }
                        c++;
                    }

                }

                for (int i = 0; i < arrdetail.Length; i++)
                {
                    for (int j = 0; j < arrdetail[i].Length; j++)
                    {
                        if (!frequentItems.Contains(arrdetail[i][j])) //aggr arr[i,j] exixts na krta ho list(frequentItems) main tw list main add krdo value arr[i,j]
                        { //if (arr[i][j] !=  frequentItems[j])

                            frequentItems.Add(arrdetail[i][j]);
                        }
                    }
                }


            }


            int[] itemsCount = new int[frequentItems.Count];
            for (int k = 0; k < frequentItems.Count; k++)
            {
                int count = 0;
                for (int i = 0; i < arrdetail.Length; i++)
                {
                    for (int j = 0; j < arrdetail[i].Length; j++)
                    {
                        if (frequentItems[k] == arrdetail[i][j])
                        {
                            count++;
                        }
                    }
                }
                itemsCount[k] = count;
            }

            int sum = 0;
            for (int i = 0; i < itemsCount.Length; i++)
            {
                sum = sum + itemsCount[i];
            }
            int average = sum / itemsCount.Length; // check sealing or flouring properly in average



            List<int> itmcountlist = itemsCount.ToList<int>();
            // int counter = 0;
            for (int i = 0; i < itmcountlist.Count; i++)
            {
                if (itmcountlist[i] <= average)
                {
                    //swap(i, itemsCount);

                    frequentItems.RemoveAt(i);
                    itmcountlist.RemoveAt(i);
                    i--;
                    // counter++;
                }
            }



            SqlDataAdapter daexp = new SqlDataAdapter("SELECT        dbo.SP_DETAIL.ITEMID, dbo.SP_MASTER.SPDate, dbo.Brand.BrandID, dbo.Brand.BrandTitle FROM            dbo.SP_DETAIL INNER JOIN                          dbo.SP_MASTER ON dbo.SP_DETAIL.SPID = dbo.SP_MASTER.SPID INNER JOIN                          dbo.ITM_ITEM ON dbo.SP_DETAIL.ITEMID = dbo.ITM_ITEM.ITEMID INNER JOIN                          dbo.Brand ON dbo.ITM_ITEM.BrandID = dbo.Brand.BrandID WHERE       (CONVERT(date, dbo.SP_MASTER.SPDate) <= DATEADD(day, 30, getdate()))", Conn);
            DataTable dtexp = new DataTable();
            daexp.Fill(dtexp);
            if (dtexp.Rows.Count > 0)
            {
                SqlCommand cmdd = new SqlCommand("delete from BrandPackage", Conn);
                Conn.Open();
                cmdd.ExecuteNonQuery();
                Conn.Close();

                for (int i = 0; i < frequentItems.Count; i++)
                {
                    SqlDataAdapter daa = new SqlDataAdapter("SELECT        dbo.ITM_ITEM.ITEMID, dbo.ITM_ITEM.ITEMName, dbo.Brand.BrandID, dbo.Brand.BrandTitle FROM            dbo.ITM_ITEM INNER JOIN       dbo.Brand ON dbo.ITM_ITEM.BrandID = dbo.Brand.BrandID WHERE        (dbo.ITM_ITEM.ISDELETE = 0) AND    dbo.ITM_ITEM.ITEMID='" + frequentItems[i] + "'", Conn);
                    DataTable dtt = new DataTable();
                    daa.Fill(dtt);
                    if (dtt.Rows.Count > 0)
                    {
                        SqlCommand cmd = new SqlCommand("insert into BrandPackage (ITEMID,ITEMName,BrandID,BrandName) values ('" + dtt.Rows[0]["ITEMID"].ToString() + "','" + dtt.Rows[0]["ITEMName"].ToString() + "','" + dtt.Rows[0]["BrandID"].ToString() + "','" + dtt.Rows[0]["BrandTitle"].ToString() + "')", Conn);
                        Conn.Open();
                        cmd.ExecuteNonQuery();
                        Conn.Close();
                    }
                }
            }
            SqlDataAdapter dafinal = new SqlDataAdapter("select *,'Brand' As Brand from BrandPackage", Conn);
            DataTable dtfinal = new DataTable();
            dafinal.Fill(dtfinal);
            List<GetRegionClass> RegionList = new List<GetRegionClass>();
            RegionList.Clear();
            if (dtfinal.Rows.Count > 0)
            {
                for (int i = 0; i < dtfinal.Rows.Count; i++)
                {
                    GetRegionClass dbdc = new GetRegionClass();

                    dbdc.ITEMID = dtfinal.Rows[i][1].ToString();
                    dbdc.ITEMName = dtfinal.Rows[i][2].ToString();
                    dbdc.ItemCode = dtfinal.Rows[i][3].ToString();
                    dbdc.BarCode = dtfinal.Rows[i][4].ToString();
                   dbdc.Type = dtfinal.Rows[i][5].ToString();
                    RegionList.Insert(i, dbdc);
                }

            }


            JavaScriptSerializer jser = new JavaScriptSerializer();


            return jser.Serialize(RegionList);
        }
        else
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT        dbo.SP_MASTER.SPID, dbo.SP_MASTER.SPDate, dbo.Accounts.AccountsID, dbo.Accounts.AccountsTitle FROM            dbo.SP_MASTER INNER JOIN                          dbo.Accounts ON dbo.SP_MASTER.AccountID = dbo.Accounts.AccountsID", Conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string[][] arr = new string[ds.Tables[0].Rows.Count][];
            string[][] arrdetail = new string[3][];
            int c = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("SELECT        dbo.Accounts.AccountsID, dbo.Accounts.AccountsTitle, dbo.SP_DETAIL.ITEMID, dbo.SP_DETAIL.QtyIn, dbo.SP_DETAIL.UnitPrice, dbo.SP_MASTER.SPDate FROM            dbo.SP_MASTER INNER JOIN                          dbo.SP_DETAIL ON dbo.SP_MASTER.SPID = dbo.SP_DETAIL.SPID INNER JOIN                          dbo.Accounts ON dbo.SP_MASTER.AccountID = dbo.Accounts.AccountsID where SP_MASTER.SPID='" + ds.Tables[0].Rows[i]["SPID"].ToString() + "'", Conn);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        arrdetail[c] = new string[dt1.Rows.Count];
                        for (int k = 0; k < dt1.Rows.Count; k++)
                        {
                            arrdetail[c][k] = dt1.Rows[k]["ITEMID"].ToString();
                        }
                        c++;
                    }

                }

                for (int i = 0; i < arrdetail.Length; i++)
                {
                    for (int j = 0; j < arrdetail[i].Length; j++)
                    {
                        if (!frequentItems.Contains(arrdetail[i][j])) //aggr arr[i,j] exixts na krta ho list(frequentItems) main tw list main add krdo value arr[i,j]
                        { //if (arr[i][j] !=  frequentItems[j])

                            frequentItems.Add(arrdetail[i][j]);
                        }
                    }
                }


            }


            int[] itemsCount = new int[frequentItems.Count];
            for (int k = 0; k < frequentItems.Count; k++)
            {
                int count = 0;
                for (int i = 0; i < arrdetail.Length; i++)
                {
                    for (int j = 0; j < arrdetail[i].Length; j++)
                    {
                        if (frequentItems[k] == arrdetail[i][j])
                        {
                            count++;
                        }
                    }
                }
                itemsCount[k] = count;
            }

            int sum = 0;
            for (int i = 0; i < itemsCount.Length; i++)
            {
                sum = sum + itemsCount[i];
            }
            int average = sum / itemsCount.Length; // check sealing or flouring properly in average



            List<int> itmcountlist = itemsCount.ToList<int>();
            // int counter = 0;
            for (int i = 0; i < itmcountlist.Count; i++)
            {
                if (itmcountlist[i] <= average)
                {
                    //swap(i, itemsCount);

                    frequentItems.RemoveAt(i);
                    itmcountlist.RemoveAt(i);
                    i--;
                    // counter++;
                }
            }



            SqlDataAdapter daexp = new SqlDataAdapter("SELECT        dbo.SP_DETAIL.ITEMID, dbo.SP_MASTER.SPDate, dbo.Category.CatID As BrandID, dbo.Category.CatTitle AS BrandTitle FROM            dbo.SP_DETAIL INNER JOIN                          dbo.SP_MASTER ON dbo.SP_DETAIL.SPID = dbo.SP_MASTER.SPID INNER JOIN                          dbo.ITM_ITEM ON dbo.SP_DETAIL.ITEMID = dbo.ITM_ITEM.ITEMID INNER JOIN                          dbo.Category ON dbo.ITM_ITEM.CatID = dbo.Category.CatID WHERE       (CONVERT(date, dbo.SP_MASTER.SPDate) <= DATEADD(day, 30, getdate()))", Conn);
            DataTable dtexp = new DataTable();
            daexp.Fill(dtexp);
            if (dtexp.Rows.Count > 0)
            {
                SqlCommand cmdd = new SqlCommand("delete from CategoryPackage", Conn);
                Conn.Open();
                cmdd.ExecuteNonQuery();
                Conn.Close();

                for (int i = 0; i < frequentItems.Count; i++)
                {
                    SqlDataAdapter daa = new SqlDataAdapter("SELECT        dbo.ITM_ITEM.ITEMID, dbo.ITM_ITEM.ITEMName, dbo.Category.CatID, dbo.Category.CatTitle FROM            dbo.ITM_ITEM INNER JOIN                          dbo.Category ON dbo.ITM_ITEM.CatID = dbo.Category.CatID  WHERE        (dbo.ITM_ITEM.ISDELETE = 0) AND    dbo.ITM_ITEM.ITEMID='" + frequentItems[i] + "'", Conn);
                    DataTable dtt = new DataTable();
                    daa.Fill(dtt);
                    if (dtt.Rows.Count > 0)
                    {
                        SqlCommand cmd = new SqlCommand("insert into CategoryPackage (ITEMID,ITEMName,CatID,CatName) values ('" + dtt.Rows[0]["ITEMID"].ToString() + "','" + dtt.Rows[0]["ITEMName"].ToString() + "','" + dtt.Rows[0]["CatID"].ToString() + "','" + dtt.Rows[0]["CatTitle"].ToString() + "')", Conn);
                        Conn.Open();
                        cmd.ExecuteNonQuery();
                        Conn.Close();
                    }
                }
            }
            SqlDataAdapter dafinal = new SqlDataAdapter("select *,'Category' As Category from CategoryPackage", Conn);
            DataTable dtfinal = new DataTable();
            dafinal.Fill(dtfinal);
            List<GetRegionClass> RegionList = new List<GetRegionClass>();
            RegionList.Clear();
            if (dtfinal.Rows.Count > 0)
            {
                for (int i = 0; i < dtfinal.Rows.Count; i++)
                {
                    GetRegionClass dbdc = new GetRegionClass();

                    dbdc.ITEMID = dtfinal.Rows[i][1].ToString();
                    dbdc.ITEMName = dtfinal.Rows[i][2].ToString();
                    dbdc.ItemCode = dtfinal.Rows[i][3].ToString();
                    dbdc.BarCode = dtfinal.Rows[i][4].ToString();
                    dbdc.Type = dtfinal.Rows[i][5].ToString();
                    RegionList.Insert(i, dbdc);
                }

            }


            JavaScriptSerializer jser = new JavaScriptSerializer();


            return jser.Serialize(RegionList);
        }

    }

    public class GetRegionClass
    {
        public string ITEMID { get; set; }
        public string ITEMName { get; set; }
        public string ItemCode { get; set; }
        public string BarCode { get; set; }
        public string Discription { get; set; }
        public string UnitTypeID { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }


    }



    public class GetRegionClasss
    {
        public string UnitTypeID { get; set; }
        public string UnitTypeDesc { get; set; }



    }










}