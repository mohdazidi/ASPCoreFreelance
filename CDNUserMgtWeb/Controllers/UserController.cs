using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using CDNUserMgtWeb.Model;
using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;




// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDNUserMgtWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private static IConfiguration Configuration;
        private readonly string _connectionString;//= Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(Configuration, "DefaultConnection");

        public UserController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            _connectionString = Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(_configuration, "DefaultConnection");

        }

        /*
        
        private readonly string _connectionString;
        


        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        */
        //private readonly string _connectionString = Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(Configuration, "DefaultConnection"); 
        //Configuration.GetConnectionString("DefaultConnection");


        private bool IsUsernameExist(string username, MySqlConnection connection)
        {
            using MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM T_freelance WHERE Username = @Username", connection);
            cmd.Parameters.AddWithValue("@Username", username);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            return count > 0;
        }

        private bool IsEmailValid(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email && email.Contains("@");
            }
            catch
            {
                return false;
            }
        }



        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser([FromBody] FreelanceUser user)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(_connectionString);
                connection.Open();

                // Check if username already exists
                if (IsUsernameExist(user.Username, connection))
                {
                    return BadRequest("Username already exists.");
                }

                // Validate username and email
                if (!Regex.IsMatch(user.Username, "^[a-zA-Z0-9_]+$"))
                {
                    return BadRequest("Invalid characters in the username. Only letters, numbers, and underscore are allowed.");
                }

                if (!IsEmailValid(user.Mail))
                {
                    return BadRequest("Invalid email format.");
                }

                // Convert lists to JSON for insertion into the database
                string skillsetsJson = JsonConvert.SerializeObject(user.Skillsets);
                string hobbyJson = JsonConvert.SerializeObject(user.Hobby);

                // Insert into the database
                using MySqlCommand cmd = new MySqlCommand("INSERT INTO T_freelance (Username, Mail, PhoneNumber, Skillsets, Hobby) VALUES (@Username, @Mail, @PhoneNumber, @Skillsets, @Hobby)", connection);

                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Mail", user.Mail);
                cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@Skillsets", skillsetsJson);
                cmd.Parameters.AddWithValue("@Hobby", hobbyJson);

                cmd.ExecuteNonQuery();

                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                LogError(ex);
                return BadRequest("Failed to register user.");
            }
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] FreelanceUser user)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(_connectionString);
                connection.Open();

                // Validate username and email
                if (!Regex.IsMatch(user.Username, "^[a-zA-Z0-9_]+$"))
                {
                    return BadRequest("Invalid characters in the username. Only letters, numbers, and underscore are allowed.");
                }

                if (!IsEmailValid(user.Mail))
                {
                    return BadRequest("Invalid email format.");
                }

                // Convert lists to JSON for update in the database
                string skillsetsJson = JsonConvert.SerializeObject(user.Skillsets);
                string hobbyJson = JsonConvert.SerializeObject(user.Hobby);

                // Update in the database
                using MySqlCommand cmd = new MySqlCommand("UPDATE T_freelance SET Mail = @Mail, PhoneNumber = @PhoneNumber, Skillsets = @Skillsets, Hobby = @Hobby WHERE Username = @Username", connection);

                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Mail", user.Mail);
                cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@Skillsets", skillsetsJson);
                cmd.Parameters.AddWithValue("@Hobby", hobbyJson);

                cmd.ExecuteNonQuery();

                return Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                LogError(ex);
                return BadRequest("Failed to update user.");
            }
        }

        [HttpGet("ListAllUser")]
    public IActionResult GetAllUsers()
    {
        try
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand cmd = new MySqlCommand("SELECT * FROM T_freelance", connection);

            using MySqlDataReader reader = cmd.ExecuteReader();

            List<FreelanceUser> users = new List<FreelanceUser>();
                LogError(" do read all user");
            while (reader.Read())
            {
                var user = new FreelanceUser
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Username = reader["Username"].ToString(),
                    Mail = reader["Mail"].ToString(),
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    Skillsets = JsonConvert.DeserializeObject<List<string>>(reader["Skillsets"].ToString()),
                    Hobby = JsonConvert.DeserializeObject<List<string>>(reader["Hobby"].ToString())
                };

                users.Add(user);
            }

            return Ok(users);
        }
        catch (Exception ex)
        {
            LogError(ex);
            return BadRequest("Failed to get users.");
        }
    }


        [HttpGet("GetUser")]
        public IActionResult GetUser(string Username)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(_connectionString);
                connection.Open();

                using MySqlCommand cmd = new MySqlCommand("SELECT * FROM T_freelance WHERE UserName = @Username", connection);
                cmd.Parameters.AddWithValue("@Username", Username);

                using MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    var user = new FreelanceUser
                    {

                        Id = Convert.ToInt32(reader["Id"]),
                        Username = reader["Username"].ToString(),
                        Mail = reader["Mail"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Skillsets = JsonConvert.DeserializeObject<List<string>>(reader["Skillsets"].ToString()),
                        Hobby = JsonConvert.DeserializeObject<List<string>>(reader["Hobby"].ToString())
                    };

                    return Ok(user);
                }

                return NotFound("User not found.");
            }
            catch (Exception ex)
            {
                LogError(ex);
                return BadRequest("Failed to get user.");
            }
        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(string Username)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(_connectionString);
                connection.Open();

                using MySqlCommand cmd = new MySqlCommand("DELETE FROM T_freelance WHERE Username = @Username", connection);
                cmd.Parameters.AddWithValue("@Username", Username);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok("User deleted successfully.");
                }

                return NotFound("User not found.");
            }
            catch (Exception ex)
            {
                LogError(ex);
                return BadRequest("Failed to delete user.");
            }
        }

        private string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "log", "Freelanceweb.log");

        private void LogError(Exception ex)
        {
             System.IO.File.AppendAllText(logFilePath, $"{DateTime.Now}: {ex.Message}\n");
        }

        private void LogError(string ex)
        {
            System.IO.File.AppendAllText(logFilePath, $" {DateTime.Now} : {ex}\n");
        }


    }
}
