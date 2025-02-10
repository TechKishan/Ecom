using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static Ecom.Common;
using System.Web.Http;
using Microsoft.Ajax.Utilities;

namespace Ecom.Models
{

    public class Products
    {
        public static MessageFor AddProduct(ProductItem data)
        {
            SqlConnection sql = new SqlConnection(DBConnection.cs);
            SqlCommand cmd = new SqlCommand("Sp_Products", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Action", "INSERT"));
            cmd.Parameters.Add(new SqlParameter("@Name", data.Name));
            cmd.Parameters.Add(new SqlParameter("@Description", data.Description));
            cmd.Parameters.Add(new SqlParameter("@Price", data.Price));
            cmd.Parameters.Add(new SqlParameter("@Stock", data.Stock));
            //cmd.Parameters.Add(new SqlParameter("@CategoryID",data.CategoryID));
            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
            return new MessageFor
            {
                Status = 1,
                Message = "Product added."
            };
        }

        public static DataTable GetProduct()
        {
            SqlConnection sql = new SqlConnection(DBConnection.cs);
            SqlCommand cmd = new SqlCommand("Sp_Products", sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Action", "GET_ALL"));

            DataTable table = new DataTable();
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            sql.Open();
            adt.Fill(table);
            sql.Close();
            return table;

        }

        public static MessageFor UpdateProduct(ProductItem data)
        {
            try
            {
                SqlParameter[] sql = new SqlParameter[] {
                new SqlParameter("@Action", "UPDATE"),
                new SqlParameter("@Name", data.Name),
                new SqlParameter("@Description", data.Description),
                new SqlParameter("@Price", data.Price),
                new SqlParameter("@Stock", data.Stock),
                new SqlParameter("@ProductID", data.ProductID),

            };
                DBConnection dB = new DBConnection();
                dB.ExecuteNonQuery("Sp_Products", sql);
                
                    return new MessageFor()
                    {
                        Status = 1,
                        Message = "Update Successfully"
                    };
            }

            catch (Exception ex)
            {
                return new MessageFor()
                {
                    Status = 0,
                    Message = "Something went wrong."
                };
            }
        }

        /* public static List<Dictionary<string, object>> GetProductById(int ProductID)
         {
             try
             {
                 SqlParameter[] sql = new SqlParameter[] {
                     new SqlParameter("@Action", "GetProductById"),
                     new SqlParameter("@ProductID", ProductID),
                 };
                 SqlConnection sqls = new SqlConnection(DBConnection.cs);
                 SqlCommand cmd = new SqlCommand("Sp_Products", sqls);
                 cmd.CommandType = CommandType.StoredProcedure;
                 DataTable dt = new DataTable();
                 SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                 sqls.Open();
                 sqlDataAdapter.Fill(dt);
                 sqls.Close();
                 List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
                 return result;

             }
             catch (Exception ex)
             {
                 return null;
             }
         }
        */
        public static DataTable GetProductById(ProductItem data)
        {
            try
            {
                SqlConnection sql = new SqlConnection(DBConnection.cs);
                SqlCommand cmd = new SqlCommand("Sp_Products", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", data.ProductID));
                cmd.Parameters.Add(new SqlParameter("@Action", "GET_BY_ID"));
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                sql.Open();
                adapter.Fill(dt);
                sql.Close();
                return dt;
            }
            catch(Exception ex)
            {
                return null;    
            }
        }
    }
}


public class ProductItem
{
    public int ProductID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Price { get; set; }
    public int Stock { get; set; }
    public int CategoryID { get; set; }
    public string ImageUrl { get; set; }
}
