using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static Ecom.Common;

namespace Ecom.Models
{
    public class Users
    {

        public static async Task<MessageFor> AddUserInfo(UserProfile data)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(DBConnection.cs))
                {
                    await sql.OpenAsync(); // ✅ Open connection asynchronously

                    // ✅ Step 1: Check if the email already exists
                    string checkQuery = "SELECT COUNT(1) FROM Users WITH(NOLOCK) WHERE Email = @Email";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, sql))
                    {
                        checkCmd.Parameters.Add(new SqlParameter("@Email", data.Email));
                        int existingUser = Convert.ToInt32(await checkCmd.ExecuteScalarAsync()); // ✅ Await the async call

                        if (existingUser > 0)
                        {
                            return new MessageFor
                            {
                                Status = 0,
                                Message = "Email already registered."
                            };
                        }
                    }

                    // ✅ Step 2: Insert User (if email doesn’t exist)
                    using (SqlCommand cmd = new SqlCommand("SpAddUserProfile", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Fullname", data.FullName));
                        cmd.Parameters.Add(new SqlParameter("@Email", data.Email));
                        cmd.Parameters.Add(new SqlParameter("@Password", DBConnection.encryptP(data.Password)));
                        cmd.Parameters.Add(new SqlParameter("@Role", data.Role));


                        await cmd.ExecuteNonQueryAsync(); // ✅ Use async call
                    }

                    return new MessageFor
                    {
                        Status = 1,
                        Message = "User registered successfully."
                    };
                }
            }
            catch (Exception ex)
            {
                return new MessageFor
                {
                    Status = -1,
                    Message = "Something went wrong." // ✅ Return error message instead of crashing
                };
            }
        }


        public static DataTable GetUserInfo()
        {
            SqlConnection sql = new SqlConnection(DBConnection.cs);
            SqlCommand cmd = new SqlCommand("GetUsersList", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            sql.Open();
            dataAdapter.Fill(dt);
            sql.Close();
            return dt;

        }

        public static DataSet GetSingleDataUser(UserProfile data)
        {

            SqlConnection sql = new SqlConnection(DBConnection.cs);
            SqlCommand cmd = new SqlCommand("GetSingleDataUser", sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UserID", data.UserID));
            DataSet dt = new DataSet();
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            sql.Open();
            adt.Fill(dt);
            sql.Close();
            return dt;
        }

        /* public static MessageFor Login(UserProfile data)
         {
             SqlConnection sql = new SqlConnection(DBConnection.cs);
             SqlCommand cmd = new SqlCommand("Login", sql);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.Add(new SqlParameter("@Email", data.Email));
             cmd.Parameters.Add(new SqlParameter("@Password", DBConnection.encryptP(data.Password)));
             sql.Open();
             SqlDataReader reader = cmd.ExecuteReader();

             if (reader.Read())
             {
                 if (reader["status"] != null && Convert.ToInt32(reader["status"]) == 1)
                 {
                     return new MessageFor
                     {
                         Status = 1,
                         Message = "Login Successfully"
                     };
                 }
             }
             else
             {
                 return new MessageFor
                 {
                     Status = 0,
                     Message = "Email or password is wrong."
                 };
             }
             sql.Close();
             return new MessageFor
             {
                 Status = 0,
                 Message = "Invalid email or password."
             };
         }
        */

        public static MessageFor Login(UserProfile data)
        {
            // Validate input early if necessary.
            if (data == null || string.IsNullOrEmpty(data.Email) || string.IsNullOrEmpty(data.Password))
            {
                return new MessageFor
                {
                    Status = 0,
                    Message = "Invalid login data provided."
                };
            }

            // Use a using statement to ensure the connection is closed properly.
            using (SqlConnection sql = new SqlConnection(DBConnection.cs))
            {
                using (SqlCommand cmd = new SqlCommand("Login", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters for email and password.
                    cmd.Parameters.Add(new SqlParameter("@Email", data.Email));
                    cmd.Parameters.Add(new SqlParameter("@Password", DBConnection.encryptP(data.Password)));

                    sql.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if the stored procedure returned any result.
                        if (reader.Read())
                        {
                            // Ensure the status column exists and is valid.
                            if (reader["status"] != null &&
                                int.TryParse(reader["status"].ToString(), out int status) &&
                                status == 1)
                            {
                                return new MessageFor
                                {
                                    Status = 1,
                                    Message = "Login Successfully"
                                };
                            }
                            else
                            {
                                return new MessageFor
                                {
                                    Status = 0,
                                    Message = "Invalid email or password."
                                };
                            }
                        }
                        else
                        {
                            // No record was returned from the stored procedure.
                            return new MessageFor
                            {
                                Status = 0,
                                Message = "Email or password is wrong."
                            };
                        }
                    }
                }
            }
        }


    }
}

public class UserProfile
{
    public int UserID { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
}
