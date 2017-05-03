using System;
using System.Configuration;
using System.Data.SqlClient;
using LibMyWorkout.Domain;
using myworkout.model.service.databaseService;
using System.Collections.Generic;
using System.Collections;


namespace myworkout.model.service.databaseService
{
    public class SqlSvcImpl : IDatabaseService
    {

        //string connectionString = @"server=(local);Integrated Security=SSPI;database=MyWorkout";
        string message = null;
        bool status = false;

        public SqlSvcImpl()
        {        

        }


       private SqlConnection getConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }


        public User getUser(string userName, string password)
        {
            User user = new User();

            SqlConnection conn = getConnection();
            conn.Open();
            try
            {
                string select = "SELECT * FROM USERS";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                status = false;

                while (reader.Read())
                {
                    if (reader.GetString(reader.GetOrdinal("UserName")).Equals(userName))
                    {

                        if (reader.GetString(reader.GetOrdinal("Password")).Equals(password))
                        {
                            user.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
                            user.UserName = reader.GetString(reader.GetOrdinal("UserName"));
                            user.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            user.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                            user.Password = reader.GetString(reader.GetOrdinal("Password"));
                            user.CurrentDay = reader.GetInt32(reader.GetOrdinal("CurrentDay"));
                            user.CurrentWeek = reader.GetInt32(reader.GetOrdinal("CurrentWeek"));
                            user.Role = reader.GetString(reader.GetOrdinal("Role"));
                            status = true;
                        }
                    }
                }
                reader.Close();

                if (status)
                {
                    select = "Select * from Weeks Where userId =" + user.UserId + "";
                    cmd = new SqlCommand(select, conn);
                    reader = cmd.ExecuteReader();
                    List<Week> weekList = new List<Week>();
                    while (reader.Read())
                    {
                        Week wk = new Week();
                        wk.WeekId = reader.GetInt32(reader.GetOrdinal("WeekId"));
                        wk.WeekNumber = reader.GetInt32(reader.GetOrdinal("WeekNumber"));
                        wk.WeekOrderNumber = reader.GetInt32(reader.GetOrdinal("WeekOrderNumber"));
                        weekList.Add(wk);
                    }
                    reader.Close();

                    foreach (Week wk in weekList)
                    {
                        select = "Select * from Days Where (UserId =" + user.UserId + " And WeekId =" + wk.WeekId + ")";
                        cmd = new SqlCommand(select, conn);
                        reader = cmd.ExecuteReader();
                        List<Day> dayList = new List<Day>();
                        while (reader.Read())
                        {
                            Day day = new Day();
                            day.DayId = reader.GetInt32(reader.GetOrdinal("DayId"));
                            day.DayNumber = reader.GetInt32(reader.GetOrdinal("DayNumber"));
                            day.DayOrderNumber = reader.GetInt32(reader.GetOrdinal("DayOrderNumber"));
                            dayList.Add(day);
                        }
                        wk.Days = dayList;
                        reader.Close();
                    }

                    foreach (Week wk in weekList)
                    {
                        foreach (Day day in wk.Days)
                        {
                            IList<int> dayExerciseNumber = new List<int>();
                            select = "Select * From DayExerciseList Where DayId =" + day.DayId;
                            cmd = new SqlCommand(select, conn);
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                dayExerciseNumber.Add(reader.GetInt32(reader.GetOrdinal("Number")));
                            }
                            day.ExerciseListNumber = dayExerciseNumber;
                            reader.Close();
                        }
                    }

                    user.Weeks = weekList;

                    select = "Select * From Exercises Where UserId =" + user.UserId;
                    List<Exercise> exerciseList = new List<Exercise>();
                    cmd = new SqlCommand(select, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Exercise ex = new Exercise();
                        ex.ExerciseId = reader.GetInt32(reader.GetOrdinal("ExerciseId"));
                        ex.Name = reader.GetString(reader.GetOrdinal("Name"));
                        ex.ExerciseNumber = reader.GetInt32(reader.GetOrdinal("ExerciseNumber"));
                        ex.Weight = (double)reader.GetDecimal(reader.GetOrdinal("Weight"));
                        ex.Sets = reader.GetInt32(reader.GetOrdinal("Sets"));
                        ex.Reps = reader.GetInt32(reader.GetOrdinal("Reps"));
                        exerciseList.Add(ex);
                    }
                    reader.Close();
                    user.ExerciseList = exerciseList;

                }else
                {
                    message = "Wrong user name or password";
                }

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return user;
        }


        public User saveNewUser(User user)
        {
            Console.WriteLine("Saving New User");
            SqlConnection conn = getConnection();
            conn.Open();
            try
            {
                string insert = "Insert into Users(UserName, FirstName, LastName, Password, Role, CurrentDay, CurrentWeek) " +
                    "Output Inserted.UserId Values ('" + user.UserName + "','" + user.FirstName + "','" + user.LastName +
                    "','" + user.Password + "','" + user.Role + "'," + user.CurrentDay + "," + user.CurrentWeek + ")";
                SqlCommand cmd = new SqlCommand(insert, conn);
                user.UserId = (int)cmd.ExecuteScalar();

                foreach (Week wk in user.Weeks)
                {
                    insert = "Insert into Weeks(WeekNumber, WeekOrderNumber, UserId) Output Inserted.WeekId Values(" + wk.WeekNumber + "," + wk.WeekOrderNumber +
                    "," + user.UserId + ")";

                    cmd = new SqlCommand(insert, conn);
                    wk.WeekId = (int)cmd.ExecuteScalar();
                    foreach (Day day in wk.Days)
                    {
                        insert = "Insert into Days(DayNumber, DayOrderNumber, UserId, WeekId) Output Inserted.DayID Values(" + day.DayNumber + "," +
                            day.DayOrderNumber + "," + user.UserId + "," + wk.WeekId + ")";

                        cmd = new SqlCommand(insert, conn);
                        day.DayId = (int)cmd.ExecuteScalar();

                        foreach (int i in day.ExerciseListNumber)
                        {
                            insert = "Insert into DayExerciseList(Number, DayId) Values(" + i + "," + day.DayId + ")";
                            cmd = new SqlCommand(insert, conn);
                            cmd.ExecuteNonQuery();
                        }

                    }
                    
                }

                foreach (Exercise ex in user.ExerciseList)
                {
                    insert = "Insert into Exercises(UserId, Name, ExerciseNumber, Weight, Sets, Reps) Output Inserted.ExerciseId Values(" + user.UserId + ",'" + ex.Name + "'," +
                        ex.ExerciseNumber + "," + ex.Weight + "," + ex.Sets + "," + ex.Reps + ")";

                    cmd = new SqlCommand(insert, conn);
                    ex.ExerciseId = (int)cmd.ExecuteScalar();        
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return user;
        }


        public User updateUser(User user)
        {
            Console.WriteLine("UserId passed into updateUser: " + user.UserId);

            SqlConnection conn = getConnection();
            conn.Open();
            try
            {
                string update = "UPDATE Users set UserName ='" + user.UserName + "', FirstName='" + user.FirstName + "', LastName='" + user.LastName +
                    "', Password='" + user.Password + "', CurrentDay=" + user.CurrentDay + ", CurrentWeek=" + user.CurrentWeek + " Where UserId=" + user.UserId;
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();
                foreach (Week wk in user.Weeks)
                {
                    foreach (Day day in wk.Days)
                    {
                        update = "UPDATE Days SET DayNumber=" + day.DayNumber + ", DayOrderNumber=" + day.DayOrderNumber + " Where dayId=" + day.DayId;

                        cmd = new SqlCommand(update, conn);
                        cmd.ExecuteNonQuery();

                        /*
                        foreach (int i in day.ExerciseListNumber)
                        {
                            insert = "Insert into DayExerciseList(Number, DayId) Values(" + i + "," + day.DayId + ")";
                            cmd = new SqlCommand(insert, conn);
                            cmd.ExecuteNonQuery();
                        } */

                    }
                    update = "UPDATE Weeks SET WeekNumber=" + wk.WeekNumber + ", WeekOrderNumber=" + wk.WeekOrderNumber +
                        " Where WeekId=" + wk.WeekId;                         

                    cmd = new SqlCommand(update, conn);
                    cmd.ExecuteNonQuery();
                }

                foreach (Exercise ex in user.ExerciseList)
                {
                    update = "UPDATE Exercises SET Name='" + ex.Name + "', ExerciseNumber=" + ex.ExerciseNumber +
                        ", Weight=" + ex.Weight + ", Sets=" + ex.Sets + ", Reps=" + ex.Reps + " WHERE ExerciseID=" + ex.ExerciseId;

                    cmd = new SqlCommand(update, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            Console.WriteLine("UserId after updateUser: " + user.UserId);

            return user;
        }


        public bool checkUserName(string userName)
        {
            bool isFound = false;

            SqlConnection conn = getConnection();
            conn.Open();
            string select = "SELECT * FROM Users WHERE UserName='" + userName + "'";
            SqlCommand command = new SqlCommand(select, conn);
            SqlDataReader reader = command.ExecuteReader();
            isFound = reader.Read();

            return isFound;
        }


        public ArrayList getUsers()
        {
            ArrayList userList = new ArrayList();
            SqlConnection conn = getConnection();
            conn.Open();
            try
            {
                string select = "SELECT * FROM USERS";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                status = false;

                while (reader.Read())
                {
                    User user = new User();
                    user.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
                    user.UserName = reader.GetString(reader.GetOrdinal("UserName"));
                    user.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    user.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    //user.Password = reader.GetString(reader.GetOrdinal("Password"));
                    user.CurrentDay = reader.GetInt32(reader.GetOrdinal("CurrentDay"));
                    user.CurrentWeek = reader.GetInt32(reader.GetOrdinal("CurrentWeek"));
                    user.Role = reader.GetString(reader.GetOrdinal("Role"));
                    status = true;
                    userList.Add(user);

                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return userList;
        }

    }
}
