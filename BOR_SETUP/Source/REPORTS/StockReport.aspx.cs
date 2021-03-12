using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

public partial class REPORTS_Ledger_Updated : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LoadReport(object sender, EventArgs e)
    {
        var BranchID = "";
        var UTDesc = "";
         string Type = Request.QueryString["Type"].ToString();
         if (Request.Cookies["BranchID"] != null)
         {
             BranchID = Request.Cookies["BranchID"].Value;
         }
         if (Request.Cookies["UTDesc"] != null)
         {
             UTDesc = Request.Cookies["UTDesc"].Value;
         }
         if (Type == "Stock-Summary")
         {
             string rptName = "Stock.rpt";
             SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
             SqlParameter AccountsID = new SqlParameter("@ITEMID", ddlITEM.SelectedValue);
             SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
             DataSet ds = AACommon.ReturnDatasetBySPForREPORT("STOCK_BALANCE", "VW_STOCK_BALANCE", Con, AccountsID, Branch);
             if (UTDesc == "Sales")
             {
             for (int intCount = 0; intCount < ds.Tables[0].Rows.Count; intCount++)
             {
                
                     ds.Tables[0].Rows[intCount]["UnitPrice"] = "0";
                     ds.Tables[0].Rows[intCount]["UnitPriceOld"] = "0";
                
             }

             ds.Tables[0].AcceptChanges();
             }

             Session["RptDS"] = ds;
             Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_STOCK_BALANCE");
         }
         else
             if (Type == "Stock-Ledger")
             {
                 string rptName = "StockLedger.rpt";
                 SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                 SqlParameter AccountsID = new SqlParameter("@ITEMID", ddlITEM.SelectedValue);
                 SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
                 DataSet ds = AACommon.ReturnDatasetBySPForREPORT("STOCK_LEDGER", "VW_STOCK_LEDGER", Con, AccountsID, Branch);
                 if (UTDesc == "Sales")
                 {
                     for (int intCount = 0; intCount < ds.Tables[0].Rows.Count; intCount++)
                     {

                         ds.Tables[0].Rows[intCount]["UnitPrice"] = "0";
                         ds.Tables[0].Rows[intCount]["TotalPrice"] = "0";

                     }

                     ds.Tables[0].AcceptChanges();
                 }

                 Session["RptDS"] = ds;
                 Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_STOCK_LEDGER");
             }


    }

    protected void LoadReportCatBrand(object sender, EventArgs e)
    {
        var BranchID = "";
        var UTDesc = "";
        string Type = Request.QueryString["Type"].ToString();
        if (Request.Cookies["BranchID"] != null)
        {
            BranchID = Request.Cookies["BranchID"].Value;
        }
        if (Request.Cookies["UTDesc"] != null)
        {
            UTDesc = Request.Cookies["UTDesc"].Value;
        }

        if (Type == "Stock-Summary")
        {
            string rptName = "Stock.rpt";
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
            SqlParameter AccountsID = new SqlParameter("@CatID", ddlCategory.SelectedValue);
            SqlParameter BrandID = new SqlParameter("@BrandID", ddlBrand.SelectedValue);
            SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
            DataSet ds = AACommon.ReturnDatasetBySPForREPORT("STOCK_BALANCE_CAT_BRAND", "VW_STOCK_BALANCE", Con, AccountsID, BrandID, Branch);
            if (UTDesc == "Sales")
            {
                for (int intCount = 0; intCount < ds.Tables[0].Rows.Count; intCount++)
                {

                    ds.Tables[0].Rows[intCount]["UnitPrice"] = "0";
                    ds.Tables[0].Rows[intCount]["UnitPriceOld"] = "0";

                }

                ds.Tables[0].AcceptChanges();
            }

            Session["RptDS"] = ds;
            Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_STOCK_BALANCE");
        }
        else
            if (Type == "Stock-Ledger")
            {
                string rptName = "StockLedger.rpt";
                SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                SqlParameter AccountsID = new SqlParameter("@CatID", ddlCategory.SelectedValue);
                SqlParameter BrandID = new SqlParameter("@BrandID", ddlBrand.SelectedValue);
                SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
                DataSet ds = AACommon.ReturnDatasetBySPForREPORT("STOCK_LEDGER_CAT_BRAND", "VW_STOCK_LEDGER", Con, AccountsID, BrandID, Branch);
                if (UTDesc == "Sales")
                {
                    for (int intCount = 0; intCount < ds.Tables[0].Rows.Count; intCount++)
                    {

                        ds.Tables[0].Rows[intCount]["UnitPrice"] = "0";
                        ds.Tables[0].Rows[intCount]["TotalPrice"] = "0";

                    }

                    ds.Tables[0].AcceptChanges();
                }

                Session["RptDS"] = ds;
                Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_STOCK_LEDGER");
            }


    }
    protected void LoadReportCat(object sender, EventArgs e)
    {
        var BranchID = "";
        var UTDesc = "";
        string Type = Request.QueryString["Type"].ToString();
        if (Request.Cookies["BranchID"] != null)
        {
            BranchID = Request.Cookies["BranchID"].Value;
        }
        if (Request.Cookies["UTDesc"] != null)
        {
            UTDesc = Request.Cookies["UTDesc"].Value;
        }
        if (Type == "Stock-Summary")
        {
            string rptName = "Stock.rpt";
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
            SqlParameter AccountsID = new SqlParameter("@CatID", ddlCategory.SelectedValue);
            SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
            DataSet ds = AACommon.ReturnDatasetBySPForREPORT("STOCK_BALANCE_CAT", "VW_STOCK_BALANCE", Con, AccountsID, Branch);
            if (UTDesc == "Sales")
            {
                for (int intCount = 0; intCount < ds.Tables[0].Rows.Count; intCount++)
                {

                    ds.Tables[0].Rows[intCount]["UnitPrice"] = "0";
                    ds.Tables[0].Rows[intCount]["UnitPriceOld"] = "0";

                }

                ds.Tables[0].AcceptChanges();
            }

            Session["RptDS"] = ds;
            Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_STOCK_BALANCE");
        }
        else
            if (Type == "Stock-Ledger")
            {
                string rptName = "StockLedger.rpt";
                SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                SqlParameter AccountsID = new SqlParameter("@CatID", ddlCategory.SelectedValue);
                SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
                DataSet ds = AACommon.ReturnDatasetBySPForREPORT("STOCK_LEDGER_CAT", "VW_STOCK_LEDGER", Con, AccountsID, Branch);
                if (UTDesc == "Sales")
                {
                    for (int intCount = 0; intCount < ds.Tables[0].Rows.Count; intCount++)
                    {

                        ds.Tables[0].Rows[intCount]["UnitPrice"] = "0";
                        ds.Tables[0].Rows[intCount]["TotalPrice"] = "0";

                    }

                    ds.Tables[0].AcceptChanges();
                }

                Session["RptDS"] = ds;
                Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_STOCK_LEDGER");
            }


    }

    protected void LoadReportBrand(object sender, EventArgs e)
    {
        var BranchID = "";
        var UTDesc = "";
        string Type = Request.QueryString["Type"].ToString();
        if (Request.Cookies["BranchID"] != null)
        {
            BranchID = Request.Cookies["BranchID"].Value;
        }
        if (Request.Cookies["UTDesc"] != null)
        {
            UTDesc = Request.Cookies["UTDesc"].Value;
        }
        if (Type == "Stock-Summary")
        {
            string rptName = "Stock.rpt";
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
            SqlParameter AccountsID = new SqlParameter("@BrandID", ddlBrand.SelectedValue);
            SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
            DataSet ds = AACommon.ReturnDatasetBySPForREPORT("STOCK_BALANCE_BRAND", "VW_STOCK_BALANCE", Con, AccountsID, Branch);
            if (UTDesc == "Sales")
            {
                for (int intCount = 0; intCount < ds.Tables[0].Rows.Count; intCount++)
                {

                    ds.Tables[0].Rows[intCount]["UnitPrice"] = "0";
                    ds.Tables[0].Rows[intCount]["UnitPriceOld"] = "0";

                }

                ds.Tables[0].AcceptChanges();
            }

            Session["RptDS"] = ds;
            Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_STOCK_BALANCE");
        }
        else
            if (Type == "Stock-Ledger")
            {
                string rptName = "StockLedger.rpt";
                SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                SqlParameter AccountsID = new SqlParameter("@BrandID", ddlBrand.SelectedValue);
                SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
                DataSet ds = AACommon.ReturnDatasetBySPForREPORT("STOCK_LEDGER_BRAND", "VW_STOCK_LEDGER", Con, AccountsID, Branch);
                if (UTDesc == "Sales")
                {
                    for (int intCount = 0; intCount < ds.Tables[0].Rows.Count; intCount++)
                    {

                        ds.Tables[0].Rows[intCount]["UnitPrice"] = "0";
                        ds.Tables[0].Rows[intCount]["TotalPrice"] = "0";

                    }

                    ds.Tables[0].AcceptChanges();
                }

                Session["RptDS"] = ds;
                Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_STOCK_LEDGER");
            }


    }
}