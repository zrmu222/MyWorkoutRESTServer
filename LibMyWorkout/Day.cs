using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LibMyWorkout.Domain
{
    //Day object that holds the exercises the user should do for that day
    [Serializable]
    [DataContract]
    public class Day
    {
        //Properites

        //Holds the current day number
        [DataMember]
        public int DayNumber { set; get; }

        //Hold the day order numbner 1-6
        [DataMember]
        public int DayOrderNumber { set; get; }

        [DataMember]
        public IList<int> ExerciseListNumber { set; get; }

        [DataMember]
        public int DayId { get; set; }


        //Constructors

        //Default constructor
        public Day()
        {
        }

        //Constructor with param of day number, day order number, exercise one, exercise two, exercise three
        public Day(int dayNumber, int dayOrderNumber, IList<int> exerciseListNumber)
        {
            DayNumber = dayNumber;
            DayOrderNumber = dayOrderNumber;
            ExerciseListNumber = exerciseListNumber;
        }

        public Day(int id, int dayNumber, int dayOrderNumber, IList<int> exerciseListNumber)
        {
            DayId = id;
            DayNumber = dayNumber;
            DayOrderNumber = dayOrderNumber;
            ExerciseListNumber = exerciseListNumber;
        }


        //Methods

        /*
        //Method to see if current Day object equals a passed in day object
        public override bool Equals(Object obj)
        {
            bool isEqual = true;
            Day day = (Day)obj;
            if (!DayNumber.Equals(day.DayNumber)) { isEqual = false; }
            else if (!DayOrderNumber.Equals(day.DayOrderNumber)) { isEqual = false; }
            return isEqual;
        }
        */

        //Check to see if the Day object is valid
        public bool validate()
        {
            bool isValid = true;
            if (DayNumber == 0) { isValid = false; }
            else if (DayOrderNumber == 0) { isValid = false; }
            else if (ExerciseListNumber == null) { isValid = false; }
            return isValid;
        }//end of valid

        //Method to get the Day object as a string
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("\nDay Number: ");
            str.Append(DayNumber);
            str.Append("\nDay Order Number: ");
            str.Append(DayOrderNumber);

            foreach (int i in ExerciseListNumber)
            {
                str.Append("\nExercise Number: " + i);
            }

            return str.ToString();
        }

        //Method to get the current HashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


    }//end of Class
}
