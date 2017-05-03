using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LibMyWorkout.Domain
{
    //Week object that holds the days the user should complete that week
    [Serializable]
    [DataContract]
    public class Week
    {
        //Propertiers

        //Holds the week number
        [DataMember]
        public int WeekNumber { set; get; }

        //Hold the week order number 1 or 2
        [DataMember]
        public int WeekOrderNumber { set; get; }

        [DataMember]
        public IList<Day> Days { get; set; }

        [DataMember]
        public int WeekId { get; set; }



        //Constructors

        //Default constructor
        public Week()
        {
        }

        //Constructor with the param of week number, day one, day two, day three
        public Week(int weekNumber, int weekOrderNumber, IList<Day> days)
        {
            WeekNumber = weekNumber;
            WeekOrderNumber = weekOrderNumber;
            Days = days;
        }

        public Week(int id, int weekNumber, int weekOrderNumber, IList<Day> days)
        {
            WeekId = id;
            WeekNumber = weekNumber;
            WeekOrderNumber = weekOrderNumber;
            Days = days;
        }


        //Methods
        
        
        /*
        //Method to see if this object equals another Week object
        public override bool Equals(Object obj)
        {
            bool isEqual = true;
            Week week = (Week)obj;
            if (!week.WeekNumber.Equals(WeekNumber)) { isEqual = false; }
            else if (!WeekOrderNumber.Equals(week.WeekOrderNumber)) { isEqual = false; }
            return isEqual;
        }
        */

        //Method to see if the object is valid
        public bool validate()
        {
            bool isValid = true;
            if (WeekNumber == 0) { isValid = false; }
            else if (WeekOrderNumber == 0) { isValid = false; }
            else if (Days == null) { isValid = false; }
            return isValid;
        }

        //Method to return the object as a string
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("\nWeek Number: " + WeekNumber);
            str.Append("\nWeek Order Number: " + WeekOrderNumber);

            foreach (Day d in Days)
            {
                str.Append("\nDay: " + d.ToString());
            }


            return str.ToString();
        }

        //Method to return the current object hashCode as an int
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


    }//end of Class
}
