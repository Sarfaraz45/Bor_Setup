using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class SaveFile
{
    public string fileName { set; get; }
    public string fileExtension { set; get; }
    public byte[] data { set; get; }

    public string SaveFileToDB()
    {
        using (SqlConnection conn = new SqlConnection("Data Source=IRSHAD;Initial Catalog=FILE_UPLOAD;Integrated Security=True" +
             "Initial Catalog=ExampleDB;Integrated Security=True;Pooling=False"))
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SaveFilesProc";
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@Data", data);
            cmd.Parameters.AddWithValue("@FileName", fileName);
            cmd.Parameters.AddWithValue("@FileExtension", fileExtension);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return "File stored Successfully!!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }
        }
    }
}