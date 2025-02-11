using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static Ecom.Common;

namespace Ecom.Models
{
    public class OrderDetails
    {
        public static MessageFor AddOrder(OrderDetail data)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[] {
                new SqlParameter("@Action","INSERT"),
                new SqlParameter("@OrderId",data.OrderId),
                new SqlParameter("@ProductId",data.ProductId),
                new SqlParameter("@Quantity",data.Quantity),
                new SqlParameter("@Price",data.Price)
            };
                DBConnection dB = new DBConnection();
                dB.ExecuteNonQuery("Sp_OrderDetails", para);
                return new MessageFor
                {
                    Status = 1,
                    Message = "Order."
                };
            }
            catch (Exception ex)
            {
                return new MessageFor
                {
                    Status = 1,
                    Message = "error."
                };
            }
        }

        public static MessageFor UpdateOrder(OrderDetail data)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[] {
                new SqlParameter("@Action","UPDATE"),
                new SqlParameter("@OrderDetailId",data.OrderDetailId),
                new SqlParameter("@Quantity",data.Quantity),
                new SqlParameter("@Price",data.Price)
            };
                DBConnection dB = new DBConnection();
                dB.ExecuteNonQuery("Sp_OrderDetails", para);
                return new MessageFor
                {
                    Status = 1,
                    Message = "Order Updated."
                };
            }
            catch (Exception ex)
            {
                return new MessageFor
                {
                    Status = 1,
                    Message = "error."
                };
            }
        }

        public static MessageFor DeleteOrder(OrderDetail data)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[] {
                new SqlParameter("@Action","DELETE"),
                new SqlParameter("@OrderDetailId",data.OrderDetailId),
            };
                DBConnection dB = new DBConnection();
                dB.ExecuteNonQuery("Sp_OrderDetails", para);
                return new MessageFor
                {
                    Status = 1,
                    Message = "Order Deleted."
                };
            }
            catch (Exception ex)
            {
                return new MessageFor
                {
                    Status = 1,
                    Message = "error."
                };
            }
        }

        public static DataTable GetOrderDetails(OrderDetail data)
        {
            SqlConnection sql = new SqlConnection(DBConnection.cs);
            SqlCommand cmd = new SqlCommand("Sp_OrderDetails", sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Action", "GET"));
            cmd.Parameters.Add(new SqlParameter("@OrderId",data.OrderId));
            DataTable dt=new DataTable();
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            sql.Open(); 
            adt.Fill(dt);
            sql.Close();
            return dt;
        }
    }
}
public class OrderDetail
{
    public int OrderDetailId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string Quantity { get; set; }
    public string Price { get; set; }
}