﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static Ecom.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                    await sql.OpenAsync();  

                   
                    string checkQuery = "SELECT COUNT(1) FROM Users WITH(NOLOCK) WHERE Email = @Email";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, sql))
                    {
                        checkCmd.Parameters.Add(new SqlParameter("@Email", data.Email));
                        int existingUser = Convert.ToInt32(await checkCmd.ExecuteScalarAsync());   

                        if (existingUser > 0)
                        {
                            return new MessageFor
                            {
                                Status = 0,
                                Message = "Email already registered."
                            };
                        }
                    }

                   
                    using (SqlCommand cmd = new SqlCommand("SpAddUserProfile", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Fullname", data.FullName));
                        cmd.Parameters.Add(new SqlParameter("@Email", data.Email));
                        cmd.Parameters.Add(new SqlParameter("@Password", DBConnection.encryptP(data.Password)));
                        cmd.Parameters.Add(new SqlParameter("@Role", data.Role));


                        await cmd.ExecuteNonQueryAsync();  
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
                    Message = "Something went wrong." 
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

        public static MessageFor Login(LoginModel data)
        {
            string token = string.Empty;

            using (SqlConnection sql = new SqlConnection(DBConnection.cs))
            {
                using (SqlCommand cmd = new SqlCommand("Login", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Email", data.Email));
                    cmd.Parameters.Add(new SqlParameter("@Password", DBConnection.encryptP(data.Password)));

                    sql.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() && reader["status"] != DBNull.Value && Convert.ToInt32(reader["status"]) == 1)
                        {
                            // Generate JWT Token
                            token = GenerateJwtToken(data.Email);

                            return new MessageFor
                            {
                                Status = 1,
                                Message = "Login Successfully",
                                Token = token,

                            };
                        }
                    }
                }
            }

            return new MessageFor
            {
                Status = 0,
                Message = "Invalid email or password."
            };
        }

        public static string GenerateJwtToken(string email)
        {
            var key = Encoding.ASCII.GetBytes("jOj_J?CZjv$sY?3t^?3f9o0R>G!NmkWnlil"); // Use a secure key!
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }),
                Expires = DateTime.UtcNow.AddHours(1), // Token valid for 1 hour
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    
}

