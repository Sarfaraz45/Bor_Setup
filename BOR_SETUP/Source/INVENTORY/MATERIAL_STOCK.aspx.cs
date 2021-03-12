using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class INVENTORY_MATERIAL_STOCK : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadDate();
        }
    }

    public void loadDate()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConACC"].ConnectionString);
        // System.Windows.Forms.MessageBox.Show(summary);
        try
        {
            DataTable dt = AACommon.LoadControl("DATE_FILTER", con, null);


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txtFrom.Text = dt.Rows[i]["WeekStart"].ToString();
                txtTo.Text= dt.Rows[i]["WeekEnd"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);

            //return null;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //string rqID = Request.QueryString["ID"].ToString();//@DtFrom @DtTo
        string rptName = "StockReport.rpt";
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter DtFrom = new SqlParameter("@DtFrom", txtFrom.Text);
        SqlParameter DtTo = new SqlParameter("@DtTo", txtTo.Text);
        SqlParameter IsPurchase = new SqlParameter("@IsPurchase", "1");
        SqlParameter IsSale = new SqlParameter("@IsSale", "0");

        DataSet ds = AACommon.ReturnDatasetBySPForREPORT("INV_MATERIAL_STOCK", "INV_STOCK_RPT", Con, DtFrom, DtTo, IsPurchase, IsSale);
        Session["RptDS"] = ds;
        Session["param"] = "StartDate=" + txtFrom.Text + "&EndDate=" + txtTo.Text + "&MP=MATERIAL";
        Response.Redirect("~/Reports/Viewer.aspx?name=" + rptName + "&no=3&RptTable=INV_STOCK_RPT&p1=" + txtFrom.Text + "&p2=" + txtTo.Text + "&p3=Material");
    }
}