using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Sql;

/// <summary>
/// Summary description for Menu
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]


// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 //[System.Web.Script.Services.ScriptService]
public class Menu : System.Web.Services.WebService {
    public  string PageURL;
    
    public Menu () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    
    [WebMethod]
    public  string drawMenu(string UTID)    
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        //PageURL = Request.Url.AbsolutePath;
        PageURL = HttpContext.Current.Request.Url.AbsolutePath;

        int i = 0;
        int j = 0; string InnerHTML = "";

        PageURL = PageURL.ToLowerInvariant();




        string str = "select distinct FormID As id, FormTitle As name , PID As parentid , Path As path from  VW_MENU  where PID=0 and UTID='" + UTID + "' order by FormID";
        SqlDataAdapter sqladp = new SqlDataAdapter(str, Conn);
        DataTable dt = new DataTable();
        sqladp.Fill(dt);

        InnerHTML = InnerHTML + "<ul class='nav'><li class='nav-header'>Navigation</li>";
        for (i = 0; i < dt.Rows.Count; i++)
        {
            string hasChild = getchildes(Convert.ToInt32(dt.Rows[i]["id"].ToString()),UTID);
            if (hasChild == "")
            {
                string ActiveChild = "";
                string path = dt.Rows[i]["path"].ToString();
                path = path.ToLowerInvariant();

                if (PageURL.IndexOf(path) != -1)
                { ActiveChild = " class=active"; }

                InnerHTML = InnerHTML + "<li" + ActiveChild + ">";
                InnerHTML = InnerHTML + "<a href='" + "../" + dt.Rows[i]["path"].ToString() + "" + "'>";
                InnerHTML = InnerHTML + "<i class='fa fa-inbox'></i>";
                InnerHTML = InnerHTML + "<span>" + dt.Rows[i]["name"].ToString() + "</span></a>";
                InnerHTML = InnerHTML + "</li>";

            }
            else
            {
                string ActiveParent = "";
                if (hasChild.IndexOf("class=active") != -1) { ActiveParent = " active"; }

                string TotalChild = hasChild;
                TotalChild = TotalChild.Substring(0, TotalChild.IndexOf("`"));
                hasChild = hasChild.Substring(hasChild.IndexOf("`") + 1);

                InnerHTML = InnerHTML + "<li class='has-sub" + ActiveParent + "'>";
                InnerHTML = InnerHTML + "<a href='javascript:;'>";
                InnerHTML = InnerHTML + "<b class='caret pull-right'></b>";
                InnerHTML = InnerHTML + "<span class='badge pull-right'>" + TotalChild + "</span>";
                InnerHTML = InnerHTML + "<i class='fa fa-inbox'></i>";
                InnerHTML = InnerHTML + "<span>" + dt.Rows[i]["name"].ToString() + "</span>";
                InnerHTML = InnerHTML + "</a>";
                InnerHTML = InnerHTML + "<ul class='sub-menu'>";
                InnerHTML = InnerHTML + hasChild;
                InnerHTML = InnerHTML + "</ul></li>";

            }
        }
        InnerHTML = InnerHTML + "<li><a href='javascript:;' class='sidebar-minify-btn' data-click='sidebar-minify'><i class='fa fa-angle-double-left'></i></a></li>";
        InnerHTML = InnerHTML + "</ul>";
        //dvMenu.InnerHtml = InnerHTML;
        return InnerHTML;
        

    }

    public  string getchildes(int x, string UTID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        string SubString = "";
        int k = 0;
        string str = "select distinct FormID As id, FormTitle As name , PID As parentid , Path As path from  VW_MENU  where PID=" + x + " and UTID='" + UTID + "' order by FormID";
        SqlDataAdapter da = new SqlDataAdapter(str, Conn);
        DataTable dtA = new DataTable();
        da.Fill(dtA);
        int noOfChild = dtA.Rows.Count;

        for (int i = 0; i < dtA.Rows.Count; i++)
        {
            if (dtA.Rows.Count == 0)
            {
                SubString = "";
            }
            else
            {
                string hasChild = getSubchildes(Convert.ToInt32(dtA.Rows[i]["id"].ToString()),UTID);
                if (hasChild == "")
                {
                    string ActiveChild = "";
                    string path = dtA.Rows[i]["path"].ToString();
                    path = path.ToLowerInvariant();

                    if (PageURL.IndexOf(path) != -1)
                    { ActiveChild = " class=active"; }

                    SubString = SubString + "<li" + ActiveChild + ">";
                    SubString = SubString + "<a href='" + "../" + dtA.Rows[i]["path"].ToString() + "" + "'>";
                    SubString = SubString + "" + dtA.Rows[i]["name"].ToString() + "</a>";
                    SubString = SubString + "</li>";

                }
                else
                {
                    string ActiveParent = "";
                    if (hasChild.IndexOf("class=active") != -1) { ActiveParent = " active"; }

                    string TotalChild = hasChild;
                    TotalChild = TotalChild.Substring(0, TotalChild.IndexOf("`"));
                    hasChild = hasChild.Substring(hasChild.IndexOf("`") + 1);

                    SubString = SubString + "<li class='has-sub" + ActiveParent + "'>";
                    SubString = SubString + "<a href='javascript:;'>";
                    SubString = SubString + "<b class='caret pull-right'></b>";
                    SubString = SubString + "<span class='badge pull-right'>" + TotalChild + "</span>";
                    SubString = SubString + "<i class='fa fa-inbox'></i>";
                    SubString = SubString + "<span>" + dtA.Rows[i]["name"].ToString() + "</span>";
                    SubString = SubString + "</a>";
                    SubString = SubString + "<ul class='sub-menu'>";
                    SubString = SubString + hasChild;
                    SubString = SubString + "</ul></li>";

                }

            }
        }
        if (noOfChild > 0) { SubString = noOfChild + "`" + SubString; }


        return SubString;
    }


    public  string getSubchildes(int x,string UTID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        string SubString = "";
        int k = 0;
        string str = "select * from  VW_MENU  where PID=" + x + " order by FormID";
        SqlDataAdapter da = new SqlDataAdapter(str, Conn);
        DataTable dtA = new DataTable();
        da.Fill(dtA);
        int noOfChild = dtA.Rows.Count;

        for (int i = 0; i < dtA.Rows.Count; i++)
        {
            if (dtA.Rows.Count == 0)
            {
                SubString = "";
            }
            else
            {
                string hasChild = getchildes(Convert.ToInt32(dtA.Rows[i]["id"].ToString()),UTID);
                if (hasChild == "")
                {
                    string ActiveChild = "";
                    string path = dtA.Rows[i]["Path"].ToString();
                    path = path.ToLowerInvariant();

                    if (PageURL.IndexOf(path) != -1)
                    { ActiveChild = " class=active"; }

                    SubString = SubString + "<li" + ActiveChild + ">";
                    SubString = SubString + "<a href='" + "../" + dtA.Rows[i]["Path"].ToString() + "" + "'>";
                    SubString = SubString + "" + dtA.Rows[i]["FormTitle"].ToString() + "</a>";
                    SubString = SubString + "</li>";

                }
                else
                {
                    string ActiveParent = "";
                    if (hasChild.IndexOf("class=active") != -1) { ActiveParent = " active"; }

                    string TotalChild = hasChild;
                    TotalChild = TotalChild.Substring(0, TotalChild.IndexOf("`"));
                    hasChild = hasChild.Substring(hasChild.IndexOf("`") + 1);

                    SubString = SubString + "<li class='has-sub" + ActiveParent + "'>";
                    SubString = SubString + "<a href='javascript:;'>";
                    SubString = SubString + "<b class='caret pull-right'></b>";
                    SubString = SubString + "<span class='badge pull-right'>" + TotalChild + "</span>";
                    SubString = SubString + "<i class='fa " + dtA.Rows[i]["ICON_CSS"].ToString() + "'></i>";
                    SubString = SubString + "<span>" + dtA.Rows[i]["FormTitle"].ToString() + "</span>";
                    SubString = SubString + "</a>";
                    SubString = SubString + "<ul class='sub-menu'>";
                    SubString = SubString + hasChild;
                    SubString = SubString + "</ul></li>";

                }

            }
        }
        if (noOfChild > 0) { SubString = noOfChild + "`" + SubString; }


        return SubString;
    }


    [WebMethod]
    public string ChangeSetting(string NewPassword, string UserID)
    {

        string retMessage = string.Empty;
        NewPassword = Encryption.Encrypt(NewPassword);
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlCommand cmd = new SqlCommand("update USERS set Password='" + NewPassword + "' where UserID='" + UserID + "'", Conn);
        Conn.Open();
        cmd.ExecuteNonQuery();
        Conn.Close();
        retMessage = "true";
        return retMessage;
    }
}
