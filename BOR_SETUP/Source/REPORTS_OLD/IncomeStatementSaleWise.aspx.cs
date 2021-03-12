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
   public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LoadReport(object sender, EventArgs e)
    {
        var BranchID = "";
        if (Request.Cookies["BranchID"] != null)
        {
            BranchID = Request.Cookies["BranchID"].Value;
        }


        double PurchaseMasterPrice = 0;
        double SaleMasterPrice = 0;        
        double SaleProfitMasterPrice = 0;
        double SaleDiscountMasterPrice = 0;
        double ExpenseMasterPrice = 0;
        double NetProfit = 0;
        double DiscountMasterPrice = 0;
        
        SqlCommand CMDDeleteMaster = new SqlCommand("delete from ProfitMaster where BranchID='" + BranchID + "'", Con);
        Con.Open();
        CMDDeleteMaster.ExecuteNonQuery();
        Con.Close();

        SqlCommand CMDDeleteDetail = new SqlCommand("delete from ProfitDetail where BranchID='" + BranchID + "'", Con);
        Con.Open();
        CMDDeleteDetail.ExecuteNonQuery();
        Con.Close();

        SqlDataAdapter da = new SqlDataAdapter("select * from VW_SALE_DETAIL where SPDate between '" + StartDate.Value + "' and '" + EndDate.Value + "' and BranchID='" + BranchID + "'   and BranchID1='" + BranchID + "'   and BranchID2='" + BranchID + "'   and BranchID3='" + BranchID + "'   and BranchID4='" + BranchID + "'  order by ID", Con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SqlDataAdapter daDetailPurchase = new SqlDataAdapter("select * from SP_Detail where ID='" + dt.Rows[i]["BatchID"].ToString() + "' and BranchID='" + BranchID + "'", Con);
                DataTable dtDetailPurchase = new DataTable();
                daDetailPurchase.Fill(dtDetailPurchase);
                if (dtDetailPurchase.Rows.Count > 0)
                {
                    double PurchasePrice = Convert.ToDouble(dt.Rows[i]["QtyOut"].ToString()) * Convert.ToDouble(dtDetailPurchase.Rows[0]["UnitPrice"].ToString());
                    double SalePrice = Convert.ToDouble(dt.Rows[i]["QtyOut"].ToString()) * Convert.ToDouble(dt.Rows[i]["UnitPrice"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into ProfitDetail (PurchaseSPID,PurchaseSPDetailID,SaleSPID,SaleSPDetailID,SaleDate,PurchaseUnitPrice,SaleUnitPrice,SoldQty,PurchasePrice,SalePrice,ITEMID,ITEMName,CatID,CatTitle,BrandID,BrandTitle,BranchID) values ('" + dtDetailPurchase.Rows[0]["SPID"].ToString() + "','" + dtDetailPurchase.Rows[0]["ID"].ToString() + "','" + dt.Rows[i]["SPID"].ToString() + "','" + dt.Rows[i]["ID"].ToString() + "','" + dt.Rows[i]["SPDate"].ToString() + "','" + dtDetailPurchase.Rows[0]["UnitPrice"].ToString() + "','" + dt.Rows[i]["UnitPrice"].ToString() + "','" + dt.Rows[i]["QtyOut"].ToString() + "','" + PurchasePrice + "','" + SalePrice + "','" + dt.Rows[i]["ITEMID"].ToString() + "','" + dt.Rows[i]["ITEMName"].ToString() + "','" + dt.Rows[i]["CatID"].ToString() + "','" + dt.Rows[i]["CatTitle"].ToString() + "','" + dt.Rows[i]["BrandID"].ToString() + "','" + dt.Rows[i]["BrandTitle"].ToString() + "','" + BranchID + "')", Con);
                    Con.Open();
                    cmd.ExecuteNonQuery();
                    Con.Close();
                }
            }
        }

        SqlDataAdapter daMasterSale = new SqlDataAdapter("select ISNULL(SUM(TotalPrice),0) from VW_PURCHASE_SALE_PRICE_DETAIL where SPDate between '" + StartDate.Value + "' and '" + EndDate.Value + "' and SP='S' and BranchID='" + BranchID + "'   and BranchID1='" + BranchID + "' ", Con);
         DataTable dtMasterSale = new DataTable();
         daMasterSale.Fill(dtMasterSale);
         if (dtMasterSale.Rows.Count > 0)
         {
             SaleMasterPrice = Convert.ToDouble(dtMasterSale.Rows[0][0].ToString());
         }

         SqlDataAdapter daMasterPurchase = new SqlDataAdapter("select ISNULL(SUM(TotalPrice),0) from VW_PURCHASE_SALE_PRICE_DETAIL where SPDate between '" + StartDate.Value + "' and '" + EndDate.Value + "' and SP='P' and BranchID='" + BranchID + "'   and BranchID1='" + BranchID + "' ", Con);
         DataTable dtMasterPurchase = new DataTable();
         daMasterPurchase.Fill(dtMasterPurchase);
         if (dtMasterPurchase.Rows.Count > 0)
         {
             PurchaseMasterPrice = Convert.ToDouble(dtMasterPurchase.Rows[0][0].ToString());
         }

         SqlDataAdapter daMasterSaleProfit = new SqlDataAdapter("select  ISNULL(sum(SalePrice),0)- ISNULL(sum(PurchasePrice),0) from ProfitDetail where BranchID='"+BranchID+"'", Con);
         DataTable dtMasterSaleProfit = new DataTable();
         daMasterSaleProfit.Fill(dtMasterSaleProfit);
         if (dtMasterSaleProfit.Rows.Count > 0)
         {
             SaleProfitMasterPrice = Convert.ToDouble(dtMasterSaleProfit.Rows[0][0].ToString());
         }

         SqlDataAdapter daMasterSaleDiscount = new SqlDataAdapter("select  ISNULL(sum(SalePrice),0)- ISNULL(sum(PurchasePrice),0) from ProfitDetail where (SalePrice - PurchasePrice < 0) and BranchID='" + BranchID + "'", Con);
         DataTable dtMasterSaleDiscount = new DataTable();
         daMasterSaleDiscount.Fill(dtMasterSaleDiscount);
         if (dtMasterSaleDiscount.Rows.Count > 0)
         {
             SaleDiscountMasterPrice = Convert.ToDouble(dtMasterSaleDiscount.Rows[0][0].ToString());
         }

         SqlDataAdapter daMasterExpense = new SqlDataAdapter("select  ISNULL(sum(DebitPKR),0) AS DebitPKR, ISNULL(sum(CreditPKR),0) AS CreditPKR from VW_EXPENSE_DETAIL  where Date between '" + StartDate.Value + "' and '" + EndDate.Value + "' and BranchID='" + BranchID + "'   and BranchID1='" + BranchID + "'   and BranchID2='" + BranchID + "'   and BranchID3='" + BranchID + "'  ", Con);
         DataTable dtMasterExpense = new DataTable();
         daMasterExpense.Fill(dtMasterExpense);
         if (dtMasterExpense.Rows.Count > 0)
         {
             ExpenseMasterPrice = Convert.ToDouble(dtMasterExpense.Rows[0]["DebitPKR"].ToString()) + Convert.ToDouble(dtMasterExpense.Rows[0]["CreditPKR"].ToString());
         }

         SqlDataAdapter daDiscountMaster = new SqlDataAdapter("SELECT      ISNULL(SUM(SalesDiscountPKR),0) FROM            SP_MASTER WHERE        (SP = N'S') AND (SalesDiscountPKR > 0)  and SPDate between '" + StartDate.Value + "' and '" + EndDate.Value + "' and BranchID='" + BranchID + "'", Con);
         DataTable dtDiscountMaster = new DataTable();
         daDiscountMaster.Fill(dtDiscountMaster);
         if (dtDiscountMaster.Rows.Count > 0)
         {
             DiscountMasterPrice = Convert.ToDouble(dtDiscountMaster.Rows[0][0].ToString());
         }

         NetProfit = SaleProfitMasterPrice - ExpenseMasterPrice;
         SqlCommand cmdProfitMaster = new SqlCommand("insert into ProfitMaster (PurchasePrice,SalePrice,SaleProfit,ExpensePrice,SaleDiscountPrice,NetProfit,DateFrom,DateTo,BranchID,SaleDiscount) values ('" + PurchaseMasterPrice + "','" + SaleMasterPrice + "','" + SaleProfitMasterPrice + "','" + ExpenseMasterPrice + "','" + SaleDiscountMasterPrice + "','" + NetProfit + "','" + StartDate.Value + "','" + EndDate.Value + "','" + BranchID + "','"+DiscountMasterPrice+"')", Con);
         Con.Open();
         cmdProfitMaster.ExecuteNonQuery();
         Con.Close();

         SqlDataAdapter daReport = new SqlDataAdapter("select * from VW_PROFIT_SUMMARY_DETAIL where BranchID='" + BranchID + "'   and BranchID1='" + BranchID + "'", Con);
         DataSet ds = new DataSet();
         daReport.Fill(ds,"VW_PROFIT_SUMMARY_DETAIL");

         Session["RptDS"] = ds;
         Response.Redirect("~/Reports/Viewer.aspx?name=IncomeProfitDetailDateWise.rpt&no=0&RptTable=VW_PROFIT_SUMMARY_DETAIL");

    }
}