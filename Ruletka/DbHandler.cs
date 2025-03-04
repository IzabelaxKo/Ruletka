using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Ruletka
{
    internal class DbHandler
    {
        private string connectionString;

        public DbHandler()
        {
            this.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["rouletteDb"].ConnectionString;
        }

        public DataTable GetUsersData()
        {
            // type of data table to store data 
            DataTable dt = new DataTable();


            // connecting to the database
            // using statement to ensure that the connection is closed after the data is retrieved
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // query to get data
                string query = "SELECT * FROM users";

                // creating command object to perform query
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // opening connection
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                // filling data table with data from database
                da.Fill(dt);
            }

            return dt;
        }

        public void AddUser(string username, string password)
        {
            // connecting to the database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // query to insert data
                string query = "INSERT INTO users (username, password) VALUES (@username, @password)";
                // creating command object to perform query
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // adding parameters to the query
                cmd.Parameters.AddWithValue("@username", username);

                password = new PasswordHelper().HashPassword(password);
                cmd.Parameters.AddWithValue("@password", password);

                // opening connection
                conn.Open();
                // executing query
                cmd.ExecuteNonQuery();
            }
        }

        // returns Id of the user 
        public int TryToLoginUser(string username, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT id, password FROM users WHERE username = @username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (new PasswordHelper().VerifyPassword(password, reader["password"].ToString()))
                    {
                        return Convert.ToInt32(reader["id"]);
                    }
                }
            }
            return -1;
        }


        public DataTable BalanceScoreboard()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT username, balance FROM users ORDER BY balance DESC";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

            }
            return dt;
        }

        public DataTable WinRatioScoreboard()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT username, wins/(wins+loses) AS ratio FROM users ORDER BY ratio DESC;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public void UpdateGames(string type, int userId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "UPDATE users SET " + type + " = " + type + " + 1 WHERE id = @userId";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@userId", userId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

        }

        public void UpdateBalance(int userId, int amount)
        {
            // if amount is negative - it just subtracts from the balance
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "UPDATE users SET balance = balance + @amount WHERE id = @userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@amount", amount);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateMultiplier(int userId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "UPDATE users SET multiplier = multiplier + 1 WHERE id = @userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateMultiplierToZero(int userId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "UPDATE users SET multiplier = 0 WHERE id = @userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ChangePassword(int userId, string newPassword)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "UPDATE users SET password = @newPassword WHERE id = @userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);

                newPassword = new PasswordHelper().HashPassword(newPassword);
                cmd.Parameters.AddWithValue("@newPassword", newPassword);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
