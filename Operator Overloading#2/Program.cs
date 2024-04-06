using System;

namespace Assignment9ex2Operatoroverloading
{
    public class Log
    {
        public string Activity { get; set; }
        public double Hours { get; set; }
        public string Category { get; set; } 

        
        public static Log operator ++(Log log)
        {
            log.Hours += 1;
            return log;
        }

        
        public static Log operator --(Log log)
        {
            log.Hours -= 1;
            return log;
        }

        
        public static Log operator +(Log log, int hours)
        {
            log.Hours += hours;
            return log;
        }

        
        public static Log operator -(Log log, int hours)
        {
            log.Hours -= hours;
            return log;
        }

       
        public static bool operator >(Log log1, Log log2)
        {
            return log1.Hours > log2.Hours;
        }

        
        public static bool operator <(Log log1, Log log2)
        {
            return log1.Hours < log2.Hours;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Log[] myLog = new Log[100];
            for (int i = 0; i < myLog.Length; i++)
            {
                myLog[i] = new Log();  
            }
            int selection = Menu();
            int index = 0, entry = 0;
            string ans = "";

            while (selection != 6)
            {
                switch (selection)
                {
                    case 1: 
                        if (index < 100)
                        {
                            Console.Write("Activity Category (Fun, Work, Other): ");
                            myLog[index].Category = Console.ReadLine();
                            Console.WriteLine();
                            Console.Write("Activity: ");
                            myLog[index].Activity = Console.ReadLine();
                            Console.WriteLine();
                            Console.Write("Hours: ");
                            myLog[index].Hours = double.Parse(Console.ReadLine());
                            Console.WriteLine();
                            index++;
                        }
                        else
                        {
                            Console.WriteLine("You have too many log entries - see programming");
                        }
                        break;
                    case 2: 
                        for (int i = 0; i < myLog.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(myLog[i].Activity))
                            {
                                Console.WriteLine($"Category: {myLog[i].Category}");
                                Console.WriteLine($"Activity: {myLog[i].Activity}");
                                Console.WriteLine($"Hours: {myLog[i].Hours}");
                            }
                        }
                        break;
                    case 3: 
                        entry = pickEntry(index);
                        Console.Write("Change Activity Category (Fun, Work, Other) Y for Yes: ");
                        ans = Console.ReadLine();
                        if (ans == "Y" || ans == "y")
                        {
                            Console.Write("Category? ");
                            myLog[entry].Category = Console.ReadLine();
                        }
                        Console.WriteLine();
                        Console.Write("Change Activity Y for Yes: ");
                        ans = Console.ReadLine();
                        if (ans == "Y" || ans == "y")
                        {
                            Console.Write("Activity: ");
                            myLog[entry].Activity = Console.ReadLine();
                        }
                        Console.WriteLine();
                        break;
                    case 4: 
                        entry = pickEntry(index);
                        AdjustHours(myLog, entry);
                        break;
                    case 5: 
                        CompareCategories(myLog);
                        break;
                    default:
                        Console.WriteLine("You made an invalid selection, please try again");
                        break;
                }
                selection = Menu();
            }
        }

        public static int Menu()
        {
            int choice = 0;
            Console.WriteLine("\nPlease make a selection from the menu:");
            Console.WriteLine("1 - Add an entry");
            Console.WriteLine("2 - Print All");
            Console.WriteLine("3 - Change category or activity");
            Console.WriteLine("4 - Adjust hours");
            Console.WriteLine("5 - Total categories and compare");
            Console.WriteLine("6 - Quit");
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
            {
                Console.WriteLine("Please select a number between 1 and 6");
            }
            return choice;
        }

        public static int pickEntry(int index)
        {
            Console.WriteLine("What entry would you like to change?");
            Console.WriteLine($"1 through {index}");
            int entry;
            while (!int.TryParse(Console.ReadLine(), out entry) || entry < 1 || entry > index)
            {
                Console.WriteLine($"Please select a number between 1 and {index}");
            }
            return entry - 1;  
        }

        public static void AdjustHours(Log[] myLog, int entry)
        {
            string ans;
            Console.Write("Increase Hours by 1? Y for Yes: ");
            ans = Console.ReadLine();
            if (ans == "Y" || ans == "y")
            {
                myLog[entry]++;
                Console.WriteLine($"Hours now: {myLog[entry].Hours}");
            }

            Console.Write("Decrease Hours by 1? Y for Yes: ");
            ans = Console.ReadLine();
            if (ans == "Y" || ans == "y")
            {
                myLog[entry]--;
                Console.WriteLine($"Hours now: {myLog[entry].Hours}");
            }

            Console.Write("Increase Hours by > 1? Y for Yes: ");
            ans = Console.ReadLine();
            if (ans == "Y" || ans == "y")
            {
                Console.Write("Enter the hours: ");
                int hr;
                while (!int.TryParse(Console.ReadLine(), out hr))
                    Console.WriteLine("Please enter a number");
                myLog[entry] += hr;
                Console.WriteLine($"Hours now: {myLog[entry].Hours}");
            }

            Console.Write("Decrease Hours by > 1? Y for Yes: ");
            ans = Console.ReadLine();
            if (ans == "Y" || ans == "y")
            {
                Console.Write("Enter the hours: ");
                int hr;
                while (!int.TryParse(Console.ReadLine(), out hr))
                    Console.WriteLine("Please enter a number");
                myLog[entry] -= hr;
                Console.WriteLine($"Hours now: {myLog[entry].Hours}");
            }
        }

        public static void CompareCategories(Log[] myLog)
        {
            Log totalFun = new Log() { Category = "Fun", Hours = 0 };
            Log totalWork = new Log() { Category = "Work", Hours = 0 };
            Log totalOther = new Log() { Category = "Other", Hours = 0 };

            foreach (var log in myLog)
            {
                if (log.Category == "Fun")
                {
                    totalFun.Hours += log.Hours;
                }
                else if (log.Category == "Work")
                {
                    totalWork.Hours += log.Hours;
                }
                else if (log.Category == "Other")
                {
                    totalOther.Hours += log.Hours;
                }
            }

            Console.WriteLine("\nTotal Hours by Category:");
            if (totalFun > totalWork && totalFun > totalOther)
            {
                Console.WriteLine("The largest number of hours was spent on fun!");
            }
            else if (totalWork > totalFun && totalWork > totalOther)
            {
                Console.WriteLine("The largest number of hours was spent on work");
            }
            else
            {
                Console.WriteLine("The largest number of hours was spent on other activities");
            }

            Console.WriteLine($"Your total fun hours = {totalFun.Hours}");
            Console.WriteLine($"Your total work hours = {totalWork.Hours}");
            Console.WriteLine($"Your total other hours = {totalOther.Hours}");
        }
    }
}
