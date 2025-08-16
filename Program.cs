using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class UserFitness
{
    public string Name { get; }
    public int[] DailySteps { get; }
    public int[] DailyCalories { get; }
    public int StepsGoal { get; }
    public int CaloriesGoal { get; }

    public UserFitness(string name, int[] steps, int[] calories, int stepsGoal, int caloriesGoal)
    {
        if (steps.Length != 7 || calories.Length != 7)
            throw new ArgumentException("Daily data must contain exactly 7 entries");

        Name = name;
        DailySteps = steps;
        DailyCalories = calories;
        StepsGoal = stepsGoal;
        CaloriesGoal = caloriesGoal;
    }

    public Tuple<double, double> Average()
    {
        return new Tuple<double, double>(DailySteps.Average(), DailyCalories.Average());
    }

    public int BestDay()
    {
        double[] combinedScores = new double[7];
        for (int i = 0; i < 7; i++)
        {
            // Calculate a combined score normalized by goals
            double stepScore = (double)DailySteps[i] / StepsGoal;
            double calorieScore = (double)DailyCalories[i] / CaloriesGoal;
            combinedScores[i] = stepScore + calorieScore;
        }
        return Array.IndexOf(combinedScores, combinedScores.Max());
    }

    public bool OnTrack()
    {
        int meetingGoals = 0;
        for (int i = 0; i < 7; i++)
        {
            if (DailySteps[i] >= StepsGoal && DailyCalories[i] >= CaloriesGoal)
            {
                meetingGoals++;
            }
        }
        return meetingGoals >= 5;
    }
}

class FitnessTracker
{
    private List<UserFitness> _users = new List<UserFitness>();

    public void AddUser(string name, int[] steps, int[] calories, int stepsGoal, int caloriesGoal)
    {
        _users.Add(new UserFitness(name, steps, calories, stepsGoal, caloriesGoal));
    }

    public UserFitness GetUser(string name)
    {
        return _users.FirstOrDefault(u =>
            u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}


namespace _7__Fitness_Tracker_Weekly_Report
{
    internal class Program
    {
        static void Main()
            static void main(FitnessTracker tracker_users)
        {
            var tracker = new FitnessTracker();

            Console.WriteLine("FITNESS ANALYZER");
            Console.WriteLine("================");

            while (true)
            {
                Console.Write("\nAdd new user? (y/n): ");
                if (Console.ReadLine().Trim().ToLower() != "y")
                    break;

                try
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine().Trim();

                    int[] steps = GetDailyData("steps");
                    int[] calories = GetDailyData("calories");

                    Console.Write("Daily steps goal: ");
                    int stepsGoal = GetPositiveInt();

                    Console.Write("Daily calories goal: ");
                    int caloriesGoal = GetPositiveInt();

                    tracker.AddUser(name, steps, calories, stepsGoal, caloriesGoal);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            Console.WriteLine("\nANALYSIS RESULTS");
            Console.WriteLine("================");

            foreach (var user in tracker._)
            {
                Console.WriteLine($"\nUser: {user.Name}");
                var averages = user.Average();
                Console.WriteLine($"Average steps: {averages.Item1:F0}");
                Console.WriteLine($"Average calories: {averages.Item2:F0}");

                int bestDay = user.BestDay();
                Console.WriteLine($"Best day: Day {bestDay + 1} with {user.DailySteps[bestDay]} steps and {user.DailyCalories[bestDay]} calories");

                Console.WriteLine($"On track: {(user.OnTrack() ? "Yes" : "No")}");
            }
        }

        static int[] GetDailyData(string metric)
        {
            int[] data = new int[7];
            string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

            Console.WriteLine($"\nEnter {metric} for each day:");
            for (int i = 0; i < 7; i++)
            {
                Console.Write($"{days[i]}: ");
                data[i] = GetPositiveInt();
            }

            return data;
        }

        static int GetPositiveInt()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int value) && value >= 0)
                    return value;
                Console.Write("Invalid input. Please enter a non-negative integer: ");
            }
        }
    }
}




