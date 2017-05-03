using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LibMyWorkout.Domain
{
    //This class holds one of the six exercises objects the customer will complete each workout.
    [Serializable]
    [DataContract]
    public class Exercise
    {
        //Properties

        //Holds the name of the exercise
        [DataMember]
        public string Name { set; get; }

        [DataMember]
        public int ExerciseId { set; get; }

        [DataMember]
        public int ExerciseNumber { set; get; }

        //Holds the weight that should be used for the exercise
        [DataMember]
        public double Weight { set; get; }

        //Holds the number of sets the user should do of this exercise. Default is set to 3
        [DataMember]
        public int Sets { set; get; }

        //Holds the number of reps the user should do of this exercise. Default is set to 5
        [DataMember]
        public int Reps { get; set; }

        //Holds the number of reps the user completed on their last set. 
        [DataMember]
        public int LastSetReps { get; set; }



        //Constructors

        //Default constructor
        public Exercise()
        {
            Sets = 3;
            Reps = 5;
        }

        //Constructor with param of name, weight, sets. reps. lastSetReps
        public Exercise(string name, int exerciseNumber, double weight, int sets, int reps, int lastSetReps)
        {
            Name = name;
            ExerciseNumber = exerciseNumber;
            Weight = weight;
            Sets = sets;
            Reps = reps;
            LastSetReps = lastSetReps;
        }

        //Constructor with param of name and weight
        public Exercise(string name, int exerciseNumber, double weight)
        {
            Name = name;
            ExerciseNumber = exerciseNumber;
            Weight = weight;
            Sets = 3;
            Reps = 5;
        }


        //Methods


        //Check to see if this Exercise equals an exercise passed in
        public override bool Equals(object obj)
        {
            bool equals = true;
            Exercise ex = (Exercise)obj;
            if (!Name.Equals(ex.Name)) { equals = false; }
            else if (!ExerciseNumber.Equals(ex.ExerciseNumber)) { equals = false; }
            else if (!Weight.Equals(ex.Weight)) { equals = false; }
            else if (!Sets.Equals(ex.Sets)) { equals = false; }
            else if (!Reps.Equals(ex.Reps)) { equals = false; }
            else if (!LastSetReps.Equals(ex.LastSetReps)) { equals = false; }
            return equals;
        }

        //Check to see if all properties are set to a valid value
        public bool validate()
        {
            bool isValid = true;
            if (Name == null || Name == "") { isValid = false; }
            else if (ExerciseNumber <= 0) { isValid = false; }
            else if (Weight < 0) { isValid = false; }
            else if (Sets <= 0) { isValid = false; }
            else if (Reps <= 0) { isValid = false; }
            return isValid;
        }

        //Changes object to a strig
        public override string ToString()
        {
            var str = new StringBuilder();
            str.Append("Exercise Name: ");
            str.Append(Name);
            str.Append("Exercise Number: ");
            str.Append(ExerciseNumber);
            str.Append("\nExercise Weight: ");
            str.Append(Weight);
            str.Append("\nExercise Sets: ");
            str.Append(Sets);
            str.Append("\nExercise Reps: ");
            str.Append(Reps);
            str.Append("\nExercise Last Set Reps: ");
            str.Append(LastSetReps);
            return str.ToString();
        }

        //Returns the hashCode and a int
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


    }

}
