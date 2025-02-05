using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static Ecom.Common;
using System.Web.Http;

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
                DBConnection dB= new DBConnection();
                int rowaffcted = dB.ExecuteNonQuery("Sp_Products", sql);
                if (rowaffcted > 0)
                {
                    return new MessageFor()
                    {
                        Status = 1,
                        Message = "Update Successfully"
                    };
                }
                else
                {
                    return new MessageFor()
                    {
                        Status = 1,
                        Message = "No record were found"
                    };
                }
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
