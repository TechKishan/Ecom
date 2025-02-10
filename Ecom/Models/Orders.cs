using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static Ecom.Common;

namespace Ecom.Models
{
    public class Orders
    {
        public static MessageFor OrderInsert(Order data)
        {
            try
            {
                SqlConnection sql = new SqlConnection(DBConnection.cs);
                SqlCommand cmd = new SqlCommand("Sp_Orders", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Action", "INSERT"));
                cmd.Parameters.Add(new SqlParameter("@UserId", data.UserId));
                cmd.Parameters.Add(new SqlParameter("@ShippingAddress", data.ShippingAddress));
                cmd.Parameters.Add(new SqlParameter("@TotalAmount", data.TotalAmount));
                sql.Open();
                cmd.ExecuteNonQuery();
                sql.Close();
                return new MessageFor
                {
                    Status = 1,
                    Message = "Order created Successfully."
                };
            }
            catch (Exception ex)
            {
                return new MessageFor
                {
                    Status = 0,
                    Message = "Something went wrong.",
                };
            }
        }
        public static MessageFor OrderUpdate(Order data)
        {
            try
            {
                SqlConnection sql = new SqlConnection(DBConnection.cs);
                SqlCommand cmd = new SqlCommand("Sp_Orders", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Action", "UPDATE"));
                cmd.Parameters.Add(new SqlParameter("@OrderId", data.OrderId));
                cmd.Parameters.Add(new SqlParameter("@OrderStatus", data.OrderStatus));
                cmd.Parameters.Add(new SqlParameter("@PaymentStatus", data.PaymentStatus));
                sql.Open();
                cmd.ExecuteNonQuery();
                sql.Close();
                return new MessageFor
                {
                    Status = 1,
                    Message = "Order updated Successfully."
                };
            }
            catch (Exception ex)
            {
                return new MessageFor
                {
                    Status = 0,
                    Message = "Something went wrong.",
                };
            }
        }
        public static MessageFor OrderDelete(Order data)
        {
            try
            {
                SqlConnection sql = new SqlConnection(DBConnection.cs);
                SqlCommand cmd = new SqlCommand("Sp_Orders", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Action", "DELETE"));
                cmd.Parameters.Add(new SqlParameter("@OrderId", data.OrderId));
        
                sql.Open();
                cmd.ExecuteNonQuery();
                sql.Close();
                return new MessageFor
                {
                    Status = 1,
                    Message = "Order Delete Successfully."
                };
            }
            catch (Exception ex)
            {
                return new MessageFor
                {
                    Status = 0,
                    Message = "Something went wrong.",
                };
            }
        }
    }
}
public class Order
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public string ShippingAddress { get; set; }
    public string TotalAmount { get; set; }
    public string OrderStatus { get; set; }
    public string PaymentStatus { get; set; }
}