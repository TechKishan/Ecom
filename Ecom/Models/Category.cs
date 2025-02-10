using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static Ecom.Common;

namespace Ecom.Models
{
    public class Category
    {
        public static MessageFor InsertCategory(Categories data) //With Encapsulation
        {
            try
            {
                SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@Action","INSERT"),
                    new SqlParameter("@Name",data.Name),
                    new SqlParameter("@Description",data.Description),
                };
                DBConnection dB = new DBConnection();

                dB.ExecuteNonQuery("Sp_Category", para);
              
                    return new MessageFor
                    {
                        Status = 1,
                        Message = "Category Added."
                    };
            }
            catch (Exception ex)
            {
                return new MessageFor
                {
                    Status = 0,
                    Message = "Failed to Insert Category"
                };
            }
        }
        public static MessageFor AddCategory(Categories data)
        {
            try
            {
                SqlConnection sql = new SqlConnection(DBConnection.cs);
                SqlCommand cmd = new SqlCommand("Sp_Category", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add( new SqlParameter("@Action", "INSERT"));
                cmd.Parameters.Add(new SqlParameter("@Name", data.Name));
                cmd.Parameters.Add(new SqlParameter("@Description", data.Description));
                
                sql.Open();
                cmd.ExecuteNonQuery();
                sql.Close();
                return new MessageFor
                {
                    Status = 1,
                    Message = "Added Category."
                };

            }
            catch(Exception ex)
            {
                return new MessageFor
                {
                    Status = 0,
                    Message = "Something Went Wrong."
                };
            }

        }
    }
}
public class Categories
{
    public int CategoryID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}