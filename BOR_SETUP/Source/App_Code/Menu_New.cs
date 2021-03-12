using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Sql;
using System.Web.Script.Services;

/// <summary>
/// Summary description for Menu_New
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]

// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Menu_New : System.Web.Services.WebService {
    public string PageURL;
    public Menu_New () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }


    [WebMethod]
    public string drawMenu(string UTID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        //PageURL = Request.Url.AbsolutePath;
        PageURL = HttpContext.Current.Request.Url.AbsolutePath;

        int i = 0;
        int j = 0; string InnerHTML = "";

        PageURL = PageURL.ToLowerInvariant();




        string str = "select distinct FormID As id, FormTitle As name , PID As parentid , Path As path from  VW_MENU  where PID=0 and UTID='" + UTID + "'  order by FormID";
        SqlDataAdapter sqladp = new SqlDataAdapter(str, Conn);
        DataTable dt = new DataTable();
        sqladp.Fill(dt);
         
        InnerHTML = InnerHTML + "<ul>";
        for (i = 0; i < dt.Rows.Count; i++)
        {
            string hasChild = getchildes(Convert.ToInt32(dt.Rows[i]["id"].ToString()), UTID);
            if (hasChild == "")
            {
                string ActiveChild = "";
                string path = dt.Rows[i]["path"].ToString();
                path = path.ToLowerInvariant();

                if (PageURL.IndexOf(path) != -1)
                { ActiveChild = " class=active"; }

                InnerHTML = InnerHTML + "<li>";
                InnerHTML = InnerHTML + "<a href='" + "../" + dt.Rows[i]["path"].ToString() + "" + "'>";
                InnerHTML = InnerHTML + "<span class=menu-icon'><i class='fa fa-envelope'></i></span> ";
                InnerHTML = InnerHTML + "<span  class='menu-text'>" + dt.Rows[i]["name"].ToString() + "</span></a>";
                //<span class="menu-badge"><span class="badge vd_bg-red">78</span></span>
                InnerHTML = InnerHTML + "</li>";       

            }
            else
            {
                string ActiveParent = "";
                if (hasChild.IndexOf("class=active") != -1) { ActiveParent = " active"; }

                string TotalChild = hasChild;
                TotalChild = TotalChild.Substring(0, TotalChild.IndexOf("`"));
                hasChild = hasChild.Substring(hasChild.IndexOf("`") + 1);


            InnerHTML = InnerHTML + "<li>";
    	    InnerHTML = InnerHTML + "<a href='javascript:void(0);' data-action='click-trigger'>";
            if (i == 0)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='fa fa-dashboard'></i></span> ";
            }
            if (i == 1)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='fa fa-envelope'></i></span> ";
            }
            if (i == 2)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='icon-palette'></i></span> ";
            }
            if (i == 3)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='icon-bookmark'></i></span> ";
            }
            if (i == 4)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='fa fa-sitemap'></i></span> ";
            }
            if (i == 5)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='fa fa-code'></i></span> ";
            }
            if (i == 6)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='fa fa-th-list'></i></span> ";
            }
            if (i == 7)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='icon-tools'></i></span> ";
            }
            if (i == 8)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='fa fa-table'></i></span> ";
            }
            if (i == 9)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='fa fa-signal'></i></span> ";
            }
            if (i == 10)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='fa fa-map-marker'></i></span> ";
            }
            if (i == 11)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='icon-newspaper'></i></span> ";
            }
            if (i == 12)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='fa fa-desktop'></i></span> ";
            }
            if (i == 13)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='fa fa-shopping-cart'></i></span> ";
            }
            if (i == 14)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='fa fa-dashboard'></i></span> ";
            }
            if (i == 15)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='fa fa-dashboard'></i></span> ";
            }
            if (i == 16)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='fa fa-dashboard'></i></span> ";
            }
            if (i == 17)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='fa fa-dashboard'></i></span> ";
            }
            if (i == 18)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='fa fa-dashboard'></i></span> ";
            }
            if (i == 19)
            {
                InnerHTML = InnerHTML + "<span class='menu-icon'><i class='fa fa-dashboard'></i></span> ";
            }
            InnerHTML = InnerHTML + "<span class='menu-text'>" + dt.Rows[i]["name"].ToString() + "</span> "; 
            InnerHTML = InnerHTML + "<span class='menu-badge'><span class='badge vd_bg-black-30'><i class='fa fa-angle-down' style='color: red;font-size: 17px;'></i></span></span>";
       	    InnerHTML = InnerHTML + "</a>";
            InnerHTML = InnerHTML + "<div class='child-menu'  data-action='click-target'><ul>";
            InnerHTML = InnerHTML + hasChild;
            InnerHTML = InnerHTML + "</ul></div>";
            InnerHTML = InnerHTML + "</li> ";

            }
        }
        InnerHTML = InnerHTML + "</ul>";
        //InnerHTML = InnerHTML + "<li><a href='javascript:;' class='sidebar-minify-btn' data-click='sidebar-minify'><i class='fa fa-angle-double-left'></i></a></li>";
        //InnerHTML = InnerHTML + "</ul>";
        //dvMenu.InnerHtml = InnerHTML;
        return InnerHTML;


    }

    public string getchildes(int x, string UTID)
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
                string hasChild = getSubchildes(Convert.ToInt32(dtA.Rows[i]["id"].ToString()), UTID);
                if (hasChild == "")
                {
                    string ActiveChild = "";
                    string path = dtA.Rows[i]["path"].ToString();
                    path = path.ToLowerInvariant();

                    if (PageURL.IndexOf(path) != -1)
                    { ActiveChild = " class=active"; }

                   
                SubString = SubString + "<li>";
                 SubString = SubString + "<a href='" + "../" + dtA.Rows[i]["path"].ToString() + "" + "'>";
                        SubString = SubString + "<span class='menu-text'>" + dtA.Rows[i]["name"].ToString() + "</span>";  
                       // SubString = SubString + "<span class='menu-badge'><span class='badge vd_bg-yellow'>NEW</span></span>";
                    SubString = SubString + "</a>";
                SubString = SubString + "</li>";  

                }
                else
                {
                    string ActiveParent = "";
                    if (hasChild.IndexOf("class=active") != -1) { ActiveParent = " active"; }

                    string TotalChild = hasChild;
                    TotalChild = TotalChild.Substring(0, TotalChild.IndexOf("`"));
                    hasChild = hasChild.Substring(hasChild.IndexOf("`") + 1);

                    //SubString = SubString + "<li><a><i class='fa fa-edit'></i>" + dtA.Rows[i]["name"].ToString() + "";
                    //SubString = SubString + "<span class='fa fa-chevron-down'></span></a>";
                    //SubString = SubString + "</a>";
                    //SubString = SubString + "<ul class='nav child_menu'>";
                    //SubString = SubString + hasChild;
                    //SubString = SubString + "</ul></li>";
SubString = SubString + "<li>";
                 SubString = SubString + "<a href='" + "../" + dtA.Rows[i]["path"].ToString() + "" + "'>";
                        SubString = SubString + "<span class='menu-text'>" + dtA.Rows[i]["name"].ToString() + "</span>";  
                        //SubString = SubString + "<span class='menu-badge'><span class='badge vd_bg-yellow'>NEW</span></span>";
                    SubString = SubString + "</a>";
                SubString = SubString + "</li>";





                }

            }
        }
        if (noOfChild > 0) { SubString = noOfChild + "`" + SubString; }


        return SubString;
    }


    public string getSubchildes(int x, string UTID)
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
                string hasChild = getchildes(Convert.ToInt32(dtA.Rows[i]["id"].ToString()), UTID);
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
