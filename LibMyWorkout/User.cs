using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace LibMyWorkout.Domain
{
    //User object that holds the user info along with the workout information
    [Serializable]
    [DataContract]
    public class User
    {

        //Properties

        //Holds the user Id number
        [DataMember]
        public int UserId { set; get; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string FirstName { set; get; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Role { set; get; }
        [DataMember]
        public int CurrentDay { set; get; }
        [DataMember]
        public int CurrentWeek { set; get; }
        [DataMember]
        public IList<Week> Weeks { set; get; }

        //List of all user exercises
        [DataMember]
        public IList<Exercise> ExerciseList { get; set; }

        //Constructors 

        //Default contructor
        public User()
        {
        }

        public User(int userId, string firstName, string lastName, string password, int currentDay, int currentWeek)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            CurrentDay = currentDay;
            CurrentWeek = currentWeek;
        }

        public User(int currentDay, int currentWeek, IList<Week> week, IList<Exercise> exerciseLise)
        {
            CurrentDay = currentDay;
            CurrentWeek = currentWeek;
            Weeks = week;
            ExerciseList = exerciseLise;
        }


        //Methods

       
        /*
        //Method to check if this user is equal to another user
        public override bool Equals(object obj)
        {
            Console.WriteLine(obj.ToString());
            bool isEqual = true;
            User user = (User)obj;

            if (!CurrentDay.Equals(user.CurrentDay)) { isEqual = false; }
            else if (!CurrentWeek.Equals(user.CurrentWeek)) { isEqual = false; }
            return isEqual;
        }
        */

        //Method to return the current object hashCode as an int
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        //Method to validate user object
        public bool validate()
        {
            bool isValid = true;
            if (CurrentDay == 0) { isValid = false; }
            else if (CurrentWeek == 0) { isValid = false; }
            else if (Weeks == null) { isValid = false; }
            else if (ExerciseList == null) { isValid = false; }
            return isValid;
        }//End of valid

        //Method to change User object into a string
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("UserId: " + UserId);
            str.Append("\nUserName: " + UserName);
            str.Append("\nFirstName: " + FirstName);
            str.Append("\nLastName: " + LastName);
            str.Append("\nPassword: " + Password);
            str.Append("\nCurrent Day Number: " + CurrentDay);
            str.Append("\nCurrent Week Number: " + CurrentWeek);
            str.Append("\nExercise List: ");

            foreach (Exercise ex in ExerciseList)
            {
                str.Append("\nExercise: " + ex.Name);
            }

            foreach (Week wk in Weeks)
            {
                str.Append("\nWeeks: " + wk.ToString());
            }

            return str.ToString();

        }//End of toString

    }//End of class
}
