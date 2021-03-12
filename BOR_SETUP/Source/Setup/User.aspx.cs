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

public partial class Setup_User : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["PageTitle"] = "User";

    }



    [WebMethod]
    public static string InsertRegion(string DistrictName, string LoginIDString, string PasswordString, string EmailString, string PhoneString, string ManagerIDString, string UTIDString, string image, string UUserID, string BranchID)
    {

        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        string ID = AACommon.GetAlphaNumericIDSIX("USERS", "USR-", "UserID", Conn);
        string ImageID = ID;
        //string Pic_Path = HttpContext.Current.Server.MapPath("~/Images/UserImages/");
        //string fileNameWitPath = Pic_Path + ImageID + ".jpg";
        //using (FileStream fs = new FileStream(Pic_Path, FileMode.Create))
        //{
        //    using (BinaryWriter bw = new BinaryWriter(fs))
        //    {
        //        byte[] data = Convert.FromBase64String(image);
        //        bw.Write(data);
        //        bw.Close();
        //    }
        //}

        string path = HttpContext.Current.Server.MapPath("~/Images/UserImages/");
        string fileNameWitPath = path + ImageID + ".jpg";
        ImageID = ImageID + ".jpg";
        if (image != "No Image")
        {
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {

                using (BinaryWriter bw = new BinaryWriter(fs))
                {

                    byte[] data = Convert.FromBase64String(image);

                    bw.Write(data);

                    bw.Close();

                }

            }
        }
        else
        {
            image = "/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAMCAgICAgMCAgIDAwMDBAYEBAQEBAgGBgUGCQgKCgkICQkKDA8MCgsOCwkJDRENDg8QEBEQCgwSExIQEw8QEBD/2wBDAQMDAwQDBAgEBAgQCwkLEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBD/wAARCABaAFoDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD7YooooAKKKms7S5v7mOztIzJNKwVFHc/0HvQBDVqz0vUtQOLGwnn944yR+fSvSdA8B6XpiJNfxpd3XUlhmNT/ALKn+ZrplVUUKqgAdAOgoA8euvCWuWFo99fW8VvEgyfMmUHPoADyfaseva9b0Sy16zNnfKxUHejKcMjeo/PvXlPiHw9e+HroQ3WHicZhmUYDj09j6igDKooooAKKKKACiiigArtPhhbxvqV5cMoLRQqF9txOf5CuLrvPhaP3mot/sxD9WoA7+iiigArC8bWsV14bvfMUEwx+ah/usp6/zH41u1keK/8AkXNS/wCvZ/5UAeN0UUUAFFFFABRRRQAV6P8ADOwSLTZ9RWZma4fy2QjhdmcY+ua84r0D4Y6lGYLrSGyJFf7QnoVOAfyIH50Ad3RRRQAVT1ex/tLTLmxMvlCeJkL7c7c98d6uVm+IdSi0nR7q8lJ+WMqgHUueFH50AeLdCeeneigcKB6CigAooooAKKKKACtnwlqsOka9b3Vy22FsxSHsA3GT9Disakb7p+lAHvYIYAg5Bpap6PF5Ok2cJBylvGD/AN8irlABXBfE3VoytvosTZfPny47DooP6n8BXeHpXkHjZs+KL8A9GQYz/sLQBiUUUUAFFFFABRSEgDJNa+keFta1rD2dpthP/LaX5U/DufwoAya6b4faV/aGufaJoFkgtELNvXI3Hhf6n8K6bSfhxpdptk1ORr2Qclfuxj8ByfxNdVBbW9rEsNtAkUa9FRQAPwFAEnA4FLRRQAVwvxN00fZ7bVooV/dsY5mA5w33SfoQR+Nd1TWVXBV1BBGCCM5FAHg3vRXrGreA9B1LdJFbmzmPO+DgZ916GuM1fwDrembpLZVvoRzmIYcD3X/DNAHNUUrKyMUdSrA4IIwR+FJQB6h4f8A6bpgS51FVvLoc/MMxofZe/wBTXVBQMYGMdKB0paACiiigAooooAKKKKACkxmlooAydZ8M6Pri/wCmWoEuMCZPlkH49/oc1yD/AAuv97eXq8GzJ27ojnHbPPWvRaKAP//Z";
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {

                using (BinaryWriter bw = new BinaryWriter(fs))
                {

                    byte[] data = Convert.FromBase64String(image);

                    bw.Write(data);

                    bw.Close();

                }

            }
        }

        PasswordString = Encryption.Encrypt(PasswordString);
        SqlParameter UserID = new SqlParameter("@UserID", ID);
        SqlParameter UserName = new SqlParameter("@UserName", DistrictName);
        SqlParameter loginID = new SqlParameter("@loginID", LoginIDString);
        SqlParameter Password = new SqlParameter("@Password", PasswordString);
        SqlParameter email = new SqlParameter("@email", EmailString);
        SqlParameter Phone = new SqlParameter("@Phone", PhoneString);
        SqlParameter Picture = new SqlParameter("@Picture", ImageID);
        SqlParameter ManagerID = new SqlParameter("@ManagerID", ManagerIDString);
        SqlParameter UTID = new SqlParameter("@UTID", UTIDString);
        SqlParameter CREATEBY = new SqlParameter("@CREATEBY", UUserID);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        msg = AACommon.Execute("USER_INSERT", Conn, UserID, UserName, loginID, Password, email, Phone, Picture, ManagerID, UTID, CREATEBY,Branch);


        if (msg == "Record Saved Successfully")
        {
            retMessage = "true";
        }
        else
        {
            retMessage = "false";
        }

        return retMessage;
    }



    [WebMethod]
    public static string UpdateRegion(string DistrictID, string DistrictName, string LoginIDString, string PasswordString, string EmailString, string PhoneString,  string ManagerIDString, string UTIDString, string UUserID,string BranchID)
    {
        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        PasswordString = Encryption.Encrypt(PasswordString);
        SqlParameter UserID = new SqlParameter("@UserID", DistrictID);
        SqlParameter UserName = new SqlParameter("@UserName", DistrictName);
        SqlParameter loginID = new SqlParameter("@loginID", LoginIDString);
        SqlParameter Password = new SqlParameter("@Password", PasswordString);
        SqlParameter email = new SqlParameter("@email", EmailString);
        SqlParameter Phone = new SqlParameter("@Phone", PhoneString);        
        SqlParameter ManagerID = new SqlParameter("@ManagerID", ManagerIDString);
        SqlParameter UTID = new SqlParameter("@UTID", UTIDString);
        SqlParameter MODIFYBY = new SqlParameter("@MODIFYBY", UUserID);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        msg = AACommon.Execute("USER_UPDATE_2", Conn, UserID, UserName, loginID, Password, email, Phone,  ManagerID, UTID, MODIFYBY,Branch);


        if (msg == "Record Saved Successfully")
        {
            retMessage = "true";
        }
        else
        {
            retMessage = "false";
        }

        return retMessage;
    }


    [WebMethod]
    public static string DeleteRegion(string DistrictID, string UUserID,string BranchID)
    {
        string retMessage = string.Empty;
        string msg = "";
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter UserID_1 = new SqlParameter("@UserID", DistrictID);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("USER_CHECK_FOR_DELETE_MANAGER", Conn, UserID_1,Branch);
        if (ds.Tables[0].Rows.Count > 0)
        { retMessage = "false"; return retMessage; }
        else
        {
            SqlParameter UserID = new SqlParameter("@UserID", DistrictID);
            SqlParameter MODIFYBY = new SqlParameter("@MODIFYBY", UUserID);
            SqlParameter BranchP = new SqlParameter("@BranchID", BranchID);
            msg = AACommon.Execute("USER_DELETE", Conn, UserID, MODIFYBY,BranchP);


            if (msg == "Record Saved Successfully")
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





    [WebMethod]
    public static string LoadRegion(string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter Branch = new SqlParameter("@BranchID",BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("USER_GET_ALL", Conn,Branch);
        List<GetDistrictClass> RegionList = new List<GetDistrictClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetDistrictClass dbdc = new GetDistrictClass();

                dbdc.ID = ds.Tables[0].Rows[i]["UserID"].ToString();
                dbdc.Name = ds.Tables[0].Rows[i]["UserName"].ToString();
                dbdc.Login = ds.Tables[0].Rows[i]["LoginID"].ToString();
                dbdc.Email = ds.Tables[0].Rows[i]["email"].ToString();
                dbdc.Phone = ds.Tables[0].Rows[i]["Phone"].ToString();
                dbdc.Picture = ds.Tables[0].Rows[i]["Picture"].ToString();
                dbdc.Manager = ds.Tables[0].Rows[i]["ManagerID"].ToString();
                dbdc.UTID = ds.Tables[0].Rows[i]["UTID"].ToString();
                dbdc.Branch = ds.Tables[0].Rows[i]["BranchID"].ToString();
                RegionList.Insert(i, dbdc);
            }

        }


        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);


    }


    public class GetDistrictClass
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Picture { get; set; }
        public string Manager { get; set; }
        public string UTID { get; set; }
        public string Branch { get; set; }


    }

    [WebMethod]
    public static string LoadRegionName(string RegionName,string BranchID)
    {
        string retMessage = string.Empty;
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter LoginID = new SqlParameter("@loginID", RegionName);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("USER_GET_BY_LOGIN", Conn, LoginID,Branch);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            retMessage = "false";

        }
        else
        {
            retMessage = "true";
        }



        return retMessage;
    }
    [WebMethod]
    public static string LoadLoginID(string LoginID,string BranchID)
    {
        string retMessage = string.Empty;
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter loginID = new SqlParameter("@loginID", LoginID);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("USER_GET_BY_LOGIN", Conn, loginID,Branch);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            retMessage = "false";

        }
        else
        {
            retMessage = "true";
        }



        return retMessage;
    }



    [WebMethod]
    public static string LoadUTIDCombo(string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("USERTYPE_GET_ALL", Conn,Branch);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClass dbdc = new GetRegionClass();

                dbdc.ID = ds.Tables[0].Rows[i][0].ToString();
                dbdc.Name = ds.Tables[0].Rows[i][1].ToString();
                RegionList.Insert(i, dbdc);
            }

        }

        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);
    }



    public class GetRegionClass
    {
        public string ID { get; set; }
        public string Name { get; set; }

    }





    [WebMethod]
    public static string LoadRegionCombo(string BranchID)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlParameter Branch = new SqlParameter("@BranchID", BranchID);
        DataSet ds = AACommon.ReturnDatasetBySP("USER_MANAGER_GET_ALL_COMBO", Conn,Branch);
        List<GetRegionClass> RegionList = new List<GetRegionClass>();
        RegionList.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetRegionClass dbdc = new GetRegionClass();

                dbdc.ID = ds.Tables[0].Rows[i][0].ToString();
                dbdc.Name = ds.Tables[0].Rows[i][1].ToString();
                RegionList.Insert(i, dbdc);
            }

        }

        JavaScriptSerializer jser = new JavaScriptSerializer();


        return jser.Serialize(RegionList);
    }




}