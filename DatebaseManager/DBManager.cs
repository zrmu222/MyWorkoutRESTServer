using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace DatebaseManager
{
    class DBManager
    {
        static void Main(string[] args)
        {
            int userId = 3;
            getUserNames();
            Console.WriteLine();
            /*Console.WriteLine("User with Id: " + userId);
            getWeek(userId);
            getDays(userId);
            getExerciseList(userId);
            Console.WriteLine();
            */
            getUserNames();
            Console.ReadKey();

   
        }

        private static void getUserNames()
        {
            SqlConnection conn = getConnection();
            conn.Open();
            try
            {
                string stm = "SELECT * FROM USERS";
                SqlCommand comm = new SqlCommand(stm, conn);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("UserId:");
                    sb.Append(reader.GetInt32(reader.GetOrdinal("UserId")));
                    sb.Append(" UserName:");
                    sb.Append(reader.GetString(reader.GetOrdinal("UserName")));
                    sb.Append(" Password:");
                    sb.Append(reader.GetString(reader.GetOrdinal("Password")));
                    sb.Append(" Role:");
                    sb.Append(reader.GetString(reader.GetOrdinal("Role")));
                    /*sb.Append(" FirstName:");
                    sb.Append(reader.GetString(reader.GetOrdinal("FirstName")));
                    sb.Append(" LastName:");
                    sb.Append(reader.GetString(reader.GetOrdinal("LastName")));
                    sb.Append(" Password:");
                    sb.Append(reader.GetString(reader.GetOrdinal("Password")));
                    sb.Append(" CurrentDay:");
                    sb.Append(reader.GetInt32(reader.GetOrdinal("CurrentDay")));
                    sb.Append(" CurrentWeek:");
                    sb.Append(reader.GetInt32(reader.GetOrdinal("CurrentWeek")));
                    sb.Append(" ");
                    */

                    Console.WriteLine(sb.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private static void deleteUser(int i)
        {
            SqlConnection conn = getConnection();
            conn.Open();
            try
            {
                string stm = "DELETE FROM USERS WHERE UserId=" + i;
                SqlCommand comm = new SqlCommand(stm, conn);
                comm.ExecuteNonQuery();

                string stm2 = "DELETE FROM Weeks WHERE UserId=" + i;
                comm = new SqlCommand(stm2, conn);
                comm.ExecuteNonQuery();

                string stm3 = "DELETE FROM Days WHERE UserId=" + i;
                comm = new SqlCommand(stm3, conn);
                comm.ExecuteNonQuery();

                string stm4 = "DELETE FROM Exercises WHERE UserId=" + i;
                comm = new SqlCommand(stm4, conn);
                comm.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private static void getExerciseList(int i)
        {
            SqlConnection conn = getConnection();
            conn.Open();
            try
            {
                string stm = "SELECT * FROM Exercises WHERE UserId=" + i;
                SqlCommand comm = new SqlCommand(stm, conn);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("UserId: ");
                    sb.Append(reader.GetInt32(reader.GetOrdinal("UserId")));
                    sb.Append(" Name: ");
                    sb.Append(reader.GetString(reader.GetOrdinal("Name")));
                    sb.Append(" ExerciseNumber: ");
                    sb.Append(reader.GetInt32(reader.GetOrdinal("ExerciseNumber")));
                    sb.Append(" Weight: ");
                    sb.Append(reader.GetDecimal(reader.GetOrdinal("Weight")));
                    sb.Append(" Sets: ");
                    sb.Append(reader.GetInt32(reader.GetOrdinal("Sets")));
                    sb.Append(" Reps: ");
                    sb.Append(reader.GetInt32(reader.GetOrdinal("Reps")));
                    
                    Console.WriteLine(sb.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }



        }

        private static void getWeek(int i)
        {
            SqlConnection conn = getConnection();
            conn.Open();
            try
            {
                string stm = "SELECT * FROM Weeks WHERE UserId=" + i;
                SqlCommand comm = new SqlCommand(stm, conn);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" WeekNumber: ");
                    sb.Append(reader.GetInt32(reader.GetOrdinal("WeekNumber")));         
                    sb.Append(" UserId: ");
                    sb.Append(reader.GetInt32(reader.GetOrdinal("UserId")));
                    sb.Append(" WeekOrderNumber: ");
                    sb.Append(reader.GetInt32(reader.GetOrdinal("WeekOrderNumber")));
                    sb.Append(" ");
                    
                    Console.WriteLine(sb.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private static SqlConnection getConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }


        private static void UpdateUserName(string userName)
        {
            SqlConnection conn = getConnection();
            conn.Open();
            try
            {
                string stm = "UPDATE Users Set Role='" + userName + "' Where UserId=" + 1 + "";
                SqlCommand comm = new SqlCommand(stm, conn);
                comm.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private static void getDays(int i)
        {
            SqlConnection conn = getConnection();
            conn.Open();
            try
            {
                string stm = "SELECT * FROM Days WHERE UserId=" + i;
                SqlCommand comm = new SqlCommand(stm, conn);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("DayNumber:");
                    sb.Append(reader.GetInt32(reader.GetOrdinal("DayNumber")));
                    sb.Append(" DayOrderNumber:");
                    sb.Append(reader.GetInt32(reader.GetOrdinal("DayOrderNumber")));
                    sb.Append(" UserId:");
                    sb.Append(reader.GetInt32(reader.GetOrdinal("UserId")));
                    sb.Append(" WeekId:");
                    sb.Append(reader.GetInt32(reader.GetOrdinal("WeekId")));

                    Console.WriteLine(sb.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }

        }







    }
}
