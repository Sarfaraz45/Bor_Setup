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
using System.Drawing;
using System.Globalization;

public partial class REPORTS_Ledger_Updated : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LoadReport(object sender, EventArgs e)
    {

        var BranchID = "";
        string LType = Request.QueryString["LType"].ToString();
        if (LType == "PKR")
        {
            if (Request.Cookies["BranchID"] != null)
            {
                BranchID = Request.Cookies["BranchID"].Value;
            }

            string rptName = "Ledger.rpt";
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
            SqlParameter AccountsID = new SqlParameter("@AccountsID", ddlITEM.SelectedValue);
            SqlParameter DateFrom = new SqlParameter("@DateFrom", StartDate.Value);
            SqlParameter DateEnd = new SqlParameter("@DateEnd", EndDate.Value);
            SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
            ds = AACommon.ReturnDatasetBySPForREPORT("LedgerReport", "VW_LEDGER", Con, AccountsID, DateFrom, DateEnd, Branch);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string query = "";
                if (ddlITEM.SelectedValue == "0")
                { query = "select * from Accounts where BranchID='" + BranchID + "'"; }
                else
                { query = "select * from Accounts where AccountsID='" + ddlITEM.SelectedValue + "' and BranchID='" + BranchID + "'"; }
                SqlDataAdapter da = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string quersum = "select ISNULL(sum(DebitPKR) - Sum(CreditPKR),0) from Transaction_Detail where AccountID = '" + ddlITEM.SelectedValue + "' and Date < '" + StartDate.Value + "' AND (IsDelete = 0) and BranchID='" + BranchID + "'";
                        SqlDataAdapter dda = new SqlDataAdapter(quersum, Con);
                        DataTable dtt = new DataTable();
                        dda.Fill(dtt);
                        if (dtt.Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Select("AccountsID = '" + dt.Rows[i]["AccountsID"].ToString() + "'"))
                            {
                                string d = Convert.ToString(Convert.ToDecimal(row[10]));
                                string ee = Convert.ToString(dtt.Rows[0][0].ToString());
                                row[10] = Convert.ToString(Convert.ToDecimal(row[10]) + Convert.ToDecimal(dtt.Rows[0][0].ToString()));
                            }

                            ds.Tables[0].AcceptChanges();
                        }

                    }
                }
            }
            else
            {
                if (ddlITEM.SelectedValue != "0")
                {
                    string query = "";
                    if (ddlITEM.SelectedValue == "0")
                    { query = "select * from Accounts where BranchID='" + BranchID + "'"; }
                    else
                    { query = "select * from Accounts where AccountsID='" + ddlITEM.SelectedValue + "' and BranchID='" + BranchID + "'"; }
                    SqlDataAdapter da = new SqlDataAdapter(query, Con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string quersum = "select ISNULL(sum(DebitPKR) - Sum(CreditPKR),0) AS Opening from Transaction_Detail where AccountID = '" + ddlITEM.SelectedValue + "' AND (IsDelete = 0) and BranchID='" + BranchID + "'";
                            SqlDataAdapter dda = new SqlDataAdapter(quersum, Con);
                            DataTable dtt = new DataTable();
                            dda.Fill(dtt);
                            if (dtt.Rows.Count > 0)
                            {
                                decimal Basicopn = Convert.ToDecimal(dt.Rows[i]["OpDebit"].ToString()) - Convert.ToDecimal(dt.Rows[i]["OpCredit"].ToString());


                                string strd = dtt.Rows[0]["Opening"].ToString();
                                NumberStyles styles;
                                styles = NumberStyles.AllowExponent | NumberStyles.Number;
                                double dbl = Double.Parse(strd, styles);


                                decimal opn = Basicopn + Convert.ToDecimal(dbl);

                                //decimal opn = Basicopn + Convert.ToDecimal(dtt.Rows[0]["Opening"].ToString());

                                DataTable dtDs = new DataTable("VW_LEDGER");
                                dtDs.Columns.Add(new DataColumn("AccountsID", typeof(string)));
                                dtDs.Columns.Add(new DataColumn("AccountsTitle", typeof(string)));
                                dtDs.Columns.Add(new DataColumn("DebitPKR", typeof(float)));
                                dtDs.Columns.Add(new DataColumn("DebitRMB", typeof(float)));
                                dtDs.Columns.Add(new DataColumn("CreditPKR", typeof(float)));
                                dtDs.Columns.Add(new DataColumn("CreditRMB", typeof(float)));
                                dtDs.Columns.Add(new DataColumn("RMBValue", typeof(float)));
                                dtDs.Columns.Add(new DataColumn("TaskID", typeof(string)));
                                dtDs.Columns.Add(new DataColumn("Date", typeof(DateTime)));
                                dtDs.Columns.Add(new DataColumn("Narration", typeof(string)));
                                dtDs.Columns.Add(new DataColumn("Opening", typeof(float)));
                                dtDs.Columns.Add(new DataColumn("NarrationDetail", typeof(string)));
                                DataRow dr = dtDs.NewRow();
                                dr["AccountsID"] = dt.Rows[i]["AccountsID"].ToString();
                                dr["AccountsTitle"] = dt.Rows[i]["AccountsTitle"].ToString();
                                dr["DebitPKR"] = "0";
                                dr["DebitRMB"] = "0";
                                dr["CreditPKR"] = "0";
                                dr["CreditRMB"] = "0";
                                dr["RMBValue"] = "0";
                                dr["TaskID"] = "0";
                                dr["Date"] = DateTime.Now;
                                dr["Narration"] = "Default Closing Balance";
                                dr["Opening"] = opn;
                                dr["NarrationDetail"] = "Default Closing Balance";
                                dtDs.Rows.Add(dr);
                                ds.Tables.Remove("VW_LEDGER");
                                ds.Tables.Add(dtDs);
                            }
                        }
                    }
                }
            }


            
            Session["RptDS"] = ds;
            Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_LEDGER");
        }
        else if (LType == "RMB")
        {
          

            if (Request.Cookies["BranchID"] != null)
            {
                BranchID = Request.Cookies["BranchID"].Value;
            }

            string rptName = "LedgerRMB.rpt";
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
            SqlParameter AccountsID = new SqlParameter("@AccountsID", ddlITEM.SelectedValue);
            SqlParameter DateFrom = new SqlParameter("@DateFrom", StartDate.Value);
            SqlParameter DateEnd = new SqlParameter("@DateEnd", EndDate.Value);
            SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
            DataSet ds = AACommon.ReturnDatasetBySPForREPORT("LedgerReport_RMB", "VW_LEDGER_RMB", Con, AccountsID, DateFrom, DateEnd, Branch);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string query = "";
                if (ddlITEM.SelectedValue == "0")
                { query = "select * from Accounts where BranchID='" + BranchID + "'"; }
                else
                { query = "select * from Accounts where AccountsID='" + ddlITEM.SelectedValue + "' and BranchID='" + BranchID + "'"; }
                SqlDataAdapter da = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string quersum = "select ISNULL(sum(DebitRMB) - Sum(CreditRMB),0) AS RMB,ISNULL(sum(DebitPKR) - Sum(CreditPKR),0) AS PKR from Transaction_Detail where AccountID = '" + ddlITEM.SelectedValue + "' and Date < '" + StartDate.Value + "' AND (IsDelete = 0) and BranchID='" + BranchID + "'";
                        SqlDataAdapter dda = new SqlDataAdapter(quersum, Con);
                        DataTable dtt = new DataTable();
                        dda.Fill(dtt);
                        if (dtt.Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Select("AccountsID = '" + dt.Rows[i]["AccountsID"].ToString() + "'"))
                            {
                                string RMB = Convert.ToString(Convert.ToDecimal(row[10]));
                                string eeRMB = Convert.ToString(dtt.Rows[0]["RMB"].ToString());
                                row[10] = Convert.ToString(Convert.ToDecimal(row[10]) + Convert.ToDecimal(dtt.Rows[0]["RMB"].ToString()));
                                row[11] = Convert.ToString(Convert.ToDecimal(row[11]) + Convert.ToDecimal(dtt.Rows[0]["PKR"].ToString()));
                            }

                            ds.Tables[0].AcceptChanges();
                        }

                    }
                }
            }
            else
            {
                if (ddlITEM.SelectedValue != "0")
                {
                    string query = "";
                    if (ddlITEM.SelectedValue == "0")
                    { query = "select * from Accounts where BranchID='" + BranchID + "'"; }
                    else
                    { query = "select * from Accounts where AccountsID='" + ddlITEM.SelectedValue + "' and BranchID='" + BranchID + "'"; }
                    SqlDataAdapter da = new SqlDataAdapter(query, Con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string quersum = "select ISNULL(sum(DebitRMB) - Sum(CreditRMB),0) AS OpeningRMB,ISNULL(sum(DebitPKR) - Sum(CreditPKR),0) AS OpeningPKR from Transaction_Detail where AccountID = '" + ddlITEM.SelectedValue + "' AND (IsDelete = 0) and BranchID='" + BranchID + "'";
                            SqlDataAdapter dda = new SqlDataAdapter(quersum, Con);
                            DataTable dtt = new DataTable();
                            dda.Fill(dtt);
                            if (dtt.Rows.Count > 0)
                            {
                                decimal Basicopn = Convert.ToDecimal(dt.Rows[i]["OpDebitRMB"].ToString()) - Convert.ToDecimal(dt.Rows[i]["OpCreditRMB"].ToString());

                                /// COnvert Exponential to Numbers Start
                                string strdRMB = dtt.Rows[0]["OpeningRMB"].ToString();
                                NumberStyles stylesRmb;
                                stylesRmb = NumberStyles.AllowExponent | NumberStyles.Number;
                                double dblRmb = Double.Parse(strdRMB, stylesRmb);
                                /// COnvert Exponential to Numbers END
                                

                                //decimal opn = Basicopn + Convert.ToDecimal(dtt.Rows[0]["OpeningRMB"].ToString());
                                decimal opn = Basicopn + Convert.ToDecimal(dblRmb);

                                decimal Basicopnpkr = Convert.ToDecimal(dt.Rows[i]["OpDebit"].ToString()) - Convert.ToDecimal(dt.Rows[i]["OpCredit"].ToString());

                                string strd = dtt.Rows[0]["OpeningPKR"].ToString();
                                NumberStyles styles;
                                styles = NumberStyles.AllowExponent | NumberStyles.Number;
                                double dbl = Double.Parse(strd, styles);

                                //decimal opnpkr = Basicopnpkr + Convert.ToDecimal(dtt.Rows[0]["OpeningPKR"].ToString());
                                decimal opnpkr = Basicopnpkr + Convert.ToDecimal(dbl);

                                DataTable dtDs = new DataTable("VW_LEDGER_RMB");
                                dtDs.Columns.Add(new DataColumn("AccountsID", typeof(string)));
                                dtDs.Columns.Add(new DataColumn("AccountsTitle", typeof(string)));
                                dtDs.Columns.Add(new DataColumn("DebitPKR", typeof(float)));
                                dtDs.Columns.Add(new DataColumn("DebitRMB", typeof(float)));
                                dtDs.Columns.Add(new DataColumn("CreditPKR", typeof(float)));
                                dtDs.Columns.Add(new DataColumn("CreditRMB", typeof(float)));
                                dtDs.Columns.Add(new DataColumn("RMBValue", typeof(float)));
                                dtDs.Columns.Add(new DataColumn("TaskID", typeof(string)));
                                dtDs.Columns.Add(new DataColumn("Date", typeof(DateTime)));
                                dtDs.Columns.Add(new DataColumn("Narration", typeof(string)));
                                dtDs.Columns.Add(new DataColumn("OpeningRMB", typeof(float)));
                                dtDs.Columns.Add(new DataColumn("OpeningPKR", typeof(float)));
                                dtDs.Columns.Add(new DataColumn("NarrationDetail", typeof(string)));
                                DataRow dr = dtDs.NewRow();
                                dr["AccountsID"] = dt.Rows[i]["AccountsID"].ToString();
                                dr["AccountsTitle"] = dt.Rows[i]["AccountsTitle"].ToString();
                                dr["DebitPKR"] = "0";
                                dr["DebitRMB"] = "0";
                                dr["CreditPKR"] = "0";
                                dr["CreditRMB"] = "0";
                                dr["RMBValue"] = "0";
                                dr["TaskID"] = "0";
                                dr["Date"] = DateTime.Now;
                                dr["Narration"] = "Default Closing Balance";
                                dr["OpeningRMB"] = opn;
                                dr["OpeningPKR"] = opnpkr;
                                dr["NarrationDetail"] = "Default Closing Balance";
                                dtDs.Rows.Add(dr);
                                ds.Tables.Remove("VW_LEDGER_RMB");
                                ds.Tables.Add(dtDs);
                            }
                        }
                    }
                }
            }
            Session["RptDS"] = ds;
            Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=0&RptTable=VW_LEDGER_RMB");
        }
    }
}