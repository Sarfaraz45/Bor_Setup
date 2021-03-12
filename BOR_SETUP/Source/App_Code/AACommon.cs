using System;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

/// <summary>
/// Summary description for AACommon
/// </summary>
public class AACommon
{
    public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

	public AACommon()
	{
		//
		// TODO: Add constructor logic here
		//
	}



    public static DataTable LoadControlQry(string SPName, params SqlParameter[] Parameters)
    {
        con.Close();
        con.Open();
        SqlCommand cmd = new SqlCommand(SPName, con);
        DataTable dt = new DataTable();
        IDataReader dr;
        cmd.CommandType = CommandType.Text;
        if (Parameters != null)
            foreach (SqlParameter item in Parameters)
                cmd.Parameters.Add(item);
        try
        {

            dr = cmd.ExecuteReader();
            if (dr != null)
            {
                dt.Load(dr);
            }
        }
        catch
        {
            // If we fail to return the SqlDatReader, we need to close the connection
            throw;
        }
        finally
        {
            cmd = null;
            con.Close();
        }
        return dt;
    }

    public static string GetData(string query, SqlConnection con, string ret)
    {
        string output = "";
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(query, con);
        //con.Open();
        da.Fill(dt);
        try
        {
            if (dt.Rows.Count > 0)
            {
                output = dt.Rows[0][ret].ToString();
            }
            else
            {
                output = "No";
            }

        }
        catch
        {
            throw;
        }
        //con.Close();
        return output;
    }


    public static string Execute(string SPName, SqlConnection con, params SqlParameter[] Parameters)
    {
        string msg = "";
        SqlCommand cmd = new SqlCommand(SPName, con);
        cmd.CommandText = SPName;
        cmd.CommandType = CommandType.StoredProcedure;
        if (Parameters != null)
            foreach (SqlParameter item in Parameters)
               cmd.Parameters.Add(item);
        try
        {
            if (con.State == ConnectionState.Closed) { con.Open(); }
            cmd.ExecuteNonQuery();
            con.Close();
            msg = "Record Saved Successfully";
        }
        catch(Exception ex)
        {
            msg = "Record NOT Saved Successfully";
        }
        cmd = null;
        return msg;
    }


    public static string ExecuteWithOUTPUT(string SPName, SqlConnection con, params SqlParameter[] Parameters)
    {
        string msg = "";
        SqlCommand cmd = new SqlCommand(SPName, con);
        cmd.CommandText = SPName;
        cmd.CommandType = CommandType.StoredProcedure;
        if (Parameters != null)
            foreach (SqlParameter item in Parameters)
                cmd.Parameters.Add(item);

        cmd.Parameters["@t"].Direction = ParameterDirection.Output;
        try
        {
            if (con.State == ConnectionState.Closed) { con.Open(); }
            //cmd.Parameters.Add(new SqlParameter("@t", SqlDbType.VarChar, 50, ParameterDirection.Output, false, 0, 0, "t", DataRowVersion.Default, null));
            //cmd.Parameters.AddWithValue("@t", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null);
            cmd.ExecuteNonQuery();
            msg = cmd.Parameters["@t"].Value.ToString();
            con.Close();

        }
        catch (Exception ex)
        {
            msg = "Un Successfully Completed";
        }
        cmd = null;
        return msg;
    }


    public string DeleteData(string sp, string par, string val, SqlConnection con)
    {
        string output = "";
        SqlCommand cmd = new SqlCommand(sp, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@" + par, val);
        if (con.State == ConnectionState.Closed) { con.Open(); }
        cmd.ExecuteNonQuery();
        con.Close();
        output = "Record Deleted";
        return output;
    }

    public static DataTable LoadControl(string SPName, SqlConnection con, params SqlParameter[] Parameters)
    {
        SqlCommand cmd = new SqlCommand(SPName, con);
        DataTable dt = new DataTable();
        IDataReader dr;
        cmd.CommandType = CommandType.StoredProcedure;
        if (Parameters != null)
            foreach (SqlParameter item in Parameters)
                cmd.Parameters.Add(item);
        try
        {
            if (con.State == ConnectionState.Closed) { con.Open(); }
            dr = cmd.ExecuteReader();
            if (dr != null)
            {
                dt.Load(dr);
            }
            con.Close();
        }
        catch
        {
            // If we fail to return the SqlDatReader, we need to close the connection
            throw;
        }

        cmd = null;
        return dt;
    }



    //public static DataTable LoadControl(string SPName, SqlConnection con, params SqlParameter[] Parameters)
    //{
    //    SqlCommand cmd = new SqlCommand(SPName, con);
    //    DataTable dt = new DataTable();
    //    IDataReader dr;
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    if (Parameters != null)
    //        foreach (SqlParameter item in Parameters)
    //            cmd.Parameters.Add(item);
    //    try
    //    {
    //        if (con.State == ConnectionState.Closed) { con.Open(); }
    //        dr = cmd.ExecuteReader();
    //        if (dr != null)
    //        {
    //            dt.Load(dr);
    //        }
    //        con.Close();
    //    }
    //    catch
    //    {
    //        // If we fail to return the SqlDatReader, we need to close the connection
    //        throw;
    //    }

    //    cmd = null;
    //    return dt;
    //}

    public static string GetWorkOrderNo(string SPName, SqlConnection con, string branch, params SqlParameter[] Parameters)
    {
        string output = "";

        SqlCommand cmd = new SqlCommand(SPName, con);

        cmd.CommandType = CommandType.StoredProcedure;
        if (Parameters != null)
            foreach (SqlParameter item in Parameters)
                cmd.Parameters.Add(item);


        try
        {
            if (con.State == ConnectionState.Closed) { con.Open(); }
            output = GetData("select BranchSeries from Branch where BranchID=" + branch + "", con, "BranchSeries") + "-" + (Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1);
            con.Close();
        }
        catch
        {
            if (con != null)
            { throw; }
        }
        finally
        { //con.Close();
        }

        cmd = null;


        return output;
    }

    public static string GetValueFromSP(string SPName, SqlConnection con, params SqlParameter[] Parameters)
    {
        string output = "";

        SqlCommand cmd = new SqlCommand(SPName, con);

        cmd.CommandType = CommandType.StoredProcedure;
        if (Parameters != null)
            foreach (SqlParameter item in Parameters)
                cmd.Parameters.Add(item);


        try
        {
            if (con.State == ConnectionState.Closed) { con.Open(); }
            output = cmd.ExecuteScalar().ToString();
            con.Close();
        }
        catch
        {
            if (con != null)
            { throw; }
        }
        finally
        { //con.Close();
        }

        cmd = null;


        return output;
    }


    public string UpDelete(string sp, string par, string val, SqlConnection con)
    {
        string output = "";
        SqlCommand cmd = new SqlCommand(sp, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@" + par, val);
        if (con.State == ConnectionState.Closed) { con.Open(); }
        cmd.ExecuteNonQuery();
        con.Close();
        output = "Record Updated Successfully";
        return output;
    }

    public string UpdateData(string sp, SqlConnection con)
    {
        string output = "";
        SqlCommand cmd = new SqlCommand(sp, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("", sp);
        if (con.State == ConnectionState.Closed) { con.Open(); }
        cmd.ExecuteNonQuery();
        con.Close();
        cmd.Dispose();

        output = "Record Updated Successfully";
        return output;


    }

    public static string GetHAWB_ID(string WON, string Des, SqlConnection con)
    {
        string ID = "";

        ID = GetData("select HAWB from HAWB where HAWB like '" + WON + "_" + Des + "' order by HAWB Desc", con, "HAWB");
        ID = ID.Substring(WON.Length, 1);
        ID = Convert.ToString(Convert.ToInt32(ID) + 1);
        ID = WON + ID + Des;


        return ID;

    }

    public static string GetIDWithSingleWord(string Table,string Format, string Order,string Col,string OrderCol, SqlConnection con)
    {
        string ID = "";
        
        ID = GetData("select " + Col + " from " + Table + " where " + Col + " like '" + Format + "%' order by " + OrderCol + " " + Order + "", con, Col);
        if (ID == "No") { ID = Format+"1"; }
        else
        {
            ID = ID.Substring(Format.Length);
            ID = Convert.ToString(Convert.ToInt32(ID) + 1);
            ID = Format + ID;
        }
        return ID;

    }


    public static string GetAlphaNumericID(string Table, string Format, string Col, SqlConnection con)
    {
        string ID = "";

        //ID = GetData("select " + Col + " from " + Table + " where " + Col + " like 'File%' order by " + Col + " " + Order + "", con, Col);
        ID = GetData("select " + Col + " from " + Table + " where " + Col + " like '" + Format + "%' order by " + Col + " Desc ", con, Col);
        if (ID == "No") { ID = Format + "00001"; }
        else
        {

            ID = ID.Substring(Format.Length);
            ID = Convert.ToString(Convert.ToInt32(ID) + 1);
            if (ID.Length == 1) { ID = Format + "0000" + ID; }
            else if (ID.Length == 2) { ID = Format + "000" + ID; }
            else if (ID.Length == 3) { ID = Format + "00" + ID; }
            else if (ID.Length == 4) { ID = Format + "0" + ID; }
            else if (ID.Length == 5) { ID = Format + ID; }
        }
        return ID;

    }

    public static string GetAlphaNumericIDSIX(string Table, string Format, string Col, SqlConnection con)
    {
        string ID = "";

        //ID = GetData("select " + Col + " from " + Table + " where " + Col + " like 'File%' order by " + Col + " " + Order + "", con, Col);
        ID = GetData("select " + Col + " from " + Table + " where " + Col + " like '" + Format + "%' order by " + Col + " Desc ", con, Col);
        if (ID == "No") { ID = Format + "000001"; }
        else
        {

            ID = ID.Substring(Format.Length);
            ID = Convert.ToString(Convert.ToInt32(ID) + 1);
            if (ID.Length == 1) { ID = Format + "00000" + ID; }
            else if (ID.Length == 2) { ID = Format + "0000" + ID; }
            else if (ID.Length == 3) { ID = Format + "000" + ID; }
            else if (ID.Length == 4) { ID = Format + "00" + ID; }
            else if (ID.Length == 5) { ID = Format + "0" + ID; }
            else if (ID.Length == 6) { ID = Format + ID; }
        }
        return ID;

    }


    public static string GetAlphaNumericIDTWO(string Table, string Format, string Col, SqlConnection con)
    {
        string ID = "";

        //ID = GetData("select " + Col + " from " + Table + " where " + Col + " like 'File%' order by " + Col + " " + Order + "", con, Col);
        ID = GetData("select " + Col + " from " + Table + " where " + Col + " like '" + Format + "%' order by " + Col + " Desc ", con, Col);
        if (ID == "No") { ID = Format + "01"; }
        else
        {

            ID = ID.Substring(Format.Length);
            ID = Convert.ToString(Convert.ToInt32(ID) + 1);
            if (ID.Length == 1) { ID = Format + "0" + ID; }
            else if (ID.Length > 1) { ID = Format + ID; }
        }
        return ID;

    }

    public static string GetAlphaNumericIDTHREE(string Table, string Format, string Col, SqlConnection con)
    {
        string ID = "";

        //ID = GetData("select " + Col + " from " + Table + " where " + Col + " like 'File%' order by " + Col + " " + Order + "", con, Col);
        ID = GetData("select " + Col + " from " + Table + " where " + Col + " like '" + Format + "%' order by " + Col + " Desc ", con, Col);
        if (ID == "No") { ID = Format + "001"; }
        else
        {

            ID = ID.Substring(Format.Length);
            ID = Convert.ToString(Convert.ToInt32(ID) + 1);
            if (ID.Length == 1) { ID = Format + "00" + ID; }
            else if (ID.Length == 2) { ID = Format + "0" + ID; }
            else if (ID.Length == 3) { ID = Format + ID; }
        }
        return ID;

    }

    public static string SaveImage(FileUpload FU,string FileName,decimal SizeLimitKB,string path)
    {
        string msg = "";
        string fName = "";
        SizeLimitKB = SizeLimitKB * 1024;
        if (FU.HasFile)
        {
            if (FU.PostedFile.ContentLength <= SizeLimitKB)
            {
                fName = path + FileName + FU.FileName.Substring(FU.FileName.IndexOf("."));
                if (File.Exists(fName)) { File.Delete(fName); }
                FU.SaveAs(fName);

                msg = "File Saved Successfully";
            }
            else
            {
                msg = "File is larger than limit";
            }
        }
        else
        {
            msg = "No File Selected";
        }

        return msg;
 
    }

    public static DataSet ReturnDatasetBySPWithoutParameter(string SPName, SqlConnection con)
    {
        DataSet ds = new DataSet();

        SqlDataAdapter DataAdapObj = new SqlDataAdapter();

        SqlCommand cmd = new SqlCommand(SPName, con);
        cmd.CommandType = CommandType.StoredProcedure;
        DataAdapObj.SelectCommand = cmd;       

        ds.Clear();
        if (con.State == ConnectionState.Closed) { con.Open(); }
        DataAdapObj.Fill(ds);
        DataAdapObj.Dispose();
        cmd.Dispose();
        cmd.Dispose();
        con.Close();
        return ds;
    }
    public static DataSet ReturnDatasetBySP(string SPName, SqlConnection con, params SqlParameter[] Parameters)
    {
        DataSet ds = new DataSet();

        SqlDataAdapter DataAdapObj = new SqlDataAdapter();

        SqlCommand cmd = new SqlCommand(SPName, con);
        cmd.CommandType = CommandType.StoredProcedure;
        DataAdapObj.SelectCommand = cmd;

        if (Parameters != null)
            foreach (SqlParameter item in Parameters)
                cmd.Parameters.Add(item);

        ds.Clear();
        if (con.State == ConnectionState.Closed) { con.Open(); }
        DataAdapObj.Fill(ds);
        DataAdapObj.Dispose();
        cmd.Dispose();
        cmd.Dispose();
        con.Close();
        return ds;
    }


    public static DataSet ReturnDatasetBySPForREPORT(string SPName, string RptTable, SqlConnection con, params SqlParameter[] Parameters)
    {
        DataSet ds = new DataSet();

        SqlDataAdapter DataAdapObj = new SqlDataAdapter();

        SqlCommand cmd = new SqlCommand(SPName, con);
        cmd.CommandType = CommandType.StoredProcedure;
        DataAdapObj.SelectCommand = cmd;
        cmd.Parameters.Clear();
        if (Parameters != null)
            foreach (SqlParameter item in Parameters)
                cmd.Parameters.Add(item);

        ds.Clear();
        if (con.State == ConnectionState.Closed) { con.Open(); }
        DataAdapObj.Fill(ds, RptTable);
        DataAdapObj.Dispose();
        cmd.Dispose();
        cmd.Dispose();
        con.Close();
        return ds;
    }


    public static DataSet ReturnDatasetBySPForREPORT_FOR_SUBREPORT(string SPName, string RptTable,DataSet ds, SqlConnection con, params SqlParameter[] Parameters)
    {
        //DataSet ds = new DataSet();

        SqlDataAdapter DataAdapObj = new SqlDataAdapter();

        SqlCommand cmd = new SqlCommand(SPName, con);
        cmd.CommandType = CommandType.StoredProcedure;
        DataAdapObj.SelectCommand = cmd;
        cmd.Parameters.Clear();
        if (Parameters != null)
            foreach (SqlParameter item in Parameters)
                cmd.Parameters.Add(item);

        //ds.Clear();
        if (con.State == ConnectionState.Closed) { con.Open(); }
        DataAdapObj.Fill(ds, RptTable);
        DataAdapObj.Dispose();
        cmd.Dispose();
        cmd.Dispose();
        con.Close();
        return ds;
    }

    public static DataSet ReturnDatasetBySPForREPORT_New_Sadiq(string SPName, string RptTable, string RptTable1, SqlConnection con, params SqlParameter[] Parameters)
    {
        DataSet ds = new DataSet();

        SqlDataAdapter DataAdapObj = new SqlDataAdapter();

        SqlCommand cmd = new SqlCommand(SPName, con);
        cmd.CommandType = CommandType.StoredProcedure;
        DataAdapObj.SelectCommand = cmd;
        cmd.Parameters.Clear();
        if (Parameters != null)
            foreach (SqlParameter item in Parameters)
                cmd.Parameters.Add(item);

        ds.Clear();
        if (con.State == ConnectionState.Closed) { con.Open(); }
        DataAdapObj.Fill(ds, RptTable);
        ds.Tables[0].TableName = RptTable;
        ds.Tables[1].TableName = RptTable1;
        DataAdapObj.Dispose();
        cmd.Dispose();
        cmd.Dispose();
        con.Close();
        return ds;
    }

    public static DataSet ReturnDatasetBySPForREPORT_Without_Parameter(string SPName, string RptTable, SqlConnection con)
    {
        DataSet ds = new DataSet();

        SqlDataAdapter DataAdapObj = new SqlDataAdapter();

        SqlCommand cmd = new SqlCommand(SPName, con);
        cmd.CommandType = CommandType.StoredProcedure;
        DataAdapObj.SelectCommand = cmd;

        ds.Clear();
        if (con.State == ConnectionState.Closed) { con.Open(); }
        DataAdapObj.Fill(ds, RptTable);
        DataAdapObj.Dispose();
        cmd.Dispose();
        cmd.Dispose();
        con.Close();
        return ds;
    }

}
