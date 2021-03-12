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

         string Type = Request.QueryString["Type"].ToString();

         if (Type == "Stock-Summary")
         {
             string rptName = "StockWareHouseMaster.rpt";
             SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
             SqlParameter AccountsID = new SqlParameter("@ITEMID", ddlITEM.SelectedValue);
             DataSet ds = AACommon.ReturnDatasetBySPForREPORT("STOCK_BALANCE_WH_MASTER", "VW_STOCK_BALANCE_WH_MASTER", Con, AccountsID);
             Session["RptDS"] = ds;
             Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_STOCK_BALANCE_WH_MASTER");
         }
         else
             if (Type == "Stock-Ledger")
             {
                 string rptName = "StockLedgerWareHouseMaster.rpt";
                 SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                 SqlParameter AccountsID = new SqlParameter("@ITEMID", ddlITEM.SelectedValue);
                 DataSet ds = AACommon.ReturnDatasetBySPForREPORT("STOCK_LEDGER_WH_Master", "VW_STOCK_LEDGER_WH_Master", Con, AccountsID);
                 Session["RptDS"] = ds;
                 Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_STOCK_LEDGER_WH_Master");
             }


    }

    protected void LoadReportCatBrand(object sender, EventArgs e)
    {

        string Type = Request.QueryString["Type"].ToString();

        if (Type == "Stock-Summary")
        {
            string rptName = "StockWareHouseMaster.rpt";
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
            SqlParameter AccountsID = new SqlParameter("@CatID", ddlCategory.SelectedValue);
            SqlParameter BrandID = new SqlParameter("@BrandID", ddlBrand.SelectedValue);
            DataSet ds = AACommon.ReturnDatasetBySPForREPORT("STOCK_BALANCE_WH_MASTER_CAT_BRAND", "VW_STOCK_BALANCE_WH_MASTER", Con, AccountsID, BrandID);
            Session["RptDS"] = ds;
            Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_STOCK_BALANCE_WH_MASTER");
        }
        else
            if (Type == "Stock-Ledger")
            {
                string rptName = "StockLedgerWareHouseMaster.rpt";
                SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                SqlParameter AccountsID = new SqlParameter("@CatID", ddlCategory.SelectedValue);
                SqlParameter BrandID = new SqlParameter("@BrandID", ddlBrand.SelectedValue);
                DataSet ds = AACommon.ReturnDatasetBySPForREPORT("STOCK_LEDGER_WH_Master_CAT_BRAND", "VW_STOCK_LEDGER_WH_Master", Con, AccountsID, BrandID);
                Session["RptDS"] = ds;
                Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_STOCK_LEDGER_WH_Master");
            }


    }
    protected void LoadReportCat(object sender, EventArgs e)
    {

        string Type = Request.QueryString["Type"].ToString();

        if (Type == "Stock-Summary")
        {
            string rptName = "StockWareHouseMaster.rpt";
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
            SqlParameter AccountsID = new SqlParameter("@CatID", ddlCategory.SelectedValue);
            DataSet ds = AACommon.ReturnDatasetBySPForREPORT("STOCK_BALANCE_WH_MASTER_CAT", "VW_STOCK_BALANCE_WH_MASTER", Con, AccountsID);
            Session["RptDS"] = ds;
            Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_STOCK_BALANCE_WH_MASTER");
        }
        else
            if (Type == "Stock-Ledger")
            {
                string rptName = "StockLedgerWareHouseMaster.rpt";
                SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                SqlParameter AccountsID = new SqlParameter("@CatID", ddlCategory.SelectedValue);
                DataSet ds = AACommon.ReturnDatasetBySPForREPORT("STOCK_LEDGER_WH_Master_CAT", "VW_STOCK_LEDGER_WH_Master", Con, AccountsID);
                Session["RptDS"] = ds;
                Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_STOCK_LEDGER_WH_Master");
            }


    }

    protected void LoadReportBrand(object sender, EventArgs e)
    {

        string Type = Request.QueryString["Type"].ToString();

        if (Type == "Stock-Summary")
        {
            string rptName = "StockWareHouseMaster.rpt";
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
            SqlParameter AccountsID = new SqlParameter("@BrandID", ddlBrand.SelectedValue);
            DataSet ds = AACommon.ReturnDatasetBySPForREPORT("STOCK_BALANCE_WH_MASTER_BRAND", "VW_STOCK_BALANCE_WH_MASTER", Con, AccountsID);
            Session["RptDS"] = ds;
            Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_STOCK_BALANCE_WH_MASTER");
        }
        else
            if (Type == "Stock-Ledger")
            {
                string rptName = "StockLedgerWareHouseMaster.rpt";
                SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                SqlParameter AccountsID = new SqlParameter("@BrandID", ddlBrand.SelectedValue);
                DataSet ds = AACommon.ReturnDatasetBySPForREPORT("STOCK_LEDGER_WH_Master_BRAND", "VW_STOCK_LEDGER_WH_Master", Con, AccountsID);
                Session["RptDS"] = ds;
                Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_STOCK_LEDGER_WH_Master");
            }


    }
}